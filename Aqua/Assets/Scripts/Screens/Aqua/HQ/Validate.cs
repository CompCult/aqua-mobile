using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Validate : GenericScreen 
{
	public Text hqAuthor, hqValue;
	public Image hqImage;

	private HQ currentHQ;

	public void Start ()
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Search HQ";

		//ReceiveRandomHQ();
		FillScreenElements();
	}

	private void ReceiveRandomHQ()
	{
		WWW hqRequest = Authenticator.RequestHQ();

		string Response = hqRequest.text,
		Error = hqRequest.error;

		if (Error == null)
		{
			currentHQ = new HQ();
			currentHQ = JsonUtility.FromJson<HQ>(Response);

			FillScreenElements();
		}
		else
		{
			if (Error.Contains("500 "))
				UnityAndroidExtras.instance.makeToast("Houve um problema no Servidor. Tente novamente mais tarde.", 1);
			else 
				UnityAndroidExtras.instance.makeToast("Falha ao enviar. Contate um administrador do sistema.", 1);

			LoadScene("Search HQ");
		}
	}

	public void SendRate(int value)
	{
		WWW rateForm = Authenticator.SendHQRate(currentHQ.id, value);

		string Response = rateForm.text,
		Error = rateForm.error;

		if (Error == null)
		{
			Debug.Log("Received:" + Response);

			UnityAndroidExtras.instance.makeToast("Avaliação enviada!", 1);
			LoadScene("Search HQ");
		}
		else
		{
			if (Error.Contains("500 "))
				UnityAndroidExtras.instance.makeToast("Houve um problema no Servidor. Tente novamente mais tarde.", 1);
			else 
				UnityAndroidExtras.instance.makeToast("Falha ao enviar. Contate um administrador do sistema.", 1);
		}
	}

	private void FillScreenElements()
	{
		currentHQ = new HQ();
		hqAuthor.text = currentHQ.author;
		hqValue.text = currentHQ.value + " pts.";
	
		StartCoroutine(LoadHQ());
	}

    private IEnumerator LoadHQ() 
    {
    	// Loads HQ Image
        WWW www = new WWW(currentHQ.url);
        yield return www;
        hqImage.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
}
