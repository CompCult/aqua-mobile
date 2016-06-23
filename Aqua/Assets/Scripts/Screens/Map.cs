using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Map : GenericScene {

	// Page elements
	public Dropdown ReportDropdown;
	public Text ReportStatus;
	public GameObject MapField;
	
	// Internal elements
	private List<Report> ReportList;

	// Use this for initialization
	public void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		BackScene = "Home";

		PrepareReportListForm();
	}

	private void PrepareReportListForm()
	{
		URL = "http://aqua-web.herokuapp.com/api/notification/user";
		pvtkey = "d86c362f4b";

		WWW www = new WWW (URL + "/" + EventSystem.GetUser().GetID() + "/" + pvtkey);
		StartCoroutine(GetReportListFromServer(www));
	}

	private IEnumerator GetReportListFromServer(WWW www)
	{
		yield return www;
        
        string Response = www.text;
        string Error = www.error;

		if (Error == null) 
		{
			Response = Response.Replace("[","").Replace("]","").Replace("},{","}@{");

			string[] Reports = Response.Split('@');
			ReportList = new List<Report>();

			foreach(string Report in Reports)
			{
				Debug.Log("Report JSON Found: " + Report);
				
				Report aux = new Report();
				if (!String.IsNullOrEmpty(Report))
					ReportList.Add(aux.CreateReportByJSON(Report));
			}

			Debug.Log(ReportList.Count + " Reports recovered.");

			FillOptionsOnDropDown();
		} 
		else 
		{
			Debug.Log("Error trying to get reports: " + Error);
		}
	}

	// Retrieve data from DB and fill the Dropdown menu options with them
	public void FillOptionsOnDropDown()
	{
		ReportDropdown.options.Clear();
		ReportDropdown.options.Add(new Dropdown.OptionData() {text = SelectFieldOnScreen});
		
		foreach (Report rp in ReportList)
			ReportDropdown.options.Add(new Dropdown.OptionData() {text = "ID " + rp.GetID() + " (" + rp.GetType() + ")"});

	  	ReportDropdown.RefreshShownValue();
	}

	// This method updates the Map Field with the selected option on Dropdown menu
	public void MarkSelectedLocation()
	{
		Text AddressSelected = ReportDropdown.captionText;
		Report SelectedReport = null;	

		foreach (Report rp in ReportList)
		{
			if (AddressSelected.text.Equals("ID " + rp.GetID() + " (" + rp.GetType() + ")"))
			{
				SelectedReport = rp;
				break;
			}
		}

		if (SelectedReport != null)
		{
			StartCoroutine(UpdateMapOnScreen(SelectedReport));
			ReportStatus.enabled = true;
			ReportStatus.text = SelectedReport.GetStatus();
		}
	}

	private IEnumerator UpdateMapOnScreen(Report SelectedReport)
	{
		var request = new WWW(SelectedReport.GetQueryToAddress());
        yield return request;

        MapField.GetComponent<Renderer>().material.mainTexture = request.texture;
	}	
}
