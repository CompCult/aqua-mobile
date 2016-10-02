using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SearchActivity : Screen {

	public InputField activityID;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();

		backScene = "Selection";
	}

	public void FindActivity()
	{
		string activityID = this.activityID.text;

		WWW activityRequest = Authenticator.RequestActivity(activityID);
		ProcessActivity (activityRequest);
	}

	public void ProcessActivity(WWW activityRequest)
	{
		string Response = activityRequest.text,
			   Error = activityRequest.error;

		if (Error == null) 
		{
			Debug.Log("Mission found: " + Response);

			QuestManager.UpdateActivity(Response);
			LoadScene("Activity Home");
		}
		else 
		{
			if (Error.Contains("404 "))
				UnityAndroidExtras.instance.makeToast("Não encontrado", 1);
			else 
				UnityAndroidExtras.instance.makeToast("Falha ao buscar missões. Contate o administrador do sistema.", 1);
		}
	}
}
