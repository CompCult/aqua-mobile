using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AquaHome : Screen 
{
	public Text silverCoins, 
	goldCoins, levelText, 
	titleText, expText;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Home";

		UpdateFields();
	}
	
	public void UpdateFields()
	{
		int playerLevel = (UsrManager.user.xp / 1000) + 1;

		silverCoins.text = UsrManager.user.coins.ToString();
		goldCoins.text = UsrManager.user.coins.ToString();
		levelText.text = "Level " + playerLevel;
		expText.text = UsrManager.user.xp + " EXP";
	}
}
