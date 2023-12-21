using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Globalization;
using Simple_JSON;
using Facebook.Unity;
using System;

public class MenuScreen_Controller : MonoBehaviour
{
	public static MenuScreen_Controller Instance = null;
	private const string LogoutUrl = "https://www.hartplays.co.uk/api/users/logout";
	public const string GetAllNotification = "https://www.hartplays.co.uk/api/users/get_all_notifications";
	public static string GameSelectionName;
	public Sprite LevelClear_Image;

	public GameObject LevelText_Image;

	public Sprite LevelLock_Imgae;

	public GameObject MenuPanel;
	public GameObject LeaderBoardScreen;
	public GameObject showLeaderBoardButton;
	public GameObject settingsPanel;
	public GameObject WheelOfFortunePanel;
	public static bool LogoutCheck;

	public Animator MainMenuAnimController;

	public Sprite ColorBallActiveSprite;
	public Sprite DogerActiveSprite;
	public Sprite NinjaActiveSprite;

	public Sprite ColorBallDeActiveSprite;
	public Sprite DogerDeActiveSprite;

	public GameObject[] GameSelection_Buttons;
	public GameObject[] LevelsSelection_Buttons;
	public GameObject MusicButton;
	public GameObject SoundButton;

	public Sprite MusicOnImage;
	public Sprite MusicOffImage;
	public Sprite SoundOnImage;
	public Sprite SoundOffImage;
	public GameObject FacebookPanel;
	public GameObject LogOutButton;
	public GameObject PlaysSharePanel;
	public Text PlaysDisplay;
	public Text OtherText;
	public GameObject PlaysObject;
	public GameObject FBObject;
	public GameObject NotificationButton;
	public GameObject PlayerLogin;
	public Text TimerText;

	public DateTime SubsTime;

	[Header ("Play To Win Order Game")]
	public static int GameId = 1;

	[Header ("Play To Win New Objects")]
	public GameObject LiveandPowerPurchaseScreen;
	public GameObject ColorBallLiveScreen;
	public GameObject DodgerLiveScreen;

	[Header ("Live Purchase Screen Color")]
	public GameObject ColorSkipButton;
	public GameObject ColorNextButton;
	[Header ("Live Purchase Screen Dodger")]
	public GameObject DogerSkipButton;
	public GameObject DogerNextButton;
	public GameObject MoreGame;

	public static string CurrntGamePlaying;

