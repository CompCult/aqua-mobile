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

	protected string InvalidLogin = "E-mail ou senha inválidos.",
					 PasswordDontMatch = "As senhas não confirmam.",
					 AlreadyRegistered = "Esse e-mail já está em uso.",
					 SuccessRegistered = "Registrado com sucesso.",
					 ServerFailed = "Ocorreu um erro no servidor. Tente novamente mais tarde.",
					 InvalidPassLength = "A senha deve ter pelo menos 5 caracteres.",
					 InvalidMailLength = "O e-mail deve conter pelo menos 5 caracteres.",
					 UpdateInfoSuccess = "Informações atualizadas.",
					 UpdateInfoFail = "Falha ao atualizar informações.",
					 RefillPasswordError = "Preencha sua senha novamente para atualizar as informações.",
					 NoErrorGet = "Nenhum erro cabível";

	// Loads the Scene in parameters
	public void LoadScene(string Scene) 
	{
		SceneManager.LoadScene(Scene);
	}
	
	// Convert input to SHA1
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

	public void EnableNotification(int seconds, string message)
	{
		NotificationArea.enabled = true;
		NotificationArea.text = message;

		StartCoroutine (NotificationTimer (seconds));
	}

	public void EnableNotification(int seconds, string message, string scene)
	{
		NotificationArea.enabled = true;
		NotificationArea.text = message;

		StartCoroutine (NotificationTimer (seconds, scene));
	}

	IEnumerator NotificationTimer(int seconds) 
	{
		yield return new WaitForSeconds(seconds);
		NotificationArea.enabled = false;
	}

	IEnumerator NotificationTimer(int seconds, string scene) 
	{
		yield return new WaitForSeconds(seconds);
		NotificationArea.enabled = false;

		LoadScene(scene);
	}
}
