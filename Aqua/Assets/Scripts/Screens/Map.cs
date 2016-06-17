using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Map : GenericScene {

	// Page elements
	public Dropdown ReportList;
	public GoogleMaps GoogleMaps;

	// Internal elements
	private List<Report> ReportObjects;

	// Use this for initialization
	public void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		
		BackScene = "Home";
	}
	
}
