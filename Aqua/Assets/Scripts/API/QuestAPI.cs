using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestAPI
{
	public static WWW RequestPublicActivities()
	{
		WebFunctions.apiPlace = "/mission/public/";
		WebFunctions.pvtKey = "ec689306c5";

		return WebFunctions.Get();
	}

	public static WWW RequestPublicQuizzes()
	{
		WebFunctions.apiPlace = "/quiz/public/";
		WebFunctions.pvtKey = "ec689306c5";

		return WebFunctions.Get();
	}

		public static WWW RequestQuiz(string quizID)
	{
		WebFunctions.apiPlace = "/quiz/" + quizID + "/";
		WebFunctions.pvtKey = "ec689306c5";

		return WebFunctions.Get();
	}

	public static WWW SendQuiz(QuizResponse quizResponse)
	{
		Debug.Log(quizResponse.ToString());
		
		WWWForm quizForm = new WWWForm ();
		quizForm.AddField("quiz_id", quizResponse.quiz_id);
		quizForm.AddField ("user_id", quizResponse.user_id);
		quizForm.AddField ("quiz_answer", quizResponse.quiz_answer);

		WebFunctions.apiPlace = "/answer/";
		WebFunctions.pvtKey = "ec689306c5";

		return WebFunctions.Post(quizForm);
	}

	public static WWW RequestActivity(string activityID)
	{
		WebFunctions.apiPlace = "/mission/" + activityID + "/";
		WebFunctions.pvtKey = "ec689306c5";

		return WebFunctions.Get();
	}

	public static WWW SendActivity(ActivityResponse activityResponse, Activity activity)
	{
		Debug.Log(activityResponse.ToString());

		WWWForm responseForm = new WWWForm ();
 		responseForm.AddField("user_id", activityResponse.user_id);
 		responseForm.AddField ("mission_id", activityResponse.activity_id);

 		if (activity.gps_enabled) 
 			responseForm.AddField ("coordinates", activityResponse.coord_start); 
 			//responseForm.AddField ("coord_start", activityResponse.coord_mid);
 			//responseForm.AddField ("coord_mid", activityResponse.coord_mid);
 			//responseForm.AddField ("coord_end", activityResponse.coord_end);

 		if (activity.text_enabled)
 			responseForm.AddField ("text", activityResponse.text);

		if (activity.photo_file)
 			responseForm.AddBinaryData("photo", activityResponse.photo, "Photo.png", "image/png");

 		if (activity.audio_file)
 			responseForm.AddBinaryData("audio", activityResponse.audio, "voice.wav", "audio/wav");

		WebFunctions.apiPlace = "/answer/";
		WebFunctions.pvtKey = "ec689306c5";

		return WebFunctions.Post(responseForm);
	}
}
