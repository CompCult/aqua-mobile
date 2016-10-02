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
