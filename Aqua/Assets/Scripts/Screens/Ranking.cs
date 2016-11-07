using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Ranking : GenericScreen 
{
	public GameObject userCard;
	public Text userName, userLevel, userXP;

	public List<User> userList;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Home";

		ReceiveRanking();
	}

	private void ReceiveRanking()
	{
		WWW rankingRequest = Authenticator.RequestRanking();

		string Response = rankingRequest.text,
		Error = rankingRequest.error;

		if (Error == null)
		{
			FillRanking(Response);
			CreatePlayerCards();
		}
		else 
		{
			UnityAndroidExtras.instance.makeToast("Falha ao receber o Ranking", 1);
			LoadBackScene();
		}
	}

	private void FillRanking(string ranking)
    {
		string[] rankingJSON = ranking.Replace ("[", "").Replace ("]", "").Replace ("},{", "}%{").Split ('%');
     	userList = new List<User>();

		foreach (string userJSON in rankingJSON)
        {
			User user = UsrManager.CreateUserFromJSON(userJSON);
        	userList.Add(user);
        }
    }

    private void CreatePlayerCards ()
     {
     	Vector3 Position = userCard.transform.position;

     	if (userList.Count < 1)
     		return;

     	foreach (User user in userList)
        {
        	userName.text = user.name;
        	userLevel.text = user.level.ToString();
        	userXP.text = user.xp.ToString();

            Position = new Vector3(Position.x, Position.y, Position.z);

            GameObject Card = (GameObject) Instantiate(userCard, Position, Quaternion.identity);
            Card.transform.SetParent(GameObject.Find("Area").transform, false);
        }

        Destroy(userCard);
     }
}
