using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;
using System.Collections;

public class Profile : GenericScene {

	// Page Elements
	public InputField NameField,
	                  EmailField,
	                  PasswordField,
	                  CPFField,
	                  BirthField,
	                  CameraPasswordField,
	                  PhoneField;

	public Button ChangePictureButton,
				  GroupsButton,
				  UpdateButton;

	public void Start () 
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		BackScene = "Home";

		FillFieldsWithUserInfo();
	}

	private void FillFieldsWithUserInfo()
	{
		User User = EventSystem.GetUser();

		NameField.text = User.GetName();
		EmailField.text = User.GetEmail();
		CPFField.text = User.GetCPF();
		BirthField.text = User.GetBirth();
		CameraPasswordField.text = User.GetCameraPassword();
		PhoneField.text = User.GetPhone();
	}

	private bool AreFieldsFilledCorrectly()
	{
		if (EmailField.text.Length < 5) 
			return EnableNotification(5, InvalidMailLength);

		if (PasswordField.text.Length == 0)
			return EnableNotification(5, RefillPasswordError);

		if (PasswordField.text.Length < 5) 
			return EnableNotification(5, InvalidPassLength);
		
		return true;
	}

	public void PrepareSendForms()
	{
		if (!AreFieldsFilledCorrectly())
			return;

		Address Address = EventSystem.GetUser().GetAddress();

		if (Address != null)
			PrepareAddressForm();
		else
			PrepareUserForm();
	}

	// Send updated data to db
	private void PrepareAddressForm()
	{
		URL = "http://aqua-web.herokuapp.com/api/address";
		pvtkey = "fc64ec6244";
		
		User User = EventSystem.GetUser();
		Address Address = User.GetAddress();

		WWWForm form = new WWWForm ();
		form.AddField ("zipcode", Address.GetZIP());
		form.AddField ("street", Address.GetStreet());
		form.AddField ("number", Address.GetNumber());
		form.AddField ("district", Address.GetDistrict());
		form.AddField ("city", Address.GetCity());
		form.AddField ("state", Address.GetState());
		form.AddField ("complement", Address.GetComplement());
		WWW www;

		bool IsCreatingAddress;

		if (User.GetAddressID() == 0) // There is no Address in the current user. Creates a new Address on DB.
		{
			Debug.Log ("Creating User Address: " + URL + "/" + pvtkey);

			www = new WWW (URL + "/" + pvtkey, form);
			IsCreatingAddress = true;
		}
		else // Update a created Address on DB.
		{
			Debug.Log ("Updating User Address: " + URL + "/" + User.GetAddressID() + "/" + pvtkey);
			
			www = new WWW (URL + "/" + User.GetAddressID() + "/" + pvtkey, form);
			IsCreatingAddress = false;
		}

		StartCoroutine (SendAddressToDB(www, IsCreatingAddress));
	}

    private IEnumerator SendAddressToDB(WWW www, bool IsCreatingAddress)
    {
        yield return www;
        
        string Response = www.text;
        string Error = www.error;

		if (www.error == null) 
		{
			Debug.Log("Address ID received: " + Response);

			if (IsCreatingAddress) // If its creating an Address, the user must receive the new address id in address variable.
			{
				User aux = EventSystem.GetUser();
				aux.SetAddressID(int.Parse(Response));

				Debug.Log("Trying to update user Address ID to " + Response);
				
				EventSystem.UpdateGlobalUser(aux);
			}

			PrepareUserForm();
		} 
		else 
		{
			Debug.Log("Error on Send Address: " + Error);
		}
     }

	// Send updated data to db
	private void PrepareUserForm()
	{
		URL = "http://aqua-web.herokuapp.com/api/user";
		pvtkey = "6b2b7f9bc0";

		User User = EventSystem.GetUser();

		User.SetName(NameField.text);
		User.SetEmail(EmailField.text);
		User.SetPassword(CalculateSHA1(PasswordField.text));
		User.SetBirth(BirthField.text);
		User.SetCPF(CPFField.text);
		User.SetPhone(PhoneField.text);

		EventSystem.UpdateGlobalUser(User);

		WWWForm form = new WWWForm();
		form.AddField ("name", User.GetName());
		form.AddField ("email", User.GetEmail());
		form.AddField ("password", User.GetPassword());
		form.AddField ("birth", User.GetBirth());
		form.AddField ("cpf", User.GetCPF());
		form.AddField ("phone", User.GetPhone());
		if (User.GetAddressID() != null && User.GetAddressID() != 0)
			form.AddField ("address", User.GetAddressID());
		WWW www = new WWW (URL + "/" + EventSystem.GetUser().GetID() + "/" + pvtkey, form);

		Debug.Log("Updating User Info to: " + URL + "/" + EventSystem.GetUser().GetID() + "/" + pvtkey);
		Debug.Log("New User Address ID: " + User.GetAddressID());

		StartCoroutine (SendUserForm(www));
	}

	// Wait until receive some data from server
    private IEnumerator SendUserForm(WWW www)
    {
        yield return www;
        
        string Response = www.text;
        string Error = www.error;

		if (Error == null) 
		{
			Debug.Log("Response: " + Response);

			if (Response.Equals("1"))
				EnableNotification(3, UpdateInfoSuccess, BackScene);
		} 
		else 
		{
			Debug.Log("Error on Send User: " + Error);
		}
     }    
}
