using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Home : GenericScene {

	public Text NameField,
		   		LevelField;

	public GameObject Fade;

	public void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		BackScene = "Login";

		// Put the Fade on Screen to preven user clicks
		Fade.SetActive(true);

		PrepareGetUserForm();
	}

    public void PrepareGetUserForm() 
	{
		URL = "http://aqua-web.herokuapp.com/api/user";
		pvtkey = "6b2b7f9bc0";

		Debug.Log("Trying to Get User at " + URL + "/" + EventSystem.GetUser().GetID() + "/" + pvtkey);

		WWW www = new WWW (URL + "/" + EventSystem.GetUser().GetID() + "/" + pvtkey);
		StartCoroutine(ReceiveUserFromDB(www));
	}

    private IEnumerator ReceiveUserFromDB(WWW www)
    {
        yield return www;

        string Response = www.text;
        string Error = www.error;

		if (Error == null) 
		{
			EventSystem.UpdateGlobalUser(Response);
			UpdateNameAndLevelOnScreen();

			Debug.Log("User JSON: " + Response);
			Debug.Log("Connected with an user with Address ID " + EventSystem.GetUser().GetAddressID());

			// Remove the fade from screen
			Fade.SetActive(false);

			if (EventSystem.GetUser().GetAddressID() != 0) // If the user have a registered address, get it.
				PrepareGetAddressForm();
		}
		else 
		{
			Debug.Log("Error on User Get: " + Error);
			
			LoadScene(BackScene);
		}
     }   

	public void PrepareGetAddressForm() 
	{
		URL = "http://aqua-web.herokuapp.com/api/address";
		pvtkey = "fc64ec6244";

		Debug.Log("Trying to get User Address at: " + URL + "/" + EventSystem.GetUser().GetAddressID() + "/" + pvtkey);

		WWW www = new WWW (URL + "/" + EventSystem.GetUser().GetAddressID() + "/" + pvtkey);
		StartCoroutine(ReceiveAddressFromDB(www));
	}  

    private IEnumerator ReceiveAddressFromDB(WWW www)
    {
        yield return www;
        
        string Response = www.text;
        string Error = www.error;

		if (Error == null) 
		{
			Debug.Log("User Address JSON: " + Response);

			EventSystem.UpdateGlobalUserAddress(Response);
		}
		else 
		{
			Debug.Log("Error on Address Get: " + Error);
			
			LoadScene(BackScene);
		}
     } 

    public void UpdateNameAndLevelOnScreen()
	{
		NameField.text = EventSystem.GetUser().GetName();
		LevelField.text = "Level " + EventSystem.GetUser().GetLevel().ToString();
	}  
}
