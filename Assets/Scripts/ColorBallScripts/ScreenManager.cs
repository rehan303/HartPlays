using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Simple_JSON;

public class ScreenManager : MonoBehaviour
{
	public static ScreenManager Instance = null;
	const string updateScoreUrl = "https://www.hartplays.co.uk/api/users/updateScore";

	public GameObject PauseButton_GameObject;
	public GameObject ScoresText_GameObject;
	public GameObject LivesText;
	public GameObject panelLevelScreen;

	private int nextLevel;
	public GameObject MainGameControler;

	private GameManager GameManager_Component;
	private RandomInstantiationOfBalls BallInstatiate_Component;
	public Text FinalScores_Text;
	public Text ScoreToAchive;
	public Text levelNumberText;
	public Text levelStatusText;
	public Text timerText;
	public GameObject homeButton;
	public GameObject NextButton;
	public GameObject TryAgain;
	public GameObject Restart;
	public GameObject EmotionsImage;
	public static bool isLevelEnded = false;
	public Sprite sadImage;
	public Sprite HappyImage;

	public AudioClip LevelCleared;
	public AudioClip LevelEndClip;
	private Animator anim;

	AudioSource audioSource;

	[Header ("ColorBall Sounds")]
	public GameObject MusicButton;
	public GameObject SoundButton;

	public Sprite MusicOnImage;
	public Sprite MusicOffImage;
	public Sprite SoundOnImage;
	public Sprite SoundOffImage;
	public static bool AdsSeen;


	[Header ("CheckInternetConnection")]
	public GameObject InternetCheckPanel;
	public Text InternetMessage;


	void OnEnable ()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		if (Instance == null) {
			Instance = this;
		} else if (Instance != null) {
			Destroy (this.gameObject);
		}
	}
	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
		GameManager_Component = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		BallInstatiate_Component = GameObject.Find ("Colorballs_RandomInstantiation").GetComponent<RandomInstantiationOfBalls> ();
		Application.targetFrameRate = 300;
		ScoreToAchive.text = "/" + GameManager.TotalScoresToBeAchieved.ToString ();
		//FinalScores_Text.text =  GameManager.TotalPointsOrScores.ToString() + " / " + GameManager.TotalScoresToBeAchieved.ToString();
		anim = panelLevelScreen.GetComponent<Animator> ();
