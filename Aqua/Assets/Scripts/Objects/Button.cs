using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Button {

	public AudioSource SoundFX;

	public void LoadScene(string Scene) 
	{
		if (Scene != null) 
			SceneManager.LoadScene(Scene);
	}

	public void PlaySound()
	{
		SoundFX.Play();
	}

	public void StopSound()
	{
		SoundFX.Stop();
	}
}
