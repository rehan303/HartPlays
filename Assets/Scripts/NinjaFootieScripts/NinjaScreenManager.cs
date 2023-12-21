using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Simple_JSON;
using System.Collections.Generic;

namespace NinjaFootie
{
	public class NinjaScreenManager : MonoBehaviour
	{
		const string updateScoreUrl = "https://www.hartplays.co.uk/api/users/updateScore";
		bool isPaused = false;
		bool isGameOver = false;


		public static float timeOfLevel;
		public float timeLeft = 300.0f;
		public bool stop = true;

		private float minutes;
		static public float seconds;

		public Text Timertext;

		// all for level end screen....
		public GameObject LevelEndPanel;
		public Text LevelEndTittleText;
		public Button NextLevelButton;
		public Button RetryLevelButton;
		public Button HomeButton;
		public Text PlayerScores;
		public Text OpponentScores;
		public Text LevelNumberText;
		public Text GameOverLevelNumberText;

		[Header ("Shop Item Panle")]
		public GameObject ShopPanel;
		public GameObject PowerUpPanel;

		[Header ("Shop Button")]
		public GameObject ShopButton;
		public GameObject BackButtonFromShop;

		// all for paused screen...
		public GameObject PausedPanel;
		public GameObject GameHudPanel;
		AudioSource audioSource;
		public AudioClip LevelClearedAudio;
		public AudioClip LevelFailedAudio;

		[Header ("Check Internet Connectivity")]
		public GameObject InternetCheckPanel;
		public Text InternetMessage;


		[Header ("Music Attribute")]
		public GameObject MusicPanel;
		public GameObject MusicButton;
		public GameObject SoundButton;

		public Sprite MusicOnImage;
		public Sprite MusicOffImage;
		public Sprite SoundOnImage;
		public Sprite SoundOffImage;

		void OnEnable ()
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft; 	// TODO
			LevelEndPanel.SetActive (isGameOver);
			PausedPanel.SetActive (isPaused);
			GameHudPanel.SetActive (!isPaused);
		}

		void Start ()
		{
			audioSource = GetComponent<AudioSource> ();
			startTimer (timeOfLevel);
			GameOverLevelNumberText.text = LevelNumberText.text = GetCurrentLevel ().ToString ();
			isPaused = false;
			isGameOver = false;
		}

		public void startTimer (float from)
		{
			stop = false;
			timeLeft = from;
			Update ();
			StartCoroutine (updateCoroutine ());
		}

		void Update ()
		{
			if (isPaused)
				return;

			timeLeft -= Time.deltaTime;
			minutes = Mathf.Floor (timeLeft / 60);
			seconds = timeLeft % 60;
			if (seconds > 59) {
				seconds = 59;
			}
			//Used when clock showing less than 0 minutes 
			if (minutes < 0) {
				stop = true;
				minutes = 0;
				seconds = 0;
			}
		}

