using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioSource globalMusic;

	public void Start()
	{
		PlayMusic(globalMusic);
	}

	public void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	public void PlaySound (AudioSource sound)
	{
		sound.Play();
		sound.loop = false;
	}

	public void PlayMusic (AudioSource music)
	{
		if (globalMusic != null)
			globalMusic.Stop();

		globalMusic = music;
		globalMusic.Play();
		globalMusic.loop = true;
	}
}
