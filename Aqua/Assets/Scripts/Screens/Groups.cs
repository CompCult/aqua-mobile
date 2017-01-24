using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Groups : GenericScreen 
{
	public InputField newGroupField;
	public GameObject groupCard;
	public Text groupName;

	public List<Group> groupsList;

	public void Start () 
	{
		backScene = "Home";

		RequestUserGroups();
	}

	private void RequestUserGroups ()
	{
		WWW groupsRequest = Authenticator.RequestGroups();

		string Response = groupsRequest.text,
		Error = groupsRequest.error;

		if (Error == null)
		{
			FillGroupsList(Response);
			CreateGroupsCard();
		}
		else 
		{
			UnityAndroidExtras.instance.makeToast("Falha ao receber seus grupos", 1);
			LoadBackScene();
		}
	}

	private void FillGroupsList(string groups)
    {
		string[] groupsJSON = groups.Replace ("[", "").Replace ("]", "").Replace ("},{", "}%{").Split ('%');
     	groupsList = new List<Group>();

		foreach (string groupJSON in groupsJSON)
        {
			Group group = JsonUtility.FromJson<Group>(groupJSON);
        	groupsList.Add(group);
        }

        Debug.Log("Size: " + groupsList.Count);
    }

    private void CreateGroupsCard ()
     {
     	Vector3 Position = groupCard.transform.position;

     	foreach (Group group in groupsList)
        {
        	groupName.text = group.name;
            Position = new Vector3(Position.x, Position.y, Position.z);

            GameObject Card = (GameObject) Instantiate(groupCard, Position, Quaternion.identity);
            Card.transform.SetParent(GameObject.Find("Area").transform, false);
        }

        Destroy(groupCard);
    }

     public void SelectGroup (Text groupName)
     {
     	foreach (Group group in groupsList)
     	{
     		if (group.name == groupName.text)
     		{
     			GroupManager.UpdateGroup(group);
     			LoadScene("Group");
     			break;
     		}
     	}
     }

	public void CreateGroup()
	{
		string newGroupName = newGroupField.text;
		int ownerID = UsrManager.user.id;

		if (newGroupName == null || newGroupName == "")
			return;

		UnityAndroidExtras.instance.makeToast("Criando o grupo " + newGroupName, 1);

		WWW createRequest = Authenticator.CreateGroup(newGroupName, ownerID);
		ProcessCreation (createRequest);
	}

	private void ProcessCreation(WWW createRequest)
	{
		string Error = createRequest.error,
		Response = createRequest.text;

		if (Error == null) 
		{
			UnityAndroidExtras.instance.makeToast("Grupo criado com sucesso", 1);
			Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);

			Debug.Log("Group creation response: " + Response);
		}
		else 
		{
			UnityAndroidExtras.instance.makeToast("Falha ao criar. Tente novamente.", 1);
			Debug.Log("Group creation error: " + Error);
		}
	}
}
