using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AquaWorld : GenericScreen
{
	void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "Home";
	}
}