	void Awake ()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
			if (Instance == null) {
				Instance = this;
			} else if (Instance != null) {
				Destroy (this.gameObject);
			}
//		PlayerPrefs.DeleteAll ();
		Screen.sleepTimeout = 0;
	}

	public void OnEnable ()
	{	
		LogoutCheck = false;
		if (HartPlayerRegistration.Isplayforpaid) {
//			WheelOfFortunePanel.SetActive (true);
			CurruntGameState ();
		}
		else
			WheelOfFortunePanel.SetActive (false);
//		SoundButton.GetComponent<Image> ().sprite = BackgroundMusicManger.instance.isSfxOn ? SoundOnImage : SoundOffImage;
//		MusicButton.GetComponent<Image> ().sprite = BackgroundMusicManger.instance.isMusicOn ? MusicOnImage : MusicOffImage;

		SetScreenOrientation ();
//		PlayerPrefs.SetInt ("LevelsUnlockForColorBallGame", 20);//To be deleted after testing	
		print ("Current Level--" + PlayerPrefs.GetInt ("LevelsUnlockForColorBallGame"));
		SetLevelButtons ();
		showLeaderBoardButton.SetActive (HartPlayerRegistration.Isplayforpaid && PlayerPrefs.GetInt ("CompetitionGoingon") == 1);
		if (PlayerPrefs.HasKey ("UserEmail") || PlayerPrefs.HasKey ("UserName")) {
			if (HartPlayerRegistration.Isplayforpaid) {
				LogOutButton.SetActive (true);
				TimerText.gameObject.SetActive (true);
			} else {
				LogOutButton.SetActive (false);
				PlaysSharePanel.SetActive (false);

			}

		} else {
			LogOutButton.SetActive (false);
			TimerText.gameObject.SetActive (false);
		}			
			
	}

	void Start ()
	{		
		HartPlayerRegistration.Instance.EmptyAllFeilds ();
		FacebookPanel.transform.localScale = Vector3.zero;
		PlaysUpdate ();
		if (HartPlayerRegistration.Isplayforpaid) {
			PlaysObject.SetActive (true);
			FBObject.SetActive (false);
			MoreGame.SetActive (false	);
			NotificationButton.SetActive (true);
			PlayerLogin.SetActive (true);
			PlayerLogin.transform.GetChild (1).GetComponent<Text> ().text = PlayerPrefs.GetString ("UserName");
			GetAllNotifiationCount ();
		} else {
			PlaysObject.SetActive (false);
			FBObject.SetActive (false);
			MoreGame.SetActive (true);
			NotificationButton.SetActive (false);
			PlayerLogin.SetActive (false);
		}
	}

	void Update()
	{
		if(HartPlayerRegistration.Isplayforpaid)
		{
			var dd = CompetitionController.EndTime - DateTime.UtcNow;
			if(dd.Days < 1)
			{
				TimerText.text = string.Format ("{0:D2}:{1:D2}:{2:D2}", dd.Hours, dd.Minutes, dd.Seconds);
			} else {
				if(dd.Days>=2)
					TimerText.text = string.Format ("{0:00}-{1:00}:{2:00}:{3:00}",dd.Days+" DAYS",  dd.Hours, dd.Minutes, dd.Seconds);
				else
					TimerText.text = string.Format ("{0:00}-{1:00}:{2:00}:{3:00}",dd.Days+" DAY",  dd.Hours, dd.Minutes, dd.Seconds);	
			}
		}
	}

	void  OnApplicationQuit ()
	{
		if (!LogoutCheck) {
			HartPlayerRegistration.UserLogoutORQuit = 1;
			PlayerPrefs.SetInt ("UserLogoutORQuit", HartPlayerRegistration.UserLogoutORQuit);
			print (HartPlayerRegistration.UserLogoutORQuit);
		}

	}

	public void ShowSearchPanel()
	{
		print (PlayerPrefs.GetInt ("GamePlaysCount").ToString () );
		if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "10_Plays_Subscription" || PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "20_Plays_Subscription" ||
		    PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "30_Plays_Subscription") {
			if (PlayerPrefs.GetInt ("GamePlaysCount") > 0) {
				PlaysSharePanel.SetActive (true);
				PushManager.Instance.PlayerSendAndSearchPanel.SetActive (true);
				PushManager.Instance.PlaysSendPanel.SetActive (false);
				PushManager.Instance.NotificationPanel.SetActive (false);
				PushManager.Instance.SearchText = "";
				PushManager.Instance.inputText.text = "";
				PushManager.Instance.OverlayObj.SetActive (true);
			} else {
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "You don't have plays to share";

			}
		}else
		{
			HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
			HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
			HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
			HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
			HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "You have subscription plan of 24 hours. You are not allowd to share  ";
		}
	}

	public void BackFromSearchPanel()
	{
		PlaysSharePanel.SetActive (false);
	}

	void CurruntGameState ()
	{
		if (HartPlayerRegistration.Isplayforpaid) {
//			if (GameId == 1) {
//				GameSelection_Buttons [0].GetComponent<Button> ().interactable = true;
//				GameSelection_Buttons [1].GetComponent<Button> ().interactable = true;
//			} else {
//				GameSelection_Buttons [0].GetComponent<Button> ().interactable = false;
//			}
//
//			if (GameId == 2) {
//				GameSelection_Buttons [1].GetComponent<Button> ().interactable = true;
//			} else {
//				GameSelection_Buttons [1].GetComponent<Button> ().interactable = false;
//			}
//
//			if (GameId == 3) {
//				GameSelection_Buttons [2].GetComponent<Button> ().interactable = true;
//			} else {
//				GameSelection_Buttons [2].GetComponent<Button> ().interactable = false;
//			}

			GameSelection_Buttons [0].GetComponent<Button> ().interactable = true;
			GameSelection_Buttons [1].GetComponent<Button> ().interactable = true;
			PlayerPrefs.SetInt ("CurruntGameId", GameId);
		}	
	}

	void SetLevelButtons ()
	{
		// to be deleted
//		PlayerPrefs.SetInt ("LevelsUnlockForColorBallGame", 20);	
		//
		for (int i = 0; i < 20; i++) {		
			LevelsSelection_Buttons [i].transform.GetChild (0).gameObject.SetActive (false);
			LevelsSelection_Buttons [i].GetComponent<Image> ().sprite = LevelLock_Imgae;
		}
		// Free to play

		if (!HartPlayerRegistration.Isplayforpaid) {
			if (!PlayerPrefs.HasKey ("LevelsUnlockForColorBallGame")) {
				PlayerPrefs.SetInt ("LevelsUnlockForColorBallGame", 1);	
			}			

			for (int i = 0; i < PlayerPrefs.GetInt ("LevelsUnlockForColorBallGame"); i++) {
				LevelsSelection_Buttons [i].transform.GetChild (0).gameObject.SetActive (true);
				LevelsSelection_Buttons [i].GetComponent<Image> ().sprite = LevelClear_Image;
			}
		}

		// Pay to Play
		if (HartPlayerRegistration.Isplayforpaid) {
			print ("In Play To Paid");
			if (!PlayerPrefs.HasKey ("ColorBallLevelLockedPaid")) {
				PlayerPrefs.SetInt ("ColorBallLevelLockedPaid", 1);	
			}

			for (int i = 0; i < PlayerPrefs.GetInt ("ColorBallLevelLockedPaid"); i++) {
				LevelsSelection_Buttons [i].transform.GetChild (0).gameObject.SetActive (true);
				LevelsSelection_Buttons [i].GetComponent<Image> ().sprite = LevelClear_Image;
			}
			if (PlayerPrefs.GetInt ("ColorBallLevelLockedPaid") >= 1) {
				LockPreviousLevel ();
			}
		}
	}

	public void LockPreviousLevel ()
	{
		for (int i = 0; i < PlayerPrefs.GetInt ("ColorBallLevelLockedPaid") - 1; i++) {		
			LevelsSelection_Buttons [i].transform.GetChild (0).gameObject.SetActive (false);
			LevelsSelection_Buttons [i].GetComponent<Image> ().sprite = LevelLock_Imgae;
			print ("Current" + PlayerPrefs.GetInt ("ColorBallLevelLockedPaid"));
		}
	}

	public IEnumerator ButtonSelect(GameObject gg, Sprite img)
	{
		yield return new WaitForSeconds (1.1f);
		if(GameSelectionName == "ColorGame")
			gg.GetComponent <Image> ().sprite = img;
		else if(GameSelectionName == "DodgerGame")
			gg.GetComponent <Image> ().sprite = img;
	}

	public void ActivateLevelSelectionScreen (GameObject Go)
	{

		if(PlayerPrefs.GetInt ("ColorBallLives")>0)
		{
			MenuScreen_Controller.Instance.ColorNextButton.SetActive (true);
			MenuScreen_Controller.Instance.ColorSkipButton.SetActive (false);
		}else
		{
			MenuScreen_Controller.Instance.ColorNextButton.SetActive (false);
			MenuScreen_Controller.Instance.ColorSkipButton.SetActive (true);
		}

		if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "10_Plays_Subscription" || PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "20_Plays_Subscription" ||
			PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "30_Plays_Subscription") {
			if (HartPlayerRegistration.Isplayforpaid && HartPlays_PlaytoWin_Subscribe.PlaysCount <= 0)
				return;
		}else if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "24Hours_Plays_Subscription") {
			if (HartPlayerRegistration.Isplayforpaid && HartPlays_PlaytoWin_Subscribe.PlaysHours < 0)
				return;
		}else if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "7Days_Plyas_Subscription") {
			if (HartPlayerRegistration.Isplayforpaid && HartPlays_PlaytoWin_Subscribe.PlaysDays < 0)
				return;
		}
		
