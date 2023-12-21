using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager, 
// one of the existing Survival Shooter scripts.
// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class IAPManager : MonoBehaviour, IStoreListener
{
	public static IAPManager Instance{ get; set;}

	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
	//Free To Play Products
	public const string PRODUCT_COLORBALL_BOMB = "com.Hartplays.game_colorball_bomb";
	public const string PRODUCT_COLORBALL_FREEZE = "com.Hartplays.game_colorball_freeze";
	public const string PRODUCT_COLORBALL_REMINDER = "com.Hartplays.game_colorball_reminder";
	public const string PRODUCT_DODGER_INVINCIBLE = "com.Hartplays.game_dodger_invincible";
	public const string PRODUCT_DODGER_MERGE = "com.Hartplays.game_dodger_merge";
	public const string PRODUCT_DODGER_BOMB = "com.Hartplays.game_dodger_bomb";
	public const string PRODUCT_DODGER_SLOWDOWN = "com.Hartplays.game_dodger_slow_down";

	// Play To win Subscription
	public const string SUBSCRIPTION_10_PLAYS ="com.Hartplays.game_10Plays_Subscription";
	public const string SUBSCRIPTION_20_PLAYS = "com.Hartplays.game_20Plays_Subscription";
	public const string SUBSCRIPTION_30_PLAYS = "com.Hartplays.game_30Plays_Subscription";
	public const string SUBSCRIPTION_24_HOURS_PLAY = "com.Hartplays.game_24Hours_Play_Subscription";
	public const string SUBSCRIPTION_7_DAYS_PLAY = "com.Hartplays.game_7Days_Play_Subscription";

	// Color Ball Lives
	public const string COLORBALL_5LIVES = "com.Hartplays.game_ColorBall_5Lives";
	public const string COLORBALL_10LIVES = "com.Hartplays.game_ColorBall_10Lives";
	// Dodger Lives
	public const string DODGER_5LIVES = "com.Hartplays.game_Dodger_5Lives";
	public const string DODGER_10LIVES = "com.Hartplays.game_Dodger_10Lives";

	public static bool PlaysDecreasedFromColorBall = true;
	public static bool PlaysDecreasedFromDodger = true;
//	public static string kProductIDNonConsumable = "nonconsumable";
//	public static string kProductIDSubscription =  "subscription"; 

//	// Apple App Store-specific product identifier for the subscription product.
//	private static string kProductNameAppleSubscription =  "com.unity3d.subscription.new";

//	// Google Play Store-specific product identifier subscription product.
//	private static string kProductNameGooglePlaySubscription =  "com.unity3d.subscription.original"; 

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		// If we haven't set up the Unity Purchasing reference
		if (m_StoreController == null)
		{
			// Begin to configure our connection to Purchasing
			InitializePurchasing();
		}
	}

	public void InitializePurchasing() 
	{
		// If we have already connected to Purchasing ...
		if (IsInitialized())
		{
			// ... we are done here.
			return;
		}
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		// Free To Play Product
		builder.AddProduct(PRODUCT_COLORBALL_BOMB, ProductType.Consumable);
		builder.AddProduct (PRODUCT_COLORBALL_FREEZE, ProductType.Consumable);
		builder.AddProduct (PRODUCT_COLORBALL_REMINDER, ProductType.Consumable);
		builder.AddProduct (PRODUCT_DODGER_INVINCIBLE, ProductType.Consumable);
		builder.AddProduct (PRODUCT_DODGER_MERGE, ProductType.Consumable);
		builder.AddProduct (PRODUCT_DODGER_BOMB, ProductType.Consumable);
		builder.AddProduct (PRODUCT_DODGER_SLOWDOWN, ProductType.Consumable);

		// Play To Win Subscription Product
		builder.AddProduct (SUBSCRIPTION_10_PLAYS, ProductType.Consumable);
		builder.AddProduct (SUBSCRIPTION_20_PLAYS, ProductType.Consumable);
		builder.AddProduct (SUBSCRIPTION_30_PLAYS, ProductType.Consumable);
		builder.AddProduct (SUBSCRIPTION_24_HOURS_PLAY, ProductType.Consumable);
		builder.AddProduct (SUBSCRIPTION_7_DAYS_PLAY, ProductType.Consumable);

		// Play To Win Color Ball Live Product
		builder.AddProduct (COLORBALL_5LIVES, ProductType.Consumable);
		builder.AddProduct (COLORBALL_10LIVES, ProductType.Consumable);

		// Play To Win Dodger Live Product
		builder.AddProduct (DODGER_5LIVES, ProductType.Consumable);
		builder.AddProduct (DODGER_10LIVES, ProductType.Consumable);

//		builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);

		// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
		// and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
		UnityPurchasing.Initialize(this, builder);
	}


	private bool IsInitialized()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	#region Free To Play 
	public void BuyBomb_ColorBallGame()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		if(HartPlayerRegistration.Isplayforpaid)
		{
			if (PlayerPrefs.GetInt ("BombCountForPaid") == 0) {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_COLORBALL_BOMB);
			} else {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_COLORBALL_BOMB);
			}
		} else {
			BuyProductID (PRODUCT_COLORBALL_BOMB);

		}
	}
	public void BuyFreeze_ColorBallGame()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		if (HartPlayerRegistration.Isplayforpaid) {
			if (PlayerPrefs.GetInt ("FreezeCountForPaid") == 0) {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_COLORBALL_FREEZE);
			} else {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_COLORBALL_FREEZE);
			}
		} else {
			BuyProductID (PRODUCT_COLORBALL_FREEZE);
		}
	}
	public void BuyReminder_ColorBallGame()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		if (HartPlayerRegistration.Isplayforpaid) {
			if (PlayerPrefs.GetInt ("ReminderCountForPaid") == 0) {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_COLORBALL_REMINDER);
			} else {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_COLORBALL_REMINDER);
			}
		} else {
			BuyProductID (PRODUCT_COLORBALL_REMINDER);
		}
	}
	public void BuyInvincible_Dodger()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		if (HartPlayerRegistration.Isplayforpaid) {
			if (PlayerPrefs.GetInt ("DodgerInvincibleCountForPaid") == 0) {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_DODGER_INVINCIBLE);
			} else {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_DODGER_INVINCIBLE);
			}
		} else {
			BuyProductID (PRODUCT_DODGER_INVINCIBLE);
		}
	}
	public void BuyMerge_Dodger()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		if (HartPlayerRegistration.Isplayforpaid) {
			if (PlayerPrefs.GetInt ("DodgerMergeCountForPaid") == 0) {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_DODGER_MERGE);
			} else {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_DODGER_MERGE);
			}
		} else {
			BuyProductID (PRODUCT_DODGER_MERGE);
		}
	}
	public void BuyBomb_Dodger()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		if (HartPlayerRegistration.Isplayforpaid) {
			if (PlayerPrefs.GetInt ("DodgerBombCountForPaid") == 0) {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_DODGER_BOMB);
			} else {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_DODGER_BOMB);
			}
		} else {
			BuyProductID (PRODUCT_DODGER_BOMB);
		}
	}
	public void BuySlowDown_Dodger()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		if (HartPlayerRegistration.Isplayforpaid) {
			if (PlayerPrefs.GetInt ("DodgerSlowCountForPaid") == 0) {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_DODGER_SLOWDOWN);
			} else {
				EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
				BuyProductID (PRODUCT_DODGER_SLOWDOWN);
			}
		} else {
			BuyProductID (PRODUCT_DODGER_SLOWDOWN);
		}
	}

	#endregion

	#region Subscription
	public void SubscriptionFor10_Plays()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(SUBSCRIPTION_10_PLAYS);
	}

	public void SubscriptionFor20_Plays()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(SUBSCRIPTION_20_PLAYS);
	}

	public void SubscriptionFor30_Plays()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(SUBSCRIPTION_30_PLAYS);
	}
	DateTime dd;
	public void SubscriptionFor24HR_Plays()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		if(CompetitionController.EndTime < DateTime.UtcNow.AddHours (24))
		{
			var tt = CompetitionController.EndTime - DateTime.UtcNow;
			HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
			HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (false);
			HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (true);
			HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (true);
			dd = dd.AddDays (tt.Days);
			dd = dd.AddHours (tt.Hours);
			dd= dd.AddMinutes (tt.Minutes);
			string msg = "";
			if (dd > DateTime.UtcNow.AddHours (1)) {
				msg = "Competition is ending in " + dd.Hour + ":" + dd.Minute +":" + dd.Second +" Hour(s).";
			}else
			{
				msg = "Competition is ending in " + dd.Hour+":"+ dd.Minute + ":" + dd.Second +" Minute(s).";
			}
			HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text =   string.Format("{0} {1}", msg, "\n Are you sure you want to continue?");
			HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.onClick.RemoveAllListeners ();
			HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.onClick.AddListener (() => {
				BuyProductID(SUBSCRIPTION_24_HOURS_PLAY);
			});
		}else
		{
			BuyProductID(SUBSCRIPTION_24_HOURS_PLAY);
		}
	}


	public void SubscriptionFor7Days_Plays()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(SUBSCRIPTION_7_DAYS_PLAY);
	}
	#endregion

	#region Color Ball Lives
	public void ColorBall5_Lives()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
