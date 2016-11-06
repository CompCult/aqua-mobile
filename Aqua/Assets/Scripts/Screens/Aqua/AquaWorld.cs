using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AquaWorld : GenericScreen
{
	// Use this for initialization
	void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Home";
	}
}
