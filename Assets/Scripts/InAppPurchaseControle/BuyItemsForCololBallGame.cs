using UnityEngine;
using System.Collections;

public class BuyItemsForCololBallGame : MonoBehaviour
{
	public GameObject mainGameControlet;
	public static BuyItemsForCololBallGame Instance = null;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != null) {
			Destroy (this.gameObject);
		}
	}

	public void BuyProductForColorBallGame (int ProductIdToBuy)
	{	
		if (Application.internetReachability == NetworkReachability.NotReachable) {
			this.gameObject.GetComponent<ScreenManager> ().InternetConnectionCheck ("Internet Not Connected \n Data Connection Required To Buy This Item!");
		} else {
			if (ProductIdToBuy == 0) {
				IAPManager.Instance.BuyBomb_ColorBallGame ();
			} else if (ProductIdToBuy == 1) {
				IAPManager.Instance.BuyFreeze_ColorBallGame ();
			} else {
				IAPManager.Instance.BuyReminder_ColorBallGame ();
			}
		}
	}

	public void IFBombPurchasedNew ()
	{	
		var powerAttribute = this.gameObject.GetComponent<PowerUpContoller> ();
		//			mainGameControlet.GetComponent<MainGameController_Touch> ().ResumeButtonClicked ();
		mainGameControlet.GetComponent<MainGameController_Touch> ().OnClikcBackFromShope ();
		int _purchasedBomb = 5;
		int tempCont = _purchasedBomb + powerAttribute.BombCount;
		powerAttribute.BombCount = tempCont;
		if (HartPlayerRegistration.Isplayforpaid) {
			PlayerPrefs.SetInt ("BombCountForPaid", powerAttribute.BombCount);
		} else {
			PlayerPrefs.SetInt ("BombCount", powerAttribute.BombCount);
		}
		powerAttribute.UpdatePowerUpCount ();
	}

	public void IFBombPurchased ()
	{	
		var powerAttribute = this.gameObject.GetComponent<PowerUpContoller> ();
		if (SmartIAPListener.ItemPurchased) {
//			mainGameControlet.GetComponent<MainGameController_Touch> ().ResumeButtonClicked ();
			mainGameControlet.GetComponent<MainGameController_Touch> ().OnClikcBackFromShope ();
			int _purchasedBomb = 5;
			int tempCont = _purchasedBomb + powerAttribute.BombCount;
			powerAttribute.BombCount = tempCont;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("BombCountForPaid", powerAttribute.BombCount);
			} else {
				PlayerPrefs.SetInt ("BombCount", powerAttribute.BombCount);
			}
			powerAttribute.UpdatePowerUpCount ();
			SmartIAPListener.ItemPurchased = false;
		} else {
			print ("Stay on same Screen");
		}
	}

	public void BuyFreezePowerUpNew ()
	{
		var powerAttribute = this.gameObject.GetComponent<PowerUpContoller> ();
		//			mainGameControlet.GetComponent<MainGameController_Touch> ().ResumeButtonClicked ();
		mainGameControlet.GetComponent<MainGameController_Touch> ().OnClikcBackFromShope ();
		int _purchasedFreeze = 5;
		int tempCont = _purchasedFreeze + powerAttribute.FreezeCount;
		powerAttribute.FreezeCount = tempCont;
		if (HartPlayerRegistration.Isplayforpaid) 
		{
			PlayerPrefs.SetInt ("FreezeCountForPaid", powerAttribute.FreezeCount);
		} 
		else 
		{
			PlayerPrefs.SetInt ("FreezeCount", powerAttribute.FreezeCount);
		}
		powerAttribute.UpdatePowerUpCount ();
	}

	public void BuyFreezePowerUp ()
	{
		var powerAttribute = this.gameObject.GetComponent<PowerUpContoller> ();

		if (SmartIAPListener.ItemPurchased) {
//			mainGameControlet.GetComponent<MainGameController_Touch> ().ResumeButtonClicked ();
			mainGameControlet.GetComponent<MainGameController_Touch> ().OnClikcBackFromShope ();
			int _purchasedFreeze = 5;
			int tempCont = _purchasedFreeze + powerAttribute.FreezeCount;
			powerAttribute.FreezeCount = tempCont;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("FreezeCountForPaid", powerAttribute.FreezeCount);
			} else {
				PlayerPrefs.SetInt ("FreezeCount", powerAttribute.FreezeCount);
			}
			powerAttribute.UpdatePowerUpCount ();
			SmartIAPListener.ItemPurchased = false;

		} else {
			print ("Stay on same Screen");
		}


	}


	public void BuyReminderPowerUpNew ()
	{
		var powerAttribute = this.gameObject.GetComponent<PowerUpContoller> ();
//		mainGameControlet.GetComponent<MainGameController_Touch> ().ResumeButtonClicked ();
		mainGameControlet.GetComponent<MainGameController_Touch> ().OnClikcBackFromShope ();
		int _purchasedReminder = 5;
		int tempCont = _purchasedReminder + powerAttribute.ReminderCount;
		powerAttribute.ReminderCount = tempCont;
		if (HartPlayerRegistration.Isplayforpaid) {
			PlayerPrefs.SetInt ("ReminderCountForPaid", powerAttribute.ReminderCount);
		} else {
			PlayerPrefs.SetInt ("ReminderCount", powerAttribute.ReminderCount);
		}
		powerAttribute.UpdatePowerUpCount ();
	}

	public void BuyReminderPowerUp ()
	{
		var powerAttribute = this.gameObject.GetComponent<PowerUpContoller> ();

		if (SmartIAPListener.ItemPurchased) {
//			mainGameControlet.GetComponent<MainGameController_Touch> ().ResumeButtonClicked ();
			mainGameControlet.GetComponent<MainGameController_Touch> ().OnClikcBackFromShope ();
			int _purchasedReminder = 5;
			int tempCont = _purchasedReminder + powerAttribute.ReminderCount;
			powerAttribute.ReminderCount = tempCont;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("ReminderCountForPaid", powerAttribute.ReminderCount);
			} else {
				PlayerPrefs.SetInt ("ReminderCount", powerAttribute.ReminderCount);
			}
			powerAttribute.UpdatePowerUpCount ();
			SmartIAPListener.ItemPurchased = false;

		} else {
			print ("Stay on same Screen");
		}
	}


}
