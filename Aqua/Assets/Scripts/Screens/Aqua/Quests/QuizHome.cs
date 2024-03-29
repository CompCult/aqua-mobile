﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuizHome : GenericScreen 
{
	public Text title, question,
	alt1, alt2, alt3, alt4, alt5;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "Search Quiz";

		UpdateActivityTexts();
	}
	
	public void UpdateActivityTexts () 
	{	
		string noQuestion = "";
		title.text = QuestManager.quiz.name;
		question.text = QuestManager.quiz.question;

		// There will be a loop here soon

		if (QuestManager.quiz.option_1 != noQuestion) alt1.text = QuestManager.quiz.option_1;
		else alt1.transform.parent.gameObject.SetActive(false);

		if (QuestManager.quiz.option_2 != noQuestion) alt2.text = QuestManager.quiz.option_2;
		else alt2.transform.parent.gameObject.SetActive(false);

		if (QuestManager.quiz.option_3 != noQuestion) alt3.text = QuestManager.quiz.option_3;
		else alt3.transform.parent.gameObject.SetActive(false);

		if (QuestManager.quiz.option_4 != noQuestion) alt4.text = QuestManager.quiz.option_4;
		else alt4.transform.parent.gameObject.SetActive(false);

		if (QuestManager.quiz.option_5 != noQuestion) alt5.text = QuestManager.quiz.option_5;
		else alt5.transform.parent.gameObject.SetActive(false);
	}

	public void SendQuiz(int alternative)
	{
		QuestManager.quizResponse.quiz_id = QuestManager.quiz.id;
		QuestManager.quizResponse.user_id = UsrManager.user.id;
		QuestManager.quizResponse.quiz_answer = alternative;

		WWW quizForm = QuestAPI.SendQuiz(QuestManager.quizResponse);
		ProcessQuiz(quizForm);
	}

	public void ProcessQuiz(WWW quizForm)
	{
		string Error = quizForm.error;

		if (Error == null)
		{
			AlertsAPI.instance.makeToast("Enviado com sucesso", 1);

			LoadBackScene();
		}
		else 
		{
			Debug.Log("Error: " + Error);

			if (Error.Contains ("404 "))
				AlertsAPI.instance.makeAlert("Que pena!\nO quiz não existe mais ou está fora do prazo de resposta.", "Tudo bem");
			else
				AlertsAPI.instance.makeAlert("Ops!\nHouve um problema no servidor. Tente novamente em alguns instantes.", "Tudo bem");
		}
	}
}
