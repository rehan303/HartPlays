using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDetails : MonoBehaviour {

	public Text Name;
	public Text ProfileText;
	public Button ButtonSelect;
	public string EmailId;
	public int PlayerID;
	public string SubsPlanType;
	public PushPlayer ThisPushName;

	//fdsafsad
	public void SetLeaderBoard (PushPlayer thisPushdata)
	{
		ThisPushName = thisPushdata;
		Name.text = thisPushdata.Name;
		EmailId = thisPushdata.Email;
		PlayerID = thisPushdata.PlayerId;
		SubsPlanType = thisPushdata.SubsPlan;
		var tt = thisPushdata.Name;
		string t = "";
		if(tt.Length >1){
			 t = tt.Remove (1);
			ProfileText.text = t;
		}else
			ProfileText.text = tt;
		ButtonSelect.onClick.RemoveAllListeners ();
		ButtonSelect.onClick.AddListener (()=>{
			if( thisPushdata.SubsPlan == "24Hours_Plays_Subscription")
			{
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "You can't send plays to this player!! He/She has 24 Hours Plays of subscription.";

				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {


				});
			}else if( thisPushdata.PlayerId == PlayerPrefs.GetInt ("PlayerId"))
			{
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "You can't send plays to yourself!!";
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {


				});
			}else{
			PushManager.Instance.SelectedPushPlayer = null;
			PushManager.Instance.SelectedPushPlayer = thisPushdata;
			PushManager.Instance.ShowSendPlaysScreen (ThisPushName.Name, PlayerPrefs.GetInt ("GamePlaysCount"));
			}
		});
	}
}
