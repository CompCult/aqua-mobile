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

		FillOptionsOnDropDown();
	}

	// Retrieve data from DB and fill the Dropdown menu options with them
	public void FillOptionsOnDropDown()
	{
		// Connect to DB and receive JSON
		// TODO ************
		ReportList = new List<Report>();
		ReportList.Add(new Report("UFCG", -7.2155615, -35.9082753));
		//******************

		ReportDropdown.options.Clear();
		ReportDropdown.options.Add(new Dropdown.OptionData() {text = SelectFieldOnScreen});
		
		foreach (Report rp in ReportList)
			ReportDropdown.options.Add(new Dropdown.OptionData() {text = rp.GetAddress()});

	  	ReportDropdown.RefreshShownValue();
	}

	// This method updates the Map Field with the selected option on Dropdown menu
	public void MarkSelectedLocation()
	{
		Text AddressSelected = ReportDropdown.captionText;
		Report SelectedReport = null;	

		foreach (Report rp in ReportList)
		{
			if (AddressSelected.text.Equals(rp.GetAddress()))
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

	// Access the address of the selected notification using google maps API
	IEnumerator UpdateMapOnScreen(Report SelectedReport)
	{
		var request = new WWW(SelectedReport.GetQueryToAddress());
        yield return request;

        MapField.GetComponent<Renderer>().material.mainTexture = request.texture;
	}	
}