//		var ImageComponenet = Go.GetComponent <Image> ();
//		var Temp = ImageComponenet.sprite;
//		ImageComponenet.sprite = ColorBallActiveSprite;
//		ColorBallActiveSprite = Temp;
		GameSelectionName = "ColorGame";
//		StartCoroutine (ButtonSelect(Go,Temp ));
		if (HartPlayerRegistration.Isplayforpaid) {
			StartCoroutine (GameSaveState.Instance.GetUserStatus (true));
			if (MenuScreen_Controller.GameSelectionName == "ColorGame") {
				if (PlayerPrefs.GetInt ("ColorBallLives") < 1 && PlayerPrefs.GetInt ("GamePlaysCount")>0) {
					if (HartPlays_PlaytoWin_Subscribe.PlaysCount >= 1  && !IAPManager.PlaysDecreasedFromColorBall) {
						HartPlays_PlaytoWin_Subscribe.PlaysCount = HartPlays_PlaytoWin_Subscribe.PlaysCount -1;
						PlayerPrefs.SetInt ("GamePlaysCount", HartPlays_PlaytoWin_Subscribe.PlaysCount);
						IAPManager.PlaysDecreasedFromColorBall = true;
						StartCoroutine (GameSaveState.Instance.PostUserStatus ());
					}
				}
			}
			// For Plays
			if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "10_Plays_Subscription" || PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "20_Plays_Subscription" ||
			   PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "30_Plays_Subscription") {
				if (PlayerPrefs.GetInt ("ColorBallLives") <= 0 && HartPlays_PlaytoWin_Subscribe.PlaysCount > 0) {
					LiveandPowerPurchaseScreen.SetActive (true);
					ColorBallLiveScreen.SetActive (true);
					DodgerLiveScreen.SetActive (false);
				} else {
					if (PlayerPrefs.GetInt ("ColorBallLives") > 0)
						ShowWheelOfFortutne ();
				}
			}
			// For Hours Plays
			if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "24Hours_Plays_Subscription") {
				SubsTime =HartPlayerRegistration.registrationTime.AddHours (24);
				if(SubsTime> DateTime.UtcNow)
				{
					LiveandPowerPurchaseScreen.SetActive (true);
					ColorBallLiveScreen.SetActive (true);
					DodgerLiveScreen.SetActive (false);
				}else
				{
					ShowWheelOfFortutne ();
				}

			}
			//For Days Plays
			if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "7Days_Plyas_Subscription") {
				SubsTime =HartPlayerRegistration.registrationTime.AddHours (24);
				if(SubsTime> DateTime.UtcNow)
				{
					LiveandPowerPurchaseScreen.SetActive (true);
					ColorBallLiveScreen.SetActive (true);
					DodgerLiveScreen.SetActive (false);
				}else
				{
					ShowWheelOfFortutne ();
				}
			}

		} else {
			StartCoroutine (AfterWait ());
		}
