using UnityEngine;
using System.Collections;

public class ForgotPassword : GenericScene {

	// Use this for initialization
	public void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		this.BackScene = "Login";
	}
}
