using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class Profile : GenericScreen {

	public InputField nameField,
	emailField,
	passField,
	repPassField,
	birthField,
	cpfField,
	phoneField;

	public void Start () 
	{
		AlertsAPI.instance.Init();
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

	public void UpdateLocalUser()
	{
		User user = UsrManager.user;

		user.name = nameField.text;
		user.email = emailField.text;
		user.birth = birthField.text;
		user.cpf = cpfField.text;
		user.phone = phoneField.text;

		UsrManager.UpdateUser(user);
	}

	public void UpdateUserInfo()
	{
		string name = nameField.text,
		email = emailField.text,
		pass = passField.text,
		repPass = repPassField.text,
		birth = birthField.text,
		cpf = cpfField.text,
		phone = phoneField.text,
		address = UsrManager.user.address.ToString ();

		if (!CheckFields(name, email, birth, cpf, phone, pass, repPass))
			return;

		WWW updateRequest = UserAPI.UpdateUser(name, email, birth, cpf, address, phone, pass);
		ProcessUpdate(updateRequest);
	}

	public void ProcessUpdate(WWW updateRequest)
	{
		string Error = updateRequest.error,
		Response = updateRequest.text;

		if (Error == null) 
		{
			Debug.Log("Update response: " + Response);
			
			AlertsAPI.instance.makeToast("Perfil atualizado", 1);

			UpdateLocalUser();
			LoadScene(backScene);
		}
		else 
		{
			Debug.Log("Error on update: " + Error);
			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao atualizar seu perfil. Tente novamente em alguns instantes.", "Tudo bem");
		}
	}

	private bool CheckFields (string name, string email, string birth, string cpf, string phone, string pass, string repPass)
	{
		string errorMessage = "";

		if (name.Length < 3)
			errorMessage = "Seu nome deve conter pelo menos 3 caracteres";
		if (!CheckEmail(email))
			errorMessage = "Insira um e-mail válido.";
		if (birth.Length != 0 && !CheckDate(birth, "dd/mm/yyyy"))
			errorMessage = "Insira uma data de nascimento válida.";
		if (cpf.Length != 0 && !CheckCPF(cpf))
			errorMessage = "Insira um CPF válido\nO formato correto é 111.222.333-55.";
		if (phone.Length != 0 && phone.Length < 10)
			errorMessage = "Insira um número de telefone válido\nInsira seu telefone com DDD.";
		if (pass.Length < 6 || repPass.Length < 6)
			errorMessage = "As senhas devem possuir pelo menos 6 caracteres.";
		if (pass != repPass)
			errorMessage = "As senhas digitadas não estão iguais.";

		if (errorMessage != "") {
			AlertsAPI.instance.makeAlert(errorMessage, "OK");
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
