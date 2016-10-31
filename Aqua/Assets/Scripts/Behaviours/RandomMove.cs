using UnityEngine;
using System.Collections;

public class RandomMove : MonoBehaviour {

	public float horizontalSpeed;
 	public float verticalSpeed;
 	public float amplitude;
 	public GameObject obj;

 	private Vector3 tempPosition;

 	void Start () 
  	{
  		tempPosition = transform.position;
 	}
 
 	void FixedUpdate () 
  	{
  		tempPosition.x += horizontalSpeed;
		tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed)* amplitude + 2.35f;
  		transform.position = tempPosition;
 	}
}

