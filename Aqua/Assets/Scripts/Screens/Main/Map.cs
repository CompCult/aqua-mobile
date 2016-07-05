using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Map : GenericScene {

	public Dropdown ReportDropdown;
	public Text ReportStatus;
	public GameObject MapField, Fade;
	
	private List<Report> ReportList;

	public void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		BackScene = "Home";

		// Set the Fade Enabled to prevent user clicking
		Fade.SetActive(true);

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
			Response = Response.Replace("[","").Replace("]","").Replace("},{","}@{"); // Filter the JSON to receive a Split

			string[] Reports = Response.Split('@');
			ReportList = new List<Report>();

			foreach(string Report in Reports)
			{
				Debug.Log("Report JSON Found: " + Report);

				// Remove the Fade from screen
				Fade.SetActive(false);
				
				Report aux = new Report();
				if (!String.IsNullOrEmpty(Report))
					ReportList.Add(aux.CreateReportByJSON(Report));
			}

			Debug.Log(ReportList.Count + " reports recovered.");

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
			ReportDropdown.options.Add(new Dropdown.OptionData() {text = "Notificação - ID " + rp.GetID()});

	  	ReportDropdown.RefreshShownValue();
	}

	// This method updates the Map Field with the selected option on Dropdown menu
	public void MarkSelectedLocation()
	{
		Text AddressSelected = ReportDropdown.captionText;
		Report SelectedReport = null;	

		foreach (Report rp in ReportList)
		{
			if (AddressSelected.text.Equals("Notificação - ID " + rp.GetID()))
			{
				SelectedReport = rp;
				break;
			}
		}

		if (SelectedReport != null)
		{
			StartCoroutine(UpdateMapOnScreen(SelectedReport));
			
			ReportStatus.enabled = true;
			
			switch (SelectedReport.GetStatus())
			{
				case "pending":
					ReportStatus.text = "Pendente"; break;
				case "invalid":
					ReportStatus.text = "Inválida"; break;
				case "validated":
					ReportStatus.text = "Validada"; break;
				default:
					ReportStatus.text = ""; break;
			}
		}
	}

	private IEnumerator UpdateMapOnScreen(Report SelectedReport)
	{
		var request = new WWW(SelectedReport.GetQueryToAddress());
        yield return request;

        MapField.GetComponent<Renderer>().material.mainTexture = request.texture;
	}	
}
