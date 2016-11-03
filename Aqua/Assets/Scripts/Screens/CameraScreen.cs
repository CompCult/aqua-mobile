using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CameraScreen : GenericScreen 
{
	public GameObject CameraField;
	public Dropdown Dropdown;

    private WebCamTexture MobileCamera;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		CameraDevice.cameraPlane = CameraField;

		backScene = "Home";

		GPS.StartGPS();
		CameraDevice.ShowCameraImage();
	}

	public new void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) 
		{
			LoadBackScene();

			CameraDevice.StopCameraImage();
			GPS.StopGPS();
		}
	}

	public void SendPhoto()
	{
		GPS.ReceivePlayerLocation();
		CameraDevice.RecordPhoto();

		int id = UsrManager.user.id;
		string latitude = GPS.location[0].ToString(),
		longitude = GPS.location[1].ToString(),
		type;

		if (latitude == "0" || longitude == "0" || !GPS.IsActive())
		{
			UnityAndroidExtras.instance.makeToast("Ative o serviço de localização do celular", 1);
			CameraDevice.ShowCameraImage();
			return;
		}

		switch (Dropdown.value)
        {
        	case 0:
        		type = "leak"; break;
        	case 1:
        		type = "mosquito_nest"; break;
        	case 2:
        		type = "contamination"; break;
        	case 3:
        		type = "water_billing"; break;
        	case 4:
        		type = "productive_activity"; break;
        	default:
        		type = "other"; break;
        }

        byte[] bytes = CameraDevice.Photo.EncodeToPNG();
        UnityAndroidExtras.instance.makeToast("Enviando...", 1);

		WWW photoResponse = Authenticator.SendPhoto (id, latitude, longitude, type, bytes);
		ProcessPhoto(photoResponse);
	}

	private void ProcessPhoto (WWW photoResponse)
	{
	    string Response = photoResponse.text;
	    string Error = photoResponse.error;

		if (Error == null) 
		{
			Debug.Log("Response from sending photo: " + Response);

			UnityAndroidExtras.instance.makeToast("Foto enviada", 1);
		} 
		else 
		{
			Debug.Log("Error on sending photo: " + Error);
			UnityAndroidExtras.instance.makeToast("Houve um problema no envio da notificação", 1);
		}

		CameraDevice.ShowCameraImage();
	 } 

}