//		long temp = Convert.ToInt64(PlayerPrefs.GetString ("CompitionEndTime"));
//		EndTime =DateTime.FromBinary(temp);
		if (HartPlayerRegistration.Isplayforpaid) {
			LivesText.SetActive (true);
			ScoresText_GameObject.GetComponent <CameraAnchor> ().anchorOffset = new Vector3 (0f, 2f, 2.77f);
			LivesText.GetComponent <CameraAnchor>().anchorOffset = new Vector3 (0f, -2f, 2.77f);
			if(PlayerPrefs.GetInt ("ColorBallLives") == 1)
				LivesText.GetComponent <Text>().text =  PlayerPrefs.GetInt ("ColorBallLives").ToString ()+ " Live";
			else
				LivesText.GetComponent <Text>().text =  PlayerPrefs.GetInt ("ColorBallLives").ToString ()+ " Lives";
		} else {
			ScoresText_GameObject.GetComponent <CameraAnchor> ().anchorOffset = new Vector3 (0f, 0f, 2.77f);
			LivesText.SetActive (false);
		}
	}

	void Update ()
	{	
		if (HartPlayerRegistration.Isplayforpaid) {	
			if (CompetitionController.compitionRunning && DateTime.UtcNow > CompetitionController.EndTime) {				
				CompetitionController.EndComipetition ();
			} else if (CompetitionController.EndTime < DateTime.UtcNow && CompetitionController.compitionRunning && DateTime.UtcNow == CompetitionController.EndTime) {
				CompetitionController.EndComipetition ();
			}
		}
	}

	public void LevelClearScreen ()
	{

		if (HartPlayerRegistration.Isplayforpaid) {
			panelLevelScreen.SetActive (true);
			NextButton.GetComponent<RectTransform> ().localPosition = new Vector2 (48, -250);
			NextButton.SetActive (false);
			homeButton.GetComponent<RectTransform> ().localPosition = new Vector2 (0, -250);
			Restart.SetActive (false);
				NextButton.SetActive (false);
				homeButton.GetComponent<RectTransform> ().localPosition = new Vector2 (0, -250);
				//Active Next Game Play
				LivesText.SetActive (false);

//			int SubtracLive = 0;
//			SubtracLive = PlayerPrefs.GetInt ("ColorBallLives");
//			SubtracLive = SubtracLive - 1;
//			PlayerPrefs.SetInt ("ColorBallLives", SubtracLive );
			if (PlayerPrefs.GetInt ("ColorBallLives") == 0) {
				IAPManager.PlaysDecreasedFromColorBall = false;
			}
			//Post Score
			int previousScore = PlayerPrefs.GetInt ("TempColorBallScore");
			previousScore = GameManager.TotalPointsOrScores + previousScore;
			PlayerPrefs.SetInt ("TempColorBallScore",previousScore);
			if(PlayerPrefs.GetInt ("ColorBallLives") <= 0)
			{
				if(PlayerPrefs.GetInt ("ColorBallPlayToWinHighScore") < PlayerPrefs.GetInt ("TempColorBallScore"))
				{
					PlayerPrefs.SetInt ("ColorBallPlayToWinHighScore", PlayerPrefs.GetInt ("TempColorBallScore"));
					ScreenManager.Instance.PostScores (PlayerPrefs.GetString ("UserEmail"), PlayerPrefs.GetInt ("ColorBallPlayToWinHighScore"));
					PlayerPrefs.DeleteKey ("TempColorBallScore");
				}
				PlayerPrefs.DeleteKey ("TempColorBallScore");
			}
			StartCoroutine (GameSaveState.Instance.PostUserStatus ());			

		} else {
			panelLevelScreen.SetActive (true);
		}
		GameObject.Find ("BombAnimation").SetActive (false);

		PlayLevelEndAnimation (true);
		if(HartPlayerRegistration.Isplayforpaid)
		{
			StartCoroutine (GameSaveState.Instance.GetUserStatus (true));			
			FinalScores_Text.transform.GetChild (0).GetComponent<Text> ().text = "Score\n" + PlayerPrefs.GetInt ("TempColorBallScore").ToString () +"\n" + "High Score\n"+ PlayerPrefs.GetInt ("total_high_score").ToString ();
		}else
		{
			FinalScores_Text.text = GameManager.TotalPointsOrScores.ToString () + " / " + GameManager.TotalScoresToBeAchieved.ToString ();
		}
//		FinalScores_Text.text = GameManager.TotalPointsOrScores.ToString ();
		EmotionsImage.GetComponent<Image> ().sprite = HappyImage;
		LivesText.SetActive (false);
		levelStatusText.text = "Level Completed!";
		if(HartPlayerRegistration.Isplayforpaid)
			levelNumberText.text = "Congratulations!!";
		else
			levelNumberText.text = "Level " + CurrentLevel ();
		
		PauseButton_GameObject.SetActive (false);
		ScoresText_GameObject.SetActive (false);
		if (this.gameObject.GetComponent<PowerUpContoller> ().PurchasedpowerUp_GameObject != null) {
			this.gameObject.GetComponent<PowerUpContoller> ().PurchasedpowerUp_GameObject.SetActive (false);
		}
		if (this.gameObject.GetComponent<PowerUpContoller> ().powerUp_GameObject != null) {
			this.gameObject.GetComponent<PowerUpContoller> ().powerUp_GameObject.SetActive (false);
		}
		MainGameControler.GetComponent<MainGameController_Touch> ().PowerUpPanel.SetActive (false);
	
		timerText.enabled = false;

		GameManager_Component.PotSpawnner (10, false);
		BallInstatiate_Component.enabled = false;

		foreach (GameObject temp_obj in RandomInstantiationOfBalls.instantiatedBalls_Runtime) {
			temp_obj.SetActive (false);
		}

		isLevelEnded = true;
		BackgroundMusicManger.instance.PlaySoundEffect (audioSource, LevelCleared);

		if(PlayerPrefs.GetInt ("ColorBallLives") <= 0)
		{
			PlayerPrefs.DeleteKey ("TempColorBallScore");
		}

	}

	public void PauseScreen ()
	{
		
	}

	public void LevelOverScreen ()
	{		
		panelLevelScreen.SetActive (true);
		NextButton.SetActive (false);
		Restart.SetActive (false);
		PlayLevelEndAnimation (true);
		if (HartPlayerRegistration.Isplayforpaid) {
			TryAgain.SetActive (false);
			homeButton.GetComponent<RectTransform> ().localPosition = new Vector2 (0, -250);
		} else {
			TryAgain.SetActive (true);
		}
		GameObject.Find ("BombAnimation").SetActive (false);
		GameManager_Component.PotSpawnner (10, false);
		BallInstatiate_Component.enabled = false;

		if(HartPlayerRegistration.Isplayforpaid)
			levelNumberText.text = "Oops!!";
		else
			levelNumberText.text = "Level " + CurrentLevel ();
		
		PauseButton_GameObject.SetActive (false);
		ScoresText_GameObject.SetActive (false);
		LivesText.SetActive (false);
		if (this.gameObject.GetComponent<PowerUpContoller> ().PurchasedpowerUp_GameObject != null) {
			this.gameObject.GetComponent<PowerUpContoller> ().PurchasedpowerUp_GameObject.SetActive (false);
		}
		if (this.gameObject.GetComponent<PowerUpContoller> ().powerUp_GameObject != null) {
			this.gameObject.GetComponent<PowerUpContoller> ().powerUp_GameObject.SetActive (false);
		}
		MainGameControler.GetComponent<MainGameController_Touch> ().PowerUpPanel.SetActive (false);
		timerText.enabled = false;
		levelStatusText.text = "Level Failed!";

		EmotionsImage.GetComponent<Image> ().sprite = sadImage;

		foreach (GameObject temp_obj in RandomInstantiationOfBalls.instantiatedBalls_Runtime) {
			temp_obj.SetActive (false);
		}

		isLevelEnded = true;
		BackgroundMusicManger.instance.PlaySoundEffect (audioSource, LevelEndClip);
		// for temp unless client aproovle
		int previousScore = 0;
		if (HartPlayerRegistration.Isplayforpaid) {
			MenuScreen_Controller.GameId = 2;
			PlayerPrefs.SetInt ("CurruntGameId", MenuScreen_Controller.GameId);
			int SubtracLive = 0;
			SubtracLive = PlayerPrefs.GetInt ("ColorBallLives");
			SubtracLive = SubtracLive - 1;
			PlayerPrefs.SetInt ("ColorBallLives", SubtracLive );
			print (PlayerPrefs.GetInt ("ColorBallLives"));
			if (PlayerPrefs.GetInt ("ColorBallLives") == 0) {
				IAPManager.PlaysDecreasedFromColorBall = false;
			}
			//Post Score
			previousScore = PlayerPrefs.GetInt ("TempColorBallScore");
			previousScore = GameManager.TotalPointsOrScores + previousScore;
			PlayerPrefs.SetInt ("TempColorBallScore",previousScore);
			if(PlayerPrefs.GetInt ("ColorBallLives") <= 0)
			{
				if(PlayerPrefs.GetInt ("ColorBallPlayToWinHighScore") < PlayerPrefs.GetInt ("TempColorBallScore"))
				{
					PlayerPrefs.SetInt ("ColorBallPlayToWinHighScore", PlayerPrefs.GetInt ("TempColorBallScore"));
					ScreenManager.Instance.PostScores (PlayerPrefs.GetString ("UserEmail"), PlayerPrefs.GetInt ("ColorBallPlayToWinHighScore"));
				}
			}
			StartCoroutine (GameSaveState.Instance.PostUserStatus ());
		}
		if(HartPlayerRegistration.Isplayforpaid)
		{
			StartCoroutine (GameSaveState.Instance.GetUserStatus (true));	
				FinalScores_Text.transform.GetChild (0).GetComponent<Text> ().text = "Score\n" + PlayerPrefs.GetInt ("TempColorBallScore").ToString () +"\n" + "High Score\n"+ PlayerPrefs.GetInt ("ColorBallPlayToWinHighScore").ToString ();

		}else
		{
			FinalScores_Text.text = GameManager.TotalPointsOrScores.ToString () + " / " + GameManager.TotalScoresToBeAchieved.ToString ();
		}
	
		if(PlayerPrefs.GetInt ("ColorBallLives") <= 0)
		{			
			PlayerPrefs.DeleteKey ("TempColorBallScore");
		}
//		ShowNormalAdsForColorBallGame ();

	}

	#region Show Ads

	public void ShowNormalAdsForColorBallGame ()
	{
		var TotleScore = GameManager.TotalPointsOrScores;

		if (GameManager.LevelSelected_Number == 1) {
			if (TotleScore > 45 && TotleScore < 60 || TotleScore > 75 && TotleScore < 95) {
				UnityAdsManager.Instance.ShowAds ();
			}
		} else if (GameManager.LevelSelected_Number == 2) {
			if (TotleScore > 90 && TotleScore < 115 || TotleScore > 126 && TotleScore < 136) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 3) {
			if (TotleScore > 80 && TotleScore < 125 || TotleScore > 140 && TotleScore < 150) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 4) {
			if (TotleScore > 70 && TotleScore < 105 || TotleScore > 125 && TotleScore < 150) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 5) {
			if (TotleScore > 80 && TotleScore < 120 || TotleScore > 140 && TotleScore < 160 || TotleScore > 190 && TotleScore < 200) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 6) {
			if (TotleScore > 90 && TotleScore < 130 || TotleScore > 150 && TotleScore < 175 || TotleScore > 205 && TotleScore < 215) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 7) {
			if (TotleScore > 100 && TotleScore < 140 || TotleScore > 160 && TotleScore < 185 || TotleScore > 230 && TotleScore < 255) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 8) {
			if (TotleScore > 100 && TotleScore < 145 || TotleScore > 165 && TotleScore < 190 || TotleScore > 240 && TotleScore < 260) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 9) {
			if (TotleScore > 110 && TotleScore < 155 || TotleScore > 175 && TotleScore < 220 || TotleScore > 250 && TotleScore < 295) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 10) {
			if (TotleScore > 120 && TotleScore < 165 || TotleScore > 185 && TotleScore < 230 || TotleScore > 280 && TotleScore < 315) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 11) {
			if (TotleScore > 120 && TotleScore < 165 || TotleScore > 185 && TotleScore < 230 || TotleScore > 280 && TotleScore < 315) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 12) {
			if (TotleScore > 130 && TotleScore < 180 || TotleScore > 195 && TotleScore < 235 || TotleScore > 300 && TotleScore < 335) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 13) {
			if (TotleScore > 145 && TotleScore < 190 || TotleScore > 230 && TotleScore < 280 || TotleScore > 330 && TotleScore < 380) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 14) {
			if (TotleScore > 160 && TotleScore < 205 || TotleScore > 245 && TotleScore < 295 || TotleScore > 350 && TotleScore < 400) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 15) {
			if (TotleScore > 175 && TotleScore < 225 || TotleScore > 255 && TotleScore < 310 || TotleScore > 365 && TotleScore < 420) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 16) {
			if (TotleScore > 185 && TotleScore < 235 || TotleScore > 270 && TotleScore < 330 || TotleScore > 400 && TotleScore < 450) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else if (GameManager.LevelSelected_Number == 17) {
			if (TotleScore > 190 && TotleScore < 245 || TotleScore > 290 && TotleScore < 345 || TotleScore > 420 && TotleScore < 460) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} else {
			if (TotleScore > 200 && TotleScore < 255 || TotleScore > 300 && TotleScore < 360 || TotleScore > 440 && TotleScore < 490) {
				UnityAdsManager.Instance.ShowAds ();
			}

		} 
		ScreenManager.AdsSeen = true;

	}

	#endregion

	public void HomeButton ()
	{
		if (HartPlayerRegistration.Isplayforpaid) {
			MainGameController_Touch.PlayingColorBall = true;
			MenuScreen_Controller.CurrntGamePlaying = "ColorBall";
		}
		
		if (levelStatusText.text == "Level Failed!" && AdsSeen == false) {
			if (!HartPlayerRegistration.Isplayforpaid) {
				Invoke ("ShowNormalAdsForColorBallGame", 0.4f);
				isLevelEnded = false;
				PlayLevelEndAnimation (false);
				StartCoroutine (LoadScene ("00_MenuScreen"));
			}else
			{
				isLevelEnded = false;
				PlayLevelEndAnimation (false);
				StartCoroutine (LoadScene ("00_MenuScreen"));
				AdsSeen = false; 
			}
		} 
		else {
			isLevelEnded = false;
			PlayLevelEndAnimation (false);
			StartCoroutine (LoadScene ("00_MenuScreen"));
			AdsSeen = false; 
		}

	}

	public void RestartButton ()
	{
		if (levelStatusText.text == "Level Failed!" && AdsSeen == false) {
			if (!HartPlayerRegistration.Isplayforpaid) {
				Invoke ("ShowNormalAdsForColorBallGame", 0.4f);
			}
		} else {
			isLevelEnded = false;
			PlayerPrefs.SetString ("LevelSelected", "Level_" + CurrentLevel ());
	
			PlayLevelEndAnimation (false);
			AdsSeen = false;
			StartCoroutine (LoadScene ("01_MainGamePlay"));
		}

	}

	public void NextLevelButton ()
	{ 
		isLevelEnded = false;
		PlayLevelEndAnimation (false);
		if (HartPlayerRegistration.Isplayforpaid == false) {
			if (PlayerPrefs.GetInt ("LevelsUnlockForColorBallGame") <= 20 && CurrentLevel () != 20) {
				PlayerPrefs.SetString ("LevelSelected", "Level_" + (CurrentLevel () + 1));
				StartCoroutine (LoadScene ("01_MainGamePlay"));
			} 
		} else if (HartPlayerRegistration.Isplayforpaid == true) {

			if (PlayerPrefs.GetInt ("ColorBallLevelLockedPaid") <= 20 && CurrentLevel () != 20) {
				PlayerPrefs.SetString ("LevelSelected", "Level_" + (CurrentLevel () + 1));
				StartCoroutine (LoadScene ("01_MainGamePlay"));			 
			} 
		}
	}

	int  CurrentLevel ()
	{
		return (PlayerPrefs.GetInt ("CurrentLevelNumber"));
	}

	public void UnlockingOfNextLevel ()
	{
		bool isCompitionGoing = PlayerPrefs.GetInt ("CompetitionGoingon") == 1 ? true : false;
		if (HartPlayerRegistration.Isplayforpaid == false) {
			if (PlayerPrefs.GetInt ("LevelsUnlockForColorBallGame") < 20) {
				PlayerPrefs.SetInt ("LevelsUnlockForColorBallGame", CurrentLevel () + 1);
				print ("Unlock for free level");
				// post scores.........

				if (PlayerPrefs.HasKey ("TotalScore")) {
					int Scores = PlayerPrefs.GetInt ("TotalScore");
					PlayerPrefs.SetInt ("TotalScore", Scores + GameManager.TotalPointsOrScores);
//					PlayerPrefs.DeleteAll ();
				} else {
					PlayerPrefs.SetInt ("TotalScore", GameManager.TotalPointsOrScores);
				}
			} 
		}
		if (HartPlayerRegistration.Isplayforpaid == true && isCompitionGoing) {
			if (PlayerPrefs.GetInt ("ColorBallLevelLockedPaid") < 20) {
				PlayerPrefs.SetInt ("ColorBallLevelLockedPaid", CurrentLevel () + 1);
				print ("Unlock for paid level");
				// post scores.........

				if (PlayerPrefs.HasKey ("TotalScore")) {
					int Scores = PlayerPrefs.GetInt ("TotalScore");
					PlayerPrefs.SetInt ("TotalScore", Scores + GameManager.TotalPointsOrScores);
				} else {
					PlayerPrefs.SetInt ("TotalScore", GameManager.TotalPointsOrScores);
				}					
			}
			string email = PlayerPrefs.GetString ("UserEmail");
//			PostScores (email, PlayerPrefs.GetInt ("TotalScore"));
		}
		//Post Score On Fb Do Score Part
		FacebookManager.Instance.PostScoreOnFacebook (PlayerPrefs.GetInt ("TotalScore"));
	}

	public void PostScores (string _email, int _scores)
	{	
		var encoding = new System.Text.UTF8Encoding ();

		Dictionary<string,string> postHeader = new Dictionary<string,string> ();

		UserInfo Info = new UserInfo ();

		Info.email = _email;
		Info.score = _scores;
		string json = JsonUtility.ToJson (Info);

		postHeader.Add ("Content-Type", "application/json");
//		postHeader.Add("Content-Length", json.Length.ToString());

		WWW www = new WWW (updateScoreUrl, encoding.GetBytes (json), postHeader);

		StartCoroutine (WaitForScoresPostRequest (www));
	}

	IEnumerator WaitForScoresPostRequest (WWW www)
	{
		print ("Waiting For Response for Scores Post");

		yield return(www);

		if (www.error == null) {
			JSONNode _jsNode = JSON.Parse (www.text);
			print ("JSON DATA IS -->>" + _jsNode.ToString ());

			if (_jsNode [2].ToString ().Contains ("error")) {
				
			} else {
				print ("Scores Post Sucess" + www.text);
			}
		} else {
			print ("Scores Post Error" + www.error);
		}
	}

	void PlayLevelEndAnimation (bool status)
	{
		anim.SetBool ("ShowLevelEnd", status);
	}

	IEnumerator LoadScene (string SceneName)
	{
		yield return new WaitForSeconds (1.0f);	
		SceneManager.LoadScene (SceneName);
		FacebookManager.Instance.LoadFacebookScreen ();
	}

	public void InternetConnectionCheck (string message)
	{
		InternetCheckPanel.SetActive (true);
		InternetMessage.text = message;
	}

	public void DisableInternetCheck ()
	{
		InternetCheckPanel.SetActive (false);
		InternetMessage.text = null;
	}

	public void CheckSoundState ()
	{
		int Mousic = PlayerPrefs.GetInt ("isMusicOn");
		PlayerPrefs.SetInt ("isMusicOn", BackgroundMusicManger.instance.isMusicOn ? Mousic : Mousic);
		MusicButton.GetComponent<Image> ().sprite = BackgroundMusicManger.instance.isMusicOn ? MusicOnImage : MusicOffImage;

		int Sound = PlayerPrefs.GetInt ("isSfxOn");
		PlayerPrefs.SetInt ("isSfxOn", BackgroundMusicManger.instance.isSfxOn ? Sound : Sound);
		SoundButton.GetComponent<Image> ().sprite = BackgroundMusicManger.instance.isSfxOn ? SoundOnImage : SoundOffImage;
		print ("Sound state  is --- " + BackgroundMusicManger.instance.isSfxOn + "\n PlayerPrefs Value is --- " +	PlayerPrefs.GetInt ("isSfxOn"));

	}

	public void MusicOn_OffButton ()
	{
		BackgroundMusicManger.instance.isMusicOn = !BackgroundMusicManger.instance.isMusicOn;
		PlayerPrefs.SetInt ("isMusicOn", BackgroundMusicManger.instance.isMusicOn ? 0 : 1);
		MusicButton.GetComponent<Image> ().sprite = BackgroundMusicManger.instance.isMusicOn ? MusicOnImage : MusicOffImage;
	}

	public void SoundOn_OffButton ()
	{
		BackgroundMusicManger.instance.isSfxOn = !BackgroundMusicManger.instance.isSfxOn;
		PlayerPrefs.SetInt ("isSfxOn", BackgroundMusicManger.instance.isSfxOn ? 0 : 1);
		SoundButton.GetComponent<Image> ().sprite = BackgroundMusicManger.instance.isSfxOn ? SoundOnImage : SoundOffImage;
		print ("Sound state  is --- " + BackgroundMusicManger.instance.isSfxOn + "\n PlayerPrefs Value is --- " +	PlayerPrefs.GetInt ("isSfxOn"));
	}

}
