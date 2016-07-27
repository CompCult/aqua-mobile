using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;
using System;
using System.Collections;
 
public class Login : GenericScene {

	public InputField EmailField, PasswordField;
	private string HomeScene = "Home";

	public void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		BackScene = null;
	}

	public bool AreFieldsFilledCorrectly()
	{	
		if (!IsValidEmail(EmailField.text))
			return EnableNotification (4, InvalidMailString);

		if (EmailField.text.Length < 5)
			return EnableNotification (4, InvalidMailLength);
		
		if (PasswordField.text.Length < 5)
			return EnableNotification (4, InvalidPassLength);

		return true;
	}

    public void PrepareGetUserIDForm() 
	{
		URL = "http://aquaguardians.com.br/api/auth";
		pvtkey = "f51e8e6754";

		if (!AreFieldsFilledCorrectly())
			return;

		EnableNotification(ConnectingMessage);
		
		WWWForm form = new WWWForm ();
		form.AddField ("login", EmailField.text);
		form.AddField ("password", CalculateSHA1(PasswordField.text));
		WWW www = new WWW (URL + "/" + pvtkey, form);

		StartCoroutine(ReceiveUserIDFromDB(www));
	}
 
    private IEnumerator ReceiveUserIDFromDB(WWW www)
    {
        yield return www;
        
        string Response = www.text;
        string Error = www.error;

		if (Error == null) 
		{
			if (Response.Equals("0"))
				EnableNotification(3, InvalidLogin);
			else 
			{
				Debug.Log("User connected with ID " + Response);

				EventSystem.UpdateGlobalUser(int.Parse(Response));
				EnableNotification(2, ConnectingMessage, HomeScene);
			}
		} 
		else 
		{
			string ErrorCode = Error.Split(' ')[0];

			Debug.Log("Error on ID Get: " + Error);

			if (ErrorCode.Contains("404"))
				EnableNotification(3, InvalidLogin);
			else if (ErrorCode.Contains("Could"))
				EnableNotification(4, ConnectionFailed);
			else
				EnableNotification(4, ServerFailed);
		}
     }    
 }