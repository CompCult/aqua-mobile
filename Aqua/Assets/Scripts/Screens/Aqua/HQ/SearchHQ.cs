using UnityEngine;
using System.Collections;

public class SearchHQ : GenericScreen 
{
	public void Start ()
	{
		UnityAndroidExtras.instance.Init();
		backScene = "AquaWorld";
	}
}
