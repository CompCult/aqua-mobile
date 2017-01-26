using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GroupAPI
{
	public static WWW CreateGroup(string groupName, int ownerID)
	{
		WWWForm createForm = new WWWForm ();
		createForm.AddField ("name", groupName);
		createForm.AddField ("owner_id", ownerID);

		WebFunctions.apiPlace = "/group/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		Debug.Log("Group Name:" + groupName);
		Debug.Log("Owner ID: " + ownerID);
		Debug.Log("URL: " + WebFunctions.url + WebFunctions.apiPlace + WebFunctions.pvtKey);

		return WebFunctions.Post(createForm);
	}

	public static WWW DeleteGroup (int groupID)
	{
		WWWForm deleteForm = new WWWForm ();
		deleteForm.AddField ("group_id", groupID);

		WebFunctions.apiPlace = "/group/remove/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Post(deleteForm);
	}

	public static WWW AddGroupMember(string memberEmail, int groupID)
	{
		WWWForm addForm = new WWWForm ();
		addForm.AddField ("user_email", memberEmail);
		addForm.AddField ("group_id", groupID);

		WebFunctions.apiPlace = "/group/add-user/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Post(addForm);
	}

	public static WWW RemoveGroupMember(string memberEmail, int groupID)
	{
		WWWForm removeForm = new WWWForm ();
		removeForm.AddField ("user_email", memberEmail);
		removeForm.AddField ("group_id", groupID);

		WebFunctions.apiPlace = "/group/remove-user/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Post(removeForm);
	}

	public static WWW RequestGroups()
	{
		WebFunctions.apiPlace = "/user/" + UsrManager.user.id + "/groups/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Get();
	}

	public static WWW RequestGroupInfo()
	{
		WebFunctions.apiPlace = "/group/" + GroupManager.group.id + "/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Get();
	}
}
