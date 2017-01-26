using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Validate : GenericScreen 
{
	public Text hqValue;
	public Image hqImage;

	private HQ currentHQ;
	private int zoomScale;

	public void Start ()
	{
		AlertsAPI.instance.Init();
		backScene = "Search HQ";
		zoomScale = 0;

		ReceiveRandomHQ();
	}

	private void ReceiveRandomHQ()
	{
		WWW hqRequest = HQAPI.RequestHQ();

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
				AlertsAPI.instance.makeToast("Nenhuma HQ recebida. Tente novamente.", 1);
			else 
				AlertsAPI.instance.makeToast("Falha ao receber HQ. Tente novamente mais tarde.", 1);

			LoadScene("Search HQ");
		}
	}

	public void SendRate(int value)
	{
		WWW rateForm = HQAPI.SendHQRate(currentHQ, value);

		string Response = rateForm.text,
		Error = rateForm.error;

		if (Error == null)
		{
			Debug.Log("Received:" + Response);

			AlertsAPI.instance.makeToast("Avaliação enviada!", 1);
			ReloadScene();
		}
		else
		{
			AlertsAPI.instance.makeToast("Falha ao avaliar. Tente novamente mais tarde.", 1);
		}
	}

	public void ZoomIn()
	{
		hqImage.transform.localScale += new Vector3(0.5F, 0.5F, 0);
		zoomScale++;
	}

	public void ZoomOut()
	{
		if (zoomScale > 0)
		{
			hqImage.transform.localScale -= new Vector3(0.5F, 0.5F, 0);
			zoomScale--;
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
