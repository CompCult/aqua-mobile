using UnityEngine;
using System.Collections;

public class Splash : GenericScreen {

	public void Start () 
	{
		StartCoroutine(SplashTime());
	}

	private IEnumerator SplashTime () 
	{
        yield return new WaitForSeconds(4);

        // Disables Android Status Bar
		ApplicationChrome.statusBarState = ApplicationChrome.States.Hidden;
		// Enables Android Navigation Bar
		ApplicationChrome.navigationBarState = ApplicationChrome.States.Visible;

        if (PlayerPrefs.HasKey("userID"))
		{
			int userID = PlayerPrefs.GetInt("userID");

			UsrManager.userID = userID;
			LoadScene("Home");
		}
		else
		{
			LoadScene("Login");
		}
    }

}
