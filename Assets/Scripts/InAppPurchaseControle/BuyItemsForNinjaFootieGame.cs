using UnityEngine;
using System.Collections;

public class BuyItemsForNinjaFootieGame : MonoBehaviour
{
	public static BuyItemsForNinjaFootieGame Instance = null;
	public GameObject NinjaAttribute;


	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != null) {
			Destroy (this.gameObject);
		}
	}

	public void BuyProductForNinjaFootieGame (int NinjaFootieProductId)
	{
		if (Application.internetReachability == NetworkReachability.NotReachable) {
			NinjaAttribute.GetComponent<NinjaFootie.NinjaScreenManager> ().CheckInternetConnectionForNinjaGame ("Internet Not Connected \n Data Connection Required To Buy This Item!");
		} else {
			HartPlays_InAppPurchase.Instance.PurchaseHartPlaysProduct (NinjaFootieProductId);
		}		

	}

	public void BuyGoldenStarPowerUp ()
	{
		var powerAttribute = this.gameObject.GetComponent<NinjaFootie.NinjaPowerUpController> ();


		if (SmartIAPListener.ItemPurchased) {
			NinjaAttribute.GetComponent<NinjaFootie.NinjaScreenManager> ().OnBackFromShop ();
//			NinjaAttribute.GetComponent<NinjaFootie.NinjaScreenManager> ().OnClickPause ();
			int _purchasedInvincible = 5;
			int tempCont = _purchasedInvincible + powerAttribute.NinjaGoldenStarCount;
			powerAttribute.NinjaGoldenStarCount = tempCont;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("NinjaGoldenStarCountForPaid", powerAttribute.NinjaGoldenStarCount);
			} else {
				PlayerPrefs.SetInt ("NinjaGoldenStarCount", powerAttribute.NinjaGoldenStarCount);
			}
			powerAttribute.UpdatePowerUpCountOnDisplay ();
		}
	}

	public void BuyWhiteStarPowerUp ()
	{
		var powerAttribute = this.gameObject.GetComponent<NinjaFootie.NinjaPowerUpController> ();

		if (SmartIAPListener.ItemPurchased) {
			NinjaAttribute.GetComponent<NinjaFootie.NinjaScreenManager> ().OnBackFromShop ();
//			NinjaAttribute.GetComponent<NinjaFootie.NinjaScreenManager> ().OnClickPause ();
			int _purchasedInvincible = 5;
			int tempCont = _purchasedInvincible + powerAttribute.NinjaWhiteStarCount;
			powerAttribute.NinjaWhiteStarCount = tempCont;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("NinjaWhiteStarCountForPaid", powerAttribute.NinjaWhiteStarCount);
			} else {
				PlayerPrefs.SetInt ("NinjaWhiteStarCount", powerAttribute.NinjaWhiteStarCount);
			}
			powerAttribute.UpdatePowerUpCountOnDisplay ();
		}
	}

	public void BuyRhombusPowerUp ()
	{
		var powerAttribute = this.gameObject.GetComponent<NinjaFootie.NinjaPowerUpController> ();

		if (SmartIAPListener.ItemPurchased) {
			NinjaAttribute.GetComponent<NinjaFootie.NinjaScreenManager> ().OnBackFromShop ();
//			NinjaAttribute.GetComponent<NinjaFootie.NinjaScreenManager> ().OnClickPause ();
			int _purchasedInvincible = 5;
			int tempCont = _purchasedInvincible + powerAttribute.NinjaRhombusCount;
			powerAttribute.NinjaRhombusCount = tempCont;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("NinjaRhombusCountForPaid", powerAttribute.NinjaRhombusCount);
			} else {
				PlayerPrefs.SetInt ("NinjaRhombusCount", powerAttribute.NinjaRhombusCount);
			}

	
			powerAttribute.UpdatePowerUpCountOnDisplay ();
		}
	}

	public void BuySquarePowerUp ()
	{
		var powerAttribute = this.gameObject.GetComponent<NinjaFootie.NinjaPowerUpController> ();

		if (SmartIAPListener.ItemPurchased) {
			NinjaAttribute.GetComponent<NinjaFootie.NinjaScreenManager> ().OnBackFromShop ();
//			NinjaAttribute.GetComponent<NinjaFootie.NinjaScreenManager> ().OnClickPause ();
			int _purchasedInvincible = 5;
			int tempCont = _purchasedInvincible + powerAttribute.NinjaSquareCount;
			powerAttribute.NinjaSquareCount = tempCont;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("NinjaSquareCountForPaid", powerAttribute.NinjaSquareCount);
			} else {
				PlayerPrefs.SetInt ("NinjaSquareCount", powerAttribute.NinjaSquareCount);
			}
			powerAttribute.UpdatePowerUpCountOnDisplay ();
		}
	}



}
