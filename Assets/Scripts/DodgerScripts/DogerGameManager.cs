using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Simple_JSON;

public class DogerGameManager : MonoBehaviour
{
	const string updateScoreUrl = "https://www.hartplays.co.uk/api/users/updateScore";
	public static DogerGameManager gameManger = null;

	private GameObject MainGame_Player;
	public GameObject PausePanel_GameObject;
	public GameObject PauseButton_GameObject;
	public GameObject GameOver_Panel;
	public GameObject Score_panel;
	public GameObject PlayerLives_Parent;

	[Header ("Shop Power ups")]
	public GameObject ShopPowerUpPanle;
	public GameObject PowerUpPanel;
	[Header ("Shop Button")]
	public GameObject Buy;
	public GameObject BackToShop;

	[Header ("Watch Video Panel")]
	public GameObject WatchVideoForLife;
	public static bool WatchVideo;
	[Header ("Pause Panel Buttons")]
	public GameObject restartButton;
	public GameObject ExitButton;
	public GameObject ResumeButton;

	public Button RetryButton;
	public Button HomeButton;
	public Text FinalScores_DynamicText;
	public Text ScoresText;

	public SpriteRenderer[] PlayerLives_Array;

	public float distanceCovered = 0.0f;
	public static float DistenceCovered = 0.0f;

	public bool isPaused = false;
	public bool GameOver = false;
	public bool isSliptAvailable = false;

	public static bool isSliptEnabled = false;
	public static bool adsSeenInDodger = false;

	public static string GameOverStatus;
	List<GameObject> tempBullet = new List<GameObject> ();

	public DogerPowerUpController powerUpController;
	AudioSource audioSource;

	[Header ("InternetConnectionCheckForDodgerGame")]
	public GameObject InternetCkeckPanel;
	public Text InternetMessage;
	

	[Header ("Music Attribute")]

	public GameObject MusicButton;
	public GameObject SoundButton;

	public Sprite MusicOnImage;
	public Sprite MusicOffImage;
	public Sprite SoundOnImage;
	public Sprite SoundOffImage;

	public GameObject LevelUp;

	[Header ("For Play To Win")]

	public GameObject FreePlayObject;
	public GameObject PaidPlayObject;
	public static bool PlayingDodger;
	public static bool Levelclear;
	public Image GameOverTitle;
	public Sprite GameOverTexture;
	public Sprite GameCompleatedTexture;

	void OnEnable ()
	{
		if (gameManger == null)
			gameManger = this;
		else if (gameManger != this)
			Destroy (gameObject);    
		Levelclear = false;
		DogerPlayerController.Health = 3;
		isSliptEnabled = false;
		Guns.NextSpawnTimeOfBullet = 2f;
		Guns.NumberOfBulletsMovementAllowed = 2;
		Guns.GunsRotationSpeed = 4f;
		Guns.GunMovement_Speed = 2f;
		RandomInstantiationOfGuns_Dodger.NextSpawnTimeOfGun = 2f;
		RandomInstantiationOfGuns_Dodger.maxGunsAllowed = 2;
		Bullets.BulletMovement_Speed = 2f;

		if(!HartPlayerRegistration.Isplayforpaid)
		{
			InvokeRepeating ("IncrementPerTime", 55, 35f);
		
		} else
		{
			InvokeRepeating ("IncrementPerTimeForPaid", 20, 30f);
		}
			
//		InvokeRepeating ("IncrementPerTime", 25f, Random.Range (8f, 13f));

		MainGame_Player = GameObject.Find ("MainPlayer");
//		powerUpController = GameObject.Find ("Doger_PowerUpController").GetComponent<DogerPowerUpController> ();
		Guns.instaintiatedBulletsOnScreen.Clear ();
		audioSource = this.gameObject.GetComponent<AudioSource> ();
		LevelUp.SetActive (false);
	}

	void Start()
	{
		HartPlayerRegistration.Instance.EmptyAllFeilds ();
		if(HartPlayerRegistration.Isplayforpaid)
		{
			FreePlayObject.SetActive (false);
			PaidPlayObject.SetActive (true);
		}else
		{
			FreePlayObject.SetActive (true);
			PaidPlayObject.SetActive (false);
		}

		if (HartPlayerRegistration.Isplayforpaid) {
			IncrementPerTimeForPaid ();
		}

	}

