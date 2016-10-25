using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SearchQuiz : GenericScreen {

	public InputField quizID;
	public Dropdown publicQuizzesDropdown;

	private List<Quiz> quizzesList;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Selection";

		ReceivePublicQuizzes();
	}

	public void ReceivePublicQuizzes ()
	{
		WWW requestQuizzes = Authenticator.RequestPublicQuizzes();

		string response = requestQuizzes.text,
        error = requestQuizzes.error;

        if (error == null) 
		{
			// Filter the JSON to receive a Split
			response = response.Replace("[","").Replace("]","").Replace("},{","}@{");

			string[] quizzes = response.Split('@');
			quizzesList = new List<Quiz>();

			foreach(string quiz in quizzes)
			{
				Quiz aux = new Quiz();
				if (!String.IsNullOrEmpty(quiz))
					quizzesList.Add(aux.CreateQuiz(quiz));
			}

			FillOptionsOnDropDown();
		} 
		else 
		{
			Debug.Log("Error trying to get public quizzes: " + error);
		}
	}

	public void FillOptionsOnDropDown()
	{
		publicQuizzesDropdown.options.Clear();
		publicQuizzesDropdown.options.Add(new Dropdown.OptionData() {text = "Escolha um quiz público"});
		
		foreach (Quiz quiz in quizzesList)
			publicQuizzesDropdown.options.Add(new Dropdown.OptionData() {text = "" + quiz.name});

	  	publicQuizzesDropdown.RefreshShownValue();
	}

	public void SelectQuiz ()
	{
		Text quizSelectedName = publicQuizzesDropdown.captionText;
		Quiz quizSelected = null;	

		foreach (Quiz quiz in quizzesList)
		{
			if (quizSelectedName.text.Equals("" + quiz.name))
			{
				quizSelected = quiz;
				break;
			}
		}

		if (quizSelected != null)
		{
			Debug.Log("Public quiz selected: " + quizSelected.name);

			QuestManager.UpdateQuiz(quizSelected);
			LoadScene("Quiz Home");
		}
	}

	public void FindQuiz()
	{
		string quizID = this.quizID.text;

		if (quizID == null || quizID.Length == 0)
		{
			UnityAndroidExtras.instance.makeToast("Insira o ID do quiz", 1);
			return;
		}

		WWW quizRequest = Authenticator.RequestQuiz(quizID);
		ProcessActivity (quizRequest);
	}

	public void ProcessActivity(WWW quizRequest)
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
			if (Error.Contains("404 "))
				UnityAndroidExtras.instance.makeToast("Não encontrado", 1);
			else 
				UnityAndroidExtras.instance.makeToast("Falha ao buscar missões. Contate o administrador do sistema.", 1);
		}
	}
}
