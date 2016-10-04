using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Home : GenericScreen {

	public Text NameField,
	LevelField;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		RequestUser();

		backScene = "Login";
	}

	public void RequestUser()
	{
		Debug.Log("Requesting user with ID " + UsrManager.userID);

		WWW userRequest = Authenticator.RequestUser(UsrManager.userID);
		string Response = userRequest.text,
		Error = userRequest.error;

		if (Error == null)
		{
			Debug.Log("Response: " + Response);

			UsrManager.UpdateUser(userRequest.text);
			UpdateFields();
		}
		else 
		{
			Debug.Log("Error: " + Error);

			UnityAndroidExtras.instance.makeToast("Falha ao receber usuário. Entre novamente.", 1);
			LoadScene("Login");
		}
	}

	public void UpdateFields()
	{
		int playerLevel = (UsrManager.user.xp / 1000) + 1;
		
		NameField.text = UsrManager.user.name;
		LevelField.text = "Level " + playerLevel;
	}
}
