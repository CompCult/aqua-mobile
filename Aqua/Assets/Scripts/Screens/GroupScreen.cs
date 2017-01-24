using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GroupScreen : GenericScreen 
{
	public GameObject memberCard;
	public Text groupName, memberName, memberEmail, newMemberEmail;

	public void Start () 
	{
		backScene = "Groups";
		groupName.text = GroupManager.group.name;

		RequestGroupInfo();
	}

	private void RequestGroupInfo ()
	{
		WWW infoRequest = Authenticator.RequestGroupInfo();

		string Response = infoRequest.text,
		Error = infoRequest.error;

		if (Error == null)
		{
			GroupManager.UpdateGroup(Response);
			CreateMembersCard();
		}
		else 
		{
			UnityAndroidExtras.instance.makeToast("Falha ao abrir o grupo", 1);
			LoadBackScene();
		}
	}

    private void CreateMembersCard ()
     {
     	Vector3 Position = memberCard.transform.position;

     	foreach (User member in GroupManager.group.members)
        {
        	memberName.text = member.name;
        	memberEmail.text = member.email;

            Position = new Vector3(Position.x, Position.y, Position.z);

            GameObject Card = (GameObject) Instantiate(memberCard, Position, Quaternion.identity);
            Card.transform.SetParent(GameObject.Find("Area").transform, false);

            Debug.Log("Membro: " + member.name + " / Email: " + member.email);
        }

        Destroy(memberCard);
    }

    public void AddMember()
	{
		string memberEmail = newMemberEmail.text;
		int groupID = GroupManager.group.id;

		if (memberEmail == null || memberEmail == "")
			return;

		foreach (User member in GroupManager.group.members)
        {
        	if (memberEmail == member.email)
        	{
        		UnityAndroidExtras.instance.makeToast("Membro já adicionado", 1);
        		return;
        	}
        }

		UnityAndroidExtras.instance.makeToast("Adicionando " + memberEmail, 1);

		WWW addRequest = Authenticator.AddGroupMember(memberEmail, groupID);
		ProcessAdding (addRequest);
	}

	private void ProcessAdding(WWW addRequest)
	{
		string Error = addRequest.error,
		Response = addRequest.text;

		if (Error == null) 
		{
			UnityAndroidExtras.instance.makeToast("Jogador(a) adicionado(a)", 1);
			Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
		}
		else 
		{
			if (Error.Contains("404 "))
				UnityAndroidExtras.instance.makeToast("E-mail não encontrado", 1);
			else 
				UnityAndroidExtras.instance.makeToast("Falha ao adicionar. Tente novamente.", 1);
			
			Debug.Log("Member add error: " + Error);
		}
	}

	public void DeleteGroup()
	{
		int groupID = GroupManager.group.id;

		WWW removeRequest = Authenticator.DeleteGroup(groupID);
		ProcessRemove (removeRequest);
	}


	private void ProcessRemove(WWW removeRequest)
	{
		string Error = removeRequest.error,
		Response = removeRequest.text;

		if (Error == null) 
		{
			UnityAndroidExtras.instance.makeToast("Grupo excluído", 1);
			LoadBackScene();
		}
		else 
		{
			UnityAndroidExtras.instance.makeToast("Falha ao excluir. Tente novamente.", 1);
			
			Debug.Log("Group delete error: " + Error);
		}
	}

}
