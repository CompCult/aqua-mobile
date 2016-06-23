using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;
using System;
using System.Collections;

public class API {

	private string APIUrl = "http://aqua-web.herokuapp.com/api",
				   LoginKey = "f51e8e6754";

	// Return the ID from logged user
	IEnumerator LoginRoutine(string Username, string Password) 
	{
		WWWForm form = new WWWForm ();
		form.AddField ("login", Username);
		form.AddField ("password", Password);
		WWW www = new WWW (APIUrl + "/auth/" + LoginKey, form);

		yield return www;

		if (www.error == null) 
			yield return www.text;
		
		else 
			yield return www.error.Split(' ')[0];
	}
	

}
