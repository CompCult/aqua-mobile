using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AquaHome : GenericScene {

	public Text AquaCoins, GoldCoins, Level, EXP;

	public void Start () 
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		BackScene = "Home";

		FillFieldsWithUserInfo();
	}

	public void FillFieldsWithUserInfo()
	{
		User User = EventSystem.GetUser();

		AquaCoins.text = "" + User.GetCoins();
		GoldCoins.text = "" + User.GetCoins();
		Level.text = "Level " + User.GetLevel();
		EXP.text = "EXP: " + User.GetEXP();
	}

}
