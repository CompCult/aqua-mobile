using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BrainTeaser : Screen {

	public Text PointsText, LivesText, TimeText, GameOverText;
	public GameObject MiniGame;

	private int Points, Lives, Time, StartTime;
	private string imageURL = "http://www.nowcultured.com/images/2013/08/beautiful-water-pokemon-fanart-gallery/aquarium-marine-tube-fanart.png";

	public void Start () 
	{
		backScene = "AquaHome";

		Lives = 3;
		Points = 0;
		StartTime = 368;
		Time = StartTime;

		UpdateGameFields();
		StartCoroutine(StartGame());
	}

    private IEnumerator StartGame() 
    {
    	// Loads game Image
        WWW www = new WWW(imageURL);
        yield return www;
        www.LoadImageIntoTexture(MiniGame.GetComponent<ST_PuzzleDisplay>().PuzzleImage);

        StartCoroutine(DecreaseTimer());
    }

	private void Restart()
	{
		Time = StartTime;

		UpdateGameFields();
	}
	
	private void UpdateGameFields() 
	{
		LivesText.text = Lives.ToString();
		PointsText.text = Points.ToString();
		TimeText.text = Time.ToString();
	}

	private IEnumerator DecreaseTimer()
	{
		yield return new WaitForSeconds(1);

		if (Time == 0)
			DecreaseLives();

		Time -= 1;
		TimeText.text = Time.ToString();

		CheckCompletePuzzle();

		StartCoroutine(DecreaseTimer());
	}

	private void DecreaseLives()
	{
		Lives -= 1;
		UpdateGameFields();

		if (Lives < 1)
			GameOver();
		else
			Restart();
	}

	private void CheckCompletePuzzle()
	{
		bool IsPuzzleComplete = MiniGame.GetComponent<ST_PuzzleDisplay>().Complete;

		if (IsPuzzleComplete)
			ScorePoints();
	}

	private void ScorePoints()
	{
		Points += 10;
		UpdateGameFields();

		foreach (Transform child in MiniGame.transform)
		{
  			Destroy(child.gameObject);
		}

		MiniGame.GetComponent<ST_PuzzleDisplay>().Complete = false;
		MiniGame.GetComponent<ST_PuzzleDisplay>().Start();
	}

	private void GameOver()
	{
		StopCoroutine(DecreaseTimer());

		GameOverText.enabled = true;
		TimeText.text = "FIM";
	}

}
