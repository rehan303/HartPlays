using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Simple_JSON;
using UnityEngine.SceneManagement;
using System.Text;
using System.Collections.Generic;
using System;
using app42InAppMessagingListner;


public class HartPlayerRegistration : MonoBehaviour
{
	public static HartPlayerRegistration Instance = null;
//	const string compitionStatusUrl = "https://hartplayers.ignivastaging.com/competitions/getCompetitionStatus";
	const string signUpUrl = "https://www.hartplays.co.uk/api/users/register";
	const string signInUrl = "https://www.hartplays.co.uk/api/users/login";
	const string resetPasswordUrl = "https://www.hartplays.co.uk/api/users/forgotPassword";
	public static DateTime registrationTime;

	#region Regitration Screen

	public InputField userNameFeild;
	public InputField emailFeild;
	public InputField passwordFeild;
	public InputField confrimpPasswordFeild;
	public GameObject signUpButton;

	public string userNameString;
	public string emailString;
	public string passwordString;

	#endregion

	#region Login Screen

	public InputField _login_EmailFeild;
	public InputField _login_passwordFeild;

	#endregion

	public InputField _forgotPassword_EmailFeild;

	public GameObject LoginPanel;
	public GameObject FreeOrPaidPanel;
	public GameObject PaymentPanel;
	public GameObject RegistrationPanel;
	public GameObject ForgotPasswordPanel;
	public Text MessageText;
	public GameObject FreePlaysButton;
	public GameObject WatingPanel;
	public static bool Isplayforpaid = false;
	CompetitionController competitionController;
	public static int IsPaidForCompetition;
	//This variable for check weather user has done logout or application quit
	public static int UserLogoutORQuit = 0;

	[Header ("HartPlays")]
	public Sprite PlayToWinActiveSprite;
	public Sprite FreeToPlayActiveSprite;
	public Sprite PlayToWinDeactiveSprite;
	public Toggle AgeCheck;

	[Header ("InternetCheck")]
	public GameObject InternetPanel;
	public Text InternetMessage;

