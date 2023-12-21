using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class DogerPlayerController : MonoBehaviour
{
	public static DogerPlayerController Instance = null;
	public GUITexture BloodTexture;

	private DogerGameManager gameMangerComponent;
	private Animator m_Animator;

	private SpriteRenderer SpriteRendererComponent;

	public static bool isInvincibleFromBeingHit = false;

	public static bool CharacterCollidedWithBullet;
	private static bool CharacterRendererEnabled;

	public static int Health;
	public static int count;
	public Text PlayerLiveText;

	float initialWalkSpeed = 1;
	AudioSource audioSource;

	[Header ("Show Ads Button")]
	public Button ShowAdButton;
	public Button CancleAdButton;
	public static float GamePauseforAds;

	void Awake ()
	{
		if (Instance = null) {
			Instance = this;
		} else if (Instance != null) {
			Destroy (gameObject);
		}
	}

	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
		isInvincibleFromBeingHit = false;
		m_Animator = gameObject.GetComponent <Animator> ();
		this.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		SpriteRendererComponent = this.gameObject.GetComponent<SpriteRenderer> ();
		gameMangerComponent = DogerGameManager.gameManger;
		BloodTexture.enabled = false;

		CharacterCollidedWithBullet = false;
		CharacterRendererEnabled = true;
		initialWalkSpeed = Guns.GunMovement_Speed;
		PlayerLiveText.text = PlayerPrefs.GetInt ("DodgerLives").ToString () ;
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_Animator.speed = Guns.GunMovement_Speed / initialWalkSpeed;	
	}



	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 21 && !CharacterCollidedWithBullet && !isInvincibleFromBeingHit) {
//			BackgroundMusicManger.instance.PlaySound (audioSource);
//			Destroy (other.gameObject);

			CharacterCollidedWithBullet = true;
			Guns.instaintiatedBulletsOnScreen.Remove (other.transform.parent.gameObject);
			Destroy (other.transform.parent.gameObject);
			gameMangerComponent.PlayerLives_Array [Health - 1].enabled = false;
			if (!HartPlayerRegistration.Isplayforpaid) {
				Health--;
				PlayerLiveText.gameObject.transform.parent.gameObject.SetActive (false);
			}
			if (HartPlayerRegistration.Isplayforpaid) {
				DogerGameManager.GameOverStatus = "LifeLose";
				int SubtracLive = 0;
				SubtracLive = PlayerPrefs.GetInt ("DodgerLives");
				SubtracLive = SubtracLive - 1;
				PlayerPrefs.SetInt ("DodgerLives", SubtracLive );
				if (PlayerPrefs.GetInt ("DodgerLives") <= 0) {
					IAPManager.PlaysDecreasedFromDodger = false;
					DogerGameManager.Levelclear = false;
					gameMangerComponent.ExecuteGameOver (true);
				}
				PlayerLiveText.text = PlayerPrefs.GetInt ("DodgerLives").ToString ();
				StartCoroutine (GameSaveState.Instance.PostUserStatus ());
			} else {
				if (Health <= 0) {	
					if (Application.internetReachability == NetworkReachability.NotReachable) {
						DogerGameManager.Levelclear = false;
						gameMangerComponent.ExecuteGameOver (true);

					} else {					
						count++;
						if (count == 1) {
						
							gameMangerComponent.WatchVideoForLife.SetActive (true);
							var MainPlayer = GameObject.Find ("MainPlayer");
							MainPlayer.GetComponent<BoxCollider2D> ().enabled = false;
							Vector3 PlayerPos = MainPlayer.GetComponent<Transform> ().position;
							MainPlayer.GetComponent<DragableObject> ().enabled = false;

							var MainPlayer2 = GameObject.Find ("SecondPlayer");
							if (MainPlayer2 != null) {
								MainPlayer2.GetComponent<BoxCollider2D> ().enabled = false;
								Vector3 PlayerPos2 = MainPlayer2.GetComponent<Transform> ().position;
								MainPlayer2.GetComponent<DragableObject> ().enabled = false;
							}

							DogerGameManager.WatchVideo = true;
							gameMangerComponent.ClearDodgerGameScreen ();
							GamePauseforAds = Time.timeScale = 0.0f;
							if (Advertisement.IsReady ("rewardedVideo")) {
								ShowAdButton.onClick.AddListener (() => UnityAdsShowFuction (PlayerPos));
								CancleAdButton.onClick.AddListener (() => UnityAdsCancleFunction ());
							} else {
								ShowAdButton.onClick.AddListener (() => gameMangerComponent.InternetCheckConnectionForDodger ("Oops! \n No Video data are Loaded"));
								gameMangerComponent.InternetCkeckPanel.transform.GetComponentInChildren<Button> ().onClick.AddListener (() => UnityAdsCancleFunction ());
								CancleAdButton.onClick.AddListener (() => UnityAdsCancleFunction ());
							}

						} else {
							DogerGameManager.Levelclear = false;
							gameMangerComponent.ExecuteGameOver (true);
							count = 0;


						}

					}			

				}
			}
			Handheld.Vibrate ();	
			if (Health > 0)
				StartCoroutine (EffectOfBulletHittingTheMainPlayer ());
		}
	}




	public void UnityAdsShowFuction (Vector3 Pos)
	{

		var MainPlayer = GameObject.Find ("MainPlayer");
		MainPlayer.GetComponent<Transform> ().position = Pos;
		MainPlayer.GetComponent<BoxCollider2D> ().enabled = true;
		MainPlayer.GetComponent<DragableObject> ().enabled = true;

		var MainPlayer2 = GameObject.Find ("SecondPlayer");
		if (MainPlayer2 != null) {
			MainPlayer2.GetComponent<BoxCollider2D> ().enabled = true;
			MainPlayer2.GetComponent<DragableObject> ().enabled = true;
		}

		gameMangerComponent.WatchVideoForLife.SetActive (false);
		DogerGameManager.WatchVideo = true;
		UnityAdsManager.Instance.ShowRewardedAd ();


	}

	public void UnityAdsCancleFunction ()
	{
		GameObject.Find ("MainPlayer").GetComponent<BoxCollider2D> ().enabled = true;
		count = 0;
		GamePauseforAds = 1.0f;
		gameMangerComponent.WatchVideoForLife.SetActive (false);
		DogerGameManager.Levelclear = false;
		gameMangerComponent.ExecuteGameOver (true);
		DogerGameManager.WatchVideo = true;
		print ("Hello");
	}

	public void GameOver ()
	{
		gameMangerComponent.ExecuteGameOver (true);
	}

	IEnumerator EffectOfBulletHittingTheMainPlayer ()
	{
		for (int i = 0; i < 4; i++) {
			CharacterRendererEnabled = !CharacterRendererEnabled;
			SpriteRendererComponent.enabled = CharacterRendererEnabled;
			BloodTexture.enabled = !CharacterRendererEnabled && gameMangerComponent.isPaused; 
			yield return new WaitForSeconds (0.1f);
		}
		yield return new WaitForSeconds (0.2f);
		CharacterCollidedWithBullet = false;
		SpriteRendererComponent.enabled = true;
		BloodTexture.enabled = false;
	}
}
