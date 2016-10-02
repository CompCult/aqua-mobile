using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SearchQuiz : Screen {

	public InputField quizID;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();

		backScene = "Selection";
	}

	
}
