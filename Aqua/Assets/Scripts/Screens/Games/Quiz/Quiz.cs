using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Quiz : GenericScene {

	public GameObject GetActivityScreen,
					  ReadingScreen,
					  QuestionsScreen,
					  RecordingScreen,
					  EndGameScreen;

	public Text QuizID, RecordingStatus;

	private List<GameObject> Screens;

	public void Start () 
	{
		EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		BackScene = "AquaHome";

		Screens = new List<GameObject>();

		Screens.Add(GetActivityScreen);
		Screens.Add(ReadingScreen);
		Screens.Add(QuestionsScreen);
		Screens.Add(RecordingScreen);
		Screens.Add(EndGameScreen);

		LoadScreen(GetActivityScreen.name);
	}
	
	public void LoadScreen (string NewScreen) 
	{
		foreach (GameObject Screen in Screens)
		{
			if (Screen.name.Equals(NewScreen))
			{
				Screen.SetActive(true);
				break;
			}
			else
			{
				Screen.SetActive(false);
			}
		}
	}

	public void PrepareGetQuizForm()
	{
		// TODO
	}
}
