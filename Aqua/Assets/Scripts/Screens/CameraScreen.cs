using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CameraScreen : GenericScene
{
		public GameObject CameraField;
		public Dropdown Dropdown;

        private WebCamTexture MobileCamera;

        public void Start ()
        {
        	EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        	BackScene = "Home";

            MobileCamera = new WebCamTexture();
            CameraField.GetComponent<Renderer>().material.mainTexture = MobileCamera;

            if (HaveCamera())
                MobileCamera.Play();
        }

        new public void Update()
		{
			if (Input.GetKeyUp(KeyCode.Escape)) 
			{
	    		MobileCamera.Stop();
	    		LoadScene();
	    	}
		}


        public void TrySendPhoto()
        {
            if (HaveCamera())
                StartCoroutine(RecordPhoto());
        }

        private IEnumerator RecordPhoto()
        {
        		EnableNotification(99, SendingMessage);

        		User User = EventSystem.GetUser();
        		URL = "http://aqua-web.herokuapp.com/api/notification/";
				pvtkey = "d86c362f4b";

                yield return new WaitForEndOfFrame(); 

				// Creates a texture to hold the photo
                Texture2D photo = new Texture2D(MobileCamera.width, MobileCamera.height);
                photo.SetPixels(MobileCamera.GetPixels());
                photo.Apply();

                //Encode to a PNG byte array
                string type = "other";
                byte[] bytes = photo.EncodeToPNG();

                Debug.Log("Selected value on List: " + Dropdown.value);

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
                }
                
                // Retrieve the user location
                yield return StartCoroutine(User.ReceiveCurrentLocationFromGPS());

                Debug.Log("Latitude: " + User.GetLocation()[0]);
                Debug.Log("Longitude: " + User.GetLocation()[1]);

                WWWForm form = new WWWForm ();
				form.AddField ("user_id", User.GetID());
				form.AddField ("latitude", "" + User.GetLocation()[0]);
				form.AddField ("longitude", "" + User.GetLocation()[1]);
				form.AddField ("status", "pending");
				form.AddField ("type", type);
				form.AddBinaryData("photo", bytes, "Photo.png", "image/png");
				WWW www = new WWW (URL + pvtkey, form);

				Debug.Log("Sending notification to: " + URL + pvtkey);

				StartCoroutine(SendRecordedPhoto(www));
        }

        private IEnumerator SendRecordedPhoto(WWW www)
	    {
	        yield return www;
	        
	        string Response = www.text;
	        string Error = www.error;

			if (Error == null) 
			{
				Debug.Log("Response from sending photo: " + Response);
				EnableNotification(3, SentMessage);
			} 
			else 
			{
				string ErrorCode = Error.Split(' ')[0];

				Debug.Log("Error on Sending photo: " + ErrorCode);
				EnableNotification(3, NotSentMessage);
			}
	     } 

        public WebCamTexture GetMobileCamera() { return MobileCamera; }
        public GameObject GetCameraField() { return CameraField; }
        public bool HaveCamera() { return (WebCamTexture.devices.Length > 0) ? true : false; }
}