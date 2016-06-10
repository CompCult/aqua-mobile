using UnityEngine;
using UnityEngine.Experimental.Networking;
using UnityEngine.UI;
using System.Collections;
 
public class Login : MonoBehaviour {

	public InputField email = null;
	public InputField password = null;
	public Button buttonLogin = null;
	string url = "http://aqua-web.herokuapp.com/mobile/auth";

    public void SendData() {
 
         WWWForm form = new WWWForm();
         form.AddField("login", email.text);
         form.AddField("password", password.text);
         WWW www = new WWW(url, form);

         StartCoroutine(WaitForRequest(www));
     }
 
    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
 
        if (www.error == null)
        {
        	string response = www.text; // {"authenticated":"no"} -> ""no"}"

            if (response == "1")
            	buttonLogin.loadScene();

        } else {
             Debug.Log("Error: "+ www.error);
        }   
     }    
 }