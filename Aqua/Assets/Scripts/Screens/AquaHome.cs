using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AquaHome : Screen 
{
	public Text silverCoins, 
	goldCoins, levelText, 
	titleText, expText;

	public Image xpBar;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Home";

		UpdateFields();
	}
	
	public void UpdateFields()
	{
		int userXP = UsrManager.user.xp,
		playerLevel = (userXP / 1000) + 1;

		float xpRemainingToLevelUP = userXP - ((playerLevel - 1) * 1000),
		coins = UsrManager.user.coins, silver, gold;

		gold = Mathf.Round(coins/100);
		silver = Mathf.Round(coins - gold * 100);

		silverCoins.text = "" + silver;
		goldCoins.text = "" + gold;
		levelText.text = "Level " + playerLevel;
		expText.text = "EXP " + xpRemainingToLevelUP + "/1000";

		xpBar.fillAmount = (xpRemainingToLevelUP / 1000);
	}
}
