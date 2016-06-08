using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Net;
using System.IO;

public class Download : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DownloadString ()
	{
	    WebClient client = new WebClient();
	    string reply = client.DownloadString ("http://www.grugi2324.com/");

	    print(reply);
	}
}
