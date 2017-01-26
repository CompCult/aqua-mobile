using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class ProfileAddress : GenericScreen {

	public InputField zipField,
	streetField,
	numberField,
	districtField,
	cityField,
	stateField,
	complementField;

	private int noAddress = 0;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		ReceiveAddress ();

		backScene = "Profile";
	}
	
	public void ReceiveAddress () 
	{
		User user = UsrManager.user;

		// If the user have address, receive it.
		if (user.address != noAddress)
		{
			WWW addressRequest = AddressAPI.RequestAddress(user.address);
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

			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao receber seu endereço. Tente novamente em alguns instantes.", "Tudo bem");
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
		User user = UsrManager.user;

		if (!CheckFields(zipcode, state))
			return;

		// Checks if the user have an address
		if (user.address != noAddress)
		{
			WWW updateRequest = AddressAPI.UpdateAddress(zipcode, street, number, district, city, state, complement);
			ProcessUpdate(updateRequest);
		}
		else 
		{
			WWW createAddressRequest = AddressAPI.CreateAddress(zipcode, street, number, district, city, state, complement);
			ProcessCreate(createAddressRequest);
		}
	}

	public void ProcessUpdate(WWW updateRequest)
	{
		string Error = updateRequest.error,
		Response = updateRequest.text;

		if (Error == null) 
		{
			Debug.Log("Response for update address: " + Response);

			LoadScene(backScene);
		}
		else 
		{
			Debug.Log("Error on update address: " + Error);

			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao receber seu endereço. Tente novamente em alguns instantes.", "Tudo bem");
		}
	}

	public void ProcessCreate(WWW createAddressRequest)
	{
		string Error = createAddressRequest.error,
		Response = createAddressRequest.text;

		if (Error == null) 
		{
			Debug.Log("Response for create address: " + Response);
			UsrManager.SetAddressID(int.Parse(Response));

			LoadScene(backScene);
		}
		else 
		{
			Debug.Log("Error on create address: " + Error);

			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao atualizar seu endereço. Tente novamente em alguns instantes.", "Tudo bem");
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
			AlertsAPI.instance.makeAlert(errorMessage, "OK");
			return false;
		}

		return true;
	}
}
