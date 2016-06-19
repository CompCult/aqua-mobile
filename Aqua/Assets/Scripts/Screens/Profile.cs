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
	                  CEPField,
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
		URL = "http://aqua-web.herokuapp.com/api/user/";
		pvtkey = "6b2b7f9bc0";

		FillFields();
	}
	
	// Fill all the fields with the given information during login
	public void FillFields()
	{
		User GlobalUser = EventSystem.GetUser();

		Debug.Log ("Showing user: " + GlobalUser.GetName() + " with ID " + GlobalUser.GetID());

		NameField.text = GlobalUser.GetName();
		EmailField.text = GlobalUser.GetEmail();
		PasswordField.text = GlobalUser.GetPassword();
		CPFField.text = GlobalUser.GetCPF();
		BirthField.text = GlobalUser.GetBirth();
		CameraPasswordField.text = GlobalUser.GetCameraPassword();
		//CEPField.text = GlobalUser.GetCEP();
	}

	// Action performed after clicking "Update Button"
	public void UpdateInformation()
	{
		if (!AreFieldsFilled())
			return;

		User GlobalUser = EventSystem.GetUser();

		GlobalUser.SetName(NameField.text);
		GlobalUser.SetEmail(EmailField.text);
		GlobalUser.SetPassword(CalculateSHA1(PasswordField.text));
		GlobalUser.SetBirth(BirthField.text);
		GlobalUser.SetCPF(CPFField.text);

		EventSystem.CreateUser(GlobalUser);

		WWWForm form = new WWWForm ();
		form.AddField ("name", GlobalUser.GetName());
		form.AddField ("email", GlobalUser.GetEmail());
		form.AddField ("password", GlobalUser.GetPassword());
		form.AddField ("birth", GlobalUser.GetBirth());
		form.AddField ("cpf", GlobalUser.GetCPF());
		WWW www = new WWW (URL + EventSystem.GetUser().GetID() + "/" + pvtkey, form);

		StartCoroutine (WaitForRequest (www));
	}

	// Checks if the fields Email and Password are filled correctly
	private bool AreFieldsFilled()
	{
		if (EmailField.text.Length < 5) 
		{
			EnableNotification(5, InvalidMailLength);
			return false;
		}

		if (PasswordField.text.Length < 5) 
		{
			EnableNotification(5, InvalidPassLength);
			return false;
		}

		if (PasswordField.text.Length > 28) {
			EnableNotification(5, RefillPasswordError);
			return false;
		}
		
		return true;
	}

	// Wait until receive some data from server
    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        string response = www.text;

		if (www.error == null) 
		{
			if (response == "1") 
			{
				Debug.Log("Updating information for USER ID " + response);
				
				FillFields();

				EnableNotification(3, UpdateInfoSuccess);
			} 
			else 
			{
				EnableNotification(3, UpdateInfoFail);
			}

		} 
		else 
		{
			Debug.Log("Error updating at URL: " + URL + EventSystem.GetUser().GetID() + "/" + pvtkey);
			Debug.Log("Error: " + www.error);

			EnableNotification(5, ServerFailed);
		}
     }    
}
