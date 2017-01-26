using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Capture : GenericScreen 
{
	public GameObject cameraField;

	private HQResponse hqResponse;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		CameraDevice.cameraPlane = cameraField;
		backScene = "Search HQ";

		CameraDevice.ShowCameraImage();
	}
	
	public void SendHQ ()
	{
		CameraDevice.RecordPhoto();
		Debug.Log("Foto capturada");

		hqResponse = new HQResponse();
		hqResponse.user_id = UsrManager.user.id;
		hqResponse.photo = CameraDevice.Photo.EncodeToPNG();

		WWW hqForm = HQAPI.SendHQ(hqResponse);
		ProcessHQ(hqForm);
	}

	public void ProcessHQ (WWW hqForm)
	{
		string Error = hqForm.error,
		Response = hqForm.text;

		if (Error == null)
		{
			Debug.Log("Response from send HQ: " + Response);

			AlertsAPI.instance.makeToast("Enviada com sucesso", 1);
			LoadScene("Search HQ");
		}
		else
		{
			CameraDevice.ShowCameraImage();
			
			if (Error.Contains("500 "))
				AlertsAPI.instance.makeToast("Houve um problema no Servidor. Tente novamente mais tarde.", 1);
			else 
				AlertsAPI.instance.makeToast("Falha ao enviar. Contate um administrador do sistema.", 1);
		}
	}
}
