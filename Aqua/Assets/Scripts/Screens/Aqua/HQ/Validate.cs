using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Validate : GenericScreen 
{
	public Text hqValue;
	public Image hqImage;

	private HQ currentHQ;

	public void Start ()
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Search HQ";

		ReceiveRandomHQ();
	}

	private void ReceiveRandomHQ()
	{
		WWW hqRequest = Authenticator.RequestHQ();

		string Response = hqRequest.text,
		Error = hqRequest.error;

		if (Error == null)
		{
			currentHQ = JsonUtility.FromJson<HQ>(Response);

			FillScreenElements();
		}
		else
		{
			if (Error.Contains("500 "))
				UnityAndroidExtras.instance.makeToast("Nenhuma HQ recebida. Tente novamente.", 1);
			else 
				UnityAndroidExtras.instance.makeToast("Falha ao receber HQ. Tente novamente mais tarde.", 1);

			LoadScene("Search HQ");
		}
	}

	public void SendRate(int value)
	{
		WWW rateForm = Authenticator.SendHQRate(currentHQ, value);

		string Response = rateForm.text,
		Error = rateForm.error;

		if (Error == null)
		{
			Debug.Log("Received:" + Response);

			UnityAndroidExtras.instance.makeToast("Avaliação enviada!", 1);
			ReloadScene();
		}
		else
		{
			UnityAndroidExtras.instance.makeToast("Falha ao avaliar. Tente novamente mais tarde.", 1);
		}
	}

	private void FillScreenElements()
	{
		hqValue.text = currentHQ.value + " pts.";
	
		StartCoroutine(LoadHQ());
	}

    private IEnumerator LoadHQ() 
    {
    	// Loads HQ Image
        WWW www = new WWW(currentHQ.photo_url);
        yield return www;
        hqImage.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
}
