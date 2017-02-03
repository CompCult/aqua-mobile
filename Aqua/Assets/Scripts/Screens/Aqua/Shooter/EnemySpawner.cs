using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] Enemys;
	public GameObject EnemyPos;

	float maxSpawnRateInSeconds = 5f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void SpawnEnemy()
	{
		GameObject enemy = (GameObject) Instantiate(Enemys[Random.Range(0, Enemys.Length - 1)]);
		enemy.transform.position = new Vector3(Random.Range(2, -2), EnemyPos.transform.position.y, EnemyPos.transform.position.z);
	
		ScheduleNextEnemySpawn();
	}

	void ScheduleNextEnemySpawn() 
	{
		float spawnInNSeconds;

		if(maxSpawnRateInSeconds > 1f)
		{
			spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
		}
		else
			spawnInNSeconds = 1f;

		Invoke("SpawnEnemy", spawnInNSeconds);
	}

	void IncreaseSpawnRate()
	{
		if(maxSpawnRateInSeconds > 1f)
			maxSpawnRateInSeconds--;
		if(maxSpawnRateInSeconds == 1f)
			CancelInvoke("IncreaseSpawnRate");
	}

	public void ScheduleEnemySpawner()
	{
		maxSpawnRateInSeconds = 5f;

		Invoke("SpawnEnemy", maxSpawnRateInSeconds);
		InvokeRepeating("IncreaseSpawnRate", 0f, 40f);
	}

	public void UnscheduleEnemySpawner()
	{
		CancelInvoke("SpawnEnemy");
		CancelInvoke("IncreaseSpawnRate");
	}
}
