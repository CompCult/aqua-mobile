using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GPSS : GenericScreen 
{
	public Text title;

	public void Start () 
	{
		AlertsAPI.instance.Init();

		if (QuestManager.activity.audio_file)
			backScene = "Voice";
		else if (QuestManager.activity.photo_file)
			backScene = "Media";
		else 
			backScene = "Activity Home";

		GPSManager.StartGPS();
		UpdateActivityTexts();
	}

	public void UpdateActivityTexts () 
	{	
		title.text = QuestManager.activity.name;
	}

	public void RequestCoordinates(string step)
	{
		bool requestSuccess = GPSManager.ReceivePlayerLocation ();
		
		if (!requestSuccess || GPSManager.location == null)
			return;

		if (GPSManager.location[0] == 0 || GPSManager.location[1] == 0 || !GPSManager.IsActive())
		{
			AlertsAPI.instance.makeAlert("GPS desligado!\nAtive o serviço de localização do celular na barra superior do dispositivo.", "Entendi");
			return;
		}

		AlertsAPI.instance.makeToast("Localização obtida", 1);
		string playerLocation = GPSManager.location[0] + " | " + GPSManager.location[1];

		switch (step) 
		{
			case "coord_start":
				QuestManager.activityResponse.coord_start = playerLocation; break;
			case "coord_mid":
				QuestManager.activityResponse.coord_mid = playerLocation; break;
			case "coord_end":
				QuestManager.activityResponse.coord_end = playerLocation; break;
		}
	}

	public void ProgressActivity()
	{
		if (QuestManager.AreCoordsFilled ())
		{ 
			GPSManager.StopGPS();

			if (QuestManager.activity.text_enabled)
				LoadScene("Write");
			else
				LoadScene("Send");
		} 
		else
		{
			AlertsAPI.instance.makeToast("Marque o local da missão", 1);
		}
	}
}
