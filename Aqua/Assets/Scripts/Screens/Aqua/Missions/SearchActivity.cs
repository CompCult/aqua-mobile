using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SearchActivity : GenericScreen 
{
	public InputField activityID;
	public Dropdown publicActivitiesDropdown;

	private List<Activity> activitiesList;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "AquaWorld";

		ReceivePublicActivities();
	}

	public void ReceivePublicActivities ()
	{
		WWW requestActivities = Authenticator.RequestPublicActivities();

		string response = requestActivities.text,
        error = requestActivities.error;

        if (error == null) 
		{
			// Filter the JSON to receive a Split
			response = response.Replace("[","").Replace("]","").Replace("},{","}@{");

			string[] activities = response.Split('@');
			activitiesList = new List<Activity>();

			foreach(string activity in activities)
			{
				Activity aux = new Activity();
				if (!String.IsNullOrEmpty(activity))
					activitiesList.Add(aux.CreateActivity(activity));
			}

			FillList();
		} 
		else 
		{
			Debug.Log("Error trying to get public activities: " + error);
		}
	}

	public void FillList()
	{
		publicActivitiesDropdown.options.Clear();
		publicActivitiesDropdown.options.Add(new Dropdown.OptionData() {text = "Escolha uma missão pública"});
		
		foreach (Activity activity in activitiesList)
			publicActivitiesDropdown.options.Add(new Dropdown.OptionData() {text = "" + activity.name});

	  	publicActivitiesDropdown.RefreshShownValue();
	}

	public void SelectActivity()
	{
		Text activitySelectedName = publicActivitiesDropdown.captionText;
		Activity activitySelected = null;	

		foreach (Activity activity in activitiesList)
		{
			if (activitySelectedName.text.Equals("" + activity.name))
			{
				activitySelected = activity;
				break;
			}
		}

		if (activitySelected != null)
		{
			Debug.Log("Public mission selected: " + activitySelected.name);

			QuestManager.UpdateActivity(activitySelected);
			LoadScene("Activity Home");
		}
	}

	public void FindActivity()
	{
		string activityID = this.activityID.text;

		if (activityID == null || activityID.Length == 0)
		{
			UnityAndroidExtras.instance.makeToast("Insira o ID da missão", 1);
			return;
		}

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
