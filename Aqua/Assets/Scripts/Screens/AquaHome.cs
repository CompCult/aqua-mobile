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
		silverCoins.text = UsrManager.user.coins.ToString();
		goldCoins.text = UsrManager.user.coins.ToString();
		levelText.text = "Level " + UsrManager.user.level;
		expText.text = UsrManager.user.xp + " EXP";
	}
}
