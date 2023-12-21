using UnityEngine;
using System.Collections;

public class BuyItemsForDodgerGame : MonoBehaviour
{
	public GameObject DodgerAttribute;
	public static BuyItemsForDodgerGame Instance = null;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != null) {
			Destroy (this.gameObject);
		}
	}

	public void BuyProductForDodgerGame (int DodgerProductId)
	{
		if (Application.internetReachability == NetworkReachability.NotReachable) {
			DodgerAttribute.GetComponent<DogerGameManager> ().InternetCheckConnectionForDodger ("Internet Not Connected \n Data Connection Required To Buy This Item!");

		} else {
			if (DodgerProductId == 3) {
				IAPManager.Instance.BuyInvincible_Dodger ();
			}
			else if (DodgerProductId == 4) {
				IAPManager.Instance.BuyMerge_Dodger ();
			}
			else if (DodgerProductId == 5) {
				IAPManager.Instance.BuyBomb_Dodger ();
			}
			else if (DodgerProductId == 6) {
				IAPManager.Instance.BuySlowDown_Dodger ();
			}
		}
	}

	public void BuyInvinciblePowerUp ()
	{
		var powerAttribute = this.gameObject.GetComponent<DogerPowerUpController> ();
		if (SmartIAPListener.ItemPurchased) {

			DodgerAttribute.GetComponent<DogerGameManager> ().BackToInGameFromShop ();
//			DodgerAttribute.GetComponent<DogerGameManager> ().OnClickPause ();
			int _purchasedInvincible = 5;
			int tempCont = _purchasedInvincible + powerAttribute.DodgerInvincibleCount;
			powerAttribute.DodgerInvincibleCount = tempCont;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("DodgerInvincibleCountForPaid", powerAttribute.DodgerInvincibleCount);	
			} else {
				PlayerPrefs.SetInt ("DodgerInvincibleCount", powerAttribute.DodgerInvincibleCount);
			}

			powerAttribute.UpdatePowerUpCount ();
		}
	}
	public void DodgerBuyInvinciblePowerUpNew ()
	{
		var powerAttribute = this.gameObject.GetComponent<DogerPowerUpController> ();
		DodgerAttribute.GetComponent<DogerGameManager> ().BackToInGameFromShop ();
//		DodgerAttribute.GetComponent<DogerGameManager> ().OnClickPause ();
		int _purchasedInvincible = 5;
		int tempCont = _purchasedInvincible + powerAttribute.DodgerInvincibleCount;
		powerAttribute.DodgerInvincibleCount = tempCont;
		if (HartPlayerRegistration.Isplayforpaid) {
			PlayerPrefs.SetInt ("DodgerInvincibleCountForPaid", powerAttribute.DodgerInvincibleCount);	
		} else {
			PlayerPrefs.SetInt ("DodgerInvincibleCount", powerAttribute.DodgerInvincibleCount);
		}
		powerAttribute.UpdatePowerUpCount ();
	}

	public void BuyBombPowerUp ()
	{
		var powerAttribute = this.gameObject.GetComponent<DogerPowerUpController> ();
		if (SmartIAPListener.ItemPurchased) {
			DodgerAttribute.GetComponent<DogerGameManager> ().BackToInGameFromShop ();
//			DodgerAttribute.GetComponent<DogerGameManager> ().OnClickPause ();
			int _purchasedBomb = 5;
			int tempCont = _purchasedBomb + powerAttribute.DodgerBombCount;
			powerAttribute.DodgerBombCount = tempCont;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("DodgerBombCountForPaid", powerAttribute.DodgerBombCount);	
			} else {
				PlayerPrefs.SetInt ("DodgerBombCount", powerAttribute.DodgerBombCount);
			}
			powerAttribute.UpdatePowerUpCount ();
		}
	}

	public void DodgerBuyBombPowerUpNew ()
	{
		var powerAttribute = this.gameObject.GetComponent<DogerPowerUpController> ();
		DodgerAttribute.GetComponent<DogerGameManager> ().BackToInGameFromShop ();
//		DodgerAttribute.GetComponent<DogerGameManager> ().OnClickPause ();
		int _purchasedBomb = 5;
		int tempCont = _purchasedBomb + powerAttribute.DodgerBombCount;
		powerAttribute.DodgerBombCount = tempCont;
		if (HartPlayerRegistration.Isplayforpaid) {
			PlayerPrefs.SetInt ("DodgerBombCountForPaid", powerAttribute.DodgerBombCount);	
		} else {
			PlayerPrefs.SetInt ("DodgerBombCount", powerAttribute.DodgerBombCount);
		}
		powerAttribute.UpdatePowerUpCount ();
	}

	public void BuySlowDownPowerUp ()
	{
		var powerAttribute = this.gameObject.GetComponent<DogerPowerUpController> ();
		if (SmartIAPListener.ItemPurchased) {
			DodgerAttribute.GetComponent<DogerGameManager> ().BackToInGameFromShop ();
//			DodgerAttribute.GetComponent<DogerGameManager> ().OnClickPause ();
			int _purchasedSlowDown = 5;
			int tempCont = _purchasedSlowDown + powerAttribute.DodgerSlowCount;
			powerAttribute.DodgerSlowCount = tempCont;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("DodgerSlowCountForPaid", powerAttribute.DodgerSlowCount);	
			} else {
				PlayerPrefs.SetInt ("DodgerSlowCount", powerAttribute.DodgerSlowCount);
			}
			powerAttribute.UpdatePowerUpCount ();
		}
	}

	public void DodgerBuySlowDownPowerUpNew ()
	{
		var powerAttribute = this.gameObject.GetComponent<DogerPowerUpController> ();
		DodgerAttribute.GetComponent<DogerGameManager> ().BackToInGameFromShop ();
//		DodgerAttribute.GetComponent<DogerGameManager> ().OnClickPause ();
		int _purchasedSlowDown = 5;
		int tempCont = _purchasedSlowDown + powerAttribute.DodgerSlowCount;
		powerAttribute.DodgerSlowCount = tempCont;
		if (HartPlayerRegistration.Isplayforpaid) {
			PlayerPrefs.SetInt ("DodgerSlowCountForPaid", powerAttribute.DodgerSlowCount);	
		} else {
			PlayerPrefs.SetInt ("DodgerSlowCount", powerAttribute.DodgerSlowCount);
		}
		powerAttribute.UpdatePowerUpCount ();
	}

	public void BuyMergePowerUp ()
	{
		var powerAttribute = this.gameObject.GetComponent<DogerPowerUpController> ();
		if (SmartIAPListener.ItemPurchased) {
			DodgerAttribute.GetComponent<DogerGameManager> ().BackToInGameFromShop ();
//			DodgerAttribute.GetComponent<DogerGameManager> ().OnClickPause ();

			int _purchasedMerge = 5;
			int tempCont = _purchasedMerge + powerAttribute.DodgerMergeCount;
			powerAttribute.DodgerMergeCount = tempCont;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("DodgerMergeCountForPaid", powerAttribute.DodgerMergeCount);	
			} else {
				PlayerPrefs.SetInt ("DodgerMergeCount", powerAttribute.DodgerMergeCount);
			}
			powerAttribute.UpdatePowerUpCount ();
		}
	}


	public void DodgerBuyMergePowerUpNew ()
	{
		var powerAttribute = this.gameObject.GetComponent<DogerPowerUpController> ();
		DodgerAttribute.GetComponent<DogerGameManager> ().BackToInGameFromShop ();
//		DodgerAttribute.GetComponent<DogerGameManager> ().OnClickPause ();
		int _purchasedMerge = 5;
		int tempCont = _purchasedMerge + powerAttribute.DodgerMergeCount;
		powerAttribute.DodgerMergeCount = tempCont;
		if (HartPlayerRegistration.Isplayforpaid) {
			PlayerPrefs.SetInt ("DodgerMergeCountForPaid", powerAttribute.DodgerMergeCount);	
		} else {
			PlayerPrefs.SetInt ("DodgerMergeCount", powerAttribute.DodgerMergeCount);
		}
		powerAttribute.UpdatePowerUpCount ();
	}

}
