using UnityEngine;
using System.Collections;

public class Splash : GenericScreen {

	public void Start () 
	{
		LocalizationManager.Start();
		CheckLocalization();
	}

	private void CheckLocalization()
	{
		if (LocalizationManager.GetLang() == "NULL")
		{
			LoadScene("Configurations");
			Debug.Log("No language selected");
		}
		else
		{
			Debug.Log("Language selected: " + LocalizationManager.GetLang());
			StartCoroutine(SplashTime());
		}
	}

	private IEnumerator SplashTime () 
	{
        yield return new WaitForSeconds(1);

        // Disables Android Status Bar
		AndroidScreen.statusBarState = AndroidScreen.States.Hidden;
		// Enables Android Navigation Bar
		AndroidScreen.navigationBarState = AndroidScreen.States.Visible;

		LoadScene("Login");
    }

}
