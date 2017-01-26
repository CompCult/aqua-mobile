using UnityEngine;
using System.Collections;

public class Splash : GenericScreen {

	public void Start () 
	{
		StartCoroutine(SplashTime());
	}

	private IEnumerator SplashTime () 
	{
        yield return new WaitForSeconds(3);

        // Disables Android Status Bar
		AndroidScreen.statusBarState = AndroidScreen.States.Hidden;
		// Enables Android Navigation Bar
		AndroidScreen.navigationBarState = AndroidScreen.States.Visible;

		LoadScene("Login");
    }

}