	void OnEnable ()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		if (Instance == null) {
			Instance = this;
		} else if (Instance != null) {
			Destroy (this.gameObject);
		}
		InternetPanel.SetActive (false);
		MessageText.gameObject.SetActive (false);
		signUpButton.GetComponent<Button> ().interactable = false;
		FreeOrPaidPanel.SetActive (true);
		competitionController = GetComponent<CompetitionController> ();
		DontDestroyOnLoad (this.gameObject);
	}

	void Start ()
	{			
//		PlayerPrefs.DeleteAll ();

//		//Color Ball
//		if (PlayerPrefs.GetInt ("BombCount") == 0 || PlayerPrefs.GetInt ("FreezeCount") == 0 || PlayerPrefs.GetInt ("ReminderCount", 10) == 0) {
//			PlayerPrefs.SetInt ("BombCount", 10);
//			PlayerPrefs.SetInt ("FreezeCount", 10);
//			PlayerPrefs.SetInt ("ReminderCount", 10);
//		}
//		//Dodger
//		if (PlayerPrefs.GetInt ("DodgerInvincibleCount") == 0 || PlayerPrefs.GetInt ("DodgerBombCount") == 0 || PlayerPrefs.GetInt ("DodgerSlowCount", 10) == 0 || PlayerPrefs.GetInt ("DodgerMergeCount") == 0) {
//			PlayerPrefs.SetInt ("DodgerInvincibleCount", 10);
//			PlayerPrefs.SetInt ("DodgerBombCount", 10);
//			PlayerPrefs.SetInt ("DodgerSlowCount", 10);
//			PlayerPrefs.SetInt ("DodgerMergeCount", 10);
//
//		}
		Time.timeScale = 1;
		AgeCheck.isOn = false;
	}


	public void WatingPanelShow(bool boolState)
	{
		if(boolState)
			WatingPanel.SetActive (boolState);
		else
			WatingPanel.SetActive (boolState);
	}

	public void ShowLoginPanel ()
	{
		EmptyAllFeilds ();
		RegistrationPanel.SetActive (false);
		LoginPanel.SetActive (true);
		ForgotPasswordPanel.SetActive (false);
		MessageText.gameObject.SetActive (false);
	}

	public void ShowSignUpPanel ()
	{
		EmptyAllFeilds ();
		RegistrationPanel.SetActive (true);
		LoginPanel.SetActive (false);
		ForgotPasswordPanel.SetActive (false);
		MessageText.gameObject.SetActive (false);
	}

	public void ShowForgotPasswordPanel ()
	{
		RegistrationPanel.SetActive (false);
		LoginPanel.SetActive (false);
		ForgotPasswordPanel.SetActive (true);
		MessageText.gameObject.SetActive (false);
	}

	public void EmptyAllFeilds ()
	{
		userNameFeild.text = "";
		emailFeild.text = "";
		passwordFeild.text = "";
		confrimpPasswordFeild.text = "";
		_forgotPassword_EmailFeild.text = "";
		_login_passwordFeild.text = "";
		_login_EmailFeild.text = "";
		_forgotPassword_EmailFeild.text = "";
		userNameString = "";
		emailString = "";
		passwordString = "";
		MessageText.text = "";
	}

	#region for Register

	public void SubmitName ()
	{
		userNameString = userNameFeild.text;
	}

	public void SubmitEmail ()
	{
		emailString = emailFeild.text;
	}

	public void SubmitConfirmPassword ()
	{
		if (passwordFeild.text == confrimpPasswordFeild.text) {
//			signUpButton.GetComponent<Button> ().interactable = true;
			passwordString = passwordFeild.text;
		} else {
			MessageText.gameObject.SetActive (true);
			MessageText.text = "Password does not match please enter again";
			confrimpPasswordFeild.text = "";

			Invoke ("EmptyWarningMsg", 5f);
		}
	}

	public void CheckAgeConfirmation()
	{
		if (AgeCheck.isOn) {
			AgeCheck.isOn = true;
			signUpButton.GetComponent<Button> ().interactable = true;
		}else
		{
			signUpButton.GetComponent<Button> ().interactable = false;
		}
	}

	void EmptyWarningMsg()
	{
		MessageText.text = "";
	}


	public void onClickSignUp ()
	{
		print ("onClickSignUp");
		if (confrimpPasswordFeild.text != "") {
			SignUp (userNameString, emailString, passwordString);
		}else
		{
			MessageText.gameObject.SetActive (true);
			MessageText.text = "Please Fill The Confirm Your Password";
			confrimpPasswordFeild.text = "";

			Invoke ("EmptyWarningMsg", 5f);
		}

	}

	void SignUp (string _userName, string _email, string _password)
	{
		WatingPanelShow (true);
		var encoding = new System.Text.UTF8Encoding ();
		System.Collections.Generic.Dictionary<string,string> postHeader = new System.Collections.Generic.Dictionary<string,string> ();

		UserInfo Info = new UserInfo ();
		Info.username = _userName;
		Info.email = _email;
		Info.password = _password;
//		PushScript.CreateNewUserOnRegistration (_userName,_password, _email);
		Info.device_token = "98KJLKLKLLN";
		string json = JsonUtility.ToJson (Info);

		postHeader.Add ("Content-Type", "application/json");
		postHeader.Add ("Content-Length", json.Length.ToString ());

		WWW request = new WWW (signUpUrl, encoding.GetBytes (json), postHeader);

		StartCoroutine (WaitForRequest (request));
	}

	IEnumerator WaitForRequest (WWW www)
	{
		print ("Waiting For Response for SignUP");
		yield return(www);

		if (www.error == null) {
			JSONNode _jsNode = JSON.Parse (www.text);
			print ("JSON DATA IS -->>" + _jsNode.ToString ());

			if (_jsNode [2].ToString ().Contains ("error")) {
				MessageText.gameObject.SetActive (true);
				MessageText.text = _jsNode ["error"] ["error_msg"].ToString ().Trim ("\"".ToCharArray ());
				WatingPanelShow (false);
			} else {
				
				print ("SignUp Sucess" + www.text);
				PlayerPrefs.SetString ("UserName", userNameString);
				PlayerPrefs.SetString ("UserEmail", emailString);
				PlayerPrefs.SetString ("UserPassword", passwordString);

				int playerId = 0;
				int.TryParse ( _jsNode["data"]["id"].ToString ().Trim ('"'), out playerId);
				PlayerPrefs.SetInt ("PlayerId", playerId);
//				if (!PlayerPrefs.HasKey ("CompetitionGoingon")) {
//					PaymentPanel.SetActive (true);
//					FreeOrPaidPanel.SetActive (false);
//				} else {
//					SceneManager.LoadScene ("00_MenuScreen");	
//				}
				PaymentPanel.SetActive (true);
				FreePlaysButton.SetActive (true);
				FreeOrPaidPanel.SetActive (false);
				//Show Subscription Msg 
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "WOW!! YOU HAVE EARNED 10 FREE PLAYS. YOU CAN SHARE AND RECEIVE PLAYS WITH YOUR FRIENDS.";
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {

				});

				PushSample.CreateNewUserOnRegistration (userNameString,passwordString, emailString);
				PlayerPrefs.SetInt ("UserLogoutORQuit",0);
				EmptyAllFeilds ();
				WatingPanelShow (false);
				yield return new WaitForSeconds (0.2f);
				PushSample.registerForRemoteNotifications ();
			}
		} else if (www.error != null) {
			print ("SignUp Error" + www.error);
			WatingPanelShow (false);

		}
	}

	#endregion


	#region for LOGIN

	public void SubmitLoginEmail ()
	{
		emailString = _login_EmailFeild.text;
	}

	public void SubmitLoginPassword ()
	{
		passwordString = _login_passwordFeild.text;
	}

	public void OnCLickSignIn ()
	{
		SignIN (emailString, passwordString);
	}

	void SignIN (string _email, string _password)
	{	
		WatingPanelShow (true);
		var encoding = new System.Text.UTF8Encoding ();

		Dictionary<string,string> postHeader = new Dictionary<string,string> ();

		UserInfo Info = new UserInfo ();

		Info.email = _email;
		Info.password = _password;
//		PushScript.CreateNewUserOnRegistration (null,_password, _email);
		Info.device_token = "kfdsakfksamdknckamn";
		string json = JsonUtility.ToJson (Info);

		postHeader.Add ("Content-Type", "application/json");
		postHeader.Add ("Content-Length", json.Length.ToString ());

		WWW www = new WWW (signInUrl, encoding.GetBytes (json), postHeader);

		StartCoroutine (WaitForLoginRequest (www));
	}

	IEnumerator WaitForLoginRequest (WWW www)
	{
		print ("Waiting For Response for SignIN");

		yield return(www);

		if (www.error == null) {
			JSONNode _jsNode = JSON.Parse (www.text);
			//			print ("JSON DATA IS -->>" + _jsNode.ToString());
			print (www.text);
			if (_jsNode [2].ToString ().Contains ("error")) {
				MessageText.gameObject.SetActive (true);
				MessageText.text = MessageText.text = _jsNode ["error"] ["error_msg"].ToString ().Trim ("\"".ToCharArray ());
				WatingPanelShow (false);
			} else {	
				
				UserLogoutORQuit = PlayerPrefs.GetInt ("UserLogoutORQuit");
				IsPaidForCompetition = int.Parse (_jsNode ["data"] ["is_paid"].ToString ().Trim ("\"".ToCharArray ()));			
				PlayerPrefs.SetInt ("IsPaidForCompetition", IsPaidForCompetition);
				int iFid = int.Parse (_jsNode ["data"] ["is_FreePlays"].ToString ().Trim ("\"".ToCharArray ()));			
				PlayerPrefs.SetInt ("IsFreePlays", iFid);
//				int isFree = int.Parse (_jsNode ["data"] ["is_FreePlays"].ToString () == "true");

				PlayerPrefs.SetInt ("PlayerId",int.Parse (_jsNode ["data"] ["id"].ToString ().Trim ("\"".ToCharArray ())));
				PlayerPrefs.SetString ("UserName", _jsNode ["data"] ["username"].ToString ().Trim ("\"".ToCharArray ()));
				PlayerPrefs.SetString ("UserEmail", emailString);
				PlayerPrefs.SetString ("UserPassword", passwordString);
				if (UserLogoutORQuit == 2) {
					if (PlayerPrefs.GetInt ("IsFreePlays") == 1) {
						FreePlaysButton.SetActive (false);
					}else
						FreePlaysButton.SetActive (true);
								
					//Set Currunt Game Id
					MenuScreen_Controller.GameId = int.Parse (_jsNode ["data"] ["current_game_id"].ToString ().Trim ("\"".ToCharArray ()));
					PlayerPrefs.SetInt ("CurruntGameId", MenuScreen_Controller.GameId);

					//Set Game Level as per game ID
					int Currentlevel = int.Parse (_jsNode ["data"] ["level_no"].ToString ().Trim ("\"".ToCharArray ()));

					if (PlayerPrefs.GetInt ("CurruntGameId") == 1) {
						if (Currentlevel != 0) {
							PlayerPrefs.SetInt ("ColorBallLevelLockedPaid", Currentlevel);
						} else {
							PlayerPrefs.SetInt ("ColorBallLevelLockedPaid", 1);
						}
					} else if (PlayerPrefs.GetInt ("CurruntGameId") == 3) {
						if (Currentlevel != 0) {
							PlayerPrefs.SetInt ("NinjaLevelUnlockedPaid", Currentlevel);
						} else {
							PlayerPrefs.SetInt ("NinjaLevelUnlockedPaid", 1);
						}
					}
					// set payment status
					IsPaidForCompetition = int.Parse (_jsNode ["data"] ["is_paid"].ToString ().Trim ("\"".ToCharArray ()));
					PlayerPrefs.SetInt ("IsPaidForCompetition", IsPaidForCompetition);

					//Set Dodger game Life
					int DodgerGameLife = int.Parse (_jsNode ["data"] ["current_lifes"].ToString ().Trim ("\"".ToCharArray ()));
					PlayerPrefs.SetInt ("DodgerHealth", DodgerGameLife);

					//Set Color ball Power up
					//TODO 27-12-2016
					int ColorBallPowers1, ColorBallPowers2, ColorBallPowers3;
					if (_jsNode ["data"] ["ColorBall_PowerUps"].ToString ().Contains ("false")) {
						PlayerPrefs.SetInt ("BombCountForPaid", 0);
						PlayerPrefs.SetInt ("FreezeCountForPaid", 0);
						PlayerPrefs.SetInt ("ReminderCountForPaid", 0);
					} else {
						ColorBallPowers1 = int.Parse (_jsNode ["data"] ["ColorBall_PowerUps"] [0].ToString ().Trim ("\"".ToCharArray ()));
						ColorBallPowers2 = int.Parse (_jsNode ["data"] ["ColorBall_PowerUps"] [1].ToString ().Trim ("\"".ToCharArray ()));
						ColorBallPowers3 = int.Parse (_jsNode ["data"] ["ColorBall_PowerUps"] [2].ToString ().Trim ("\"".ToCharArray ()));

						PlayerPrefs.SetInt ("BombCountForPaid", ColorBallPowers1);
						PlayerPrefs.SetInt ("FreezeCountForPaid", ColorBallPowers2);
						PlayerPrefs.SetInt ("ReminderCountForPaid", ColorBallPowers3);


					}

					//Set Dodger PowerUps
					int DodgerPowerUps1, DodgerPowerUps2, DodgerPowerUps3, DodgerPowerUps4;
					if (_jsNode ["data"] ["Dodger_PowerUps"].ToString ().Contains ("false")) {
						PlayerPrefs.SetInt ("DodgerBombCountForPaid", 0);
						PlayerPrefs.SetInt ("DodgerInvincibleCountForPaid", 0);
						PlayerPrefs.SetInt ("DodgerSlowCountForPaid", 0);
						PlayerPrefs.SetInt ("DodgerMergeCountForPaid", 0);
					} else {
						DodgerPowerUps1 = int.Parse (_jsNode ["data"] ["Dodger_PowerUps"] [0].ToString ().Trim ("\"".ToCharArray ()));
						DodgerPowerUps2 = int.Parse (_jsNode ["data"] ["Dodger_PowerUps"] [1].ToString ().Trim ("\"".ToCharArray ()));
						DodgerPowerUps3 = int.Parse (_jsNode ["data"] ["Dodger_PowerUps"] [2].ToString ().Trim ("\"".ToCharArray ()));
						DodgerPowerUps4 = int.Parse (_jsNode ["data"] ["Dodger_PowerUps"] [3].ToString ().Trim ("\"".ToCharArray ()));

						PlayerPrefs.SetInt ("DodgerBombCountForPaid", DodgerPowerUps1);
						PlayerPrefs.SetInt ("DodgerInvincibleCountForPaid", DodgerPowerUps2);
						PlayerPrefs.SetInt ("DodgerSlowCountForPaid", DodgerPowerUps3);
						PlayerPrefs.SetInt ("DodgerMergeCountForPaid", DodgerPowerUps4);
					}

					// Ninja Game PowerUps
					int NinjaPowerUps1, NinjaPowerUps2, NinjaPowerUps3, NinjaPowerUps4;
					if (_jsNode ["data"] ["NinjaFootie_PowerUps"].ToString ().Contains ("false")) {
						PlayerPrefs.SetInt ("NinjaGoldenStarCountForPaid", 0);
						PlayerPrefs.SetInt ("NinjaRhombusCountForPaid", 0);
						PlayerPrefs.SetInt ("NinjaSquareCountForPaid", 0);
						PlayerPrefs.SetInt ("NinjaWhiteStarCountForPaid", 0);
					} else {
						NinjaPowerUps1 = int.Parse (_jsNode ["data"] ["NinjaFootie_PowerUps"] [0].ToString ().Trim ("\"".ToCharArray ()));
						NinjaPowerUps2 = int.Parse (_jsNode ["data"] ["NinjaFootie_PowerUps"] [1].ToString ().Trim ("\"".ToCharArray ()));
						NinjaPowerUps3 = int.Parse (_jsNode ["data"] ["NinjaFootie_PowerUps"] [2].ToString ().Trim ("\"".ToCharArray ()));
						NinjaPowerUps4 = int.Parse (_jsNode ["data"] ["NinjaFootie_PowerUps"] [3].ToString ().Trim ("\"".ToCharArray ()));

						PlayerPrefs.SetInt ("NinjaGoldenStarCountForPaid", NinjaPowerUps1);
						PlayerPrefs.SetInt ("NinjaRhombusCountForPaid", NinjaPowerUps2);
						PlayerPrefs.SetInt ("NinjaSquareCountForPaid", NinjaPowerUps3);
						PlayerPrefs.SetInt ("NinjaWhiteStarCountForPaid", NinjaPowerUps4);
					}
					if (PlayerPrefs.GetInt ("IsPaidForCompetition") == 1) {
						SceneManager.LoadScene ("00_MenuScreen");
						PaymentPanel.SetActive (false);

					} else {
						//Show Subscription Msg 
						HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
						HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
						HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
						HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
						HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "WOW!! YOU HAVE EARNED 10 FREE PLAYS. YOU CAN SHARE AND RECEIVE PLAYS WITH YOUR FRIENDS.";
						HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
						HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {

						});
						//
						PaymentPanel.SetActive (true);
						FreeOrPaidPanel.SetActive (false);
					} 

				} else {
					if (PlayerPrefs.GetInt ("IsPaidForCompetition") == 1) {
						StartCoroutine (GameSaveState.Instance.GetUserStatus (true));
						Invoke ("ProcesstToMenuScreen",0.8f);
					} else {
						//Show Subscription Msg 
						HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
						HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
						HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
						HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
						HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "WOW!! YOU HAVE EARNED 10 FREE PLAYS. YOU CAN SHARE AND RECEIVE PLAYS WITH YOUR FRIENDS.";
						HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
						HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {

						});
						//
						PaymentPanel.SetActive (true);
						if (PlayerPrefs.GetInt ("IsFreePlays") == 1) {
							FreePlaysButton.SetActive (false);
						}
						else
							FreePlaysButton.SetActive (true);
						FreeOrPaidPanel.SetActive (false);
					} 
					//				} 
					//				else {
					//					SceneManager.LoadScene ("00_MenuScreen");	
					//				}
				}
				Invoke ("DisableLoginPanel",1f);
				StartCoroutine (GameSaveState.Instance.GetUserStatus (true));
				PushSample.registerForRemoteNotifications ();

			}
		} else {
			print ("SignIN Error" + www.error);
			WatingPanelShow (false);
		}
	}
	void ProcesstToMenuScreen ()
	{
		SceneManager.LoadScene ("00_MenuScreen");
		PaymentPanel.SetActive (false);
	
	}

	void DisableLoginPanel()
	{
		LoginPanel.SetActive (false);
		WatingPanelShow (false);
	}
	#endregion


	#region For Forgot Password

	public void onForgotSubmitEmail ()
	{
		emailString = _forgotPassword_EmailFeild.text;
	}

	public void OnClickResetMyPassword ()
	{
		ResetMyPassword (emailString);
	}

	void ResetMyPassword (string _email)
	{
		WatingPanelShow (true);
		var encoding = new System.Text.UTF8Encoding ();
		Dictionary<string,string> postHeader = new Dictionary<string,string> ();
		var jsonElement = new JSONClass ();
		jsonElement ["email"] = _email;

		postHeader.Add ("Content-Type", "application/json");
		postHeader.Add ("Content-Length", jsonElement.Count.ToString ());

		WWW www = new WWW (resetPasswordUrl, encoding.GetBytes (jsonElement.ToString ()), postHeader);

		StartCoroutine (WaitForPassword (www));
	}

	IEnumerator WaitForPassword (WWW www)
	{
		print ("Waiting For Response for WaitForPassword");
		yield return(www);
		print ("Password Sucess" + www.text);
		JSONNode _jsNode = JSON.Parse (www.text);

		if (_jsNode ["status"].ToString ().Contains ("200")) {
			if (_jsNode ["description"].ToString ().Contains ("Invalid email address")) {
				MessageText.gameObject.SetActive (true);
				MessageText.text = MessageText.text = _jsNode ["description"].ToString ().Trim ("\"".ToCharArray ()) + " \n " + _jsNode ["error"] ["error_msg"].ToString ().Trim ("\"".ToCharArray ());
				WatingPanelShow (false);
			}else if(_jsNode ["description"].ToString ().Contains ("Reset password email has been sent to your registered email id."))
			{
				EmptyAllFeilds ();
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = _jsNode ["description"].ToString ().Trim ('"');
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				WatingPanelShow (false);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {
					ShowLoginPanel ();

				});

			}	else if(_jsNode ["description"].ToString ().Contains ("Invalid Email."))
			{
				WatingPanelShow (false);
				MessageText.gameObject.SetActive (true);
				MessageText.text = MessageText.text =  _jsNode ["error"] ["error_msg"].ToString ().Trim ("\"".ToCharArray ());
			}	
			else if(_jsNode ["description"].ToString ().Contains ("Please send required data"))
			{
				WatingPanelShow (false);
				MessageText.gameObject.SetActive (true);
				MessageText.text = MessageText.text =  _jsNode ["error"] ["error_msg"].ToString ().Trim ("\"".ToCharArray ());
			}
		}else{		

			MessageText.gameObject.SetActive (true);
				MessageText.text = MessageText.text = _jsNode ["description"].ToString ().Trim ("\"".ToCharArray ()) + " \n " + _jsNode ["error"] ["error_msg"].ToString ().Trim ("\"".ToCharArray ());
			} 
	}

	#endregion


	#region PlayToWin ScreenControlls

	public void OnClickPlayToWin ()
	{
		// goto PayMent Sceen if compition is not running 
		// else goto MainMenu directly..
		Isplayforpaid = true;

		if (!PlayerPrefs.HasKey ("CompetitionGoingon")) {
			PaymentPanel.SetActive (true);
			FreeOrPaidPanel.SetActive (false);
		} else {			
			if (PlayerPrefs.HasKey ("UserEmail") && PlayerPrefs.HasKey ("UserPassword")) {
				emailString = PlayerPrefs.GetString ("UserEmail");
				passwordString = PlayerPrefs.GetString ("UserPassword");
				SignIN (emailString, passwordString);
			} else {				
				RegistrationPanel.SetActive (true);
				LoginPanel.SetActive (false);
				ForgotPasswordPanel.SetActive (false);
				FreeOrPaidPanel.SetActive (false);
				PaymentPanel.SetActive (false);

			}
		}
	}

	public void OnClickPlayForFree ()
	{
		// Goto MainMenu directly..
		Isplayforpaid = false;
		SceneManager.LoadScene ("00_MenuScreen");
	}

	public void BackToFreeAndPaidScreen ()
	{
		EmptyAllFeilds ();
		PaymentPanel.SetActive (false);
		FreeOrPaidPanel.SetActive (true);

	}
	//
	//	void SetUpIAP()
	//	{
	//		StoreKitManager.purchaseSuccessfulEvent += purchaseSuccessful;
	//	}

	// To be deleted if it is not using in future
	public void OnClickPayNow ()
	{			
		OnPurchaseSucessReturned ();//FIXME ---- To be commented, only for testing..
	}

	public void OnPurchaseSucessReturned ()
	{
		competitionController.SaveStartTime ();

		Isplayforpaid = true; // set is playing paid version
		IsPaidForCompetition = 1;
		PlayerPrefs.SetInt ("IsPaidForCompetition", HartPlayerRegistration.IsPaidForCompetition);
		SceneManager.LoadScene ("00_MenuScreen");
		//TODO To be UnComment
		if (PlayerPrefs.HasKey ("UserEmail") && PlayerPrefs.HasKey ("UserPassword")) {
			emailString = PlayerPrefs.GetString ("UserEmail");
			passwordString = PlayerPrefs.GetString ("UserPassword");
			SignIN (emailString, passwordString);
		} else {
			RegistrationPanel.SetActive (true);
			LoginPanel.SetActive (false);
			ForgotPasswordPanel.SetActive (false);
			FreeOrPaidPanel.SetActive (false);
			PaymentPanel.SetActive (false);
		}
	}

	#endregion

	public void BackToFreeAndPaidScreen (GameObject Go)
	{
		PaymentPanel.SetActive (false);
		FreeOrPaidPanel.SetActive (true);
		var ImageComponenet = Go.GetComponent <Image> ();
		ImageComponenet.sprite = PlayToWinDeactiveSprite;
		EmptyAllFeilds ();
	}

	public void FreeToPlaySelectionScreen (GameObject Go)
	{
		if (SceneManager.GetActiveScene ().name == "02_LoginScene") {
			var ImageComponenet = Go.GetComponent <Image> ();
					var Temp = ImageComponenet.sprite;
			ImageComponenet.sprite = FreeToPlayActiveSprite;
					FreeToPlayActiveSprite = Temp;
			StartCoroutine (AfterWait_FreeToPlay ());
			StartCoroutine (ResetButtoOfFreetoplay(Go, Temp));
		}
	}

	IEnumerator ResetButtoOfFreetoplay(GameObject Go,Sprite gg)
	{
		yield return new WaitForSeconds (1f);
		var ImageComponenet = Go.GetComponent <Image> ();
		//		var Temp = ImageComponenet.sprite;
		ImageComponenet.sprite = gg;
	}

	IEnumerator AfterWait_FreeToPlay ()
	{
		yield return new WaitForSeconds (0.1f);
		Isplayforpaid = false;
		SceneManager.LoadScene ("00_MenuScreen");
		FacebookManager.Instance.LoadFacebookScreen ();
	}


	public void PlayToWinSelectionScreen (GameObject Go)
	{
		if (SceneManager.GetActiveScene ().name == "02_LoginScene") {
//		Comming Soon for Play to win
			if (Application.internetReachability != NetworkReachability.NotReachable) {					
				//				var com = PlayerPrefs.GetInt ("CompetitionGoingon");
				if (PlayerPrefs.GetInt ("CompetitionGoingon") == 1) {
					var ImageComponenet = Go.GetComponent <Image> ();
					var Temp = ImageComponenet.sprite;
					ImageComponenet.sprite = PlayToWinActiveSprite;
					ImageComponenet.sprite = Temp;
					Isplayforpaid = true;
					StartCoroutine (AfterWait_PlayToWin ());
//					AfterWait_PlayToWin ();

				} else {
					InternetConnectionCheck ("No Competition Available. Please Come Back Later");
				}
			} else {
				InternetConnectionCheck ("No wireless network connections or Wi-Fi connected!");
				//InternetConnectionCheck ("Internet connection Required!");
				var ImageComponenet = Go.GetComponent <Image> ();
				var Temp = ImageComponenet.sprite;
				ImageComponenet.sprite = PlayToWinActiveSprite;
				ImageComponenet.sprite = Temp;
			}
		}

	}

	IEnumerator AfterWait_PlayToWin ()
	{
		
		if (PlayerPrefs.HasKey ("UserEmail") && PlayerPrefs.HasKey ("UserPassword")) {
			emailString = PlayerPrefs.GetString ("UserEmail");
			passwordString = PlayerPrefs.GetString ("UserPassword");

			SignIN (emailString, passwordString);

		} else {				
			RegistrationPanel.SetActive (true);
			LoginPanel.SetActive (false);
			ForgotPasswordPanel.SetActive (false);
			FreeOrPaidPanel.SetActive (false);
			PaymentPanel.SetActive (false);
			yield return new WaitForSeconds (1.0f);
		}	
	}


	public void InternetConnectionCheck (string message)
	{
		InternetPanel.SetActive (true);
		InternetMessage.text = message;
	}

	public void DisableInternetPopup ()
	{
		InternetPanel.SetActive (false);
		InternetMessage.text = null;
	}
}


public class UserInfo
{
	public  string username;
	public string email;
	public string password;
	public int score;
	public int level_no;
	public string device_token;

	//LogOut Attribute
	public int isPaid;
	public int CurrentGameId;
	public int CurrentDodgerGameLife;
	//ColorBallPowerUp
	public int ColorBallGameBombPowers;
	public int ColorBallGameFreezPowers;
	public int ColorBallGameReminderPowers;

	//Dodger Game PowerUp
	public int DodgerGameInvinciblePowers;
	public int DodgerGameSlowDownPowers;
	public int DodgerGameBombPowers;
	public int DodgerGameMergePowers;

	//NinjaFootie PowerUp
	public int NinjaFootieGoldStarPowers;
	public int NinjaFootieRhombusPowers;
	public int NinjaFootieWhiteStarPowers;
	public int NinjaFootieSquarePowers;

}

