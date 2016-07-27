using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class GenericScene : MonoBehaviour {

	public Text NotificationArea;

	protected EventSystem EventSystem;
	protected IEnumerator Coroutine;

	// Temporary. Aqua will use a internationalization system
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
					 InvalidMailString = "Formato de e-mail inválido.",
					 InvalidNameLength = "Insira um nome válido.",
					 InvalidDate = "Data de nascimento inválida.",
					 InvalidCPF = "CPF inválido.",
					 UpdateInfoSuccess = "Informações atualizadas.",
					 UpdateInfoFail = "Falha ao atualizar informações.",
					 RefillPasswordError = "Preencha sua senha novamente para atualizar as informações.",
					 NoErrorGet = "Nenhum erro cabível",
					 ConnectingMessage = "Conectando...",
					 SendingMessage = "Enviando...",
					 RegisteringMessage = "Registrando-se...",
					 SentMessage = "Enviado!",
					 NotSentMessage = "Falha ao enviar",
					 YourLocation = "Sua localização",
					 SelectFieldOnScreen = "Selecione uma notificação",
					 EmailAlreadyRegistered = "E-mail já registrado",
					 ConnectionFailed = "Falha na conexão",
					 LocalizationFailed = "Falha ao obter sua localização";

	public virtual void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) 
			LoadScene();
	}

	public void LoadScene(string Scene) 
	{
		if (Scene != null) 
			SceneManager.LoadScene(Scene);
		else
			Application.Quit();
	}

	public void LoadScene()
	{
		LoadScene(BackScene);
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
		return EnableNotification(99, Message, null);
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

   	public static bool IsValidEmail(string email)
	{
		string strRegex = @"^([a-zA-Z0-9_\-\.a-zA-Z0-9]+)@((\[[0-9]{1,3}" +
    	 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + 
     	@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
	 	Regex reg = new Regex(strRegex);
		
		return reg.IsMatch(email);
	}	

	public static bool IsValidDate(string date, string format)
	{
		DateTime Test;
		
		return DateTime.TryParseExact(date, format, null, DateTimeStyles.None, out Test);
	}

	public static bool IsValidCpf(string cpf)
	{
		string strRegex = @"^\d{3}\.\d{3}\.\d{3}\-\d{2}$";
	 	Regex reg = new Regex(strRegex);
		
		return reg.IsMatch(cpf);
	}
}
