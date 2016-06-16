using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Home : MonoBehaviour {

	// Page elements
	public EventSystem EventSystem;
	public Text UserName,
		   		UserLevel;

	// Page connection variables to use
	string URL = "http://aqua-web.herokuapp.com/api/user/";
	string pvtkey = "6b2b7f9bc0";

	// Use this for initialization
	void Start()
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

		// Check if the user is already loaded
		if (!EventSystem.GetUser().GetLoaded())
			TryGetUser();
	}

	void UpdateUserOnScreen()
	{
		UserName.text = EventSystem.GetUser().GetName();
		UserLevel.text = "Level " + EventSystem.GetUser().GetLevel().ToString();
	}

	// Try to connect with the db using the ID to get user data
    void TryGetUser() 
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

		if (www.error == null) {
			string json = www.text;

			EventSystem.CreateUser(json);
			UpdateUserOnScreen();
		}
     }   
}
