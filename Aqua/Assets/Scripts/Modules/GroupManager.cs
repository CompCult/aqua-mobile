using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager
{
	private static Group _group;
	public static Group group { get { return _group; } set { _group = value; } }

	public static Group CreateGroup (string json)
	{
		return JsonUtility.FromJson<Group>(json);
	}

	public static void UpdateGroup (string json)
	{
		Debug.Log("Group updated");
		
		Group group = CreateGroup(json);
		_group = group;
	}

	public static void UpdateGroup (Group group)
	{
		Debug.Log("Group updated");

		_group = group;
	}
}