//		LevelSelectionAnimController.SetBool ("ShowLevels", true);
//
//		LevelText_Image.SetActive(true);
//		LevelBackGround_Transparent.SetActive (true);
//		backButton_LevelSelection.SetActive (true);
//
//		for (int i = 0; i < 3; i++) 
//		{
//			GameSelection_Buttons [i].SetActive (false);
//		}
//		for (int i = 0; i < 20; i++) 
//		{
//			LevelsSelection_Buttons [i].SetActive (true);
//		}
//		return;
	}

	public void PlaysUpdate()
	{
		//GetNotification Count

		if (MenuScreen_Controller.GameSelectionName == "ColorGame") {
			if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "10_Plays_Subscription" || PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "20_Plays_Subscription" ||
			   PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "30_Plays_Subscription") {
				if (PlayerPrefs.GetInt ("ColorBallLives") < 1 && PlayerPrefs.GetInt ("GamePlaysCount") > 0) {
					if (HartPlays_PlaytoWin_Subscribe.PlaysCount >= 1 && !IAPManager.PlaysDecreasedFromColorBall) {
						HartPlays_PlaytoWin_Subscribe.PlaysCount = HartPlays_PlaytoWin_Subscribe.PlaysCount - 1;
						PlayerPrefs.SetInt ("GamePlaysCount", HartPlays_PlaytoWin_Subscribe.PlaysCount);
						IAPManager.PlaysDecreasedFromColorBall = true;
						StartCoroutine (GameSaveState.Instance.PostUserStatus ());
					}
				}
			}else if(PlayerPrefs.GetString ("PlayToWinSubscriptionType")== "24Hours_Plays_Subscription")
			{
				SubsTime =HartPlayerRegistration.registrationTime.AddHours (24);
				if(SubsTime> DateTime.UtcNow)
				{
					var TimeinHr = SubsTime - DateTime.UtcNow;
					HartPlays_PlaytoWin_Subscribe.PlaysHours = TimeinHr.Hours;
					StartCoroutine (GameSaveState.Instance.PostUserStatus ());
				}
						
			}else if(PlayerPrefs.GetString ("PlayToWinSubscriptionType")=="7Days_Plyas_Subscription")
			{
				SubsTime =HartPlayerRegistration.registrationTime.AddDays (7);
				if(SubsTime> DateTime.UtcNow)
				{
					var TimeinHr = SubsTime - DateTime.UtcNow;
					HartPlays_PlaytoWin_Subscribe.PlaysDays = TimeinHr.Days;
					StartCoroutine (GameSaveState.Instance.PostUserStatus ());
				}					

			}

		}else if(MenuScreen_Controller.GameSelectionName == "DodgerGame")
		{
			if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "10_Plays_Subscription" || PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "20_Plays_Subscription" ||
			   PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "30_Plays_Subscription") {
				if (PlayerPrefs.GetInt ("DodgerLives") < 1 && PlayerPrefs.GetInt ("GamePlaysCount") > 0) {
					if (HartPlays_PlaytoWin_Subscribe.PlaysCount >= 1 && !IAPManager.PlaysDecreasedFromDodger) {
						HartPlays_PlaytoWin_Subscribe.PlaysCount = HartPlays_PlaytoWin_Subscribe.PlaysCount - 1;
						PlayerPrefs.SetInt ("GamePlaysCount", HartPlays_PlaytoWin_Subscribe.PlaysCount);
						IAPManager.PlaysDecreasedFromDodger = true;
						StartCoroutine (GameSaveState.Instance.PostUserStatus ());
					}
				}
			}
			else if(PlayerPrefs.GetString ("PlayToWinSubscriptionType")== "24Hours_Plays_Subscription")
			{
				SubsTime =HartPlayerRegistration.registrationTime.AddHours (24);
				if(SubsTime> DateTime.UtcNow)
				{
					var TimeinHr = SubsTime - DateTime.UtcNow;
					HartPlays_PlaytoWin_Subscribe.PlaysHours = TimeinHr.Hours;
					StartCoroutine (GameSaveState.Instance.PostUserStatus ());
				}

			}else if(PlayerPrefs.GetString ("PlayToWinSubscriptionType")=="7Days_Plyas_Subscription")
			{
				SubsTime =HartPlayerRegistration.registrationTime.AddDays (7);
				if(SubsTime> DateTime.UtcNow)
				{
					var TimeinHr = SubsTime - DateTime.UtcNow;
					HartPlays_PlaytoWin_Subscribe.PlaysDays = TimeinHr.Days;
					StartCoroutine (GameSaveState.Instance.PostUserStatus ());
				}				

			}
		}
		
		if(PlayerPrefs.GetString ("PlayToWinSubscriptionType")== "10_Plays_Subscription" || PlayerPrefs.GetString ("PlayToWinSubscriptionType")== "20_Plays_Subscription" ||
			PlayerPrefs.GetString ("PlayToWinSubscriptionType")=="30_Plays_Subscription")
		{
			HartPlays_PlaytoWin_Subscribe.PlaysCount = PlayerPrefs.GetInt ("GamePlaysCount");
			PlaysDisplay.text = HartPlays_PlaytoWin_Subscribe.PlaysCount.ToString ();
			OtherText.text = "Send Plays";
		}
		else if(PlayerPrefs.GetString ("PlayToWinSubscriptionType")== "24Hours_Plays_Subscription")
		{
			
			DateTime SubsTime =HartPlayerRegistration.registrationTime.AddHours (24);
			if(SubsTime> DateTime.UtcNow.AddHours (1))
			{


				var TimeinHr = SubsTime - DateTime.UtcNow;
				HartPlays_PlaytoWin_Subscribe.PlaysHours = TimeinHr.Hours;
				PlaysDisplay.text = HartPlays_PlaytoWin_Subscribe.PlaysHours.ToString ();
				OtherText.text = " Hours";

			}
		}else if(PlayerPrefs.GetString ("PlayToWinSubscriptionType").Contains ("7Days"))
		{
			PlaysDisplay.text = HartPlays_PlaytoWin_Subscribe.PlaysDays.ToString ();
			OtherText.text = " Days";
		}

		if (HartPlayerRegistration.Isplayforpaid) {
			HartPlayerRegistration.Instance.PaymentPanel.SetActive (false);
			HartPlayerRegistration.Instance.RegistrationPanel.SetActive (false);
			HartPlayerRegistration.Instance.FreeOrPaidPanel.SetActive (false);
		} else {
			HartPlayerRegistration.Instance.PaymentPanel.SetActive (false);
			HartPlayerRegistration.Instance.RegistrationPanel.SetActive (false);
			HartPlayerRegistration.Instance.FreeOrPaidPanel.SetActive (false);
		}


		if( MenuScreen_Controller.CurrntGamePlaying == "ColorBall" )
		{
			int ff = PlayerPrefs.GetInt ("ColorBallLives");
			if(MainGameController_Touch.PlayingColorBall && PlayerPrefs.GetInt ("ColorBallLives") > 0)
			{				
				ActivateLevelSelectionScreen (null);
			}
				
		}else if( MenuScreen_Controller.CurrntGamePlaying == "Doger" )
		{
			if(DogerGameManager.PlayingDodger&& PlayerPrefs.GetInt ("DodgerLives") > 0)
			{				
				OnClickDodgerGameButton (null);
			}

		}
		return;
	}

	public void FreeLivesColorBall()
	{
		PlayerPrefs.SetInt ("ColorBallLives", 1);
		IAPManager.PlaysDecreasedFromColorBall = true;
		StartCoroutine (GameSaveState.Instance.PostUserStatus ());
		Invoke ("ShowWheelOfFortutne",1f);
	}

	public void FreeLivesDodger()
	{
		PlayerPrefs.SetInt ("DodgerLives", 1);
		IAPManager.PlaysDecreasedFromDodger = true;
		StartCoroutine (GameSaveState.Instance.PostUserStatus ());
		Invoke ("ShowWheelOfFortutne",1f);
	}


	public void ShowWheelOfFortutne ()
	{
		
		print (PlayerPrefs.GetInt ("DodgerLives"));
		print (PlayerPrefs.GetInt ("ColorBallLives"));
		if (GameSelectionName == "DodgerGame" && PlayerPrefs.GetInt ("DodgerLives") > 0) {
			StartCoroutine (GameSaveState.Instance.PostUserStatus ());
			WheelOfFortunePanel.SetActive (true);
		}else if(GameSelectionName == "ColorGame"&& PlayerPrefs.GetInt ("ColorBallLives") > 0)
		{
			StartCoroutine (GameSaveState.Instance.PostUserStatus ());
			WheelOfFortunePanel.SetActive (true);
		}

	}

	public void BackFormWheelOfFortutne ()
	{
		WheelOfFortunePanel.SetActive (false);
	}

	public void BackFromLivePurchaseScreenColor (GameObject Go)
	{
		LiveandPowerPurchaseScreen.SetActive (false);
		var ImageComponenet = Go.GetComponent <Image> ().sprite = ColorBallDeActiveSprite;

	}

	public void BackFromLivePurchaseScreenDodger (GameObject Go)
	{
		LiveandPowerPurchaseScreen.SetActive (false);
		var ImageComponenet = Go.GetComponent <Image> ().sprite = DogerDeActiveSprite;

	}
	IEnumerator AfterWait ()
	{
		yield return new WaitForSeconds (0.2f);
		MainMenuAnimController.SetBool ("ShowPanel", true);
	}

	public void LevelSelectionButtonClicked ()
	{
//		Debug.Log ("Button Clicked is ---->>>>" + EventSystem.current.currentSelectedGameObject.name);
		if (EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite == LevelClear_Image) {
			PlayerPrefs.SetString ("LevelSelected", EventSystem.current.currentSelectedGameObject.name);
			print ("LevelSelected is -->>" + PlayerPrefs.GetString ("LevelSelected"));
			SceneManager.LoadScene ("01_MainGamePlay");
		}
	}
	public void LevelSelectionButtonClickedFromWheelOfFortutne ( int Level)
	{
		PlayerPrefs.SetString ("LevelSelected", "Level_"+Level.ToString ());
		print ("LevelSelected is -->>" + PlayerPrefs.GetString ("LevelSelected"));
		//Substract plays And Lives
	

		SceneManager.LoadScene ("01_MainGamePlay");
//		WheelofFortune.SelectLevel = false;
	}

	public void OnClickDodgerGameButton (GameObject Go)
	{

		if(PlayerPrefs.GetInt ("DodgerLives")>0)
		{
			MenuScreen_Controller.Instance.DogerNextButton.SetActive (true);
			MenuScreen_Controller.Instance.DogerSkipButton.SetActive (false);
		}else
		{
			MenuScreen_Controller.Instance.DogerNextButton.SetActive (false);
			MenuScreen_Controller.Instance.DogerSkipButton.SetActive (true);
		}
		if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "10_Plays_Subscription" || PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "20_Plays_Subscription" ||
		   PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "30_Plays_Subscription") {
			if (HartPlayerRegistration.Isplayforpaid && HartPlays_PlaytoWin_Subscribe.PlaysCount <= 0)
				return;
		}else if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "24Hours_Plays_Subscription") {
		 if (HartPlayerRegistration.Isplayforpaid && HartPlays_PlaytoWin_Subscribe.PlaysHours <= 0)
			return;
		}else if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "7Days_Plyas_Subscription") {
			if (HartPlayerRegistration.Isplayforpaid && HartPlays_PlaytoWin_Subscribe.PlaysDays <= 0)
				return;
		}
