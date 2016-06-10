using UnityEngine;
using System.IO;
using System.Collections;

public class EventSystem : MonoBehaviour {

	public LanguageManager languageManager = null;
	public string backScene = "";
	string language = "Not found";

	void Start () {
		loadSettings();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) 
    		returnScene();
	}

	void loadSettings()
	{
	   StreamReader reader = new StreamReader(Application.dataPath+"/options/data.txt");

	   while(!reader.EndOfStream)
	   {
	   		string line = reader.ReadLine();
	   		string[] splitLine = line.Split('=');

	       	if (splitLine[0] == "LANGUAGE") {
	       		this.language = splitLine[1];
	       		break;
	       	}

	   	}

		reader.Close();  
	}

	public void returnScene()
	{
		if (backScene != "")
			Application.LoadLevel(backScene);
	}
}
