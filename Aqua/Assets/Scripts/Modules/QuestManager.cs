using UnityEngine;
using System.Collections;

public static class QuestManager
{
	private static Activity _activity;
	private static ActivityResponse _activityResponse;

	public static Activity activity { get { return _activity; } set { _activity = value; } }
	public static ActivityResponse activityResponse { get { return _activityResponse; } set { _activityResponse = value; } }
			
	private static Quiz _quiz;
	private static QuizResponse _quizResponse;

	public static Quiz quiz { get { return _quiz; } set { _quiz = value; } }
	public static QuizResponse quizResponse { get { return _quizResponse; } set { _quizResponse = value; } }

	public static void UpdateActivity(string JSON)
	{
		Debug.Log("Mission updated and old quiz removed");

		_quiz = null;
		_activity = JsonUtility.FromJson<Activity>(JSON);
		_activityResponse = new ActivityResponse ();
	}

 	public static void UpdateQuiz(string JSON)
 	{
 		Debug.Log("Quiz updated and old activity removed");

 		_activity = null;
 		_quiz = JsonUtility.FromJson<Quiz>(JSON);
 		_quizResponse = new QuizResponse ();
 	}

 	public static bool AreCoordsFilled()
 	{
 		if (Application.platform != RuntimePlatform.Android) 
 			return true;

 		return (activityResponse.coord_start != null); //&&activityResponse.coord_mid != null && activityResponse.coord_end != null);
 	}
}
