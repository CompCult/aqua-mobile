using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AquaWorld : MonoBehaviour {

	public AudioSource musicPlayer;
	public ParticleSystem bubblesLeft;
	public ParticleSystem bubblesRigth;

	// Use this for initialization
	void Start () {
		bubblesLeft.GetComponent<ParticleSystem>().playbackSpeed = 0.15f;
		bubblesRigth.GetComponent<ParticleSystem>().playbackSpeed = 0.15f;

		bubblesLeft.Play();
		bubblesRigth.Play();
	}
	
	void OpenGame(string scene) {
	}

	// Update is called once per frame
	void Update () {
		DontDestroyOnLoad(musicPlayer);
		
		if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "AquaWorld")
        {
        	musicPlayer.Stop();
            LoadScene("Home");
        }
	}

	public void LoadScene(string scene) {
		SceneManager.LoadScene(scene);
	}
}
