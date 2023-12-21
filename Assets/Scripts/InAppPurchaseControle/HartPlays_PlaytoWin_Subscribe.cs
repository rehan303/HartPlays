using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class HartPlays_PlaytoWin_Subscribe : MonoBehaviour
{
	public static HartPlays_PlaytoWin_Subscribe Instance = null;
	public GameObject HartPlaysSceenAtrribute;

	public GameObject SubscriptionScreen;
	public static int PlaysCount;
	public static int PlaysDays;
	public static int PlaysHours;
	public int IntialPlays;
	public GameObject PopupMassage;
	public Text ShowSuccefullMsg;
	public Button btn;
	public Button YesBtn;
	public Button NoBtn;
	public Text ComName;
	public Text ComStartDate;
	public Text ComEndDate;
	public static bool FreePlays = false;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != null) {
			Destroy (this.gameObject);
		}
	}

	public void HartPlaysPlayToWinSubscribe (int ProductIdToBuy)
	{	
		if (Application.internetReachability == NetworkReachability.NotReachable) {
			HartPlaysSceenAtrribute.GetComponent<HartPlayerRegistration> ().InternetConnectionCheck ("Internet connection Required!");
		} else {
			HartPlays_InAppPurchase.Instance.PurchaseHartPlaysProduct (ProductIdToBuy);
		}


	}

	void Update()
	{
		ComName.text = CompetitionController.ComName;
		ComStartDate.text ="Start / " + CompetitionController.StartTime.Date.ToString ("yyyy-MM-dd");
		ComEndDate.text = "End / " +CompetitionController.EndTime.Date.ToString ("yyyy-MM-dd");

	}
	public void IFHartPlaysPlayToWinSubscribe () 
	{	
		if (SmartIAPListener.PlayToWinSubscribe) {
			HartPlaysSceenAtrribute.GetComponent<HartPlayerRegistration> ().OnPurchaseSucessReturned ();
		} else {
			print ("Stay on same Screen");
		}
	}


	public void OnClickFreePlays()
	{
		HartPlayerRegistration.Instance.EmptyAllFeilds ();
		HartPlayerRegistration.IsPaidForCompetition = 1;
		string SubscriptionType = "10_Plays_Subscription";
		HartPlayerRegistration.Isplayforpaid = true;
		PlayerPrefs.SetString ("PlayToWinSubscriptionType",SubscriptionType);
		PlaysCount = 10;
		PlayerPrefs.SetInt ("GamePlaysCount", PlaysCount);
		HartPlayerRegistration.registrationTime = DateTime.UtcNow;
		PlayerPrefs.SetString ("RegistrationTime", HartPlayerRegistration.registrationTime.ToString ());
		StartCoroutine (GameSaveState.Instance.IsFreePlaysUpdate ());
		StartCoroutine (GameSaveState.Instance.PostUserStatus ());
		SceneManager.LoadScene ("00_MenuScreen");

	}

	public void ClosePopup()
	{
		PopupMassage.SetActive (false);
		ShowSuccefullMsg.text = "";
	}

	#region Subscription Successfull

	public void Buy_10_Plays_Subscription ()
	{
		
		HartPlayerRegistration.IsPaidForCompetition = 1;
		HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
		HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
		PopupMassage.SetActive (true);
		ShowSuccefullMsg.text = "Congratulations!! 10 Plays Subscription Plan Is Successfully Activated.";
		btn.onClick.RemoveAllListeners ();
		btn.onClick.AddListener (() => {

			if(PlayerPrefs.GetInt ("IsFreePlays") == 0)
				IntialPlays = 10;
			else 
				IntialPlays = 0;
			StartCoroutine (GameSaveState.Instance.IsFreePlaysUpdate ());
			string SubscriptionType = "10_Plays_Subscription";
			HartPlayerRegistration.Isplayforpaid = true;
			PlayerPrefs.SetString ("PlayToWinSubscriptionType",SubscriptionType);
			PlaysCount = 10 + IntialPlays;
			PlayerPrefs.SetInt ("GamePlaysCount", PlaysCount);
			HartPlayerRegistration.registrationTime = DateTime.UtcNow;
			PlayerPrefs.SetString ("RegistrationTime", HartPlayerRegistration.registrationTime.ToString ());
			StartCoroutine (GameSaveState.Instance.PostUserStatus ());
			SceneManager.LoadScene ("00_MenuScreen");

			});

	}

	public void Buy_20_Plays_Subscription ()
	{
		HartPlayerRegistration.IsPaidForCompetition = 1;
		PopupMassage.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
		HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
		ShowSuccefullMsg.text = "Congratulations!! 20 Plays Subscription Plan Is Successfully Activated.";
		btn.onClick.RemoveAllListeners ();
		btn.onClick.AddListener (() => {

			if(PlayerPrefs.GetInt ("IsFreePlays") == 0)
				IntialPlays = 10;
			else 
				IntialPlays = 0;
			StartCoroutine (GameSaveState.Instance.IsFreePlaysUpdate ());
			string SubscriptionType = "20_Plays_Subscription";
			HartPlayerRegistration.Isplayforpaid = true;
			PlayerPrefs.SetString ("PlayToWinSubscriptionType",SubscriptionType);
			PlaysCount = 20 + IntialPlays;
			PlayerPrefs.SetInt ("GamePlaysCount", PlaysCount);
			HartPlayerRegistration.registrationTime = DateTime.UtcNow;
			PlayerPrefs.SetString ("RegistrationTime", HartPlayerRegistration.registrationTime.ToString ());
			StartCoroutine (GameSaveState.Instance.PostUserStatus ());
			SceneManager.LoadScene ("00_MenuScreen");
			});	}
	public void Buy_30_Plays_Subscription ()
	{
		HartPlayerRegistration.IsPaidForCompetition = 1;
		PopupMassage.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
		HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
		ShowSuccefullMsg.text = "Congratulations!! 30 Plays Subscription Plan Is Successfully Activated.";
		btn.onClick.RemoveAllListeners ();
		btn.onClick.AddListener (() => {

			if(PlayerPrefs.GetInt ("IsFreePlays") == 0)
				IntialPlays = 10;
			else 
				IntialPlays = 0;
			StartCoroutine (GameSaveState.Instance.IsFreePlaysUpdate ());
			string SubscriptionType = "30_Plays_Subscription";
			HartPlayerRegistration.Isplayforpaid = true;
			PlayerPrefs.SetString ("PlayToWinSubscriptionType",SubscriptionType);
			PlaysCount = 30 + IntialPlays;
			PlayerPrefs.SetInt ("GamePlaysCount", PlaysCount);
			HartPlayerRegistration.registrationTime = DateTime.UtcNow;
			PlayerPrefs.SetString ("RegistrationTime", HartPlayerRegistration.registrationTime.ToString ());
			StartCoroutine (GameSaveState.Instance.PostUserStatus ());
			SceneManager.LoadScene ("00_MenuScreen");

			});	}

	public void Buy_24Hours_Plays_Subscription ()
	{
		HartPlayerRegistration.IsPaidForCompetition = 1;
		PopupMassage.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
		HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
		StartCoroutine (GameSaveState.Instance.IsFreePlaysUpdate ());
		string SubscriptionType = "24Hours_Plays_Subscription";
		HartPlayerRegistration.Isplayforpaid = true;
		PlayerPrefs.SetString ("PlayToWinSubscriptionType",SubscriptionType);
		HartPlayerRegistration.registrationTime = DateTime.UtcNow;
		PlayerPrefs.SetString ("RegistrationTime", HartPlayerRegistration.registrationTime.ToString ());
		StartCoroutine (GameSaveState.Instance.PostUserStatus ());
		ShowSuccefullMsg.text = "Congratulations!! 24 Hours Subscription Plan Is Successfully Activated.";
		btn.onClick.RemoveAllListeners ();
		btn.onClick.AddListener (() => {
			SceneManager.LoadScene ("00_MenuScreen");
			});	}

	public void Buy_7Days_Plyas_Subscription ()
	{
		HartPlayerRegistration.IsPaidForCompetition = 1;
		PopupMassage.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
		HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
		ShowSuccefullMsg.text = "Congratulations!! 7 Days Subscription Plan Is Successfully Activated.";
		btn.onClick.RemoveAllListeners ();
		btn.onClick.AddListener (() => {

			StartCoroutine (GameSaveState.Instance.IsFreePlaysUpdate ());
			string SubscriptionType = "7Days_Plyas_Subscription";
			HartPlayerRegistration.Isplayforpaid = true;
			PlayerPrefs.SetString ("PlayToWinSubscriptionType",SubscriptionType);
			HartPlayerRegistration.registrationTime = DateTime.UtcNow;
			PlayerPrefs.SetString ("RegistrationTime", HartPlayerRegistration.registrationTime.ToString ());
			StartCoroutine (GameSaveState.Instance.PostUserStatus ());
			SceneManager.LoadScene ("00_MenuScreen");

			});	}

	#endregion

	#region Color Ball Live Purchased Success Full

	public void Buy5LivesForColorBallGame ()
	{	

		PopupMassage.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
		HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
		ShowSuccefullMsg.text = "Congratulations!! You Have Successfully Purchased 5 Lives For Color Ball Game.";
		int _purchasedFreeze = 5;
		if (HartPlayerRegistration.Isplayforpaid) 
		{
			PlayerPrefs.SetInt ("ColorBallLives", _purchasedFreeze);
			StartCoroutine (GameSaveState.Instance.PostUserStatus ());
			//				Invoke ("ShowWheel",1.5f);
		} 
		btn.onClick.RemoveAllListeners ();
		btn.onClick.AddListener (() => {

			if(PlayerPrefs.GetInt ("ColorBallLives")>0)
			{
				MenuScreen_Controller.Instance.ColorNextButton.SetActive (true);
				MenuScreen_Controller.Instance.ColorSkipButton.SetActive (false);
			}else
			{
				MenuScreen_Controller.Instance.ColorNextButton.SetActive (false);
				MenuScreen_Controller.Instance.ColorSkipButton.SetActive (true);
			}
		});
	
	}

	void ShowWheel()
	{
		MenuScreen_Controller.Instance.ShowWheelOfFortutne ();
	}
	void GetuserStat()
	{
		StartCoroutine (GameSaveState.Instance.GetUserStatus (true));
	}
	public void Buy10LivesForColorBallGame ()
	{		
		PopupMassage.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
		HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
		ShowSuccefullMsg.text = "Congratulations!! You Have Successfully Purchased 10 Lives For Color Ball Game.";
		int _purchasedFreeze = 10;
		if (HartPlayerRegistration.Isplayforpaid) 
		{
			PlayerPrefs.SetInt ("ColorBallLives", _purchasedFreeze);
			StartCoroutine (GameSaveState.Instance.PostUserStatus ());
			//				Invoke ("ShowWheel",1.5f);
		}
		btn.onClick.RemoveAllListeners ();
		btn.onClick.AddListener (() => {

		 
			if(PlayerPrefs.GetInt ("ColorBallLives")>0)
			{
				MenuScreen_Controller.Instance.ColorNextButton.SetActive (true);
				MenuScreen_Controller.Instance.ColorSkipButton.SetActive (false);
			}else
			{
				MenuScreen_Controller.Instance.ColorNextButton.SetActive (false);
				MenuScreen_Controller.Instance.ColorSkipButton.SetActive (true);
			}

		});
	
	}
	#endregion

	#region Dodger Live Purchased Success Full

	public void Buy5LivesForDodgerGame ()
	{
		PopupMassage.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
		HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
		ShowSuccefullMsg.text = "Congratulations!! You Have Successfully Purchased 5 Lives For Dodger Game.";
		int _purchasedFreeze = 5;
		if (HartPlayerRegistration.Isplayforpaid) 
		{
			PlayerPrefs.SetInt ("DodgerLives", _purchasedFreeze);
			StartCoroutine (GameSaveState.Instance.PostUserStatus ());
			//				Invoke ("ShowWheel",1.5f);
		} 
		btn.onClick.RemoveAllListeners ();
		btn.onClick.AddListener (() => {


			if(PlayerPrefs.GetInt ("DodgerLives")>0)
			{
				MenuScreen_Controller.Instance.DogerNextButton.SetActive (true);
				MenuScreen_Controller.Instance.DogerSkipButton.SetActive (false);
			}else
			{
				MenuScreen_Controller.Instance.DogerNextButton.SetActive (false);
				MenuScreen_Controller.Instance.DogerSkipButton.SetActive (true);
			}

		});

	}

	public void Buy10LivesForDodgerGame ()
	{
		PopupMassage.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
		HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
		HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
		ShowSuccefullMsg.text = "Congratulations!! You Have Successfully Purchased 10 Lives For Dodger Game.";
		int _purchasedFreeze = 10;
		if (HartPlayerRegistration.Isplayforpaid) 
		{
			PlayerPrefs.SetInt ("DodgerLives", _purchasedFreeze);
			StartCoroutine (GameSaveState.Instance.PostUserStatus ());
			//				Invoke ("ShowWheel",1.5f);
		} 
		btn.onClick.RemoveAllListeners ();
		btn.onClick.AddListener (() => {
			if(PlayerPrefs.GetInt ("DodgerLives")>0)
			{
				MenuScreen_Controller.Instance.DogerNextButton.SetActive (true);
				MenuScreen_Controller.Instance.DogerSkipButton.SetActive (false);
			}else
			{
				MenuScreen_Controller.Instance.DogerNextButton.SetActive (false);
				MenuScreen_Controller.Instance.DogerSkipButton.SetActive (true);
			}


		});
	}
	#endregion

	public void SkipColorBallLiveScreen()
	{
		SubscriptionScreen.SetActive (false);
	}
	public void SkipDodgerLiveScreen()
	{			
		SubscriptionScreen.SetActive (false);
		HartPlayerRegistration.Isplayforpaid = true;
		StartCoroutine (GameSaveState.Instance.PostUserStatus());
		SceneManager.LoadScene ("00_MenuScreen");

	}

	public void BackFromDodgerPuchaseScreen()
	{
		SubscriptionScreen.SetActive (false);
	}


}
