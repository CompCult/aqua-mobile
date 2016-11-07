using UnityEngine;
using System.Collections;

public class HideItself : MonoBehaviour 
{
	public GameObject This;

	public void Start () {}

	public void Update() {}
	
	public void OnMouseOver()
	{
		Debug.Log("Passou");
		This.SetActive(false);
	}
}
