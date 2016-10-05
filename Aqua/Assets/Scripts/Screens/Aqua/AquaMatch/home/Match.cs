using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Match : MonoBehaviour
{

    public GameObject HomeScreen;
    public GameObject Help;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(GlobalConsts.SCENE_AQUA_MAP);
        }
    }
}
