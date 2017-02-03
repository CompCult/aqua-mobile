using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boat : MonoBehaviour {

	public GameObject Bullet;
	public GameObject bulletPos;
	public GameObject Explosion;
	public GameObject ManagerObj;
	public Text Lives;

	const int MaxLives = 3;
	int lives;

	float xPosition;			


	public void Init()
	{
		lives = MaxLives;
		Lives.text = lives.ToString();

		gameObject.SetActive(true);	
	}

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public void MoveLeft()
	{
		float xBoat = transform.position.x;
    	
    	if(xBoat > -2.5f)
    	{
    		xBoat -= 0.5f;
    		transform.position = new Vector3(xBoat, transform.position.y, transform.position.z);
		}
	}

	public void MoveRight()
	{
		float xBoat = transform.position.x;
		
		if(xBoat < 2.17f)
    	{
    		xBoat += 0.5f;
    		transform.position = new Vector3(xBoat, transform.position.y, transform.position.z);
		}
	}

	public void Fire() 
	{
		GameObject bullet = (GameObject) Instantiate(Bullet);
		bullet.transform.position = bulletPos.transform.position;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if((col.tag == "EnemyTag"))
		{
			PlayExplosion();
			lives--;
			Lives.text = lives.ToString();

			if(lives == 0)
			{
				ManagerObj.GetComponent<Manager>().SetManagerState(Manager.ManagerState.GameOver);
				gameObject.SetActive(false);
			}
		}
	}

	void PlayExplosion()
	{
		GameObject explosion = (GameObject) Instantiate(Explosion);
		explosion.transform.position = transform.position;
	}
}
