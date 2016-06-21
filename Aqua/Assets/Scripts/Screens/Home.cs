using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Home : GenericScene {

	// Page elements
	public Text NameField,
		   		LevelField;

	// Use this for initialization
	public void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		BackScene = "Login";

		TryGetUser();
	}

	// Update name and level on screen
	public void UpdateUserOnScreen()
	{
		NameField.text = EventSystem.GetUser().GetName();
		LevelField.text = "Level " + EventSystem.GetUser().GetLevel().ToString();
	}

	// Try to connect with the db using the ID to get user data
    public void TryGetUser() 
	{
		URL = "http://aqua-web.herokuapp.com/api/user/";
		pvtkey = "6b2b7f9bc0";

		Debug.Log("Trying to get user data at " + URL + EventSystem.GetUser().GetID() + "/" + pvtkey);

		WWW www = new WWW (URL + EventSystem.GetUser().GetID() + "/" + pvtkey);
		StartCoroutine(ReceiveUserForm(www));
	}

	public void TryGetAddress() 
	{
		URL = "http://aqua-web.herokuapp.com/api/address/";
		pvtkey = "fc64ec6244";

		Debug.Log("Trying to get user address at: " + URL + EventSystem.GetUser().GetAddressID() + "/" + pvtkey);

		WWW www = new WWW (URL + EventSystem.GetUser().GetAddressID() + "/" + pvtkey);
		StartCoroutine(ReceiveAddressForm(www));
	}

 	// Wait until receive some data from server
    private IEnumerator ReceiveUserForm(WWW www)
    {
        yield return www;

		if (www.error == null) 
		{
			string json = www.text;

			EventSystem.UpdateGlobalUser(json);
			UpdateUserOnScreen();

			Debug.Log("Connected with an user with Address ID " + EventSystem.GetUser().GetAddressID());

			TryGetAddress();
		}
		else 
		{
			Debug.Log("Error on User Get: " + www.error);
		}
     }   

    private IEnumerator ReceiveAddressForm(WWW www)
    {
        yield return www;

		if (www.error == null) 
		{
			string json = www.text;
			Debug.Log("json:" + json);

			EventSystem.UpdateGlobalUserAddress(json);
		}
		else 
		{
			Debug.Log("Error on Address Get: " + www.error);
		}
     }   
}
