using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;
using System;
using System.Collections;
 
public class Login : GenericScene {

	// Page Elements
	public InputField EmailField, PasswordField;

	// Page connection variables to use
	private string HomeScene = "Home";


	public void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		
		BackScene = null;
		URL = "http://aqua-web.herokuapp.com/api/auth/";
		pvtkey = "f51e8e6754";
	}

	// Try to connect with the db using the email's text field and password's text field
    private void TryConnection() 
	{
		if (PasswordField.text.Length < 5)
			EnableNotification (4, InvalidPassLength);
		else if (EmailField.text.Length < 5)
			EnableNotification (4, InvalidMailLength);
		else 
		{
			WWWForm form = new WWWForm ();
			form.AddField ("login", EmailField.text);
			form.AddField ("password", CalculateSHA1(PasswordField.text));
			WWW www = new WWW (URL + pvtkey, form);

			StartCoroutine (WaitForRequest (www));
		}
	}
 
 	// Wait until receive some data from server
    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        string Response = www.text;

		if (www.error == null) 
		{
			if (Response.Equals("0"))
				EnableNotification(3, InvalidLogin);
			else 
			{
				Debug.Log("User connected with ID " + Response);

				EventSystem.CreateUser(int.Parse(Response));
				LoadScene(HomeScene);
			}
		} 
		else 
		{
			string Error = www.error.Split(' ')[0];

			if (Error.Equals("404"))
				EnableNotification(3, InvalidLogin);
			else 
				EnableNotification(4, ServerFailed);
		}
     }    
 }