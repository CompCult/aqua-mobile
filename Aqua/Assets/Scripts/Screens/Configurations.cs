using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configurations : GenericScreen 
{
	public void Start()
	{
		backScene = "Login";
	}

	public void SelectLanguage(string tag)
	{
		LocalizationManager.SetLang(tag);
		LocalizationManager.Start();

		Debug.Log("Language selected: " + LocalizationManager.GetLang());

		LoadScene("Login");
	}
}
