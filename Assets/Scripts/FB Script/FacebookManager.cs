using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using Facebook.MiniJSON;
using Simple_JSON;

using UnityEngine.SceneManagement;

public class FacebookManager : MonoBehaviour
{
	public static FacebookManager Instance = null;
	public GameObject FbPlayerObj;
	public GameObject LeadeBoardHolder;
	public List<FaceBookPlayer> FbPlayerList = new List<FaceBookPlayer> ();
	public static int FristUser = 0;

	void Awake ()
	{		
		DontDestroyOnLoad (this.gameObject);
		if (Instance == null) {
			Instance = this;
		} else if (Instance != null) {
			Destroy (this.gameObject);
		}

		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init (InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp ();
		}

	}

	//Added for Testing only
	void Start ()
	{
		
		if(!PlayerPrefs.HasKey ("FristPlayer"))
		{
			FristUser++;
			PlayerPrefs.SetInt ("FristPlayer",FristUser);			
		}
		else{
			FristUser = PlayerPrefs.GetInt ("FristPlayer");
			FristUser++;
			PlayerPrefs.SetInt ("FristPlayer",FristUser);
			print (FristUser);
		}
		if (PlayerPrefs.GetInt ("FristPlayer") == 1) {
                      //Color Ball
					if (PlayerPrefs.GetInt ("BombCount") == 0 || PlayerPrefs.GetInt ("FreezeCount") == 0 || PlayerPrefs.GetInt ("ReminderCount", 10) == 0) {
						PlayerPrefs.SetInt ("BombCount", 5);
						PlayerPrefs.SetInt ("FreezeCount", 5);
						PlayerPrefs.SetInt ("ReminderCount", 5);
					}
			        //Dodger
					if (PlayerPrefs.GetInt ("DodgerInvincibleCount") == 0 || PlayerPrefs.GetInt ("DodgerBombCount") == 0 || PlayerPrefs.GetInt ("DodgerSlowCount", 10) == 0 || PlayerPrefs.GetInt ("DodgerMergeCount") == 0) {
						PlayerPrefs.SetInt ("DodgerInvincibleCount", 5);
						PlayerPrefs.SetInt ("DodgerBombCount", 5);
						PlayerPrefs.SetInt ("DodgerSlowCount", 5);
						PlayerPrefs.SetInt ("DodgerMergeCount", 5);
					}
		}
	
	}

	public void LoadFacebookScreen ()
	{
		StartCoroutine (LoadFbScreenWait ());
	}

	public IEnumerator LoadFbScreenWait ()
	{
		yield return new WaitForSeconds (0.5f);

		if (SceneManager.GetActiveScene ().name == "00_MenuScreen") {
			LeadeBoardHolder = GameObject.Find ("FbListHolder");
			var leaderBoardParent = LeadeBoardHolder.transform.parent.parent.parent;
			if (leaderBoardParent.transform.localScale == Vector3.one)
				leaderBoardParent.transform.localScale = Vector3.zero;

		}

	}


	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp ();
			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log ("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	//Facebook Login


	public void FBLogin ()
	{
		var perms = new List<string> () {
			"public_profile",
			"email",
			"user_friends",
			//			"publish_actions",
			"user_games_activity"
		};
		FB.LogInWithReadPermissions (perms, AuthCallback);		
		//		FB.LogInWithPublishPermissions (perms, AuthCallback);
	}


	private void AuthCallback (ILoginResult result)
	{
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log (aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log (perm);
			}
			StartCoroutine (ShowLeaderBoardAfterLogin ());
		} else {
			Debug.Log ("User cancelled login");
		}
	}

	IEnumerator ShowLeaderBoardAfterLogin ()
	{
		yield return new  WaitForSeconds (0.5f);
		QueryScores ();

	}

	public void FbLogOut ()
	{
		FB.LogOut ();

	}

