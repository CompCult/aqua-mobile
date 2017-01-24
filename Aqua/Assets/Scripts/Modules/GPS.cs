using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public static class GPS
{
	private static double[] _location;
	public static double[] location { get { return _location; } }

	public static void StartGPS()
	{
		if (Application.platform != RuntimePlatform.Android) 
			UnityAndroidExtras.instance.makeToast("Dispositivo sem serviço de localização", 1);
		else
			Input.location.Start();
	}

	public static void StopGPS()
	{
		if (Application.platform != RuntimePlatform.Android) 
			UnityAndroidExtras.instance.makeToast("Dispositivo sem serviço de localização", 1);
		else
			Input.location.Stop();
	}

	public static bool IsActive()
	{
		return (Input.location.isEnabledByUser);
	}

	public static bool ReceivePlayerLocation()
	{
		if (Application.platform != RuntimePlatform.Android) 
		{
			UnityAndroidExtras.instance.makeToast("Dispositivo sem serviço de localização", 1);
			return false;
		}

		if (Input.location.status == LocationServiceStatus.Initializing)
		{
			UnityAndroidExtras.instance.makeToast("Aguarde o serviço de localização iniciar", 1);
			return false;
		}

		if (Input.location.status == LocationServiceStatus.Stopped)
		{
			UnityAndroidExtras.instance.makeToast("Ative o serviço de localização do celular", 1);
			return false;
		}

		if (Input.location.status == LocationServiceStatus.Failed)
		{
			UnityAndroidExtras.instance.makeToast("Autorize o Aqua Guardians a receber a localização do celular", 1);
			return false;
		}
		else
		{
			_location = new double[2];

			_location[0] = System.Convert.ToDouble(Input.location.lastData.latitude);
			_location[1] = System.Convert.ToDouble(Input.location.lastData.longitude);
		}

		return true;
	}

}
