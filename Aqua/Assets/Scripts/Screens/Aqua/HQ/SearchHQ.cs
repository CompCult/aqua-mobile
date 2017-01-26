using UnityEngine;
using System.Collections;

public class SearchHQ : GenericScreen 
{
	public void Start ()
	{
		AlertsAPI.instance.Init();
		backScene = "AquaWorld";
	}
}
