using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GroupScreen : GenericScreen 
{
	public GameObject memberCard, deleteMemberButton, deleteGroupButton, exitGroupButton;
	public Text groupName, memberName, memberEmail, newMemberEmail;
	public Sprite trashIcon, logoutIcon;

	private bool isOwner;

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

			// If the user is the group owner
			if (GroupManager.group.owner_id == UsrManager.user.id)
			{
				isOwner = true;
				deleteGroupButton.SetActive(true);
			}
			else
			{
				isOwner = false;
				exitGroupButton.SetActive(true);
			}

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

        	if (member.id == GroupManager.group.owner_id)
        		deleteMemberButton.SetActive(false);
        	else 
        		if (isOwner)
        			deleteMemberButton.SetActive(true);

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

	public void LogoutGroup()
	{
		Text myEmail = gameObject.AddComponent<Text>();
     	myEmail.text = UsrManager.user.email;

		RemoveMember(myEmail);
	}

	public void RemoveMember(Text removeEmail)
	{
		WWW removeRequest;

		foreach (User member in GroupManager.group.members)
        {
        	if (removeEmail.text == member.email)
        	{
        		int groupID = GroupManager.group.id;
        		string memberEmail = member.email;

        		Debug.Log("Removing e-mail: " + memberEmail);

        		removeRequest = Authenticator.RemoveGroupMember(memberEmail, groupID);
        		ProcessRemove(removeRequest);
        		break;
        	}
        }
	}

	private void ProcessRemove(WWW addRequest)
	{
		string Error = addRequest.error,
		Response = addRequest.text;

		if (Error == null) 
		{
			if (isOwner)
			{
				UnityAndroidExtras.instance.makeToast("Jogador(a) removido(a)", 1);

				Scene scene = SceneManager.GetActiveScene();
	            SceneManager.LoadScene(scene.name);
	        }
	        else 
	        {
	        	UnityAndroidExtras.instance.makeToast("Você saiu do grupo", 1);
	        	LoadBackScene();
	        }
		}
		else 
		{
			UnityAndroidExtras.instance.makeToast("Falha ao remover. Tente novamente.", 1);
			Debug.Log("Member remove error: " + Error);
		}
	}

	public void DeleteGroup()
	{
		int groupID = GroupManager.group.id;

		WWW removeRequest = Authenticator.DeleteGroup(groupID);
		ProcessDelete (removeRequest);
	}

	private void ProcessDelete(WWW removeRequest)
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
