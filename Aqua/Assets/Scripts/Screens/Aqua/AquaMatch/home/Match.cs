using UnityEngine;
using System.Collections;

public class Match : MonoBehaviour
{

    public GameObject HomeScreen;
    public GameObject Help;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
