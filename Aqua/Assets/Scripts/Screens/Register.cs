﻿using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;
using System.Collections;

public class Register : GenericScene {

	public InputField EmailField,
					  PasswordField,
					  RepeatPasswordField;

	string URL = "http://aqua-web.herokuapp.com/api/user/";
	string pvtkey = "6b2b7f9bc0";
	string LoginScene = "Login";

	public void TryRegister() 
	{
		if (!PasswordField.text.Equals (RepeatPasswordField.text))
			EnableNotification (4, PasswordDontMatch);
		else if (PasswordField.text.Length < 5 || RepeatPasswordField.text.Length < 5)
			EnableNotification (4, InvalidPassLength);
		else if (EmailField.text.Length < 5)
			EnableNotification (4, InvalidMailLength);
		else 
		{
			WWWForm form = new WWWForm ();
			form.AddField ("email", EmailField.text);
			form.AddField ("password", CalculateSHA1(PasswordField.text));
			WWW www = new WWW (URL + pvtkey, form);

			StartCoroutine (WaitForRequest (www));
		}
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		string response = www.text;

		Debug.Log(response);
		Debug.Log(www.error);

		if (www.error == null)
		{
			if (response == "1") 
			{
				Destroy(GameObject.Find("EventSystem"));
				EnableNotification(3, SuccessRegistered, LoginScene);
			}
			else
				EnableNotification(3, AlreadyRegistered);	
		}
		else 
		{
			string error = www.error.Split(' ')[0];

			if (error.Equals("500"))
				EnableNotification(3, AlreadyRegistered);
			else 
				EnableNotification(4, ServerFailed);
		}   
	}    
}