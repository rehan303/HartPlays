	using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Simple_JSON;
using System;
using UnityEngine.SceneManagement;

public class GameSaveState : MonoBehaviour {
	const string SaveStateLink = "https://www.hartplays.co.uk/api/users/save_state_user";
	const string GetUserStateLink = "https://www.hartplays.co.uk/api/users/get_user_state?email=";
	const string FreePlaysUpdate = "https://www.hartplays.co.uk/api/users/update_free_plays";
	// Use this for initialization
	public static GameSaveState Instance = null;
	void Awake()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		if (Instance == null) {
			Instance = this;
		} else if (Instance != null) {
			Destroy (this.gameObject);
		}
//		StartCoroutine (GetUserStatus ());
	}
	
	public IEnumerator PostUserStatus () // Web Service to check competition is available or not..
	{
		if (PlayerPrefs.HasKey ("UserEmail")) {	
		
			HartPlayerRegistration.Instance.WatingPanelShow (true);
			var encoding = new System.Text.UTF8Encoding ();
			Dictionary<string,string> postHeader = new Dictionary<string,string> ();
			var jsonElement = new JSONClass ();
			jsonElement ["email"] = PlayerPrefs.GetString ("UserEmail");
			jsonElement ["subscribe_plan"] = PlayerPrefs.GetString ("PlayToWinSubscriptionType");
			jsonElement ["competition_id"] = PlayerPrefs.GetInt ("CompID").ToString ();
			jsonElement ["user_id"] = PlayerPrefs.GetInt ("PlayerId").ToString ();
			jsonElement ["is_paid"] = HartPlayerRegistration.IsPaidForCompetition.ToString ();
			if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "10_Plays_Subscription")
				jsonElement ["plays_count"] = PlayerPrefs.GetInt ("GamePlaysCount").ToString ();
			else if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "20_Plays_Subscription")
				jsonElement ["plays_count"] = PlayerPrefs.GetInt ("GamePlaysCount").ToString ();
			else if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "30_Plays_Subscription")
				jsonElement ["plays_count"] = PlayerPrefs.GetInt ("GamePlaysCount").ToString ();
			else if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "24Hours_Plays_Subscription")
				jsonElement ["plays_count"] = "N";
			else if (PlayerPrefs.GetString ("PlayToWinSubscriptionType") == "7Days_Plyas_Subscription")
				jsonElement ["plays_count"] = "N";
		
			jsonElement ["registration_time"] = PlayerPrefs.GetString ("RegistrationTime");
			jsonElement ["color_ball_lives"] = PlayerPrefs.GetInt ("ColorBallLives").ToString ();
			jsonElement ["color_ball_bomb"] = PlayerPrefs.GetInt ("BombCountForPaid").ToString ();
			jsonElement ["color_ball_freeze"] = PlayerPrefs.GetInt ("FreezeCountForPaid").ToString ();
			jsonElement ["color_ball_remember"] = PlayerPrefs.GetInt ("ReminderCountForPaid").ToString ();
			if(PlayerPrefs.GetInt ("DodgerLives") <0 )
			{
				PlayerPrefs.SetInt ("DodgerLives", 0);				
				jsonElement ["dodger_lives"] = PlayerPrefs.GetInt ("DodgerLives").ToString ();
			}else
				jsonElement ["dodger_lives"] = PlayerPrefs.GetInt ("DodgerLives").ToString ();
			jsonElement ["dodger_bomb"] = PlayerPrefs.GetInt ("DodgerBombCountForPaid").ToString ();
			jsonElement ["dodger_reverse"] = PlayerPrefs.GetInt ("DodgerSlowCountForPaid").ToString ();
			jsonElement ["dodger_invincible"] = PlayerPrefs.GetInt ("DodgerInvincibleCountForPaid").ToString ();
			jsonElement ["dodger_merge"] = PlayerPrefs.GetInt ("DodgerMergeCountForPaid").ToString ();
			jsonElement ["color_ball_high_score"] = PlayerPrefs.GetInt ("ColorBallPlayToWinHighScore").ToString ();
			jsonElement ["dodger_high_score"] = PlayerPrefs.GetInt ("DodgerPlayToWinHighScore").ToString ();

			postHeader.Add ("Content-Type", "application/json");
			postHeader.Add ("Content-Length", jsonElement.Count.ToString ());
			print (jsonElement.ToString ());
			WWW www = new WWW (SaveStateLink, encoding.GetBytes (jsonElement.ToString ()), postHeader);

			yield return www;
			if (www.error == null) {
				JSONNode _jsnode = Simple_JSON.JSON.Parse (www.text);

				if (_jsnode ["status"].ToString ().Contains ("200")) {					
					print ("User Post Stats Updated Succesfull");
					HartPlayerRegistration.Instance.WatingPanelShow (false);
				} else
					print ("Some Porblem Is There");
				HartPlayerRegistration.Instance.WatingPanelShow (false);
			}
		}

	}


	public IEnumerator GetUserStatus (bool openScreen) // Web Service to check competition is available or not..
	{
		HartPlayerRegistration.Instance.WatingPanelShow (true);
		var encoding = new System.Text.UTF8Encoding ();
		Dictionary<string,string> postHeader = new Dictionary<string,string> ();

		var jsonElement = new JSONClass ();
		postHeader.Add ("Content-Type", "application/json");
		postHeader.Add ("Content-Length", jsonElement.Count.ToString ());
	
		WWW www = new WWW (GetUserStateLink+PlayerPrefs.GetString ("UserEmail"), encoding.GetBytes (jsonElement.ToString ()), postHeader);

		yield return www;
		if (www.error == null) {
			
			JSONNode _jsnode = Simple_JSON.JSON.Parse (www.text);

			if(_jsnode["data"] != null )
			{	
				JSONNode data = _jsnode["data"];
				print (data.ToString ());
				if (_jsnode ["status"].ToString ().Contains ("200")) {
					PlayerPrefs.SetString ("UserEmail", data ["UserState"] ["email"].ToString ().Trim ('"'));
					PlayerPrefs.SetString ("PlayToWinSubscriptionType", data ["UserState"] ["subscribe_plan"].ToString ().Trim ('"'));
					HartPlayerRegistration.registrationTime =  Convert.ToDateTime (data ["UserState"] ["registration_time"].ToString ().Trim ('"'));
					PlayerPrefs.SetString ("RegistrationTime", HartPlayerRegistration.registrationTime.ToString ());
					;
					if ( PlayerPrefs.GetString ("PlayToWinSubscriptionType")=="10_Plays_Subscription") {
						int tempPlays = 0;
						var Values = data ["UserState"] ["plays_count"].ToString ().Trim ('"').Split ('_');
						int.TryParse (Values[0], out tempPlays);
						HartPlays_PlaytoWin_Subscribe.PlaysCount = tempPlays;
						PlayerPrefs.SetInt ("GamePlaysCount", tempPlays);
					}else if ( PlayerPrefs.GetString ("PlayToWinSubscriptionType")=="20_Plays_Subscription") {
						int tempPlays = 0;
						var Values = data ["UserState"] ["plays_count"].ToString ().Trim ('"').Split ('_');
						int.TryParse (Values[0], out tempPlays);
						HartPlays_PlaytoWin_Subscribe.PlaysCount = tempPlays;
						PlayerPrefs.SetInt ("GamePlaysCount", tempPlays);
					}if ( PlayerPrefs.GetString ("PlayToWinSubscriptionType")=="30_Plays_Subscription") {
						int tempPlays = 0;
						var Values = data ["UserState"] ["plays_count"].ToString ().Trim ('"').Split ('_');
						int.TryParse (Values[0], out tempPlays);
						HartPlays_PlaytoWin_Subscribe.PlaysCount = tempPlays;
						PlayerPrefs.SetInt ("GamePlaysCount", tempPlays);
					} 
					else if ( PlayerPrefs.GetString ("PlayToWinSubscriptionType")=="24Hours_Plays_Subscription") {
						
						DateTime SubsTime =HartPlayerRegistration.registrationTime.AddHours (24);
						if(SubsTime> DateTime.UtcNow)
						{
							var TimeinHr = SubsTime - DateTime.UtcNow;
							HartPlays_PlaytoWin_Subscribe.PlaysHours = TimeinHr.Hours;
						}

					} else if (PlayerPrefs.GetString ("PlayToWinSubscriptionType")=="7Days_Plyas_Subscription") {
						DateTime SubsTime =HartPlayerRegistration.registrationTime.AddHours (24);
						if(SubsTime> DateTime.UtcNow)
						{
							var TimeinHr = SubsTime - DateTime.UtcNow;
							HartPlays_PlaytoWin_Subscribe.PlaysHours = TimeinHr.Days;
						}
					}				
					int colorballLive = 0;
					int.TryParse (data ["UserState"] ["color_ball_lives"].ToString ().Trim ('"'), out colorballLive);
					PlayerPrefs.SetInt ("ColorBallLives", colorballLive);
					int colorballBomb = 0;
					int.TryParse (data ["UserState"] ["color_ball_bomb"].ToString ().Trim ('"'), out colorballBomb);
					PlayerPrefs.SetInt ("BombCountForPaid", colorballBomb);
					int colorballFreez = 0;
					int.TryParse (data ["UserState"] ["color_ball_freeze"].ToString ().Trim ('"'), out colorballFreez);
					PlayerPrefs.SetInt ("FreezeCountForPaid",colorballFreez);
					int colorballReminder = 0;
					int.TryParse (data ["UserState"] ["color_ball_remember"].ToString ().Trim ('"'), out colorballReminder);
					PlayerPrefs.SetInt ("ReminderCountForPaid", colorballReminder);
					int DodgerLive = 0;
					int.TryParse (data ["UserState"] ["dodger_lives"].ToString ().Trim ('"'), out DodgerLive);
					PlayerPrefs.SetInt ("DodgerLives", DodgerLive);
					int DodgerBomb = 0;
					int.TryParse (data ["UserState"] ["dodger_bomb"].ToString ().Trim ('"'), out DodgerBomb);
					PlayerPrefs.SetInt ("DodgerBombCountForPaid", DodgerBomb);
					int DodgerReverse = 0;
					int.TryParse (data ["UserState"] ["dodger_reverse"].ToString ().Trim ('"'), out DodgerReverse);
					PlayerPrefs.SetInt ("DodgerSlowCountForPaid", DodgerReverse);
					int DodgerInvincible = 0;
					int.TryParse (data ["UserState"] ["dodger_invincible"].ToString ().Trim ('"'), out DodgerInvincible);
					PlayerPrefs.SetInt ("DodgerInvincibleCountForPaid", DodgerInvincible);
					int DodgerMerge = 0;
					int.TryParse (data ["UserState"] ["dodger_merge"].ToString ().Trim ('"'), out DodgerMerge);
					PlayerPrefs.SetInt ("DodgerMergeCountForPaid", DodgerMerge);
					int ColorBallHighScore = 0;
					int.TryParse (data ["UserState"] ["color_ball_high_score"].ToString ().Trim ('"'), out ColorBallHighScore);
					PlayerPrefs.SetInt ("ColorBallPlayToWinHighScore", ColorBallHighScore);
					int DodgerHighScore = 0;
					int.TryParse (data ["UserState"] ["dodger_high_score"].ToString ().Trim ('"'), out DodgerHighScore);
					PlayerPrefs.SetInt ("DodgerPlayToWinHighScore", DodgerHighScore);
					int TotalHighScore = 0;
					int.TryParse (data ["UserState"] ["total_high_score"].ToString ().Trim ('"'), out TotalHighScore);
					PlayerPrefs.SetInt ("total_high_score", TotalHighScore);

					print ("User Get Status Done");
					HartPlayerRegistration.Instance.WatingPanelShow (false);
				}else
					print ("Some Porblem Is There");
				HartPlayerRegistration.Instance.WatingPanelShow (false);

			}
		
			if ((SceneManager.GetActiveScene ().name != "02_LoginScene") && openScreen) {
//				MenuScreen_Controller.Instance.PlaysUpdate ();
			}
			HartPlayerRegistration.Instance.WatingPanelShow (false);
		}

	}

	public IEnumerator IsFreePlaysUpdate () // Web Service to check competition is available or not..
	{
		if (PlayerPrefs.HasKey ("UserEmail")) {	


			var encoding = new System.Text.UTF8Encoding ();
			Dictionary<string,string> postHeader = new Dictionary<string,string> ();
			var jsonElement = new JSONClass ();
			jsonElement ["user_id"] = PlayerPrefs.GetInt ("PlayerId").ToString ();
			jsonElement ["is_free_plays"] = "1";

			postHeader.Add ("Content-Type", "application/json");
			postHeader.Add ("Content-Length", jsonElement.Count.ToString ());

			WWW www = new WWW (FreePlaysUpdate, encoding.GetBytes (jsonElement.ToString ()), postHeader);

			yield return www;
			if (www.error == null) {
				JSONNode _jsnode = Simple_JSON.JSON.Parse (www.text);
				if (_jsnode ["status"].ToString ().Contains ("200")) {					
					print (_jsnode ["description"].ToString ());
					PlayerPrefs.SetInt ("IsFreePlays", 1);
				} 
			}
		}

	}

}