	//FacebookShare
	public void FBShare ()
	{
		FB.ShareLink (
			new Uri ("https://www.facebook.com/globalinteractivevlog/"),
			"Welcome to Global Intercative Vlog",
			"This line is used for discription",
			new Uri ("https://www.google.co.in/url?sa=i&rct=j&q=&esrc=s&source=images&cd=&cad=rja&uact=8&ved=0ahUKEwjC45vvx73XAhUKOI8KHR1pANIQjRwIBw&url=https%3A%2F%2Fmodernseoul.org%2F2013%2F02%2F28%2Ftop-5-vlogs-video-blogs-about-south-korea-korean-culture%2F&psig=AOvVaw3k8coHA9O6y49ANUHTOD3H&ust=1510731582614896"),
			callback: ShareCallback
		);

	}


	private void ShareCallback (IShareResult result)
	{
		if (result.Cancelled || !String.IsNullOrEmpty (result.Error)) {
			Debug.Log ("ShareLink Error: " + result.Error);
		} else if (!String.IsNullOrEmpty (result.PostId)) {
			// Print post identifier of the shared content
			Debug.Log (result.PostId);
		} else {
			// Share succeeded without postID
			Debug.Log ("ShareLink success!");
		}
	}

	// Get and set Score

	public void QueryScores ()
	{
		FB.API ("/app/scores?fields=score,user.limit(30)", HttpMethod.GET, ScoresCallback);
//		FB.API ("/me/scores", HttpMethod.GET, ScoresCallback);
	}

	private void ScoresCallback (IResult result)
	{
		//Clear LeaedeBoard
		EmptyLeaderBoard ();
		JSONNode _jsnode = Simple_JSON.JSON.Parse (result.ToString ());
		print (_jsnode);
		JSONNode dataArray = _jsnode ["data"];
		print (dataArray.Count);

		// add To List
		int rank = 0;
		for (int i = 0; i < dataArray.Count; i++) {
			
			FaceBookPlayer newFbplayer = new FaceBookPlayer ();

			newFbplayer.Id = dataArray [i] ["user"] ["id"];
			newFbplayer.Name = dataArray [i] ["user"] ["name"];
			//Change if rank in disorder
			rank++;
			newFbplayer.Rank = rank.ToString ();
			newFbplayer.Score = dataArray [i] ["score"];
			GameObject leaderBoardList = Instantiate (FbPlayerObj, Vector3.zero, Quaternion.identity) as GameObject;
			leaderBoardList.transform.parent = LeadeBoardHolder.transform;
			leaderBoardList.transform.localScale = new Vector3 (1, 1, 1);
			var thisScript = leaderBoardList.GetComponent<FacebookLeaderboardObj> ();
			thisScript.PlayerFbId = newFbplayer.Id;
			thisScript.PlayerName.text = newFbplayer.Name;
			thisScript.Rank.text = rank.ToString ();
			thisScript.Score.text = newFbplayer.Score;

			//Get User Profile
			FB.API ("https" + "://graph.facebook.com/" + newFbplayer.Id.ToString () + "/picture?type=large", HttpMethod.GET, delegate (IGraphResult pictureResult) {
				if (pictureResult.Error != null) {
					Debug.Log (pictureResult.Error);
				} else {
					thisScript.ProfileView.sprite = Sprite.Create (pictureResult.Texture, new Rect (0, 0, 200, 200), new Vector2 (0, 0));
				}
			});
			FbPlayerList.Add (newFbplayer);
		}
		var menuScreen = GameObject.Find ("Main Camera").GetComponent<MenuScreen_Controller> ();
		menuScreen.FacebookPanel.transform.localScale = Vector3.one;
	}

	void EmptyLeaderBoard ()
	{
		for (int i = 0; i < LeadeBoardHolder.transform.childCount; i++) {
			Destroy (LeadeBoardHolder.transform.GetChild (i).gameObject);
		}
		FbPlayerList.Clear ();

	}

	public void PostScoreOnFacebook (float Score)
	{
		if (FB.IsLoggedIn) {
			var scoreData = new Dictionary<string,string> ();
			scoreData.Clear ();
			scoreData ["score"] = Score.ToString ();
			FB.API ("/me/scores", HttpMethod.POST, delegate(IGraphResult result) {
				Debug.Log ("Score submit result: " + result);
				Debug.Log ("Score is: " + Score.ToString ());
			}, scoreData);
		}
	}

}

[Serializable]
public class FaceBookPlayer
{
	public string Rank;
	public string Name;
	public string Score;
	public string Id;
}