//		StartCoroutine (GameSaveState.Instance.GetUserStatus (true));
		int dd = PlayerPrefs.GetInt ("ColorBallLives");
		if (PlayerPrefs.GetInt ("ColorBallLives") <= 0) {
			EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;

			BuyProductID (COLORBALL_5LIVES);
		} else {
			EventSystem.current.currentSelectedGameObject.GetComponent <Button>().interactable = false;
		}
	}
	public void ColorBall10_Lives()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
//		StartCoroutine (GameSaveState.Instance.GetUserStatus (true));
		if (PlayerPrefs.GetInt ("ColorBallLives") <=  0) {
			EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
			BuyProductID (COLORBALL_10LIVES);
		}
		else
			EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = false;
	}
	#endregion

	#region Dodger Lives
	public void Dodger5_Lives()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
//		StartCoroutine (GameSaveState.Instance.GetUserStatus (true));
		if (PlayerPrefs.GetInt ("DodgerLives") <=  0) {
			EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
			BuyProductID (DODGER_5LIVES);
		}else
			EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = false;
	}
	public void Dodger10_Lives()
	{
		// Buy the consumable product using its general identifier. Expect a response either 
		// through ProcessPurchase or OnPurchaseFailed asynchronously.
//		StartCoroutine (GameSaveState.Instance.GetUserStatus (true));
		if (PlayerPrefs.GetInt ("DodgerLives") <=  0) {
			EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = true;
			BuyProductID (DODGER_10LIVES);
		}else
		{
			EventSystem.current.currentSelectedGameObject.GetComponent <Button> ().interactable = false;
		
		}
	}
	#endregion
