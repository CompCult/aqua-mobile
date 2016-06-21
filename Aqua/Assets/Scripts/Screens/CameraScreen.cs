﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CameraScreen : GenericScene
{
		// page elements
		public GameObject CameraField;

        private WebCamTexture MobileCamera;
        private RawImage SelectedImage;
        private bool IsFlashOn;

        // Use this for initialization
        public void Start ()
        {
            MobileCamera = new WebCamTexture();
            BackScene = "Home";
            IsFlashOn = false;

            CameraField.GetComponent<Renderer>().material.mainTexture = MobileCamera;

            if (HaveCamera())
                MobileCamera.Play();
        }

        public void ToggleFlash()
        {
        	// TODO
        	IsFlashOn = !IsFlashOn;
        }

        public void TakePhoto()
        {
            if (HaveCamera())
            {
                StopCoroutine(Coroutine);
                
                Coroutine = CapturePhoto();
                StartCoroutine(Coroutine);
            }
        }

        private IEnumerator CapturePhoto()
        {
                yield return new WaitForEndOfFrame(); 

                // it's a rare case where the Unity doco is pretty clear,
                // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
                // be sure to scroll down to the SECOND long example on that doco page 

                Texture2D photo = new Texture2D(MobileCamera.width, MobileCamera.height);
                photo.SetPixels(MobileCamera.GetPixels());
                photo.Apply();

                //Encode to a PNG
                string timeStamp = (new DateTime()).ToString("yyyyMMddHHmmssfff");
                byte[] bytes = photo.EncodeToPNG();
                //Write out the PNG. Aqua will use a custom path dir, so it will change.
                File.WriteAllBytes(@"photo.png-" + timeStamp, bytes);
        }

        private List<string> GetAllGalleryImagePaths()
     	{
             List<string> results = new List<string>();
             HashSet<string> allowedExtesions = new HashSet<string>() { ".png", ".jpg",  ".jpeg"  };
     
            try
            {
                AndroidJavaClass mediaClass = new AndroidJavaClass("android.provider.MediaStore$Images$Media");
     
                // Set the tags for the data we want about each image.  This should really be done by calling; 
                //string dataTag = mediaClass.GetStatic<string>("DATA");
                // but I couldn't get that to work...
                 
                const string dataTag = "_data";
     
                string[] projection = new string[] { dataTag };
                AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = player.GetStatic<AndroidJavaObject>("currentActivity");
     
                string[] urisToSearch = new string[] { "EXTERNAL_CONTENT_URI", "INTERNAL_CONTENT_URI" };
                foreach (string uriToSearch in urisToSearch)
                {
                    AndroidJavaObject externalUri = mediaClass.GetStatic<AndroidJavaObject>(uriToSearch);
                    AndroidJavaObject finder = currentActivity.Call<AndroidJavaObject>("managedQuery", externalUri, projection, null, null, null);
                    bool foundOne = finder.Call<bool>("moveToFirst");
                    while (foundOne)
                    {
                        int dataIndex = finder.Call<int>("getColumnIndex", dataTag);
                        string data = finder.Call<string>("getString", dataIndex);
                        if (allowedExtesions.Contains(Path.GetExtension(data).ToLower()))
                        {
                            string path = @"file:///" + data;
                            results.Add(path);
                        }

                        foundOne = finder.Call<bool>("moveToNext");
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("Exception on get images from gallery:" + e.Data);
            }
     
            return results;
     	}

     	public void SetImage()
     	{
			List<string> galleryImages = GetAllGalleryImagePaths();
			Texture2D t = new Texture2D(2, 2);
			(new WWW(galleryImages[0])).LoadImageIntoTexture(t);
			SelectedImage.texture = t;
     	}

        public WebCamTexture GetMobileCamera() { return MobileCamera; }
        public GameObject GetCameraField() { return CameraField; }
        public bool HaveCamera() 
        { 
            WebCamDevice[] devices = WebCamTexture.devices;
            return (devices.Length > 0);
        }
}