//		Go.GetComponent <Image> ().sprite = DogerActiveSprite;
//		var ImageComponenet = Go.GetComponent <Image> ();
//		var Temp = ImageComponenet.sprite;
//		ImageComponenet.sprite = DogerActiveSprite;
		GameSelectionName = "DodgerGame";
//		StartCoroutine (ButtonSelect(Go,Temp ));
		if(HartPlayerRegistration.Isplayforpaid){
				StartCoroutine (GameSaveState.Instance.GetUserStatus (true));
				 if(MenuScreen_Controller.GameSelectionName == "DodgerGame")
				{
				if (PlayerPrefs.GetInt ("DodgerLives") < 1 && PlayerPrefs.GetInt ("GamePlaysCount")>0 ) {
					if (HartPlays_PlaytoWin_Subscribe.PlaysCount >= 1 && !IAPManager.PlaysDecreasedFromDodger) {
							HartPlays_PlaytoWin_Subscribe.PlaysCount = HartPlays_PlaytoWin_Subscribe.PlaysCount-1;
							PlayerPrefs.SetInt ("GamePlaysCount", HartPlays_PlaytoWin_Subscribe.PlaysCount);
						IAPManager.PlaysDecreasedFromDodger = true;
							StartCoroutine (GameSaveState.Instance.PostUserStatus ());
						}
					}
				}
			if (PlayerPrefs.GetInt ("DodgerLives") <= 0 && HartPlays_PlaytoWin_Subscribe.PlaysCount > 0) {
				LiveandPowerPurchaseScreen.SetActive (true);
				ColorBallLiveScreen.SetActive (false);
				DodgerLiveScreen.SetActive (true);
			} else {
				if(HartPlays_PlaytoWin_Subscribe.PlaysCount > 0)
					ShowWheelOfFortutne ();
				else
				{
					print ("Your Subscription is over");
				}

			}
			// For Plays
			if(PlayerPrefs.GetString ("PlayToWinSubscriptionType")== "10_Plays_Subscription" || PlayerPrefs.GetString ("PlayToWinSubscriptionType")== "20_Plays_Subscription" ||
				PlayerPrefs.GetString ("PlayToWinSubscriptionType")=="30_Plays_Subscription")
			{
				if (PlayerPrefs.GetInt ("DodgerLives") <= 0 && HartPlays_PlaytoWin_Subscribe.PlaysCount > 0) {
					LiveandPowerPurchaseScreen.SetActive (true);
					ColorBallLiveScreen.SetActive (false);
					DodgerLiveScreen.SetActive (true);
				} else {
					if (HartPlays_PlaytoWin_Subscribe.PlaysCount > 0)
						ShowWheelOfFortutne ();
				}
			}
			// For Hours Plays
			if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "24Hours_Plays_Subscription") {
				
				SubsTime =HartPlayerRegistration.registrationTime.AddHours (24);
				if(SubsTime> DateTime.UtcNow)
				{
					LiveandPowerPurchaseScreen.SetActive (true);
					ColorBallLiveScreen.SetActive (false);
					DodgerLiveScreen.SetActive (true);
				}else
				{
					ShowWheelOfFortutne ();
				}
			}
			//For Days Plays
			if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "7Days_Plyas_Subscription") {
				SubsTime =HartPlayerRegistration.registrationTime.AddHours (24);
				if(SubsTime> DateTime.UtcNow)
				{
					LiveandPowerPurchaseScreen.SetActive (true);
					ColorBallLiveScreen.SetActive (false);
					DodgerLiveScreen.SetActive (true);
				}else
				{
					ShowWheelOfFortutne ();
				}
			}

		} else {
			StartCoroutine (LoadSceneAfterWait ("03_DogerGamePlay"));
		}
	}

	public void OnClickNinjaGameButton (GameObject Go)
	{
		
		Go.GetComponent <Image> ().sprite = NinjaActiveSprite;
					
		StartCoroutine (LoadSceneAfterWait_NinjaFootie ("05_NinjaLevelSelectionScene"));	
	}

	public void BackButtonClicked_LevelSelectionScreen (GameObject Go)
	{
//		var ImageComponenet = Go.GetComponent <Image> ();
//		var Temp = ImageComponenet.sprite;
//		ImageComponenet.sprite = ColorBallActiveSprite;
//		ColorBallActiveSprite = Temp;
		MainMenuAnimController.SetBool ("ShowPanel", false);
//		LevelSelectionAnimController.SetBool ("ShowLevels", false);
//		LevelText_Image.SetActive(false);
//		LevelBackGround_Transparent.SetActive (false);
//		backButton_LevelSelection.SetActive (false);
//
//		for (int i = 0; i < 3; i++) 
//		{
//			GameSelection_Buttons [i].SetActive (true);
//		}
//		for (int i = 0; i < 20; i++) 
//		{
//			LevelsSelection_Buttons [i].SetActive (false);
//		}
	}

	public void GameSelectionButtonSetActiveFalse ()
	{
		for (int i = 0; i < 3; i++) {
			GameSelection_Buttons [i].SetActive (false);
		}
	}

	public void GameSelectionButtonSetActiveTrue ()
	{
		for (int i = 0; i < 3; i++) {
			GameSelection_Buttons [i].SetActive (true);
		}
	}

	public void BackToPlayToWin ()
	{
		SceneManager.LoadScene ("02_LoginScene");
		MenuScreen_Controller.CurrntGamePlaying = "";
		HartPlayerRegistration.Instance.FreeOrPaidPanel.SetActive (true);
	}

	public void ShowLeaderBoardScreen ()
	{
		LeaderBoardScreen.SetActive (true);
	}

	public void BackFromLeaderBoard ()
	{
		LeaderBoardScreen.SetActive (false);
	}

	public void ResetAll ()
	{
		print ("ALL PlayerPrefs deleted");
//		PlayerPrefs.DeleteAll ();
	}

	public void LogOut ()
	{
		if (PlayerPrefs.HasKey ("UserEmail") || PlayerPrefs.HasKey ("UserName")) {
			string email = PlayerPrefs.GetString ("UserEmail");
			//Level basis on Game id
			int currentLevel = 0;
			if (PlayerPrefs.GetInt ("CurruntGameId") == 1) {
				currentLevel = PlayerPrefs.GetInt ("ColorBallLevelLockedPaid");	
			} else if (PlayerPrefs.GetInt ("CurruntGameId") == 3) {
				currentLevel = PlayerPrefs.GetInt ("NinjaLevelUnlockedPaid");	
			}

			int IsPaid = PlayerPrefs.GetInt ("IsPaidForCompetition");
			///TODO
			int CurrentGameId = PlayerPrefs.GetInt ("CurruntGameId");
			int CurrentGameLifeForDodger = PlayerPrefs.GetInt ("DodgerHealth");
			int[] ColorBallPowers = {
				PlayerPrefs.GetInt ("BombCountForPaid"),
				PlayerPrefs.GetInt ("FreezeCountForPaid"),
				PlayerPrefs.GetInt ("ReminderCountForPaid")
			};

			int[] DodgerPowers = {
				PlayerPrefs.GetInt ("DodgerBombCountForPaid"),
				PlayerPrefs.GetInt ("DodgerInvincibleCountForPaid"),
				PlayerPrefs.GetInt ("DodgerSlowCountForPaid"),
				PlayerPrefs.GetInt ("DodgerMergeCountForPaid")
			};
			int[] NinjaFootiePower = {
				PlayerPrefs.GetInt ("NinjaGoldenStarCountForPaid"),
				PlayerPrefs.GetInt ("NinjaRhombusCountForPaid"),
				PlayerPrefs.GetInt ("NinjaSquareCountForPaid"),
				PlayerPrefs.GetInt ("NinjaWhiteStarCountForPaid")			
			};

			Logout ();
			HartPlayerRegistration.UserLogoutORQuit = 2;
			PlayerPrefs.SetInt ("UserLogoutORQuit", HartPlayerRegistration.UserLogoutORQuit);
		}
	}

	void Logout ()
	{	
		var encoding = new System.Text.UTF8Encoding ();

		Dictionary<string,string> postHeader = new Dictionary<string,string> ();

//		UserInfo Info = new UserInfo ();
//		{
//			"email":"aditi.goel@ignivasolutions.com",
//			"level_no":"3",
//			"is_paid":"1",
//			"current_game_id":"1",
//			"current_lifes":"2",
//			"ColorBall_PowerUps":[8,3,5],
//			"Dodger_PowerUps":[4,3,6],
//			"NinjaFootie_PowerUps":[3,7,1]
//		}
//
//		Info.email = _email;
//		Info.level_no = _Currentlevel; 
//		Info.password = _password;
//		///TODO:
//		Info.isPaid = _IsPaidStatus;
//		Info.CurrentGameId = _CurrentGameId;
//		Info.CurrentDodgerGameLife = _DodgerCurrentLife;
//		Info.ColorBallGameBombPowers = _ColorBallPowerUps [0]; //Bomb
//		Info.ColorBallGameFreezPowers = _ColorBallPowerUps [1]; // Freez
//		Info.ColorBallGameReminderPowers = _ColorBallPowerUps [2]; // Reminder
//
//		Info.DodgerGameBombPowers = _DodgerPowerUps [0];
//		Info.DodgerGameInvinciblePowers = _DodgerPowerUps [1];
//		Info.DodgerGameMergePowers = _DodgerPowerUps [2];
//		Info.DodgerGameSlowDownPowers = _DodgerPowerUps [3];
//
//		Info.NinjaFootieGoldStarPowers = _NinjaFootiePowerUps [0];
//		Info.NinjaFootieRhombusPowers = _NinjaFootiePowerUps [1];
//		Info.NinjaFootieSquarePowers = _NinjaFootiePowerUps [2];
//		Info.NinjaFootieWhiteStarPowers = _NinjaFootiePowerUps [3];
		//		string json = JsonUtility.ToJson (Info);

	
		var JsonsElement = new JSONClass ();
		JsonsElement ["email"] = PlayerPrefs.GetString ("UserEmail");
		JsonsElement ["level_no"] = 2.ToString ();
//		JsonsElement ["is_paid"] = _IsPaidStatus.ToString ();
//		JsonsElement ["current_game_id"] = _CurrentGameId.ToString ();
//		JsonsElement ["current_lifes"] = _DodgerCurrentLife.ToString ();
//	
//		var _jsonArrayColorBall = new JSONArray ();
//		_jsonArrayColorBall.Add (_ColorBallPowerUps [0].ToString ());
//		_jsonArrayColorBall.Add (_ColorBallPowerUps [1].ToString ());
//		_jsonArrayColorBall.Add (_ColorBallPowerUps [2].ToString ());
//		JsonsElement ["ColorBall_PowerUps"] = _jsonArrayColorBall;
//	
//		var _jsonArrayDodger = new JSONArray ();
//		_jsonArrayDodger.Add (_DodgerPowerUps [0].ToString ());
//		_jsonArrayDodger.Add (_DodgerPowerUps [1].ToString ());
//		_jsonArrayDodger.Add (_DodgerPowerUps [2].ToString ());
//		_jsonArrayDodger.Add (_DodgerPowerUps [3].ToString ());
//		JsonsElement ["Dodger_PowerUps"] = _jsonArrayDodger;
//
//		var _jsonArrayNinja = new JSONArray ();
//		_jsonArrayNinja.Add (_NinjaFootiePowerUps [0].ToString ());
//		_jsonArrayNinja.Add (_NinjaFootiePowerUps [1].ToString ());
//		_jsonArrayNinja.Add (_NinjaFootiePowerUps [2].ToString ());
//		_jsonArrayNinja.Add (_NinjaFootiePowerUps [3].ToString ());
//
//		JsonsElement ["NinjaFootie_PowerUps"] = _jsonArrayNinja;
		Debug.Log (JsonsElement.ToString ());
		postHeader.Add ("Content-Type", "application/json");
		postHeader.Add ("Content-Length", JsonsElement.Count.ToString ());	

		WWW www = new WWW (LogoutUrl, encoding.GetBytes (JsonsElement.ToString ()), postHeader);

		StartCoroutine (WaitForLogOutRequest (www));
	}

	IEnumerator WaitForLogOutRequest (WWW www)
	{
		print ("Waiting For Response for Logout");

		yield return(www);

		if (www.error == null) {
			JSONNode _jsNode = JSON.Parse (www.text);
			print (www.text.ToString ());
			print ("JSON DATA IS -->>" + _jsNode.ToString ());

			if (_jsNode [1].ToString ().Contains ("Invalid")) {
				print (_jsNode ["error"] ["error_msg"].ToString ());
			} else {
				PlayerPrefs.DeleteKey ("UserEmail"); 
				PlayerPrefs.DeleteKey ("UserName");
				PlayerPrefs.DeleteKey ("ColorBallLevelLockedPaid");
				PlayerPrefs.DeleteKey ("IsPaidForCompetition");
				PlayerPrefs.DeleteKey ("CurruntGameId"); 
				PlayerPrefs.DeleteKey ("DodgerHealth");
				PlayerPrefs.DeleteKey ("BombCountForPaid");
				PlayerPrefs.DeleteKey ("FreezeCountForPaid");
				PlayerPrefs.DeleteKey ("ReminderCountForPaid");
				PlayerPrefs.DeleteKey ("DodgerBombCountForPaid");
				PlayerPrefs.DeleteKey ("DodgerInvincibleCountForPaid");
				PlayerPrefs.DeleteKey ("DodgerSlowCountForPaid");
				PlayerPrefs.DeleteKey ("DodgerMergeCountForPaid");
				PlayerPrefs.DeleteKey ("NinjaGoldenStarCountForPaid");
				PlayerPrefs.DeleteKey ("NinjaRhombusCountForPaid");
				PlayerPrefs.DeleteKey ("NinjaSquareCountForPaid");
				PlayerPrefs.DeleteKey ("NinjaWhiteStarCountForPaid");
				PlayerPrefs.DeleteAll ();
				HartPlayerRegistration.Instance.AgeCheck.isOn = false;
				SceneManager.LoadScene ("02_LoginScene");

				GameObject go = GameObject.Find ("IAPManager");
				Destroy (go.GetComponent<CompetitionController> ());
				go.AddComponent<CompetitionController> ();
				HartPlayerRegistration.Instance.FreeOrPaidPanel.SetActive (true);
				///TODO: for LogOut PlayerPref
				/// 
				LogoutCheck = true;
			}
		} else if (www.error != null) {
			print ("SignIN Error" + www.error);
		}
	}

	IEnumerator LoadSceneAfterWait (string SceneName)
	{
		yield return new WaitForSeconds (0.2f);	
//		SetForNinjaScreenOrientation ();
		SceneManager.LoadScene (SceneName);
	}

	IEnumerator LoadSceneAfterWait_NinjaFootie (string SceneName)
	{
		yield return new WaitForSeconds (0.0f);	
		//		SetForNinjaScreenOrientation ();
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;

		Screen.orientation = ScreenOrientation.LandscapeLeft; 
		SceneManager.LoadScene (SceneName);
	}

	void SetScreenOrientation ()
	{
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = true;
		Screen.orientation = ScreenOrientation.Portrait; 

	}

	//	void SetForNinjaScreenOrientation ()
	//	{
	//		Screen.orientation = ScreenOrientation.LandscapeLeft;
	//		Screen.autorotateToLandscapeLeft = true;
	//		Screen.autorotateToLandscapeRight = true;
	//		Screen.autorotateToPortrait = false;
	//		Screen.autorotateToPortraitUpsideDown = false;
	//	}

	public void ShowSettingsScreen ()
	{
		MenuPanel.SetActive (false);
		settingsPanel.SetActive (true);
	}

	public void BackToMenuScreen ()
	{
		MenuPanel.SetActive (true);
		settingsPanel.SetActive (false);
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

	public void OpenCloseFbScreen ()
	{
		if (FacebookPanel.transform.localScale == Vector3.one)
			FacebookPanel.transform.localScale = Vector3.zero;
		else {
			var fbmanager = GameObject.Find ("UnityAdsManager").GetComponent<FacebookManager> ();
			if (FB.IsLoggedIn) {				
				fbmanager.QueryScores ();
				FacebookPanel.transform.localScale = Vector3.one;
			} else {
				fbmanager.FBLogin ();
			}
			
		}

	}

	public void ShareOnFB ()
	{
		var fbmanager = GameObject.Find ("UnityAdsManager").GetComponent<FacebookManager> ();
		fbmanager.FBShare ();
	}


		public void GetAllNotifiationCount ()
		{
	
			HartPlayerRegistration.Instance.WatingPanelShow (true);
			var encoding = new System.Text.UTF8Encoding ();
			Dictionary<string,string> postHeader = new Dictionary<string,string> ();
			var jsonElement = new JSONClass ();
	
			jsonElement["page"] ="1";
			jsonElement["limit"] = "10";
			jsonElement["user_id"] = PlayerPrefs.GetInt ("PlayerId").ToString ();
			postHeader.Add ("Content-Type", "application/json");
			postHeader.Add ("Content-Length", jsonElement.Count.ToString ());
	
			WWW www = new WWW (GetAllNotification, encoding.GetBytes (jsonElement.ToString ()), postHeader);
	
			StartCoroutine (WaitForAllNotificationCount (www));
		}
	
		IEnumerator WaitForAllNotificationCount (WWW www)
		{
			yield return(www);
			print (www.text);
			JSONNode _jsNode = JSON.Parse (www.text);
			if (_jsNode ["status"].ToString ().Contains ("200")) {
				if (_jsNode ["description"].ToString ().Contains ("all notifications")) {
					JSONNode data = _jsNode ["data"];
					HartPlayerRegistration.Instance.WatingPanelShow (false);
					MenuScreen_Controller.Instance.NotificationButton.transform.GetChild (0).GetComponent <Text> ().text = data.Count.ToString () ;
				}
			}
		}

}
