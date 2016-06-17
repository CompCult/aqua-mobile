using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class CameraScreen : GenericScene 
{
	public void Start()
	{
	    EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
	    BackScene = "Home";
	}
}