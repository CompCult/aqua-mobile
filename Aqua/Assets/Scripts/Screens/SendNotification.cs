using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SendNotification : GenericScreen 
{
	public GameObject CameraField,
	captureButton, sendButton, cancelButton;
	public Dropdown Dropdown;

    private WebCamTexture MobileCamera;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		CameraDevice.cameraPlane = CameraField;

		backScene = "Home";

		CameraDevice.ShowCameraImage();
	}

	public new void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) 
		{
			CameraDevice.StopCameraImage();
			GPSManager.StopGPS();

			LoadBackScene();
		}
	}

	public void ConfirmPhoto()
	{
		CameraDevice.RecordPhoto();
		GPSManager.StartGPS();

		captureButton.SetActive(false);
		sendButton.SetActive(true);
		cancelButton.SetActive(true);
	}

	public void CancelPhoto()
	{
		CameraDevice.ShowCameraImage();
		GPSManager.StopGPS();

		captureButton.SetActive(true);
		sendButton.SetActive(false);
		cancelButton.SetActive(false);
	}

	public void SendPhoto()
	{
		GPSManager.ReceivePlayerLocation();

		int id = UsrManager.user.id;
		string latitude = GPSManager.location[0].ToString(),
		longitude = GPSManager.location[1].ToString(),
		type;

		if (latitude == "0" || longitude == "0" || !GPSManager.IsActive())
		{
			AlertsAPI.instance.makeAlert("GPS desligado!\nAtive o serviço de localização do celular na barra superior do dispositivo.", "Entendi");
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
        AlertsAPI.instance.makeToast("Enviando...", 1);

        GPSManager.StopGPS();

		WWW photoForm = NotificationAPI.SendNotification(id, latitude, longitude, type, bytes);
		ProcessPhoto(photoForm);
	}

	private void ProcessPhoto (WWW photoResponse)
	{
	    string Response = photoResponse.text;
	    string Error = photoResponse.error;

		if (Error == null) 
		{
			Debug.Log("Response from sending notification: " + Response);

			AlertsAPI.instance.makeToast("Notificação enviada", 1);
			CancelPhoto();
		} 
		else 
		{
			Debug.Log("Error on sending photo: " + Error);
			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao enviar sua notificação. Tente novamente em instantes.", "Tudo bem");
		}

		CameraDevice.ShowCameraImage();
	 } 

}
