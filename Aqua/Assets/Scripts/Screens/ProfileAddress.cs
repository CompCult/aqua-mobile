﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class ProfileAddress : Screen {

	public InputField zipField,
	streetField,
	numberField,
	districtField,
	cityField,
	stateField,
	complementField;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		ReceiveAddress ();

		backScene = "Profile";
	}
	
	public void ReceiveAddress () 
	{
		User user = UsrManager.user;
		int noAddress = -1;

		// If the user have address, receive it.
		if (user.address != noAddress)
		{
			WWW addressRequest = Authenticator.RequestAddress(user.address);
			ProcessAddress(addressRequest);
		}
	}

	public void ProcessAddress (WWW addressRequest)
	{
		string Error = addressRequest.error,
		Response = addressRequest.text;

		if (Error == null) 
		{
			AddressManager.UpdateAddress(Response);
			UpdateFields();
		}
		else 
		{
			Debug.Log("Error on get address: " + Error);

			UnityAndroidExtras.instance.makeToast("Falha ao obter seu endereço. Tente novamente mais tarde.", 1);
			LoadScene(backScene);
		}
	}

	public void UpdateFields()
	{
		Address address = AddressManager.address;

		zipField.text = address.zipcode;
		streetField.text = address.street;
		numberField.text = address.number;
		districtField.text = address.district;
		cityField.text = address.city;
		stateField.text = address.state;
		complementField.text = address.complement;
	}

	public void UpdateAddress()
	{
		string zipcode = zipField.text,
		street = streetField.text,
		number = numberField.text,
		district = districtField.text,
		city = cityField.text,
		state = stateField.text,
		complement = complementField.text;

		if (!CheckFields(zipcode, state))
			return;

		WWW updateRequest = Authenticator.UpdateAddress(zipcode, street, number, district, city, state, complement);
		ProcessUpdate(updateRequest);
	}

	public void ProcessUpdate(WWW updateRequest)
	{
		string Error = updateRequest.error,
		Response = updateRequest.text;

		if (Error == null) 
		{
			Debug.Log("Response: " + Response);

			LoadScene(backScene);
		}
		else 
		{
			Debug.Log("Error on update address: " + Error);

			UnityAndroidExtras.instance.makeToast("Falha ao obter seu endereço. Tente novamente mais tarde.", 1);
		}
	}

	public bool CheckFields(string zipcode, string state)
	{
		string errorMessage = "";

		if (zipcode.Length != 8)
			errorMessage = "Insira um CEP válido.";
		if (state.Length != 2)
			errorMessage = "Insira um estado válido.";

		if (errorMessage != "")
		{
			UnityAndroidExtras.instance.makeToast(errorMessage, 1);
			return false;
		}

		return true;
	}
}
