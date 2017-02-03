using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	public GameObject playButton;
	public GameObject gameOver;
	public GameObject enemySpawner;
	public GameObject Boat;
	public GameObject ScoreUI;
	public GameObject buttonPanel;

	public enum ManagerState
	{
		Opening,
		Gameplay,
		GameOver,
	}

	ManagerState MState;

	// Use this for initialization
	void Start ()
	{
		MState = ManagerState.Opening;
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(GlobalConsts.SCENE_AQUA_MAP);
        }
	}

	// Update is called once per frame
	void UpdateManagerState ()
	{
		switch(MState)
		{
		case ManagerState.Opening:

			gameOver.SetActive(false);
			playButton.SetActive(true);
			break;
		case ManagerState.Gameplay:

			ScoreUI.GetComponent<GameScore>().Score = 0;
			playButton.SetActive(false);
			Boat.GetComponent<Boat>().Init();

			buttonPanel.SetActive(true);
			enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
			break;

		case ManagerState.GameOver:

			enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
			gameOver.SetActive(true);
			Invoke("ChangeToOpeningState", 8f);
			break;
		}	
	}

	public void SetManagerState(ManagerState state)
	{
		MState = state;
		UpdateManagerState();
	}

	public void StartGamePlay()
	{
		MState = ManagerState.Gameplay;
		UpdateManagerState();
	}

	public void ChangeToOpeningState() 
	{
		SetManagerState(ManagerState.Opening);
	}

	public void Fire()
	{
		Boat.GetComponent<Boat>().Fire();
	}

	public void MoveLeft()
	{
		Boat.GetComponent<Boat>().MoveLeft();
	}

	public void MoveRight()
	{
		Boat.GetComponent<Boat>().MoveRight();
	}
}
