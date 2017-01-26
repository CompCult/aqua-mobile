using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Bubbles : MonoBehaviour
{
	public ParticleSystem bubblesLeft;
	public ParticleSystem bubblesRigth;
	
	public void Start () 
	{
		var bubbleLeftMain = bubblesLeft.GetComponent<ParticleSystem>().main;
		var bubbleRightMain = bubblesRigth.GetComponent<ParticleSystem>().main;

		bubbleLeftMain.simulationSpeed = 0.15f;
		bubbleRightMain.simulationSpeed = 0.15f;

		bubblesLeft.Play();
		bubblesRigth.Play();
	}
}
