using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;
using System.Collections;

public class ProfileAddress : GenericScene {

	public InputField ZIPField,
                  StreetField,
                  NumberField,
                  DistrictField,
                  CityField,
                  StateField,
                  ComplementField;

	public Button UpdateButton;

	public void Start () 
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		BackScene = "Profile";

		FillFieldsWithAddressInfo();
	}

	private void FillFieldsWithAddressInfo()
	{
		Address Address = EventSystem.GetUser().GetAddress();

		if (Address == null) 
			Address = new Address();

		ZIPField.text = Address.GetZIP();
		StreetField.text = Address.GetStreet();
		NumberField.text = Address.GetNumber();
		DistrictField.text = Address.GetDistrict();
		CityField.text = Address.GetCity();
		StateField.text = Address.GetState();
		ComplementField.text = Address.GetComplement();
	}

	public void UpdateGlobalUserAddress()
	{
		Address Address = new Address();
		User User = EventSystem.GetUser();

		Address.SetZIP(ZIPField.text);
		Address.SetStreet(StreetField.text);
		Address.SetNumber(NumberField.text);
		Address.SetDistrict(DistrictField.text);
		Address.SetCity(CityField.text);
		Address.SetState(StateField.text);
		Address.SetComplement(ComplementField.text);

		User.SetAddress(Address);
		EventSystem.UpdateGlobalUser(User);

		EnableNotification(2, UpdateInfoSuccess, BackScene);
	}
}
