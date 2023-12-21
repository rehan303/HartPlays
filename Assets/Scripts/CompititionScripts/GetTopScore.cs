using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Simple_JSON;
using UnityEngine.SceneManagement;
using System.Text;
using System.Collections.Generic;
using System;

public class GetTopScore : MonoBehaviour {

	const string getTopScoreUrl = "https://www.hartplays.co.uk/api/users/leaderboards";

	public GameObject LeaderBoardPrefeb;
	public GameObject LeaderBoaedContainer;
	public List<LeaderBoardAtribute>  LeaderBoare = new List<LeaderBoardAtribute>();

	// Use this for initialization
	public void OnEnable()
	{		
		string email = PlayerPrefs.GetString ("UserEmail");
		ClearContainer ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void ClearContainer()
	{
		LeaderBoare.Clear ();
		for (int i =0; i< LeaderBoaedContainer.transform.childCount; i++)
		{
			Destroy (LeaderBoaedContainer.transform.GetChild (i).gameObject); 
		}
	}
	public void GetTopScoreOnLeaderBoard()
	{	
		ClearContainer ();
		HartPlayerRegistration.Instance.WatingPanel.SetActive (true);
		var encoding = new System.Text.UTF8Encoding ();
		Dictionary<string,string> postHeader = new Dictionary<string,string> ();
		var jsonElement = new JSONClass ();
		jsonElement ["limit"] = "10";
		jsonElement ["competition_id"] = PlayerPrefs.GetInt ("CompID").ToString ();
		jsonElement ["user_id"] = PlayerPrefs.GetInt ("PlayerId").ToString ();

		postHeader.Add ("Content-Type", "application/json");
		postHeader.Add ("Content-Length", jsonElement.Count.ToString ());

		WWW www = new WWW (getTopScoreUrl, encoding.GetBytes (jsonElement.ToString ()), postHeader);

		StartCoroutine (WaitForLeaderBoardRequest(www));
	}

	IEnumerator WaitForLeaderBoardRequest(WWW www)
	{
		
		yield return(www);
		print(www.text);
		if (www.error == null) {
			JSONNode _jsNode = JSON.Parse (www.text);

			if (_jsNode ["status"].ToString ().Contains ("200") && _jsNode ["description"].ToString ().Contains ("Competition winners.")) {	
				JSONNode data = _jsNode ["data"];
				if (data.Count > 0) {
					for (int i = 0; i < _jsNode ["data"].Count; i++) {
						bool tt = false;
						LeaderBoardAtribute tempData = new LeaderBoardAtribute ();
						tempData.Name = _jsNode ["data"] [i] ["User"] ["username"].ToString ().Trim ('"');
						tempData.Email = _jsNode ["data"] [i] ["User"] ["email"].ToString ().Trim ('"');
						int.TryParse (_jsNode ["data"] [i] ["UserState"] ["rank"].ToString ().Trim ('"'), out tempData.Rank);
						int.TryParse (_jsNode ["data"] [i] ["UserState"] ["user_id"].ToString ().Trim ('"'), out tempData.PlayerId);
						int.TryParse (_jsNode ["data"] [i] ["UserState"] ["color_ball_high_score"].ToString ().Trim ('"'), out tempData.ColorBallHighScore);
						int.TryParse (_jsNode ["data"] [i] ["UserState"] ["dodger_high_score"].ToString ().Trim ('"'), out tempData.DodgerHighScore);
						int.TryParse (_jsNode ["data"] [i] ["UserState"] ["total_high_score"].ToString ().Trim ('"'), out tempData.TotalHighScore);
						if (i < 10)
							int.TryParse (_jsNode ["data"] [i] ["UserState"] ["prize"].ToString ().Trim ('"'), out tempData.Prize);
						else {
							tempData.Prize = 0;
						}
					
						LeaderBoare.Add (tempData);

						GameObject go = Instantiate (LeaderBoardPrefeb, Vector3.one, Quaternion.identity) as GameObject;
						go.transform.parent = LeaderBoaedContainer.transform;
						go.transform.localScale = new Vector3 (1, 1, 1);
						go.GetComponent <LeaderBoardObj> ().SetLeaderBoardData (tempData);
						int tenpCount = LeaderBoare.Count;
						if (LeaderBoare.Count == tenpCount) {
							for (int j = 0; j < tenpCount-1; j++) {
								if (LeaderBoare [j].PlayerId == tempData.PlayerId) {
									LeaderBoare.RemoveAt (tenpCount-1);
									Destroy (LeaderBoaedContainer.transform.GetChild (tenpCount-1).gameObject); 
								}
							}
						}
					}
					HartPlayerRegistration.Instance.WatingPanel.SetActive (false);
				}
			} else if (_jsNode ["status"].ToString ().Contains ("200") && _jsNode ["description"].ToString ().Contains ("Send Required data.")) {
				HartPlayerRegistration.Instance.WatingPanelShow (false);
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "Invalid data. Please send required data.";
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {
					MenuScreen_Controller.Instance.BackFromSearchPanel ();
					MenuScreen_Controller.Instance.PlaysUpdate ();

				});			
			}	
		}
	}
}


[Serializable]
public class LeaderBoardAtribute
{
	public string Name;
	public string Email;
	public int PlayerId;
	public int Rank;
	public int ColorBallHighScore;
	public int DodgerHighScore;
	public int TotalHighScore;
	public int Prize;
}