	void Update ()
	{
		if (!isPaused)
			distanceCovered += Time.deltaTime * (Guns.GunMovement_Speed);
		ScoresText.text = "SCORE- " + distanceCovered.ToString ("00");
		if (!HartPlayerRegistration.Isplayforpaid) {	
			FinalScores_DynamicText.gameObject.transform.parent.localPosition = new Vector3 (-72f, -80f, 0f);
			FinalScores_DynamicText.text =distanceCovered.ToString ("00");
		}
		DistenceCovered = distanceCovered;
		if (!isSliptAvailable)
			PowerUpPanel.transform.GetChild (3).GetComponent<Button> ().interactable = false;
		if(HartPlayerRegistration.Isplayforpaid)
		{
			if (WheelofFortune.SpinResult == "EASY") {
				if(DistenceCovered >= 250f && !GameOver)
				{
					GameOverStatus = "TimeOver";
					Levelclear = true;
					ExecuteGameOver (true);

				}
			} else if (WheelofFortune.SpinResult == "MEDIUM") {
				if(DistenceCovered >= 320f && !GameOver)
				{
					GameOverStatus = "TimeOver";
					Levelclear = true;
					ExecuteGameOver (true);

				}
			} else if (WheelofFortune.SpinResult == "HARD") {
				if(DistenceCovered >= 360f && !GameOver)
				{
					GameOverStatus = "TimeOver";
					Levelclear = true;
					ExecuteGameOver (true);

				} 
			} else if (WheelofFortune.SpinResult == "EXTREME") {
				if(DistenceCovered >= 400f && !GameOver)
				{
					GameOverStatus = "TimeOver";
					Levelclear = true;
					ExecuteGameOver (true);

				}
			}


		}


	}

	void IncrementPerTime ()
	{
		if (RandomInstantiationOfGuns_Dodger.NextSpawnTimeOfGun > 0.5f)
			RandomInstantiationOfGuns_Dodger.NextSpawnTimeOfGun -= 0.05f;

		if (Guns.NextSpawnTimeOfBullet > 0.5f)
			Guns.NextSpawnTimeOfBullet -= 0.05f;

		Guns.GunsRotationSpeed += 0.051f;
		
		Guns.GunMovement_Speed += 0.051f;

		Bullets.BulletMovement_Speed += 0.05f;

		if (Guns.NumberOfBulletsMovementAllowed < 4) {
			PowerUpPanel.transform.GetChild (3).GetComponent<Button> ().interactable = false;
			Guns.NumberOfBulletsMovementAllowed += 1f;
			RandomInstantiationOfGuns_Dodger.maxGunsAllowed = 2;
//			print (" MOVEMENTS ====>>>> " + Guns.NumberOfMovementsAllowed);
			StartCoroutine (ShowLevelUp ());
		} else if (Guns.NumberOfBulletsMovementAllowed == 4 && distanceCovered >= 1000f) {
			RandomInstantiationOfGuns_Dodger.maxGunsAllowed = 4;

			if (!isSliptAvailable) {
				isSliptAvailable = true;

				StartCoroutine (ActivateSplit ());
				PowerUpPanel.transform.GetChild (3).GetComponent<Button> ().interactable = true;
//				isSliptAvailable = false;
			} else
				PowerUpPanel.transform.GetChild (3).GetComponent<Button> ().interactable = false;
			StartCoroutine (ShowLevelUp ());
		}
	}

