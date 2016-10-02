using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Send : Screen 
{
	public void Start () 
	{
		UnityAndroidExtras.instance.Init();

		if (QuestManager.activity.gps_enabled)
			backScene = "GPS";
		else if (QuestManager.activity.audio_file)
			backScene = "Voice";
		else if (QuestManager.activity.photo_file)
		 	backScene = "Media";
		else 
			backScene = "Activity Home";
	}

	public void SendActivity ()
	{
		int userID = UsrManager.user.id;
		Activity activity = QuestManager.activity;
		ActivityResponse activityResponse = QuestManager.activityResponse;

		WWW responseForm = Authenticator.SendActivity(userID, activity, activityResponse);
		ProcessSend(responseForm);
	}

	public void ProcessSend (WWW responseForm)
	{
		string Error = responseForm.error,
		Response = responseForm.text;

		if (Error == null) 
		{
			Debug.Log("Response from send activity: " + Response);

			UnityAndroidExtras.instance.makeToast("Enviado com sucesso", 1);
			LoadScene("Selection");
		}
		else 
		{
			if (Error.Contains("404 "))
				UnityAndroidExtras.instance.makeToast("Não encontrado. Verifique o e-mail e senha.", 1);
			else if (Error.Contains("500 "))
				UnityAndroidExtras.instance.makeToast("Houve um problema no Servidor. Tente novamente mais tarde.", 1);
			else 
				UnityAndroidExtras.instance.makeToast("Falha ao enviar. Contate um administrador do sistema.", 1);
		}
	}
}
