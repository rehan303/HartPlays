using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

	// Use this for initialization

	public List<GameObject> potList = new List<GameObject> ();
	public List<GameObject> potList_Textures = new List<GameObject> ();

	public static int TotalPointsOrScores;
	public static int LevelSelected_Number = 0;
	public static int TotalScoresToBeAchieved;

	private string levelSelected_String;
	public static bool PotMove;
	private Text Scores_Text;
	public int potValue = 0;
	public PowerUpContoller powerUpControllerComponent;
	public ScreenManager screenManager;

	private CountdownTimer countdownTimerComponent;

	void OnEnable ()
	{
		print ("I am on GameManger Enable");
		potValue = 0;
		TotalPointsOrScores = 0;
		Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 0f;
		TotalScoresToBeAchieved = 0;
		Scores_Text = GameObject.Find ("Scores_Text").GetComponent<Text> ();
		Scores_Text = GameObject.Find ("Scores_Text (1)").GetComponent<Text> ();

		if (PlayerPrefs.HasKey ("NumberOfBallsInCurrentLevel")) {
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 3);
		}

		if (PlayerPrefs.HasKey ("LevelSelected")) {
			levelSelected_String = PlayerPrefs.GetString ("LevelSelected");
			print ("LevelNo->" + PlayerPrefs.GetString ("LevelSelected"));
		}
		string[] words = levelSelected_String.Split ('_');
		if (words.Length > 0) {
			LevelSelected_Number = int.Parse (words [1]);
			PlayerPrefs.SetInt ("CurrentLevelNumber", LevelSelected_Number);
		}

		StartCoroutine ("LoadLevelAccordingToLevelNumber");
	}

	void Update ()
	{
		Scores_Text.text = TotalPointsOrScores.ToString () + "/" + GameManager.TotalScoresToBeAchieved.ToString ();
	}

	void LoadLevelAccordingToLevelNumber ()
	{
		switch (LevelSelected_Number) {
		case 1:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 3);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 2.078f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 1.4f;
			TotalScoresToBeAchieved = 109;
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = false;
			CountdownTimer.timeOfLevel = 60f;
			PotMove = false;
			break;

		case 2:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 4);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 2.156f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 1.3f;
			TotalScoresToBeAchieved = 139;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = false;
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 70f;
			PotMove = false;
			break;
		case 3:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 4);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 2.234f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 1.3f;
			TotalScoresToBeAchieved = 149;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = false;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 75f;
			PotMove = false;
			break;
		case 4:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 4);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 2.312f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 1.3f;
			TotalScoresToBeAchieved = 161;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = false;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 80f;
			PotMove = false;
			break;

		case 5:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 5);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 2.39f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 1.2f;
			TotalScoresToBeAchieved = 185;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 85f;
			PotMove = false;
			break;
		case 6:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 5);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 2.468f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 1.2f;
			TotalScoresToBeAchieved = 196;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 90f;
			PotMove = false;
			break;
		case 7:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 6);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 2.546f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 1.1f;
			TotalScoresToBeAchieved = 228;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 95f;
			PotMove = false;
			break;
		case 8:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 6);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 2.624f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 1.1f;
			TotalScoresToBeAchieved = 240;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 100f;
			PotMove = false;
			break;
		case 9:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 6);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 2.702f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 1.1f;
			TotalScoresToBeAchieved = 252;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 105f;
			PotMove = false;
			break;
		case 10:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 7);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 2.78f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 1.00f;
			TotalScoresToBeAchieved = 294;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 110f;
			PotMove = false;
			break;
		case 11:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 7);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 2.858f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 1.00f;
			TotalScoresToBeAchieved = 307;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 115f;
			PotMove = false;
			break;
		case 12:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 7);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 2.936f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 1.00f;
			TotalScoresToBeAchieved = 320;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 120f;
			PotMove = false;
			break;
		case 13:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 8);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 3.014f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 0.90f;
			TotalScoresToBeAchieved = 375;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 125f;
			PotMove = false;
			break;
		case 14:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 8);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 3.092f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 0.90f;
			TotalScoresToBeAchieved = 394;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 130f;
			PotMove = false;
			break;
		case 15:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 8);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 3.17f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 0.90f;
			TotalScoresToBeAchieved = 414;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 135f;
			PotMove = false;
			break;
		case 16:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 9);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 3.248f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 0.80f;
			TotalScoresToBeAchieved = 488;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 140f;
			PotMove = false;
			break;
		case 17:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 9);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 3.326f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 0.80f;
			TotalScoresToBeAchieved = 529;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 150f;
			PotMove = false;
			break;
		case 18:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 9);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 3.404f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 0.80f;
			TotalScoresToBeAchieved = 570;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 160f;
			PotMove = false;
			break;
		case 19:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 10);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 3.482f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 0.70f;
			TotalScoresToBeAchieved = 699;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 170f;
			PotMove = true;
			break;
		case 20:
			PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", 10);
			Movement_RandomInstantiatedBalls.SpeedOfBallToMove = 1f;
			RandomInstantiationOfBalls.NextSpawnTimeOfBall = 0.70f;
			TotalScoresToBeAchieved = 748;
			Movement_RandomInstantiatedBalls.isDigonalMovementOn = true;
			powerUpControllerComponent.PowerupCount (false, false, false, false);
			PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
			CountdownTimer.timeOfLevel = 180;
			PotMove = true;
			break;
		}
	}

	public void PotSpawnner (int potValue, bool status)
	{
		for (int i = 0; i <= potValue - 1; i++) {
			potList [i].GetComponent<SpriteRenderer> ().enabled = status;
			potList [i].GetComponent<BoxCollider2D> ().enabled = status;
			potList_Textures [i].GetComponent<Image> ().enabled = status;
		}
	}

	public void LevelLayout (int numberPots, float timer, float ballSpeedMin, float ballSpeedMax)
	{ 
		PlayerPrefs.SetInt ("NumberOfBallsInCurrentLevel", numberPots);
		PlayerPrefs.SetFloat ("MinimumValueSpeedOfBall", ballSpeedMin);
		PlayerPrefs.SetFloat ("MaximumValueSpeedOfBall", ballSpeedMax);

		countdownTimerComponent.startTimer (timer);
		PotSpawnner (numberPots, false);

	}
	//	public bool WinningCondition( int count = 0 )
	//	{
	//		print ("potValue -->"+potValue );
	//
	//		for (int i = 0; i <=  potValue - 1; i++)
	//		{
	//			if (!potList [i].activeSelf)
	//			{
	//				count++;
	//			}
	//
	//		}
	//
	//
	//		if ( count >= potValue )
	//
	//			return true;
	//
	//		else
	//			return false;
	//
	//
	//	}
}
