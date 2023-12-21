using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;






public class UnityAdsManager : MonoBehaviour
{
	public static UnityAdsManager Instance = null;
	public static bool UnityAdsRunning;
	[SerializeField] string GameIdForUnityAds = "1230683";
	[SerializeField] string UnityRewardVideoZoneId = "rewardedVideo";
	private GameObject DodgerControler;

	void Awake ()
	{
		DontDestroyOnLoad (this.gameObject);
		if (Instance == null) {
			Instance = this;
		} else if (Instance != null) {
			Destroy (this.gameObject);
		}
	}

	void Start ()
	{	

		Advertisement.Initialize (GameIdForUnityAds, true);
	}

	public void ShowAds ()
	{
//		StartCoroutine (WaitForAdsToFinish ());
		
		if (Advertisement.IsReady ())
			Advertisement.Show ();
		
	}

	public void ShowRewardedAd ()
	{
		
		if (Advertisement.IsReady (UnityRewardVideoZoneId)) {
			StartCoroutine (WaitForAdsToFinish ());
			print ("isReady");
			UnityAdsRunning = true;
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show (UnityRewardVideoZoneId, options);
		} else {
			print ("is not Ready");
			GameObject.Find ("MainPlayer").GetComponent<DogerPlayerController> ().GameOver ();

		}
	}

	public void HandleShowResult (ShowResult result)
	{

		switch (result) {
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
				//
				// YOUR CODE TO REWARD THE GAMER
				// Give coins etc.
			DogerPlayerController.Health = 1;
			DogerGameManager.gameManger.PlayerLives_Array [0].enabled = true;
			DogerPlayerController.isInvincibleFromBeingHit = false;
			DogerPlayerController.CharacterCollidedWithBullet = false;
			UnityAdsRunning = false;
			break;
		case ShowResult.Skipped:
			Debug.Log ("The ad was skipped before reaching the end.");
			GameObject.Find ("MainPlayer").GetComponent<DogerPlayerController> ().GameOver ();
			UnityAdsRunning = false;
			break;
		case ShowResult.Failed:
			Debug.LogError ("The ad failed to be shown.");
			GameObject.Find ("MainPlayer").GetComponent<DogerPlayerController> ().GameOver ();
			UnityAdsRunning = false;
			break;
		}
	}

	IEnumerator WaitForAdsToFinish ()
	{
		DogerPlayerController.GamePauseforAds = Time.timeScale;

		yield return null;
		while (Advertisement.isShowing)
			yield return null;

		Time.timeScale = 1.0f;
		DogerPlayerController.GamePauseforAds = Time.timeScale;

	}




}
