using UnityEngine;
using System.Collections;

public class Selection : GenericScreen {

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "AquaHome";
	}
}
