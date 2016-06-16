using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;
using System;
using System.Collections;
 
public class Login : GenericScene {

	// Page Elements
	public InputField EmailField,
	                  PasswordField;

	public EventSystem EventSystem;

	// Page connection variables to use
	string URL = "http://aqua-web.herokuapp.com/api/auth/";
	string pvtkey = "f51e8e6754";
	string HomeScene = "Home";

	void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
	}

	// Try to connect with the db using the email's text field and password's text field
    void TryConnection() 
	{
		if (PasswordField.text.Length < 5)
			EnableNotification (4, InvalidPassLength);
		else if (EmailField.text.Length < 5)
			EnableNotification (4, InvalidMailLength);
		else {
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
        string response = www.text;

		if (www.error == null) {

			if (response == "0")
				EnableNotification(3, InvalidLogin);
			else {
				Debug.Log("User connected with ID " + response);

				EventSystem.CreateUser(int.Parse(response));
				LoadScene(HomeScene);
			}
		} 
		else 
		{
			string error = www.error.Split(' ')[0];

			if (error.Equals("404"))
				EnableNotification(3, InvalidLogin);
			else 
				EnableNotification(4, ServerFailed);
		}
     }    
 }