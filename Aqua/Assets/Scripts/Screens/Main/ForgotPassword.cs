using UnityEngine;
using System.Collections;

public class ForgotPassword : GenericScene {

	public void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		BackScene = "Logout";
	}
}
