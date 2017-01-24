using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1000f;
    public Rigidbody2D rigidbody;
 
    void Update()
    {
        InputMovement();
    }
 
    void InputMovement()
    {
        if (Input.GetKey(KeyCode.W))
            rigidbody.MovePosition(rigidbody.position + Vector2.up * speed * Time.deltaTime);
 
        if (Input.GetKey(KeyCode.S))
            rigidbody.MovePosition(rigidbody.position + Vector2.down * speed * Time.deltaTime);
 
        if (Input.GetKey(KeyCode.D))
            rigidbody.MovePosition(rigidbody.position + Vector2.right * speed * Time.deltaTime);
 
        if (Input.GetKey(KeyCode.A))
            rigidbody.MovePosition(rigidbody.position + Vector2.left * speed * Time.deltaTime);
    }
}