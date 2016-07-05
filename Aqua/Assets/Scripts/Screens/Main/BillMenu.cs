using UnityEngine;
using System.Collections;

public class BillMenu : GenericScene {

	// Use this for initialization
	public void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		
		BackScene = "Home";
	}
}
