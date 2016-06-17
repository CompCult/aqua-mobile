using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Home : GenericScene {

	// Page elements
	public Text UserName,
		   		UserLevel;

	// Use this for initialization
	public void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		
		BackScene = "Login";
		URL = "http://aqua-web.herokuapp.com/api/user/";
		pvtkey = "6b2b7f9bc0";

		// Check if the user is already loaded
		TryGetUser();
	}

	// Update name and level on screen
	public void UpdateUserOnScreen()
	{
		UserName.text = EventSystem.GetUser().GetName();
		UserLevel.text = "Level " + EventSystem.GetUser().GetLevel().ToString();
	}

	// Try to connect with the db using the ID to get user data
    public void TryGetUser() 
	{
		URL += EventSystem.GetUser().GetID() + "/" + pvtkey;
		Debug.Log("Connecting at URL: " + URL);

		WWW www = new WWW (URL);
		StartCoroutine (WaitForRequest(www));
	}
 
 	// Wait until receive some data from server
    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

		if (www.error == null) 
		{
			string json = www.text;

			EventSystem.CreateUser(json);
			UpdateUserOnScreen();
		}
     }   
}
