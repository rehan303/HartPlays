using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Simple_JSON;

public class CompetitionController : MonoBehaviour
{
	const string compitionStatusUrl = "https://www.hartplays.co.uk/api/users/list_competition_api";

	public static DateTime StartTime;
	public static DateTime EndTime;
	public static string ComName;
	public static int CompId;

	public static bool compitionRunning = false;


	void OnEnable ()
	{ 	
//		SaveStartTime ();
		StartCoroutine (GetCompitionStatus());

//		compitionRunning = PlayerPrefs.GetInt ("CompetitionGoingon") == 1 ? true : false;
//		print ("Compitition Running === " + compitionRunning);
		//		print	 ("Compitition << OnEnable >> End date  ===>>  "+EndDate+" /  End Hour is ==>>>>  " + Endhour + "   Utc Now Time is ==>> "+DateTime.UtcNow); 
	}



	IEnumerator GetCompitionStatus () // Web Service to check competition is available or not..
	{
		var encoding = new System.Text.UTF8Encoding ();
		Dictionary<string,string> postHeader = new Dictionary<string,string> ();

		var jsonElement = new JSONClass();
		jsonElement ["page"] = 1.ToString ();
		jsonElement ["per_page"] = 1.ToString ();

		postHeader.Add ("Content-Type", "application/json");
		postHeader.Add ("Content-Length", jsonElement.Count.ToString ());

		WWW www = new WWW (compitionStatusUrl, encoding.GetBytes (jsonElement.ToString ()), postHeader);

		yield return www;
		if (www.error == null) {
			JSONNode _jsnode = Simple_JSON.JSON.Parse (www.text);
			JSONNode data = _jsnode[0]["competitions"];
			if(data != null)
			{	
				if (data["status"].ToString ().Contains ("1")) {
					string name =  data["name"].ToString ().Trim ('"');
					ComName = name;

					int cId = 0;
					int.TryParse (data["id"].ToString ().Trim ('"'), out cId);
					PlayerPrefs.SetInt ("CompID",cId);

					int startDate;
					int.TryParse (data ["start_date"].ToString ().Trim ('"'), out startDate);
					StartTime = UnixTimeStampToDateTime (startDate);
					print ("Start Time is " +StartTime);

					int enDate;
					int.TryParse (data ["end_date"].ToString ().Trim ('"'), out enDate);
					EndTime = UnixTimeStampToDateTime (enDate);
					print ("End Time is " +EndTime);

					PlayerPrefs.SetInt ("CompetitionGoingon", 1);
				} else {
					PlayerPrefs.SetInt ("CompetitionGoingon", 0);	
				}
			} else {
				PlayerPrefs.SetInt ("CompetitionGoingon", 0);	
			}
		}
	}

	public void SaveStartTime ()
	{
		if (PlayerPrefs.GetInt ("CompetitionGoingon") == 1) {
			compitionRunning = true;

			if (EndTime >= DateTime.UtcNow) {
				
			}
		}
	}

	public static void EndComipetition ()
	{
		compitionRunning = false;
		PlayerPrefs.DeleteKey ("CompetitionGoingon");
		PlayerPrefs.DeleteKey ("CompitionEndDate");
		PlayerPrefs.DeleteKey ("ColorBallLevelPaid");
		PlayerPrefs.DeleteKey ("NinjaLevelUnlockedPaid");
		PlayerPrefs.DeleteKey ("TotalScore");

		//latest 
		PlayerPrefs.DeleteKey ("PlayToWinSubscriptionType");
		PlayerPrefs.DeleteKey ("ColorBallLives");
		PlayerPrefs.DeleteKey ("BombCountForPaid");
		PlayerPrefs.DeleteKey ("FreezeCountForPaid");
		PlayerPrefs.DeleteKey ("ReminderCountForPaid");
		PlayerPrefs.DeleteKey ("DodgerLives");
		PlayerPrefs.DeleteKey ("DodgerBombCountForPaid");
		PlayerPrefs.DeleteKey ("DodgerSlowCountForPaid");
		PlayerPrefs.DeleteKey ("DodgerInvincibleCountForPaid");
		PlayerPrefs.DeleteKey ("DodgerMergeCountForPaid");
		PlayerPrefs.DeleteKey ("ColorBallPlayToWinHighScore");
		PlayerPrefs.DeleteKey ("DodgerPlayToWinHighScore");
		PlayerPrefs.DeleteKey ("GamePlaysCount");
		PlayerPrefs.DeleteKey ("RegistrationTime");
		PlayerPrefs.DeleteKey ("GamePlaysHours");
		PlayerPrefs.DeleteKey ("GamePlaysDays");

		var nn = PlayerPrefs.GetInt ("UserLogoutORQuit");
		PlayerPrefs.DeleteAll ();

		//
		if ((SceneManager.GetActiveScene ().name != "02_LoginScene")) {
			SceneManager.LoadScene ("02_LoginScene");		
		}
		HartPlayerRegistration.IsPaidForCompetition = 0;
		PlayerPrefs.SetInt ("IsPaidForCompetition", HartPlayerRegistration.IsPaidForCompetition);
		HartPlayerRegistration.Isplayforpaid = false;
		PlayerPrefs.SetInt ("UserLogoutORQuit", nn);
		print ("compitition Over");	
	}



	void Update ()
	{
		if (PlayerPrefs.GetInt ("CompetitionGoingon") != 1) {
			if (compitionRunning && DateTime.UtcNow >= EndTime) {				
				EndComipetition ();
			} 
		}
	}
	public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
	{
		DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0 ).ToLocalTime ();
		dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
		return dtDateTime;
	}
}
