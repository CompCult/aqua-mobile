using UnityEngine;
using System.Collections;

public class Splash : GenericScreen {

	public void Start () 
	{
		StartCoroutine(SplashTime());
	}

	private IEnumerator SplashTime () 
	{
        yield return new WaitForSeconds(4);

        LoadScene("Login");
    }

}
