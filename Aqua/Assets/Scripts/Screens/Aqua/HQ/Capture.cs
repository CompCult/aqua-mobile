using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Capture : GenericScreen 
{
	public GameObject cameraField;

	private HQResponse hqResponse;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		CameraDevice.cameraPlane = cameraField;
		backScene = "Search HQ";

		CameraDevice.ShowCameraImage();
	}

	public new void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) 
		{
			LoadBackScene();
			CameraDevice.StopCameraImage();
		}
	}

	public void SendHQ ()
	{
		CameraDevice.RecordPhoto();
		Debug.Log("Foto capturada");

		hqResponse = new HQResponse();
		hqResponse.user_id = UsrManager.user.id;
		hqResponse.photo = CameraDevice.Photo.EncodeToPNG();

		WWW hqForm = Authenticator.SendHQ(hqResponse);
		ProcessHQ(hqForm);
	}

	public void ProcessHQ (WWW hqForm)
	{
		string Error = hqForm.error,
		Response = hqForm.text;

		if (Error == null)
		{
			Debug.Log("Response from send HQ: " + Response);

			UnityAndroidExtras.instance.makeToast("Enviada com sucesso", 1);
			LoadScene("Search HQ");
		}
		else
		{
			CameraDevice.ShowCameraImage();
			
			if (Error.Contains("500 "))
				UnityAndroidExtras.instance.makeToast("Houve um problema no Servidor. Tente novamente mais tarde.", 1);
			else 
				UnityAndroidExtras.instance.makeToast("Falha ao enviar. Contate um administrador do sistema.", 1);
		}
	}
}
