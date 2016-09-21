﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class Login : Screen 
{
	[Header("Screen elements")]
	public InputField emailField, passField;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();

		backScene = null;
	}
	
	public void SignIn () 
	{
		string email = emailField.text,
		password = passField.text;

		if (!AreFieldsCorrect(email, password))
			return;

		UnityAndroidExtras.instance.makeToast("Conectando", 1);

		WWW loginRequest = Authenticator.RequestUserID(email, password);
		ProcessLogin (loginRequest);
	}

	public void ProcessLogin (WWW loginRequest)
	{
		string Error = loginRequest.error,
		Response = loginRequest.text;

		if (Error == null) 
		{
			Debug.Log("ID received: " + Response);

			UsrManager.userID = int.Parse(Response);
			LoadScene("Home");
		}
		else 
		{
			if (Error.Contains("404 "))
				UnityAndroidExtras.instance.makeToast("Não encontrado. Verifique o e-mail e senha.", 1);
			if (Error.Contains("500 "))
				UnityAndroidExtras.instance.makeToast("Houve um problema no Servidor. Tente novamente mais tarde.", 1);
		}
	}

	public bool AreFieldsCorrect (string email, string password)
	{
		if (!CheckEmail(email)) 
		{
			UnityAndroidExtras.instance.makeToast("Insira um e-mail válido", 1);
			return false;
		}

		if (password.Length < 6) 
		{
			UnityAndroidExtras.instance.makeToast("A senha deve conter, pelo menos, 6 caracteres", 1);
			return false;
		}

		return true;
	}

	public bool CheckEmail(string email)
	{
		string emailRegularExpression = @"^([a-zA-Z0-9_\-\.a-zA-Z0-9]+)@((\[[0-9]{1,3}" +
    	 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + 
     	@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

	 	Regex reg = new Regex(emailRegularExpression);
		return reg.IsMatch(email);
	}	
}