		private IEnumerator updateCoroutine ()
		{
			while (!stop) {
				if (!isPaused) {
					Timertext.text = "Time - " + string.Format ("{0:0}:{1:00}", minutes, seconds);
					yield return new WaitForSeconds (0.2f);

					//Notifying User about 30 second Left in the clock
					if (minutes <= 0 && seconds <= 30) {
						TimerBlinker ();
						yield return new WaitForSeconds (0.1f);
					}
					if (minutes <= 0 && seconds <= 0 && NinjaFootiePlayer.PlayerScore > AI_NinjaFootie.AiScores && !isGameOver) {
						//Winning Condition
						ExecuteGameOver (true);
						UnlockNextLevel ();
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, LevelClearedAudio);

						print ("Winning Condition");
					} else if (minutes <= 0 && seconds <= 0 && NinjaFootiePlayer.PlayerScore <= AI_NinjaFootie.AiScores && !isGameOver) {
						//Looseing Condition
						ExecuteGameOver (false);
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, LevelFailedAudio);
						if (HartPlayerRegistration.Isplayforpaid) {
							MenuScreen_Controller.GameId = 0;
							PlayerPrefs.SetInt ("CurruntGameId", MenuScreen_Controller.GameId);
						}
						print ("Loosing Condition");
					} else {
						//Tied
					}
				} else {
					yield return new WaitUntil (() => !isPaused);
				}
			}
		}

		private void TimerBlinker ()
		{
			Timertext.color = Color.red;
			Timertext.enabled = !Timertext.enabled; 
		}

		public void OnClickPause ()
		{
			isPaused = !isPaused;
				
			PausedPanel.SetActive (isPaused);
			GameHudPanel.SetActive (!isPaused);
			PowerUpPanel.SetActive (!isPaused);

			Time.timeScale = isPaused ? 0 : 1;
		}

		void ExecuteGameOver (bool isLevelCleared)
		{
			isGameOver = true;
			ShowLevelEndScreen (isLevelCleared);
			Time.timeScale = 0;
		}

		public void OpenInAppItemsShop ()
		{
			ShopPanel.SetActive (true);
			if (isPaused) {
				PausedPanel.GetComponent<RectTransform> ().localScale = Vector3.zero;
			} else {
				LevelEndPanel.GetComponent<RectTransform> ().localScale = Vector3.zero;
			}

		}

		public void OnBackFromShop ()
		{
			ShopPanel.SetActive (false);
			if (isPaused) {
				PausedPanel.GetComponent<RectTransform> ().localScale = Vector3.one;
			} else {
				LevelEndPanel.GetComponent<RectTransform> ().localScale = Vector3.one;
			}
		}

		/// <summary>
		/// Unlocks the next level.
		/// </summary>
		void UnlockNextLevel ()
		{
			if (!HartPlayerRegistration.Isplayforpaid) {
				if (PlayerPrefs.GetInt ("NinjaLevelUnlockedFree") < 15 && PlayerPrefs.GetInt ("NinjaLevelUnlockedFree") == GetCurrentLevel ()) {
					PlayerPrefs.SetInt ("NinjaLevelUnlockedFree", GetCurrentLevel () + 1);
					print ("Unlock for free level");
				} 
			}

			if (HartPlayerRegistration.Isplayforpaid) {
				if (PlayerPrefs.GetInt ("NinjaLevelUnlockedPaid") < 15 && PlayerPrefs.GetInt ("NinjaLevelUnlockedPaid") == GetCurrentLevel ()) {
					PlayerPrefs.SetInt ("NinjaLevelUnlockedPaid", GetCurrentLevel () + 1);
					print ("Unlock for Paid level");

					if (PlayerPrefs.HasKey ("TotalScore")) {
						int Scores = PlayerPrefs.GetInt ("TotalScore");
						PlayerPrefs.SetInt ("TotalScore", Scores + NinjaFootiePlayer.PlayerScore); // Player Scores instead of total 
					} else {
						PlayerPrefs.SetInt ("TotalScore", NinjaFootiePlayer.PlayerScore);
					}					
				}
				string email = PlayerPrefs.GetString ("UserEmail");
				PostScores (email, PlayerPrefs.GetInt ("TotalScore"));
			}	
		}

		void PostScores (string _email, int _scores)
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

		public void OnClickNextButton ()
		{
			if (!HartPlayerRegistration.Isplayforpaid) {
				if (PlayerPrefs.GetInt ("NinjaLevelUnlockedFree") <= 15 && GetCurrentLevel () != 15) {
					PlayerPrefs.SetString ("NinjaLevelSelected", (GetCurrentLevel () + 1).ToString ());
					SceneManager.LoadScene ("04_NinjaFootie");
					Time.timeScale = 1;
				} else {
					SceneManager.LoadScene ("05_NinjaLevelSelectionScene");
					Time.timeScale = 1;
				}
			} else {
				if (PlayerPrefs.GetInt ("NinjaLevelUnlockedPaid") <= 15 && GetCurrentLevel () != 15) {
					PlayerPrefs.SetString ("NinjaLevelSelected", (GetCurrentLevel () + 1).ToString ());
					SceneManager.LoadScene ("04_NinjaFootie");
					Time.timeScale = 1;

					// Temp unless client aproovel
					if (PlayerPrefs.GetInt ("NinjaLevelUnlockedPaid") == 15 && GetCurrentLevel () == 15) {
						if (HartPlayerRegistration.Isplayforpaid) {
							MenuScreen_Controller.GameId = 0;
							PlayerPrefs.SetInt ("CurruntGameId", MenuScreen_Controller.GameId);
						}
					}
				} else {
					SceneManager.LoadScene ("05_NinjaLevelSelectionScene");
					Time.timeScale = 1;
				}				
			}
		}

		int GetCurrentLevel ()
		{
			return int.Parse (PlayerPrefs.GetString ("NinjaLevelSelected"));
		}

		void ShowLevelEndScreen (bool isLevelCleared)
		{
			if (isLevelCleared) {
				NextLevelButton.gameObject.SetActive (true);
				LevelEndTittleText.text = "CONGRATULATIONS!";

				if (HartPlayerRegistration.Isplayforpaid) {
					RetryLevelButton.gameObject.SetActive (false);
					NextLevelButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (50, -110);
					HomeButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-50, -110);
				} else {
					NextLevelButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (100f, -110);		
					RetryLevelButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0f, -110);
					HomeButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-100f, -110);
				}
			} else {
				NextLevelButton.gameObject.SetActive (false);
				RetryLevelButton.gameObject.SetActive (true);
				LevelEndTittleText.text = "GAME OVER!";
//				NextLevelButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2(100f,-110);
				RetryLevelButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (50f, -110);
				HomeButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-50f, -110);

			}

			LevelEndPanel.SetActive (true);
			PowerUpPanel.SetActive (!isLevelCleared);
			OpponentScores.text = AI_NinjaFootie.AiScores.ToString ();
			PlayerScores.text = NinjaFootiePlayer.PlayerScore.ToString ();
			// show ads
			if (!HartPlayerRegistration.Isplayforpaid) {
				if (NinjaFootiePlayer.PlayerScore > 0 && NinjaFootiePlayer.PlayerScore < 25 || NinjaFootiePlayer.PlayerScore > 40 && NinjaFootiePlayer.PlayerScore < 60 || NinjaFootiePlayer.PlayerScore > 100 && NinjaFootiePlayer.PlayerScore < 120 || NinjaFootiePlayer.PlayerScore > 160 && NinjaFootiePlayer.PlayerScore < 190) {
					UnityAdsManager.Instance.ShowAds ();
				}
			
				ShowNormalAdsForNinjaGame ();
			}
		}

		#region ShowNormalAdsForNinjaFootieGame

		public void ShowNormalAdsForNinjaGame ()
		{
			var NinjaPlayerScore = NinjaFootiePlayer.PlayerScore;
			var NinjaAIScore = AI_NinjaFootie.AiScores;
			var temp = NinjaPlayerScore - NinjaAIScore;
			print (temp);
			if (NinjaPlayerScore < NinjaAIScore) {
				if (temp > 0 && temp < 30 || temp > 40 && temp < 60 || temp > 90 && temp < 120) {
					UnityAdsManager.Instance.ShowAds ();
				}	
			}
			if (NinjaPlayerScore > NinjaAIScore) {

				if (temp > 0 && temp < 10 || temp > 30 && temp < 45 || temp > 70 && temp < 85) {
					UnityAdsManager.Instance.ShowAds ();
				}	
			}



			
		}

		#endregion

		public void OnReplayButtonClicked ()
		{
			Time.timeScale = 1;
			SceneManager.LoadScene ("04_NinjaFootie");
		}

		public void OnHomeButtonClicked ()
		{
			Time.timeScale = 1;
			SceneManager.LoadScene ("05_NinjaLevelSelectionScene");
		}

		void OnApplicationPause (bool pauseStatus)
		{
			if (!UnityAdsManager.UnityAdsRunning) {
				if (pauseStatus && !isPaused && !isGameOver) {
					OnClickPause ();
				}
			}
		}
		//	void OnApplicationFocus(bool pauseStatus)
		//	{
		//		if (!pauseStatus && !isPaused && !isGameOver)
		//		{
		//			OnClickPause ();
		//		}
		//	}

		public void CheckInternetConnectionForNinjaGame (string message)
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

		public void MusicPanelOpen ()
		{
			MusicPanel.SetActive (true);
			PausedPanel.SetActive (false);
		}

		public void MusicPanelClose ()
		{
			MusicPanel.SetActive (false);
			PausedPanel.SetActive (true);
		}
	}
}