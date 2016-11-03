﻿using UnityEngine;
using System.Collections;

public class RandomMove : MonoBehaviour 
{
	public float horizontalSpeed,
	verticalSpeed,
	finalPosition,
 	amplitude;

 	private Vector3 tempPosition;

 	void Start () 
  	{
  		tempPosition = transform.position;
  		finalPosition = 6.2f;
 	}

 	void FixedUpdate () 
  	{
  		tempPosition.x += horizontalSpeed;
		tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed)* amplitude + 2.35f;
  		transform.position = tempPosition;

  		if (tempPosition.x >= finalPosition)
  			tempPosition.x = -finalPosition;
  	}
}

