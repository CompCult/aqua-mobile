using UnityEngine;
using System.Collections;

public class LanguageManager : MonoBehaviour {

	string lang = "Not found";

	/*
	string LOGO_HEADER = null;
	string EMAIL_FORM_TEXT = null;
	string PASSWORD_FORM_TEXT = null;
	string REGISTER_BUTTON_TEXT = null;
	string FORGOT_PASSWORD_TEXT = null;

	string[] strings = null;
	*/
	
	void Start() 
	{
	}

	public void loadLanguage(string lang)
	{
		this.lang = lang;
	}
}
