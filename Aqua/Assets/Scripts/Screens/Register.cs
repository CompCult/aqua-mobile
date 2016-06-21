﻿using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;
using System.Collections;

public class Register : GenericScene {

	public InputField NameField,
					  EmailField,
					  PasswordField,
					  RepeatPasswordField;

	public void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		
		BackScene = "Login";
		URL = "http://aqua-web.herokuapp.com/api/user/";
		pvtkey = "6b2b7f9bc0";
	}

	private bool AreFieldsFilled()
	{
		if (NameField.text.Length < 2) 
			return EnableNotification (4, InvalidMailLength);

		else if (EmailField.text.Length < 5) 
			return EnableNotification (4, InvalidMailLength);

		else if (!PasswordField.text.Equals(RepeatPasswordField.text)) 
			return EnableNotification (4, PasswordDontMatch);

		else if (PasswordField.text.Length < 5 || RepeatPasswordField.text.Length < 5) 
			return EnableNotification (4, InvalidPassLength);
			
		return true;
	}

	public void TryRegister() 
	{
		if (!AreFieldsFilled())
			return;

		WWWForm form = new WWWForm ();
		form.AddField ("name", NameField.text);
		form.AddField ("email", EmailField.text);
		form.AddField ("password", CalculateSHA1(PasswordField.text));
		WWW www = new WWW (URL + pvtkey, form);

		if (Coroutine != null)
			StopCoroutine(Coroutine);

		Coroutine = SendNewUserForm(www);
		StartCoroutine(Coroutine);
	}

	private IEnumerator SendNewUserForm(WWW www)
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
				EnableNotification(3, SuccessRegistered, BackScene);
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