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
	                  CameraPasswordField;

	public Button ChangePictureButton,
				  GroupsButton,
				  UpdateButton;

	// Use this at scene initialization
	public void Start () 
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		BackScene = "Home";

		FillFields();
	}
	
	// Fill all the fields with the given information during login
	public void FillFields()
	{
		User GlobalUser = EventSystem.GetUser();

		NameField.text = GlobalUser.GetName();
		EmailField.text = GlobalUser.GetEmail();
		CPFField.text = GlobalUser.GetCPF();
		BirthField.text = GlobalUser.GetBirth();
		CameraPasswordField.text = GlobalUser.GetCameraPassword();
	}

	// Checks if the fields Email and Password are filled correctly
	private bool AreFieldsFilled()
	{
		if (EmailField.text.Length < 5) 
			return EnableNotification(5, InvalidMailLength);

		else if (PasswordField.text.Length == 0)
			return EnableNotification(5, RefillPasswordError);

		else if (PasswordField.text.Length < 5) 
			return EnableNotification(5, InvalidPassLength);
		
		return true;
	}

	// Send the new user information to DB
	public void UpdateGlobalUserOnDB()
	{
		if (!AreFieldsFilled())
			return;

		Address Address = EventSystem.GetUser().GetAddress();

		if (Address != null)
			UpdateAddressOnDB();
		else
			UpdateUserOnDB();
	}

	// Send updated data to db
	public void UpdateAddressOnDB()
	{
		User GlobalUser = EventSystem.GetUser();
		Address Address = GlobalUser.GetAddress();

		URL = "http://aqua-web.herokuapp.com/api/address/";
		pvtkey = "6b2b7f9bc0";
		
		bool IsCreatingAddress;

		WWWForm form = new WWWForm ();
		form.AddField ("zipcode", Address.GetZIP());
		form.AddField ("street", Address.GetStreet());
		form.AddField ("number", Address.GetNumber());
		form.AddField ("district", Address.GetDistrict());
		form.AddField ("city", Address.GetCity());
		form.AddField ("state", Address.GetState());
		form.AddField ("complement", Address.GetComplement());
		WWW www;

		if (GlobalUser.GetAddressID() == 0) // There is no Address in the current user. Creates a new Address on DB
		{
			www = new WWW (URL + "/" + pvtkey, form);
			IsCreatingAddress = true;
		}
		else
		{
			www = new WWW (URL + GlobalUser.GetAddressID() + "/" + pvtkey, form);
			IsCreatingAddress = false;
		}

		StartCoroutine (SendAddressForm(www, IsCreatingAddress));
	}

	// Wait until receive some data from server
    private IEnumerator SendAddressForm(WWW www, bool IsCreatingAddress)
    {
        yield return www;
        string response = www.text;

		if (www.error == null) 
		{
			Debug.Log("Updating Address information...");
			Debug.Log("Response: " + response);

			if (IsCreatingAddress) 
			{
				User aux = EventSystem.GetUser();
				aux.SetAddressID(int.Parse(response));

				Debug.Log("Trying to update user Address ID to " + response);
				EventSystem.UpdateGlobalUser(aux);
			}

			UpdateUserOnDB();
		} 
		else 
		{
			Debug.Log("Error: " + www.error);
		}
     }

	// Send updated data to db
	public void UpdateUserOnDB()
	{
		User GlobalUser = EventSystem.GetUser();

		URL = "http://aqua-web.herokuapp.com/api/user/";
		pvtkey = "6b2b7f9bc0";

		GlobalUser.SetName(NameField.text);
		GlobalUser.SetEmail(EmailField.text);
		GlobalUser.SetPassword(CalculateSHA1(PasswordField.text));
		GlobalUser.SetBirth(BirthField.text);
		GlobalUser.SetCPF(CPFField.text);

		EventSystem.UpdateGlobalUser(GlobalUser);

		WWWForm form = new WWWForm ();
		form.AddField ("name", GlobalUser.GetName());
		form.AddField ("email", GlobalUser.GetEmail());
		form.AddField ("password", GlobalUser.GetPassword());
		form.AddField ("birth", GlobalUser.GetBirth());
		form.AddField ("cpf", GlobalUser.GetCPF());
		form.AddField ("address", GlobalUser.GetAddressID());
		WWW www = new WWW (URL + EventSystem.GetUser().GetID() + "/" + pvtkey, form);

		Debug.Log("New User Address ID: " + GlobalUser.GetAddressID());

		StartCoroutine (SendUserForm(www));
	}

	// Wait until receive some data from server
    private IEnumerator SendUserForm(WWW www)
    {
        yield return www;
        string response = www.text;

		if (www.error == null) 
		{
			Debug.Log("Updating user information...");
			Debug.Log("Response: " + response);
		} 
		else 
		{
			Debug.Log("Error: " + www.error);
		}
     }    
}
