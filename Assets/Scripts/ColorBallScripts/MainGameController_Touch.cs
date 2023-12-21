using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameController_Touch : MonoBehaviour
{
	private GameObject PauseButton_GameObject;
	private GameObject ScoresText_GameObject;
	public GameObject LivesText;
	public GameObject PauseScreenPanel;
	public GameObject SettingsScreenPanel;
	public static bool IsSettingEnable;
	public GameObject BombAnimation;
	public GameObject RestartButton;
	public GameObject BuyButton;
	public GameObject SettingButton;
	public GameObject ExitButton;
	[Header ("Shope Panel")]
	public GameObject ShopItemPanel;
	public GameObject PowerUpPanel;

	[Header ("Shope Button")]
	public GameObject shopBackButton;
	public GameObject shopButton;

	public static GameObject GameObject_toDrag;

	private Vector3 offset;
	private Vector3 GameObject_WorldPosition;

	private float Z_Dist;

	public static bool bombCollidedWithPot = false;
	public static bool dragging = false;
	public static bool ballCollidedWithPots = false;
	private bool isPowerUpEnableOnPause;

	public Transform ballRestrictedPositionX;
	public Transform ballRestrictedPositionY;


	private GameManager gameManagerComponent;
	private PowerUpContoller powerUpControllerComponent;
	private Reminder_Bomb ReminderBomb_Component;
	int randomPowerUp;
	PowerUpContoller PowerUpContoller;

	//

	int TapCount;
	public float MaxDubbleTapTime;
	float NewTime;

	//

	public static bool PlayingColorBall;
	void Start ()
	{
		HartPlayerRegistration.Instance.MessageText.text = "";
		PauseButton_GameObject = GameObject.Find ("PauseButton_GameObject");
		ScoresText_GameObject = GameObject.Find ("Scores_Text");
		ScoresText_GameObject = GameObject.Find ("Scores_Text (1)");
		gameManagerComponent = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		powerUpControllerComponent = GameObject.Find ("ScreenManager").GetComponent<PowerUpContoller> ();
		ReminderBomb_Component = GameObject.Find ("Reminder_PowerUp").GetComponent<Reminder_Bomb> ();
		PowerUpContoller = GameObject.Find ("ScreenManager").GetComponent<PowerUpContoller>();
	}


	void Update ()
	{
		int touchCount = Input.touchCount;
		if (touchCount == 1) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Ended) {
				TapCount += 1;
			}

			if (TapCount == 1) {

				NewTime = Time.time + MaxDubbleTapTime;
			}else if(TapCount == 2 && Time.time <= NewTime){
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (touch.position), Vector2.zero);
				if (hit.collider == null) {
				//Whatever you want after a dubble tap    
				print ("Dubble tap");
				randomPowerUp = Random.Range (1, 4);
					if (HartPlayerRegistration.Isplayforpaid) {
						if (randomPowerUp == 1) {
							if (PlayerPrefs.GetInt ("BombCountForPaid") > 0)
								PowerUpContoller.BombPowerUp ();
							else {
								if (PlayerPrefs.GetInt ("FreezeCountForPaid") > 0)
									PowerUpContoller.FreezePowerUp ();
								else if (PlayerPrefs.GetInt ("ReminderCountForPaid") > 0)
									PowerUpContoller.ReminderPowerUp ();
							}
						} else if (randomPowerUp == 2) {
							if (PlayerPrefs.GetInt ("FreezeCountForPaid") > 0)
								PowerUpContoller.FreezePowerUp ();
							else {
								if (PlayerPrefs.GetInt ("BombCountForPaid") > 0)
									PowerUpContoller.BombPowerUp ();
								else if (PlayerPrefs.GetInt ("ReminderCountForPaid") > 0)
									PowerUpContoller.ReminderPowerUp ();
							}

						} else if (randomPowerUp == 3) {

							if (PlayerPrefs.GetInt ("ReminderCountForPaid") > 0)
								PowerUpContoller.ReminderPowerUp ();
							else {
								if (PlayerPrefs.GetInt ("BombCountForPaid") > 0)
									PowerUpContoller.BombPowerUp ();
								else if (PlayerPrefs.GetInt ("FreezeCountForPaid") > 0)
									PowerUpContoller.FreezePowerUp ();
							}
						}
					} else {
						if (randomPowerUp == 1) {
							if (PlayerPrefs.GetInt ("BombCount") > 0)
								PowerUpContoller.BombPowerUp ();
							else {
								if (PlayerPrefs.GetInt ("FreezeCount") > 0)
									PowerUpContoller.FreezePowerUp ();
								else if (PlayerPrefs.GetInt ("ReminderCount") > 0)
									PowerUpContoller.ReminderPowerUp ();
							}
						} else if (randomPowerUp == 2) {
							if (PlayerPrefs.GetInt ("FreezeCount") > 0)
								PowerUpContoller.FreezePowerUp ();
							else {
								if (PlayerPrefs.GetInt ("BombCount") > 0)
									PowerUpContoller.BombPowerUp ();
								else if (PlayerPrefs.GetInt ("ReminderCount") > 0)
									PowerUpContoller.ReminderPowerUp ();
							}

						} else if (randomPowerUp == 3) {

							if (PlayerPrefs.GetInt ("ReminderCount") > 0)
								PowerUpContoller.ReminderPowerUp ();
							else {
								if (PlayerPrefs.GetInt ("BombCount") > 0)
									PowerUpContoller.BombPowerUp ();
								else if (PlayerPrefs.GetInt ("FreezeCount") > 0)
									PowerUpContoller.FreezePowerUp ();
							}
						}
					}
				}

				TapCount = 0;
			}

		}
		if (Time.time > NewTime) {
			TapCount = 0;
		}
