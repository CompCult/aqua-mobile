using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class GenericScene : MonoBehaviour {

	// page elements
	public Text NotificationArea;

	protected EventSystem EventSystem;
	protected IEnumerator Coroutine;

	protected string URL,
				     pvtkey,
					 BackScene,
					 InvalidLogin = "E-mail ou senha inválidos.",
					 PasswordDontMatch = "As senhas não confirmam.",
					 AlreadyRegistered = "Esse e-mail já está em uso.",
					 SuccessRegistered = "Registrado com sucesso.",
					 ServerFailed = "Ocorreu um erro no servidor. Tente novamente mais tarde.",
					 InvalidPassLength = "A senha deve ter pelo menos 5 caracteres.",
					 InvalidMailLength = "O e-mail deve conter pelo menos 5 caracteres.",
					 InvalidNameLength = "Insira um nome válido.",
					 UpdateInfoSuccess = "Informações atualizadas.",
					 UpdateInfoFail = "Falha ao atualizar informações.",
					 RefillPasswordError = "Preencha sua senha novamente para atualizar as informações.",
					 NoErrorGet = "Nenhum erro cabível",
					 ConnectingMessage = "Conectando...",
					 YourLocation = "Sua localização",
					 SelectFieldOnScreen = "Selecione uma notificação",
					 EmailAlreadyRegistered = "E-mail já registrado";

	public void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) 
    		LoadScene();
	}

	public void LoadScene(string Scene) 
	{
		SceneManager.LoadScene(Scene);
	}

	public void LoadScene()
	{
		if (BackScene != null) 
		{
			if (BackScene.Equals("Login"))
				Destroy(GameObject.Find("EventSystem"));

			SceneManager.LoadScene(BackScene);
		}
		else
		{
			Application.Quit();
		}
	}
	
	// Convert input string to SHA1
	public string CalculateSHA1 (string input)
	{
		using (SHA1Managed sha1 = new SHA1Managed())
		{
			var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
			var sb = new StringBuilder(hash.Length * 2);

			foreach (byte b in hash)
			{
				// X2 to uppercase and x2 to lowercase
				sb.Append(b.ToString("x2"));
			}

			return sb.ToString();
		}
	}

	public bool EnableNotification(string Message)
	{
		return EnableNotification(10, Message, null);
	}

	public bool EnableNotification(int Seconds, string Message)
	{
		return EnableNotification(Seconds, Message, null);
	}

	public bool EnableNotification(int Seconds, string Message, string Scene)
	{
		NotificationArea.enabled = true;

		if (Message != null)
			NotificationArea.text = Message;

		if (Coroutine != null)
			StopCoroutine(Coroutine);

		Coroutine = NotificationTimer(Seconds, Scene);
		StartCoroutine(Coroutine);

		return false;
	}

	private IEnumerator NotificationTimer(int Seconds, string Scene) 
	{
		yield return new WaitForSeconds(Seconds);
		NotificationArea.enabled = false;

		if (Scene != null)
			LoadScene(Scene);
	}
}
