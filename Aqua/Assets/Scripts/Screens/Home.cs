using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Home : GenericScreen {

	public Text NameField;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		RequestUser();

		backScene = "Login";
	}

	public new void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) 
		{
			UnityAndroidExtras.instance.makeToast("Você saiu do mundo de Aqua!", 1);
			LoadBackScene();
		}
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
		NameField.text = UsrManager.user.name;
	}
}