	void IncrementPerTimeForPaid ()
	{
		if (RandomInstantiationOfGuns_Dodger.NextSpawnTimeOfGun > 0.35f)
			RandomInstantiationOfGuns_Dodger.NextSpawnTimeOfGun -= 0.35f;
		if (Guns.NextSpawnTimeOfBullet > 0.35f)
			Guns.NextSpawnTimeOfBullet -= 0.35f;
		
		if(WheelofFortune.SpinResult == "EASY" )
		{
			Guns.NumberOfBulletsMovementAllowed = 0;
			Guns.GunsRotationSpeed += 0.500f;

			Guns.GunMovement_Speed += 0.500f;

			Bullets.BulletMovement_Speed += 0.150f;
			if (Guns.NumberOfBulletsMovementAllowed < 4) {
				PowerUpPanel.transform.GetChild (3).GetComponent<Button> ().interactable = false;
				RandomInstantiationOfGuns_Dodger.maxGunsAllowed =0;
			} 
			
		}else if(WheelofFortune.SpinResult == "MEDIUM" )
		{
			Guns.NumberOfBulletsMovementAllowed = 2;
			Guns.GunsRotationSpeed += 0.800f;

			Guns.GunMovement_Speed += 0.800f;

			Bullets.BulletMovement_Speed += 0.200f;
			if (Guns.NumberOfBulletsMovementAllowed < 4) {
				PowerUpPanel.transform.GetChild (3).GetComponent<Button> ().interactable = false;
				RandomInstantiationOfGuns_Dodger.maxGunsAllowed = 2;			
			} 
		}else if(WheelofFortune.SpinResult == "HARD" )
		{
			Guns.NumberOfBulletsMovementAllowed = 3;
			Guns.GunsRotationSpeed += 1.100f;

			Guns.GunMovement_Speed += 1.100f;

			Bullets.BulletMovement_Speed += 0.280f;
			if (Guns.NumberOfBulletsMovementAllowed < 4) {
				PowerUpPanel.transform.GetChild (3).GetComponent<Button> ().interactable = false;
				RandomInstantiationOfGuns_Dodger.maxGunsAllowed = 3;			
			} 
		}
		else if(WheelofFortune.SpinResult == "EXTREME" )
		{
			Guns.NumberOfBulletsMovementAllowed = 4;
			Guns.GunsRotationSpeed += 1.408f;

			Guns.GunMovement_Speed += 1.408f;

			Bullets.BulletMovement_Speed += 0.400f;

			if (Guns.NumberOfBulletsMovementAllowed == 4) {
				RandomInstantiationOfGuns_Dodger.maxGunsAllowed = 4;
				if (!isSliptAvailable) {
					isSliptAvailable = true;
					StartCoroutine (ActivateSplit ());
					PowerUpPanel.transform.GetChild (3).GetComponent<Button> ().interactable = true;
				} 
//				else
//					PowerUpPanel.transform.GetChild (3).GetComponent<Button> ().interactable = false;
			}
		}


	



	}

	IEnumerator ShowLevelUp ()
	{
		yield return new WaitForSeconds (1.2f);
		LevelUp.SetActive (true);
		StartCoroutine (HideLevelUp ());
	}

	IEnumerator HideLevelUp ()
	{
		yield return new WaitForSeconds (2f);
		LevelUp.SetActive (false);
	}

	IEnumerator ActivateSplit ()
	{
		if(HartPlayerRegistration.Isplayforpaid)
			yield return new WaitForSeconds (Random.Range (1f, 2f));
		else
			yield return new WaitForSeconds (Random.Range (15f, 25f));
		OnEnableSplit ();
	}

	public void OnEnableSplit ()
	{
		Vector3 RandomPosition = new Vector2 (Random.Range (-2.0f, 2.0f), Random.Range (-4.0f, 4.0f));
		GameObject SecondPlayer = (GameObject)Instantiate (MainGame_Player, MainGame_Player.transform.position, Quaternion.identity);
		StartCoroutine (MoveObject (SecondPlayer, SecondPlayer.transform.position, RandomPosition, 1f));
		SecondPlayer.GetComponent <BoxCollider2D> ().enabled = false;	

		SecondPlayer.name = "SecondPlayer";

	}

	IEnumerator MoveObject (GameObject Go, Vector3  startPos, Vector3  endPos, float time)
	{
		float i = 0.0f;
		float rate = 1.0f / time;

		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			Go.transform.position = Vector3.Lerp (startPos, endPos, i);
			yield return null;
		}

