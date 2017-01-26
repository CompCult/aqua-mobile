using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SearchActivity : GenericScreen 
{
	public InputField activityID;
	private List<Activity> activitiesList;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "AquaWorld";
	}

	public void FindActivity()
	{
		string activityID = this.activityID.text;

		if (activityID == null || activityID.Length == 0)
		{
			AlertsAPI.instance.makeToast("Insira o ID da missão", 1);
			return;
		}

		WWW activityRequest = QuestAPI.RequestActivity(activityID);
		ProcessActivity (activityRequest);
	}

	public void ProcessActivity (WWW activityRequest)
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
			AlertsAPI.instance.makeAlert("Que pena!\nParece que essa missão foi removida ou já expirou.", "Tudo bem");
		}
	}
}
