using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SearchQuiz : GenericScreen 
{
	public InputField quizID;
	private List<Quiz> quizzesList;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "AquaWorld";
	}

	public void FindQuiz()
	{
		string quizID = this.quizID.text;

		if (quizID == null || quizID.Length == 0)
		{
			AlertsAPI.instance.makeToast("Insira o ID do quiz", 1);
			return;
		}

		WWW quizRequest = QuestAPI.RequestQuiz(quizID);
		ProcessQuiz (quizRequest);
	}

	public void ProcessQuiz (WWW quizRequest)
	{
		string Response = quizRequest.text,
			   Error = quizRequest.error;

		if (Error == null) 
		{
			Debug.Log("Quiz found: " + Response);

			QuestManager.UpdateQuiz(Response);
			LoadScene("Quiz Home");
		}
		else 
		{
			AlertsAPI.instance.makeAlert("Que pena!\nParece que esse quiz foi removido ou já expirou.", "Tudo bem");
		}
	}
}
