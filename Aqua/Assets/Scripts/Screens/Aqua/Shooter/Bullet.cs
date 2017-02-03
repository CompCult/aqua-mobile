using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	float speed;

	// Use this for initialization
	void Start () 
	{
		speed = 5f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		 Vector2 position = transform.position;
		 position = new Vector2 (position.x, position.y + speed * Time.deltaTime);

		 transform.position = position;

		 if(position.y > 6.5f) {
		 	Destroy(gameObject);
		 }	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if((col.tag == "EnemyTag"))
		{
			Destroy(gameObject);
		}
	}
}
