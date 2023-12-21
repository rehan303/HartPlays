using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Simple_JSON;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PushManager : MonoBehaviour {

	public static PushManager Instance = null;
	public const string SearchPlayerLink = "https://www.hartplays.co.uk/api/users/search_user?name=";
	public const string SendPlaysToPlayerLink = "https://www.hartplays.co.uk/api/users/send_lives";
	public const string GetAllNotification = "https://www.hartplays.co.uk/api/users/get_all_notifications";
	public GameObject PushPlayerObj;
	public GameObject PushPlayerHolder;
	public List<PushPlayer> SearchPlayerList = new List<PushPlayer> ();
	public GameObject OverlayObj;
	public string SearchText;
	public Text Title;
	public PushPlayer SelectedPushPlayer;
	// Use this for initialization
	public InputField inputText;

	[Header ("Send Plays")]
	public GameObject PlayerSendAndSearchPanel;
	public GameObject PlaysSendPanel;
	public GameObject NotificationPanel;
	public Text PlayerReciverName;
	public Text LeftIconCount;
	public Text PlaysContToSend;
	public int Count = 0;

	[Header ("Notification")]
	public GameObject NotificationHolder;
	public GameObject NotificationObj;




	void Awake()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != null) {
			Destroy (this.gameObject);
		}
	}

	public void DeletAllPlayer()
	{
		for(int i= 0; i< PushPlayerHolder.transform.childCount; i++ )
		{
			Destroy (PushPlayerHolder.transform.GetChild (i).gameObject);
		}
	}

	public void SearchPlayer ()
	{
		SearchText = inputText.text;
		for(int i= 0; i< PushPlayerHolder.transform.childCount; i++ )
		{
			Destroy (PushPlayerHolder.transform.GetChild (i).gameObject);
		}
		HartPlayerRegistration.Instance.WatingPanelShow (true);
		var encoding = new System.Text.UTF8Encoding ();
		Dictionary<string,string> postHeader = new Dictionary<string,string> ();
		var jsonElement = new JSONClass ();

		postHeader.Add ("Content-Type", "application/json");
		postHeader.Add ("Content-Length", jsonElement.Count.ToString ());

		if(SearchText.ToString ().Contains (" "))
		{
			HartPlayerRegistration.Instance.WatingPanelShow (false);
			HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
			HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
			HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
			HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
			HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "Please type without leaving any space.";
			HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
			HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {
				HartPlayerRegistration.Instance.WatingPanelShow (false);
			});
		} else {
			WWW www = new WWW (SearchPlayerLink + SearchText.ToString (), encoding.GetBytes (jsonElement.ToString ()), postHeader);
			StartCoroutine (WaitForSearch (www));
		}


	}

	IEnumerator WaitForSearch (WWW www)
	{
		yield return(www);
		print (www.text.ToLower ());
		JSONNode _jsNode = JSON.Parse (www.text);
		if (_jsNode ["status"].ToString ().Contains ("200")) {
			if (_jsNode ["description"].ToString ().Contains ("Search Success.")) {
				JSONNode data = _jsNode ["data"];
			
				SearchPlayerList.Clear ();
				if (data.Count > 0)
					OverlayObj.SetActive (false);
				else
					OverlayObj.SetActive (true);
				for (int i =0; i< data.Count; i++)
				{
					PushPlayer TempPushPlayer = new PushPlayer ();
					TempPushPlayer.Name = data [i] ["Users"] ["username"].ToString ().Trim ('"');
					TempPushPlayer.Email = data [i] ["Users"] ["email"].ToString ().Trim ('"');
					int.TryParse (data [i] ["Users"] ["id"].ToString ().Trim ('"'), out TempPushPlayer.PlayerId );
					TempPushPlayer.SubsPlan = data [i] ["UserState"] ["subscribe_plan"].ToString ().Trim ('"');
					GameObject go = Instantiate(PushPlayerObj, Vector3.one, Quaternion.identity) as GameObject ;
					go.transform.parent = PushPlayerHolder.transform;
					go.transform.localScale = new Vector3 (1, 1, 1);
					go.GetComponent <PlayerDetails>().SetLeaderBoard (TempPushPlayer);
					SearchPlayerList.Add (TempPushPlayer);
					HartPlayerRegistration.Instance.WatingPanelShow (false);
				}
			
			}else if(_jsNode ["description"].ToString ().Contains ("No record found."))
			{
				HartPlayerRegistration.Instance.WatingPanelShow (false);
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = _jsNode ["description"].ToString ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {

					//		HartPlayerRegistration.Instance.WatingPanelShow (true);
					HartPlayerRegistration.Instance.WatingPanelShow (false);
				});

			}else if(_jsNode ["description"].ToString ().Contains ("Name is required") || _jsNode ["description"].ToString ().Contains ("name is required"))	
			{
				HartPlayerRegistration.Instance.WatingPanelShow (false);
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "Please enter the name to whom you wish to send plays.";
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {
					HartPlayerRegistration.Instance.WatingPanelShow (false);
				});
			}	
		}
	}

	public void ShowSendPlaysScreen( string PlayerName, int PlaysLeft  )
	{
		PlayerSendAndSearchPanel.SetActive (false);
		PlaysSendPanel.SetActive (true);
		NotificationPanel.SetActive (false);
		PlayerReciverName.text = PlayerName;
		LeftIconCount.text = PlaysLeft.ToString ()+" Plays Left";
		Count = 0;
		PlaysContToSend.text = Count.ToString ();
	}

	public void BackToSearchPlayerList()
	{
		PlayerSendAndSearchPanel.SetActive (true);
		PlaysSendPanel.SetActive (false);
		NotificationPanel.SetActive (false);
	}

	public void ShowAllNotification()
	{

		for(int i= 0; i< NotificationHolder.transform.childCount; i++ )
		{
			Destroy (NotificationHolder.transform.GetChild (i).gameObject);
		}
		MenuScreen_Controller.Instance.PlaysSharePanel.SetActive (true);
		PlayerSendAndSearchPanel.SetActive (false);
		PlaysSendPanel.SetActive (false);
		NotificationPanel.SetActive (true);

	}

	public void SendPlaysAdd()
	{
		if (Count < PlayerPrefs.GetInt ("GamePlaysCount")) {
			Count++;
			PlaysContToSend.text = Count.ToString ();
		}
	}
	public void SendPlaysRemove()
	{
		if (Count > 0) {
			Count--;
			PlaysContToSend.text = Count.ToString ();
		}
	}

	public void SendPlaysToPlayer ()
	{		
		HartPlayerRegistration.Instance.WatingPanelShow (true);
		var encoding = new System.Text.UTF8Encoding ();
		Dictionary<string,string> postHeader = new Dictionary<string,string> ();
		var jsonElement = new JSONClass ();

		jsonElement["sender_email"] = PlayerPrefs.GetString ("UserEmail");
		jsonElement["receiver_email"] = PushManager.Instance.SelectedPushPlayer.Email;
		jsonElement["plays_count"] = Count.ToString ();
		jsonElement["competition_id"] = PlayerPrefs.GetInt ("CompID").ToString ();

		postHeader.Add ("Content-Type", "application/json");
		postHeader.Add ("Content-Length", jsonElement.Count.ToString ());

		WWW www = new WWW (SendPlaysToPlayerLink, encoding.GetBytes (jsonElement.ToString ()), postHeader);

		StartCoroutine (WaitForPlaysToSend (www));
	}

	IEnumerator WaitForPlaysToSend (WWW www)
	{
		yield return(www);
		print (www.text);
		JSONNode _jsNode = JSON.Parse (www.text);
		if (_jsNode ["status"].ToString ().Contains ("200")) {
			StartCoroutine (GameSaveState.Instance.GetUserStatus (true));
			if (_jsNode ["description"].ToString ().Contains ("Plays send successfully.")) {
				JSONNode data = _jsNode ["data"];
				HartPlayerRegistration.Instance.WatingPanelShow (false);
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = _jsNode ["description"].ToString ().Trim ('"') + " To " + SelectedPushPlayer.Name;
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {
					MenuScreen_Controller.Instance.BackFromSearchPanel ();
					MenuScreen_Controller.Instance.PlaysUpdate ();
				});

			}else if (_jsNode ["description"].ToString ().Contains ("Send Required data.")) {
				JSONNode data = _jsNode ["data"];
				HartPlayerRegistration.Instance.WatingPanelShow (false);
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "Please select some value to send";
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {			

				});

			}else if (_jsNode ["description"].ToString ().Contains ("You cant send lives to this user")) {
				JSONNode data = _jsNode ["data"];
				HartPlayerRegistration.Instance.WatingPanelShow (false);
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = _jsNode ["error"] ["error_msg"].ToString ().Trim ('"') + " " +_jsNode ["description"].ToString ().Trim ('"');
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {		

				});

			}
		}
	}

	public void GetAllNotifiation ()
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

		StartCoroutine (WaitForAllNotification (www));
	}

	IEnumerator WaitForAllNotification (WWW www)
	{
		yield return(www);
		print (www.text);
		JSONNode _jsNode = JSON.Parse (www.text);
		if (_jsNode ["status"].ToString ().Contains ("200")) {
			StartCoroutine (GameSaveState.Instance.GetUserStatus (true));
			if (_jsNode ["description"].ToString ().Contains ("all notifications")) {
				JSONNode data = _jsNode ["data"];
				if (data.Count > 0) {
					for (int i =0; i< data.Count; i++)
					{
						Notification TempNotification = new Notification ();
						int.TryParse (data [i] ["Notification"] ["id"].ToString ().Trim ('"'), out TempNotification.NotificationId );
						TempNotification.NotificationType = data [i] ["Notification"]["type"].ToString ().Trim ('"');
						int.TryParse (data [i] ["Notification"] ["sender_id"].ToString ().Trim ('"'), out TempNotification.SenderId );
						int.TryParse (data [i] ["Notification"] ["receiver_id"].ToString ().Trim ('"'), out TempNotification.ReciverId );
						TempNotification.Msg = data [i] ["Notification"]["message"].ToString ().Trim ('"');
						int startDate;
						int.TryParse (data [i] ["Notification"]["created"].ToString ().Trim ('"'), out startDate);
						TempNotification.Pushtime = CompetitionController.UnixTimeStampToDateTime (startDate);

						GameObject go = Instantiate(NotificationObj, Vector3.one, Quaternion.identity) as GameObject ;
						go.transform.parent = NotificationHolder.transform;
						go.transform.localScale = new Vector3 (1, 1, 1);
						go.GetComponent <Notifications> ().SetDataToThisNotification (TempNotification);
					}
					HartPlayerRegistration.Instance.WatingPanelShow (false);
				}else
				{
					HartPlayerRegistration.Instance.WatingPanelShow (false);
					HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
					HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
					HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
					HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
					HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "Welcome "+ 	PlayerPrefs.GetString ("UserName")+ " to notifications " + "\n No notification found.";
					HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
					HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {
						MenuScreen_Controller.Instance.BackFromSearchPanel ();
						MenuScreen_Controller.Instance.PlaysUpdate ();

					});
				}
			}else if (_jsNode ["description"].ToString ().Contains ("Send Required data")) {
				JSONNode data = _jsNode ["data"];
				HartPlayerRegistration.Instance.WatingPanelShow (false);
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "Required data is invalid";
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {

				});

			}
		}
	}

}


[Serializable]
public class PushPlayer
{
	public string Name;
	public string Email;
	public int PlayerId;
	public string SubsPlan;
}

[Serializable]
public class Notification
{
	public int NotificationId;
	public string NotificationType;
	public int SenderId;
	public int ReciverId;
	public string Msg;
	public DateTime Pushtime;
}
