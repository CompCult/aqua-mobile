﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class Profile : Screen {

	public InputField nameField,
	emailField,
	passField,
	repPassField,
	birthField,
	cpfField,
	phoneField;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		UpdateFields();

		backScene = "Home";
	}
	
	public void UpdateFields () 
	{
		User user = UsrManager.user;

		nameField.text = user.name;
		emailField.text = user.email;
		birthField.text = user.birth;
		cpfField.text = user.cpf;
		phoneField.text = user.phone;
	}

	public void UpdateUserInfo()
	{
		string name = nameField.text,
		email = emailField.text,
		pass = passField.text,
		repPass = repPassField.text,
		birth = birthField.text,
		cpf = cpfField.text,
		phone = phoneField.text;

		if (!CheckFields(name, email, birth, cpf, phone, pass, repPass))
			return;

		UnityAndroidExtras.instance.makeToast("Atualizando", 1);

		WWW updateRequest = Authenticator.UpdateUser(name, email, birth, cpf, phone, pass);
		ProcessUpdate(updateRequest);
	}

	public void ProcessUpdate(WWW updateRequest)
	{
		string Error = updateRequest.error,
		Response = updateRequest.text;

		if (Error == null) 
		{
			Debug.Log("Update response: " + Response);
			
			UnityAndroidExtras.instance.makeToast("Perfil atualizado", 1);
			LoadScene(backScene);
		}
		else 
		{
			Debug.Log("Error on update: " + Error);
			UnityAndroidExtras.instance.makeToast("Falha ao atualizar. Tente novamente mais tarde.", 1);
		}
	}

	private bool CheckFields (string name, string email, string birth, string cpf, string phone, string pass, string repPass)
	{
		string errorMessage = "";

		if (name.Length < 3)
			errorMessage = "Seu nome deve conter pelo menos 3 caracteres";
		if (!CheckEmail(email))
			errorMessage = "Insira um e-mail válido";
		if (birth.Length != 0 && !CheckDate(birth, "dd/mm/yyyy"))
			errorMessage = "Insira uma data de nascimento válida";
		if (cpf.Length != 0 && !CheckCPF(cpf))
			errorMessage = "Insira um CPF válido";
		if (phone.Length != 0 && phone.Length < 10)
			errorMessage = "Insira um número de telefone válido";
		if (pass.Length < 6 || repPass.Length < 6)
			errorMessage = "As senhas devem possuir pelo menos 6 caracteres";
		if (pass != repPass)
			errorMessage = "As senhas não confirmam";

		if (errorMessage != "") {
			UnityAndroidExtras.instance.makeToast(errorMessage, 1);
			return false;
		}
		
		return true;
	}

	public static bool CheckEmail(string email)
	{
		string strRegex = @"^([a-zA-Z0-9_\-\.a-zA-Z0-9]+)@((\[[0-9]{1,3}" +
    	 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + 
     	@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
	 	Regex reg = new Regex(strRegex);
		
		return reg.IsMatch(email);
	}	

	public static bool CheckDate(string date, string format)
	{
		DateTime Test;
		
		return DateTime.TryParseExact(date, format, null, DateTimeStyles.None, out Test);
	}

	public static bool CheckCPF(string cpf)
	{
		string strRegex = @"^\d{3}\.\d{3}\.\d{3}\-\d{2}$";
	 	Regex reg = new Regex(strRegex);
		
		return reg.IsMatch(cpf);
	}
}
