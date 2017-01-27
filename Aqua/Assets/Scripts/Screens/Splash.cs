using UnityEngine;
using System.Collections;

public class Splash : GenericScreen {

	public void Start () 
	{
		LocalizationManager.Start();

		StartCoroutine(SplashTime());
	}

	private void CheckLocalization()
	{
		if (LocalizationManager.GetLang() == "NULL")
		{
			Debug.Log("No language selected");
			LoadScene("Configurations");
		}
		else
		{
			Debug.Log("Language selected: " + LocalizationManager.GetLang());
			LoadScene("Login");
		}
	}

	private IEnumerator SplashTime () 
	{
        yield return new WaitForSeconds(2);

        // Disables Android Status Bar
		AndroidScreen.statusBarState = AndroidScreen.States.Hidden;
		// Enables Android Navigation Bar
		AndroidScreen.navigationBarState = AndroidScreen.States.Visible;

		CheckLocalization();
    }

}