//		TouchPosition_GameObject = Input.mousePosition;

		if (touchCount == 1) {
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (touch.position), Vector2.zero);
				if (hit.collider != null) {
					var Cd = GameObject.Find ("GameManager").GetComponent<CountdownTimer> ();
					if (hit.collider.gameObject.layer == 8 || hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 10 || hit.collider.gameObject.layer == 11 || hit.collider.gameObject.layer == 20 || hit.collider.gameObject.layer == 21 || hit.collider.gameObject.layer == 22 || hit.collider.gameObject.layer == 23 || hit.collider.gameObject.layer == 17 || hit.collider.gameObject.layer == 18 || hit.collider.gameObject.layer == 19) {
						GameObject_toDrag = hit.collider.gameObject;
						GameObject_toDrag.GetComponent<Movement_RandomInstantiatedBalls> ().enabled = false;
						GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (0.65f, 0.65f);
						Z_Dist = hit.transform.position.z - Camera.main.transform.position.z;
						GameObject_WorldPosition = new Vector3 (touch.position.x, touch.position.y, Z_Dist);
						GameObject_WorldPosition = Camera.main.ScreenToWorldPoint (GameObject_WorldPosition);
						offset = GameObject_toDrag.transform.position - GameObject_WorldPosition;
						dragging = true;				

					} else if (hit.collider.gameObject.name == "PauseButton_GameObject" && Cd.timeLeft > 1f) {
						PauseButtonClicked ();
					} else if (hit.collider.gameObject.layer == 16) {
						GameObject_toDrag = hit.collider.gameObject;
						dragging = true;
					}
				}
			}

			if (dragging && !ballCollidedWithPots) {
				GameObject_WorldPosition = new Vector3 (touch.position.x, touch.position.y, Z_Dist);
				GameObject_WorldPosition = Camera.main.ScreenToWorldPoint (GameObject_WorldPosition);
				if (GameObject_toDrag) {
					GameObject_toDrag.transform.position = GameObject_WorldPosition + offset;
					Vector3 restricted_Position = GameObject_toDrag.transform.position;

//					restricted_Position.x = Mathf.Clamp (restricted_Position.x, -ballRestrictedPositionX.position.x, ballRestrictedPositionX.position.x);
//					restricted_Position.y = Mathf.Clamp (restricted_Position.y, -ballRestrictedPositionY.position.y, ballRestrictedPositionY.position.y);
					//Changed By Rehan
					restricted_Position.x = Mathf.Clamp (restricted_Position.x, -2.5125f, 2.5125f);
					restricted_Position.y = Mathf.Clamp (restricted_Position.y, -4.7f, 4.7f);
					restricted_Position.z = Mathf.Clamp (restricted_Position.z, 0, 0);
					GameObject_toDrag.transform.position = restricted_Position;
				} else {
					if (dragging) {
						dragging = false;
						if (GameObject_toDrag && GameObject_toDrag.layer != 16) {
							ballCollidedWithPots = false;
							GameObject_toDrag.GetComponent<Movement_RandomInstantiatedBalls> ().enabled = true;
							GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (0.9f, 0.9f);
							GameObject_toDrag = null;
						} else if (GameObject_toDrag && GameObject_toDrag.layer == 16) {
							bombCollidedWithPot = false;
							GameObject_toDrag = null;
						}
					}
				}
			}

			if (touch.phase == TouchPhase.Ended) {
				if (dragging) {
					dragging = false;
					if (GameObject_toDrag && GameObject_toDrag.layer != 16) {
						ballCollidedWithPots = false;
						GameObject_toDrag.GetComponent<Movement_RandomInstantiatedBalls> ().enabled = true;
						GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (0.9f, 0.9f);
						GameObject_toDrag = null;
					} else if (GameObject_toDrag && GameObject_toDrag.layer == 16) {
						bombCollidedWithPot = false;
						GameObject_toDrag = null;
					}
				} else {
					return;
				}
			}
		} else if (touchCount > 1 || touchCount == 2) {
			if (dragging) {
				dragging = false;
				if (GameObject_toDrag && GameObject_toDrag.layer != 16) {
					ballCollidedWithPots = false;
					GameObject_toDrag.GetComponent<Movement_RandomInstantiatedBalls> ().enabled = true;
//					GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (0.9f,0.9f);
					GameObject_toDrag = null;
				} else if (GameObject_toDrag && GameObject_toDrag.layer == 16) {
					bombCollidedWithPot = false;
					GameObject_toDrag = null;
				}
			}
		}	
	}
			
		



	public void ResumeButtonClicked ()
	{
		Time.timeScale = 1;
		TapCount = 0;
		PauseButton_GameObject.SetActive (true);
		ScoresText_GameObject.SetActive (true);
		if (HartPlayerRegistration.Isplayforpaid) {
			LivesText.SetActive (true);
		}
		PowerUpPanel.SetActive (true);
		CloseShopPanel ();
		PauseScreenPanel.SetActive (false);
		BombAnimation.SetActive (true);	

		foreach (GameObject blastAnim in PowerUpBomb_All.BombAnimation) {
			if (blastAnim != null)
				blastAnim.GetComponent<SpriteRenderer> ().enabled = true;

		}
		GameObject.Find ("BombAnimation").GetComponent<SpriteRenderer> ().sprite = null;

		foreach (GameObject temp_obj in RandomInstantiationOfBalls.instantiatedBalls_Runtime) {
			temp_obj.SetActive (true);
		}
		foreach (GameObject powerUp_GameObjects in powerUpControllerComponent.powerUps) {
			if (!PowerUpContoller.isPowerUpAlive && isPowerUpEnableOnPause) {
				powerUp_GameObjects.GetComponent<SpriteRenderer> ().enabled = true;
				powerUp_GameObjects.GetComponent<BoxCollider2D> ().enabled = true;

				isPowerUpEnableOnPause = false;
			} 
		}
		if (powerUpControllerComponent.powerUp_GameObject != null) {
			powerUpControllerComponent.powerUp_GameObject.SetActive (true);
		}
		if (powerUpControllerComponent.PurchasedpowerUp_GameObject != null) {
			powerUpControllerComponent.PurchasedpowerUp_GameObject.SetActive (true);
		}

		foreach (GameObject active_Pots in ReminderBomb_Component.activePots) {
//			active_Pots.SetActive ( true );
			active_Pots.GetComponent<SpriteRenderer> ().enabled = true;
			active_Pots.GetComponent<BoxCollider2D> ().enabled = true;
		}
		foreach (GameObject active_Pots_Textures in ReminderBomb_Component.activePots_Textures) {
			active_Pots_Textures.SetActive (true);
//			active_Pots_Textures.GetComponent<SpriteRenderer> ().enabled = true;
//			active_Pots_Textures.GetComponent<BoxCollider2D> ().enabled = true;
		}
		gameManagerComponent.PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), true);
	}

	public void RestartButtonClicked ()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene ("01_MainGamePlay");
	}

	public void QuitButtonClicked ()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene ("00_MenuScreen");
		if (HartPlayerRegistration.Isplayforpaid) {
			PlayingColorBall = true;
			MenuScreen_Controller.CurrntGamePlaying = "ColorBall";
			int SubtracLive = 0;
			SubtracLive = PlayerPrefs.GetInt ("ColorBallLives");
			SubtracLive = SubtracLive - 1;
			PlayerPrefs.SetInt ("ColorBallLives", SubtracLive );
			if (PlayerPrefs.GetInt ("ColorBallLives") == 0) {
				IAPManager.PlaysDecreasedFromColorBall = false;
			}
			//Post High Score
			//Post Score
			int previousScore = PlayerPrefs.GetInt ("TempColorBallScore");
			previousScore = GameManager.TotalPointsOrScores + previousScore;
			PlayerPrefs.SetInt ("TempColorBallScore",previousScore);
			print (PlayerPrefs.GetInt ("ColorBallPlayToWinHighScore"));
			if(PlayerPrefs.GetInt ("ColorBallLives") <= 0)
			{
				if(PlayerPrefs.GetInt ("ColorBallPlayToWinHighScore") < PlayerPrefs.GetInt ("TempColorBallScore"))
				{
					PlayerPrefs.SetInt ("ColorBallPlayToWinHighScore", PlayerPrefs.GetInt ("TempColorBallScore"));
					ScreenManager.Instance.PostScores (PlayerPrefs.GetString ("UserEmail"), PlayerPrefs.GetInt ("ColorBallPlayToWinHighScore"));
					PlayerPrefs.DeleteKey ("TempColorBallScore");
				}
			}
			StartCoroutine (GameSaveState.Instance.PostUserStatus ());
		}
		FacebookManager.Instance.LoadFacebookScreen ();
	}


	public void SettingsButtonClicked ()
	{
		PauseScreenPanel.SetActive (false);
		SettingsScreenPanel.SetActive (true);
		IsSettingEnable = true;

	}

	public void BackToPauseMenuButtonClicked ()
	{
		PauseScreenPanel.SetActive (true);
		SettingsScreenPanel.SetActive (false);
		IsSettingEnable = false;
	}

	public void OnShopPanleClick ()
	{
		ShopItemPanel.SetActive (true);
		PauseScreenPanel.GetComponent<RectTransform> ().localScale = Vector3.zero;

	}

	public void OnClikcBackFromShope ()
	{
		ShopItemPanel.SetActive (false);
		PauseScreenPanel.GetComponent<RectTransform> ().localScale = Vector3.one;
	}

	public void CloseShopPanel ()
	{
		PauseScreenPanel.GetComponent<RectTransform> ().localScale = Vector3.one;
		ShopItemPanel.SetActive (false);		
	}


	public void PauseButtonClicked ()
	{
		
		gameManagerComponent.PotSpawnner (PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel"), false);

		foreach (GameObject temp_obj in RandomInstantiationOfBalls.instantiatedBalls_Runtime) {
			temp_obj.SetActive (false);
		}

		foreach (GameObject blastAnim in PowerUpBomb_All.BombAnimation) {
			if (blastAnim != null)
				blastAnim.GetComponent<SpriteRenderer> ().enabled = false;
		}
		GameObject.Find ("Timer_Text").GetComponent<Text> ().enabled = true;
		PauseButton_GameObject.SetActive (false);
		ScoresText_GameObject.SetActive (false);
		if (HartPlayerRegistration.Isplayforpaid) {
			LivesText.SetActive (false);
		}
		PowerUpPanel.SetActive (false);
		PauseScreenPanel.SetActive (true);
		BombAnimation.SetActive (false);

		if (HartPlayerRegistration.Isplayforpaid) {
			SettingButton.GetComponent <RectTransform>().localPosition = new Vector2(10f,-270f);
			RestartButton.SetActive (false);
			BuyButton.SetActive (false);
			ExitButton.GetComponent<RectTransform> ().localPosition = new Vector2 (0.2958679f, -40f);

		} else {
			print ("Its Free To Play");
		}
		foreach (GameObject powerUp_GameObjects in powerUpControllerComponent.powerUps) {
			if (powerUp_GameObjects.GetComponent<SpriteRenderer> ().enabled) {
				powerUp_GameObjects.GetComponent<SpriteRenderer> ().enabled = false;
				powerUp_GameObjects.GetComponent<BoxCollider2D> ().enabled = false;	
				isPowerUpEnableOnPause = true;
			}
		}
		if (powerUpControllerComponent.powerUp_GameObject != null) {
			powerUpControllerComponent.powerUp_GameObject.SetActive (false);
		}
		if (powerUpControllerComponent.PurchasedpowerUp_GameObject != null) {
			powerUpControllerComponent.PurchasedpowerUp_GameObject.SetActive (false);
		}

		foreach (GameObject active_Pots in ReminderBomb_Component.activePots) {
//			active_Pots.SetActive ( false );
			active_Pots.GetComponent<SpriteRenderer> ().enabled = false;
			active_Pots.GetComponent<BoxCollider2D> ().enabled = false;
		}

		foreach (GameObject active_Pots_Textures in ReminderBomb_Component.activePots_Textures) {
			active_Pots_Textures.SetActive (false);
//			active_Pots_Textures.GetComponent<SpriteRenderer> ().enabled = false;
//			active_Pots_Textures.GetComponent<BoxCollider2D> ().enabled = false;
		}
		Time.timeScale = 0;
	}

	// Dodger Game On Application
	void OnApplicationPause (bool pauseStatus)
	{		
		if (!UnityAdsManager.UnityAdsRunning) {
			if (!IsSettingEnable) {
				if (pauseStatus && !ScreenManager.isLevelEnded) {
					PauseButtonClicked ();
				}
			}
		}
	}


	void OnApplicationQuit(){
		if (HartPlayerRegistration.Isplayforpaid) {
			PlayingColorBall = false;
			MenuScreen_Controller.CurrntGamePlaying = "";
		}
	}
}