//	public void BuyNonConsumable()
//	{
//		// Buy the non-consumable product using its general identifier. Expect a response either 
//		// through ProcessPurchase or OnPurchaseFailed asynchronously.
//		BuyProductID(kProductIDNonConsumable);
//	}
//
//
//	public void BuySubscription()
//	{
//		// Buy the subscription product using its the general identifier. Expect a response either 
//		// through ProcessPurchase or OnPurchaseFailed asynchronously.
//		// Notice how we use the general product identifier in spite of this ID being mapped to
//		// custom store-specific identifiers above.
//		BuyProductID(kProductIDSubscription);
//	}


	private void BuyProductID(string productId)
	{
		// If Purchasing has been initialized ...
		if (IsInitialized())
		{
			// ... look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = m_StoreController.products.WithID(productId);

			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				m_StoreController.InitiatePurchase(product);
			}
			// Otherwise ...
			else
			{
				// ... report the product look-up failure situation  
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		// Otherwise ...
		else
		{
			// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
			// retrying initiailization.
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}


	// Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
	// Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
	public void RestorePurchases()
	{
		// If Purchasing has not yet been set up ...
		if (!IsInitialized())
		{
			// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}

		// If we are running on an Apple device ... 
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
			Application.platform == RuntimePlatform.OSXPlayer)
		{
			// ... begin restoring purchases
			Debug.Log("RestorePurchases started ...");

			// Fetch the Apple store-specific subsystem.
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			// Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
			// the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
			apple.RestoreTransactions((result) => {
				// The first phase of restoration. If no more responses are received on ProcessPurchase then 
				// no purchases are available to be restored.
				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		// Otherwise ...
		else
		{
			// We are not running on an Apple device. No work is necessary to restore purchases.
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}


	//  
	// --- IStoreListener
	//

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}


	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		// A consumable product has been purchased by this user.
		// This Product is for Free To Play Game 
		if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_COLORBALL_BOMB, StringComparison.Ordinal))
		{
			Debug.Log ("You Just Bought Bombs in Color Ball Game. Congratulations.....!");
			if(HartPlayerRegistration.Isplayforpaid)
			{
				
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "Congratulations!! You Have Successfully Purchased 5 Bomb Power Up.";
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {
					int PreVelue = PlayerPrefs.GetInt ("BombCountForPaid");
					PlayerPrefs.SetInt ("BombCountForPaid", PreVelue +5);
					StartCoroutine (GameSaveState.Instance.PostUserStatus ());

				});
			}else
				BuyItemsForCololBallGame.Instance.IFBombPurchasedNew ();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_COLORBALL_FREEZE, StringComparison.Ordinal))
		{
			if(HartPlayerRegistration.Isplayforpaid)
			{
				
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "Congratulations!! You Have Successfully Purchased 5 Freeze Power Up.";
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {
					int PreVelue = PlayerPrefs.GetInt ("FreezeCountForPaid");
					PlayerPrefs.SetInt ("FreezeCountForPaid", PreVelue +5);
					StartCoroutine (GameSaveState.Instance.PostUserStatus ());

				});
			}else
			BuyItemsForCololBallGame.Instance.BuyFreezePowerUpNew ();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_COLORBALL_REMINDER, StringComparison.Ordinal))
		{
			if(HartPlayerRegistration.Isplayforpaid)
			{

				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "Congratulations!! You Have Successfully Purchased 5 Reminder Power Up.";
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {
					int PreVelue = PlayerPrefs.GetInt ("ReminderCountForPaid");
					PlayerPrefs.SetInt ("ReminderCountForPaid", PreVelue +5);
					StartCoroutine (GameSaveState.Instance.PostUserStatus ());

				});

			}else
			BuyItemsForCololBallGame.Instance.BuyReminderPowerUpNew ();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_DODGER_INVINCIBLE, StringComparison.Ordinal))
		{
			if(HartPlayerRegistration.Isplayforpaid)
			{
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "Congratulations!! You Have Successfully Purchased 5 Invincible Power Up.";
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {
					int PreVelue = PlayerPrefs.GetInt ("DodgerInvincibleCountForPaid");
					PlayerPrefs.SetInt ("DodgerInvincibleCountForPaid", PreVelue +5);
					StartCoroutine (GameSaveState.Instance.PostUserStatus ());

				});
			}else
			BuyItemsForDodgerGame.Instance.DodgerBuyInvinciblePowerUpNew ();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_DODGER_MERGE, StringComparison.Ordinal))
		{
			if(HartPlayerRegistration.Isplayforpaid)
			{
				
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "Congratulations!! You Have Successfully Purchased 5 Merge Power Up.";
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {
					int PreVelue = PlayerPrefs.GetInt ("DodgerMergeCountForPaid");
					PlayerPrefs.SetInt ("DodgerMergeCountForPaid", PreVelue +5);
					StartCoroutine (GameSaveState.Instance.PostUserStatus ());

				});
			}else
			BuyItemsForDodgerGame.Instance.DodgerBuyMergePowerUpNew ();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_DODGER_BOMB, StringComparison.Ordinal))
		{
			if(HartPlayerRegistration.Isplayforpaid)
			{
				
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.btn.gameObject.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.YesBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.NoBtn.gameObject.SetActive (false);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "Congratulations!! You Have Successfully Purchased 5 Bomb Power Up.";
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {
					int PreVelue = PlayerPrefs.GetInt ("DodgerBombCountForPaid");
					PlayerPrefs.SetInt ("DodgerBombCountForPaid", PreVelue +5);
					StartCoroutine (GameSaveState.Instance.PostUserStatus ());

				});
			}else
			BuyItemsForDodgerGame.Instance.DodgerBuyBombPowerUpNew ();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_DODGER_SLOWDOWN, StringComparison.Ordinal))
		{
			if(HartPlayerRegistration.Isplayforpaid)
			{
				
				HartPlays_PlaytoWin_Subscribe.Instance.PopupMassage.SetActive (true);
				HartPlays_PlaytoWin_Subscribe.Instance.ShowSuccefullMsg.text = "Congratulations!! You Have Successfully Purchased 5 Reverse Power Up.";
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.RemoveAllListeners ();
				HartPlays_PlaytoWin_Subscribe.Instance.btn.onClick.AddListener (() => {
					int PreVelue = PlayerPrefs.GetInt ("DodgerSlowCountForPaid");
					PlayerPrefs.SetInt ("DodgerSlowCountForPaid", PreVelue +5);
					StartCoroutine (GameSaveState.Instance.PostUserStatus ());

				});
			}else
			BuyItemsForDodgerGame.Instance.DodgerBuySlowDownPowerUpNew ();
		}

		// This Product For Play To Win Subscription 
		else if (String.Equals(args.purchasedProduct.definition.id, SUBSCRIPTION_10_PLAYS, StringComparison.Ordinal))
		{
			HartPlays_PlaytoWin_Subscribe.Instance.Buy_10_Plays_Subscription ();

		}
		else if (String.Equals(args.purchasedProduct.definition.id, SUBSCRIPTION_20_PLAYS, StringComparison.Ordinal))
		{
			HartPlays_PlaytoWin_Subscribe.Instance.Buy_20_Plays_Subscription ();	

		}
		else if (String.Equals(args.purchasedProduct.definition.id, SUBSCRIPTION_30_PLAYS, StringComparison.Ordinal))
		{
			HartPlays_PlaytoWin_Subscribe.Instance.Buy_30_Plays_Subscription ();		}
		else if (String.Equals(args.purchasedProduct.definition.id, SUBSCRIPTION_24_HOURS_PLAY, StringComparison.Ordinal))
		{
			HartPlays_PlaytoWin_Subscribe.Instance.Buy_24Hours_Plays_Subscription ();
		}
		else if (String.Equals(args.purchasedProduct.definition.id, SUBSCRIPTION_7_DAYS_PLAY, StringComparison.Ordinal))
		{
			HartPlays_PlaytoWin_Subscribe.Instance.Buy_7Days_Plyas_Subscription ();	
		}
		// This Product For Play To Win Color Ball Lives 
		else if (String.Equals(args.purchasedProduct.definition.id, COLORBALL_5LIVES, StringComparison.Ordinal))
		{
			HartPlays_PlaytoWin_Subscribe.Instance.Buy5LivesForColorBallGame ();
			IAPManager.PlaysDecreasedFromColorBall = true;
		}
		else if (String.Equals(args.purchasedProduct.definition.id, COLORBALL_10LIVES, StringComparison.Ordinal))
		{
			HartPlays_PlaytoWin_Subscribe.Instance.Buy10LivesForColorBallGame ();
			IAPManager.PlaysDecreasedFromColorBall = true;
		}
		// This Product For Play To Win Dodger Lives 
		else if (String.Equals(args.purchasedProduct.definition.id, DODGER_5LIVES, StringComparison.Ordinal))
		{
			HartPlays_PlaytoWin_Subscribe.Instance.Buy5LivesForDodgerGame ();
			IAPManager.PlaysDecreasedFromColorBall = true;
		}
		else if (String.Equals(args.purchasedProduct.definition.id, DODGER_10LIVES, StringComparison.Ordinal))
		{
			HartPlays_PlaytoWin_Subscribe.Instance.Buy10LivesForDodgerGame ();
			IAPManager.PlaysDecreasedFromColorBall = true;
		}

//		// Or ... a non-consumable product has been purchased by this user.
//		else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
//		{
//			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			// TODO: The non-consumable item has been successfully purchased, grant this item to the player.
//		}
//		// Or ... a subscription product has been purchased by this user.
//		else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
//		{
//			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			// TODO: The subscription item has been successfully purchased, grant this to the player.
//		}
//		// Or ... an unknown product has been purchased by this user. Fill in additional products here....
		else 
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

		// Return a flag indicating whether this product has completely been received, or if the application needs 
		// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
		// saving purchased products to the cloud, and when that save is delayed.

			return PurchaseProcessingResult.Complete;

	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
		// this reason with the user to guide their troubleshooting actions.
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}