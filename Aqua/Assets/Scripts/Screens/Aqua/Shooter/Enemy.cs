using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public GameObject Explosion;
	GameObject ScoreUI;
	float speed;

	// Use this for initialization
	void Start () {
		speed = 1f;	

		ScoreUI = GameObject.FindGameObjectWithTag("ScoreTag");
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 position = transform.position;
		position = new Vector2 (position.x, position.y - speed * Time.deltaTime);

		transform.position = position;

		if(position.y < -6.5f) {
			ScoreUI.GetComponent<GameScore>().Score -= 5;
			Destroy(gameObject);
		}	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if((col.tag == "BoatTag") || (col.tag == "PlayerBulletTag"))
		{
			PlayExplosion();

			ScoreUI.GetComponent<GameScore>().Score += 50;
			Destroy(gameObject);
		}
	}

	void PlayExplosion()
	{
		GameObject explosion = (GameObject) Instantiate(Explosion);

		explosion.transform.position = transform.position;
	}
}
