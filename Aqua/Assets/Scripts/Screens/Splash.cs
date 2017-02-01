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

	public void CheckVersion()
	{
		WWW versionRequest = MiscAPI.CheckVersion();

		string errorText = versionRequest.error,
		versionText = versionRequest.text;

		if (errorText == null)
		{
			if (versionText == MiscAPI.GetVersion())
			{
				Debug.Log("Updated version! v" + versionText);
			}
			else 
			{
				AlertsAPI.instance.makeAlert("Versão desatualizada!\nBaixe a versão mais recente na Play Store.", "Entendi");
			}
		}
		else 
		{
			AlertsAPI.instance.makeAlert("Ops!\nOcorreu um erro ao verificar sua versão. Tentando mais uma vez...", "Tudo bem");
			ReloadScene();
		}

		CheckLocalization();
	}

	private IEnumerator SplashTime () 
	{
        yield return new WaitForSeconds(2);

        // Disables Android Status Bar
		AndroidScreen.statusBarState = AndroidScreen.States.Hidden;
		// Enables Android Navigation Bar
		AndroidScreen.navigationBarState = AndroidScreen.States.Visible;

		CheckVersion();
    }

}
