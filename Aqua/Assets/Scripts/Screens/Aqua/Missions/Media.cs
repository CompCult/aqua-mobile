using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Media : Screen 
{
	public Text title;
	public GameObject cameraField;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		CameraDevice.cameraPlane = cameraField;
		backScene = "Activity Home";

		CameraDevice.ShowCameraImage();
		UpdateActivityTexts();
	}
	
	public void UpdateActivityTexts () 
	{	
		title.text = QuestManager.activity.name;
	}

	public void RecordPhoto()
	{
		CameraDevice.RecordPhoto();
		ProgressActivity();
	}

	public void ProgressActivity()
	{
		Activity activity = QuestManager.activity;

		QuestManager.activityResponse.photo = CameraDevice.Photo.EncodeToPNG();

		if (activity.audio_file)
			LoadScene("Voice");
		else if (activity.gps_enabled)
			LoadScene("GPS");
		else if (activity.text_enabled)
			LoadScene("Write");
		else 
			LoadScene("Send");
	}
}