		Go.GetComponent <BoxCollider2D> ().enabled = true;
		isSliptEnabled = true;
	}

	#region SceenController functions...

	public void OnClickPause ()
	{
		Bullets[] Bullets = GameObject.FindObjectsOfType<Bullets> ();
		isPaused = !isPaused;
		PowerUpPanel.SetActive (!isPaused);
					
//		MainGame_Player.GetComponent<SpriteRenderer> ().enabled = !isPaused;

		foreach (var Character in GameObject.FindGameObjectsWithTag ("Player")) {
			Character.GetComponent<SpriteRenderer> ().enabled = !isPaused;
		}

//		if(Doger_InvinciblePowerUp.isPowerUpEffectGoing)
//			MainGame_Player.transform.GetChild (0).gameObject.SetActive (!isPaused);

		foreach (Bullets bullet in Bullets) {
			bullet.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = !isPaused;
		}
		foreach (var Guns in RandomInstantiationOfGuns_Dodger.instantiatedGuns_Runtime) {
			Guns.GetComponent<SpriteRenderer> ().enabled = !isPaused;
		}

		if (powerUpController.isPowerUpActive) {
			powerUpController.powerUpGameObject.GetComponent<SpriteRenderer> ().enabled = !isPaused;
		}
//		if (powerUpController.UsingBuyPowerUp) {
//			powerUpController.PurchasedPowerUp.GetComponent<SpriteRenderer> ().enabled = !isPaused;
//		}

		foreach (var BlastObj in DogerPowerUpController.PowerUpAnimations) { // disable all blast animation 
			BlastObj.GetComponent<SpriteRenderer> ().enabled = !isPaused;
		}
		var MainPlayer = GameObject.Find ("MainPlayer");
		PausePanel_GameObject.SetActive (isPaused);
		PauseButton_GameObject.SetActive (!isPaused);
		Score_panel.SetActive (!isPaused);
		if(HartPlayerRegistration.Isplayforpaid)
		{
			Buy.SetActive (false);
			restartButton.SetActive (false);
			ExitButton.GetComponent <RectTransform >().localPosition= new Vector3(0f, -81f, 0f);
			ResumeButton.GetComponent <RectTransform >().localPosition= new Vector3(0f, 22f, 0f);
		}else
		{
			restartButton.SetActive (true);
			Buy.SetActive (true);
			ExitButton.GetComponent <RectTransform >().localPosition = new Vector3(0f, -120f, 0f);
			ResumeButton.GetComponent <RectTransform >().localPosition= new Vector3(0f, 90f, 0f);
		}
		MainPlayer.GetComponent<BoxCollider2D> ().enabled = !isPaused;
	
		if (!HartPlayerRegistration.Isplayforpaid) {
			PaidPlayObject.SetActive (false);
			PlayerLives_Parent.SetActive (!isPaused);
			FreePlayObject.SetActive (!isPaused);
		}else
		{
			PaidPlayObject.SetActive (!isPaused);
			PlayerLives_Parent.SetActive (!isPaused);
			FreePlayObject.SetActive (false);
		}
		DragableObject.TapCount = 0;
		Time.timeScale = isPaused ? 0 : 1;
	}

	public void BuyInAppItems ()
	{
		ShopPowerUpPanle.SetActive (true);
		if (isPaused) {
			PausePanel_GameObject.GetComponent<RectTransform> ().localScale = Vector3.zero;
		} else
			GameOver_Panel.GetComponent<RectTransform> ().localScale = Vector3.zero;
	}

	public void BackFormShopItem ()
	{
		ShopPowerUpPanle.SetActive (false);
		if (isPaused) {
			PausePanel_GameObject.GetComponent<RectTransform> ().localScale = Vector3.one;
		} else
			GameOver_Panel.GetComponent<RectTransform> ().localScale = Vector3.one;
	}

	public void BackToInGameFromShop ()
	{
		ShopPowerUpPanle.SetActive (false);
		PausePanel_GameObject.GetComponent<RectTransform> ().localScale = Vector3.one;
	}

	public void ExecuteGameOver (bool isGameOver)
	{
		BackgroundMusicManger.instance.PlaySound (audioSource);
//		GameObject.Find ("Main Camera").GetComponent<BackgroundMusicManger> ().PlaySound (audioSource);
		GameOver = isGameOver;
		PausePanel_GameObject.SetActive (!isGameOver);
		PauseButton_GameObject.SetActive (!isGameOver);
		Score_panel.SetActive (!isGameOver);
		GameOver_Panel.SetActive (isGameOver);
		PowerUpPanel.SetActive (!isGameOver);
		int tempScore = 0;
		if (HartPlayerRegistration.Isplayforpaid) {
			RetryButton.gameObject.SetActive (false);
			HomeButton.GetComponent <RectTransform> ().anchoredPosition = new Vector2 (0, -160);
			PaidPlayObject.SetActive (false);
			// post Dodger Score
//				int SubtracLive = 0;
//				SubtracLive = PlayerPrefs.GetInt ("DodgerLives");
//				SubtracLive = SubtracLive - 1;
//				PlayerPrefs.SetInt ("DodgerLives", SubtracLive );

			if(Levelclear)
			{
				GameOverTitle.sprite = GameCompleatedTexture;
			}else
				GameOverTitle.sprite = GameOverTexture;
			if (PlayerPrefs.GetInt ("DodgerLives") == 0) {
				IAPManager.PlaysDecreasedFromDodger = false;
			}
				//Post High Score
				//Post Score
				int previousScore = PlayerPrefs.GetInt ("TempDodgerScore");
				previousScore = Mathf.RoundToInt(DistenceCovered)+ previousScore;
				PlayerPrefs.SetInt ("TempDodgerScore",previousScore);
			tempScore = PlayerPrefs.GetInt ("TempDodgerScore");
			if(PlayerPrefs.GetInt ("DodgerLives") <= 0)
				{
				if(PlayerPrefs.GetInt ("DodgerPlayToWinHighScore") < PlayerPrefs.GetInt ("TempDodgerScore"))
					{
					PlayerPrefs.SetInt ("DodgerPlayToWinHighScore", PlayerPrefs.GetInt ("TempDodgerScore"));
					tempScore = PlayerPrefs.GetInt ("TempDodgerScore");
					PlayerPrefs.DeleteKey ("TempDodgerScore");
					}
				PlayerPrefs.DeleteKey ("TempDodgerScore");
				}
				StartCoroutine (GameSaveState.Instance.PostUserStatus ());

		} else
		{
			//Post Score
			if (PlayerPrefs.HasKey ("TotalScore")) {
				int Scores = PlayerPrefs.GetInt ("TotalScore");
				PlayerPrefs.SetInt ("TotalScore", Scores + Mathf.FloorToInt (distanceCovered));
			} else {
				PlayerPrefs.SetInt ("TotalScore", Mathf.FloorToInt (distanceCovered));
			}
			//PostScore
			FacebookManager.Instance.PostScoreOnFacebook (PlayerPrefs.GetInt ("TotalScore"));
		}

		if (HartPlayerRegistration.Isplayforpaid) {
			StartCoroutine (GameSaveState.Instance.GetUserStatus (true));
			FinalScores_DynamicText.gameObject.transform.parent.localPosition = new Vector3 (-72f, -80f, 0f);
			FinalScores_DynamicText.text = tempScore.ToString () + "\n" +PlayerPrefs.GetInt ("DodgerPlayToWinHighScore").ToString () ;
			FinalScores_DynamicText.gameObject.transform.parent.GetComponent <Text>().text = "Score\nHigh Score";
		}

		foreach (var Character in GameObject.FindGameObjectsWithTag ("Player")) {
			Character.SetActive (false);
		}

		if (powerUpController.isPowerUpActive) { // disable active Powerup..
			powerUpController.powerUpGameObject.GetComponent<SpriteRenderer> ().enabled = false;
		}

//		if (powerUpController.UsingBuyPowerUp) {
//			powerUpController.PurchasedPowerUp.GetComponent<SpriteRenderer> ().enabled = false;
//		}
		foreach (var BlastObj in DogerPowerUpController.PowerUpAnimations) { // Disable any Blast animation playing 
			BlastObj.GetComponent<SpriteRenderer> ().enabled = false;
		}

		foreach (GameObject gunsOnTheScreen in RandomInstantiationOfGuns_Dodger.instantiatedGuns_Runtime) {
			gunsOnTheScreen.SetActive (false); //Disable all guns.. 
		}
		foreach (Bullets bulletsOnTheScreen in GameObject.FindObjectsOfType<Bullets>()) {
			Destroy (bulletsOnTheScreen.gameObject); //Disable all Bullets...
		}
		Guns.instaintiatedBulletsOnScreen.Clear ();
		RandomInstantiationOfGuns_Dodger.instantiatedGuns_Runtime.Clear ();

		if(!HartPlayerRegistration.Isplayforpaid)
			GameOverTitle.sprite = GameOverTexture;

		float time = Time.timeScale = 0;
		DogerPlayerController.GamePauseforAds = time;
		// for temp unless client aproovle
		if (HartPlayerRegistration.Isplayforpaid) {
			MenuScreen_Controller.GameId = 3;
			PlayerPrefs.SetInt ("CurruntGameId", MenuScreen_Controller.GameId);
		}
//		GameObject.Find ("MainPlayer").GetComponent<BoxCollider2D> ().enabled = false;
		// if this case comes under the ads 
		if (!HartPlayerRegistration.Isplayforpaid) {
			if (distanceCovered > 25f && distanceCovered < 35f || distanceCovered > 60f && distanceCovered < 70f || distanceCovered > 90f && distanceCovered < 100f ||
			   distanceCovered > 145f && distanceCovered < 160f || distanceCovered > 200f && distanceCovered < 230f || distanceCovered > 280f && distanceCovered < 300f ||
			   distanceCovered > 340f && distanceCovered < 355f || distanceCovered > 385f && distanceCovered < 410f || distanceCovered > 450f && distanceCovered < 470f ||
			   distanceCovered > 500f && distanceCovered < 520f || distanceCovered > 570f && distanceCovered < 500f || distanceCovered > 640f && distanceCovered < 680f ||
			   distanceCovered > 800f && distanceCovered < 900f || distanceCovered > 1000f && distanceCovered < 1100f || distanceCovered > 1200f && distanceCovered < 1300f ||
			   distanceCovered > 1400f && distanceCovered < 1500f) {
				adsSeenInDodger = false;
			} else
				adsSeenInDodger = true;
		}
		return;
	}

	#region ShowAdsForDodgerGame

	public void ShowNormalAdsForDodgerGame ()
	{		
		if (distanceCovered > 25f && distanceCovered < 35f || distanceCovered > 60f && distanceCovered < 70f || distanceCovered > 90f && distanceCovered < 100f ||
		    distanceCovered > 145f && distanceCovered < 160f || distanceCovered > 200f && distanceCovered < 230f || distanceCovered > 280f && distanceCovered < 300f ||
		    distanceCovered > 340f && distanceCovered < 355f || distanceCovered > 385f && distanceCovered < 410f || distanceCovered > 450f && distanceCovered < 470f ||
		    distanceCovered > 500f && distanceCovered < 520f || distanceCovered > 570f && distanceCovered < 500f || distanceCovered > 640f && distanceCovered < 680f ||
		    distanceCovered > 800f && distanceCovered < 900f || distanceCovered > 1000f && distanceCovered < 1100f || distanceCovered > 1200f && distanceCovered < 1300f ||
		    distanceCovered > 1400f && distanceCovered < 1500f) {
			UnityAdsManager.Instance.ShowAds ();
			adsSeenInDodger = true;
		}


	}

	#endregion

	public void ClearDodgerGameScreen ()
	{

		foreach (GameObject gunsOnTheScreen in RandomInstantiationOfGuns_Dodger.instantiatedGuns_Runtime) {
			gunsOnTheScreen.SetActive (false); //Disable all guns.. 
			Destroy (gunsOnTheScreen.gameObject);
		}

		foreach (GameObject bullet in Guns.instaintiatedBulletsOnScreen) {
			Destroy (bullet.gameObject);
		}
		Guns.instaintiatedBulletsOnScreen.Clear ();

		RandomInstantiationOfGuns_Dodger.instantiatedGuns_Runtime.Clear ();
	}

	public void OnReplayButtonClicked ()
	{
		if (!isPaused) {
			if (!adsSeenInDodger) {
				if (!HartPlayerRegistration.Isplayforpaid) {
					ShowNormalAdsForDodgerGame ();
				}
			} else {
				Time.timeScale = 1;
				GameOver_Panel.SetActive (false);
				SceneManager.LoadScene ("03_DogerGamePlay");
				adsSeenInDodger = false;
			}
		} else {
			Time.timeScale = 1;
			GameOver_Panel.SetActive (false);
			SceneManager.LoadScene ("03_DogerGamePlay");
		}
	}

	public void OnHomeButtonClicked ()
	{
		if(HartPlayerRegistration.Isplayforpaid)
		{
				PlayingDodger = true;
				MenuScreen_Controller.CurrntGamePlaying = "Doger";
				Time.timeScale = 1;
				RetryButton.gameObject.SetActive (false);
				HomeButton.GetComponent <RectTransform> ().anchoredPosition = new Vector2 (0, -160);
				// post Dodger Score
			if(	!Levelclear )
			{
				int SubtracLive = 0;
				SubtracLive = PlayerPrefs.GetInt ("DodgerLives");
				SubtracLive = SubtracLive - 1;
				PlayerPrefs.SetInt ("DodgerLives", SubtracLive );
				if (PlayerPrefs.GetInt ("DodgerLives") == 0) {
					IAPManager.PlaysDecreasedFromDodger = false;
				}
			}	
				//Post High Score
				//Post Score
				int previousScore = PlayerPrefs.GetInt ("TempDodgerScore");
				PlayerPrefs.SetInt ("TempDodgerScore",previousScore);
				if(PlayerPrefs.GetInt ("DodgerLives") <= 0)
				{
					if(PlayerPrefs.GetInt ("DodgerPlayToWinHighScore") < PlayerPrefs.GetInt ("TempDodgerScore"))
					{
						PlayerPrefs.SetInt ("DodgerPlayToWinHighScore", PlayerPrefs.GetInt ("TempDodgerScore"));
						PostScores (PlayerPrefs.GetString ("UserEmail"), PlayerPrefs.GetInt ("DodgerPlayToWinHighScore"));
						PlayerPrefs.DeleteKey ("TempDodgerScore");
					}
				}
				StartCoroutine (GameSaveState.Instance.PostUserStatus ());
			GameOver_Panel.SetActive (false);
			SceneManager.LoadScene ("00_MenuScreen");
		}else {
			if (!isPaused) {
				if (!adsSeenInDodger) {
					if (!HartPlayerRegistration.Isplayforpaid) {
						ShowNormalAdsForDodgerGame ();
					}
				} else {		
					Time.timeScale = 1;
					GameOver_Panel.SetActive (false);
					SceneManager.LoadScene ("00_MenuScreen");
					FacebookManager.Instance.LoadFacebookScreen ();
					adsSeenInDodger = false;
				}
			} else {
				Time.timeScale = 1;
				GameOver_Panel.SetActive (false);
				SceneManager.LoadScene ("00_MenuScreen");
			}
		}
	}
	// Dodger Game On Application
	void OnApplicationPause (bool pauseStatus)
	{
		if (!UnityAdsManager.UnityAdsRunning) {
			if (!DogerGameManager.WatchVideo) {
				if (pauseStatus && !isPaused && !GameOver) {
					OnClickPause ();
				}
			}
		}

	}

	void OnApplicationQuit(){
		if (HartPlayerRegistration.Isplayforpaid) {
			PlayingDodger = false;
			MenuScreen_Controller.CurrntGamePlaying = "";
		}
	}

	#endregion


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

	public void InternetCheckConnectionForDodger (string message)
	{
		InternetCkeckPanel.SetActive (true);
		InternetMessage.text = message;
	}

	public void DisableInternetCheck ()
	{
		InternetCkeckPanel.SetActive (false);
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
