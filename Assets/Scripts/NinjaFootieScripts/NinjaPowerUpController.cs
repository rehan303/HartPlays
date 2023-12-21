using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace NinjaFootie
{
	public class NinjaPowerUpController : MonoBehaviour
	{
		//	public static List<GameObject> PowerUpAnimations = new List<GameObject>() ;
		public Text TimerText;
		public GameObject[] PowerUp;

		List<GameObject> PowerUpsList = new List<GameObject> ();
		public bool isPowerUpActive;
		public bool isPowerUpTaken;
		public GameObject powerUpGameObject;
		// to be private Only....

		float ResctritedX = 5f;
		float ResctritedY = 1.5f;

		[Header ("Dodger Power Ups Count")]
		public int NinjaGoldenStarCount;
		public int NinjaRhombusCount;
		public int NinjaSquareCount;
		public int NinjaWhiteStarCount;

		[Header ("Dodger Power ups count GUI text")]
		public Text NinjaGoldenStarText;
		public Text NinjaRhombusText;
		public Text NinjaSquareText;
		public Text NinjaWhiteStarText;

		[Header ("DodgerPowerUpBool")]
		bool NinjaGoldenStar;
		bool NinjaRhombus;
		bool NinjaSquare;
		bool NinjaWhiteStar;
		public GameObject PurchasedpowerUpGameObject;
		public bool isShopedPowerUpActive;
		public bool isShopedPowerUpTaken;

		public GameObject NinjaFootieGameScreen;

		void Start ()
		{
			//		PowerUpAnimations.Clear ();
//			Purchased Power Up Save on PlayerPrefs
			UpdatePowerUpCountOnDisplay ();
		}

		public void UpdatePowerUpCountOnDisplay ()
		{
			if (HartPlayerRegistration.Isplayforpaid) {
				NinjaGoldenStarCount = PlayerPrefs.GetInt ("NinjaGoldenStarCountForPaid", 0);
				NinjaRhombusCount = PlayerPrefs.GetInt ("NinjaRhombusCountForPaid", 0);
				NinjaSquareCount = PlayerPrefs.GetInt ("NinjaSquareCountForPaid", 0);
				NinjaWhiteStarCount = PlayerPrefs.GetInt ("NinjaWhiteStarCountForPaid", 0);
			} else {
				NinjaGoldenStarCount = PlayerPrefs.GetInt ("NinjaGoldenStarCount", 0);
				NinjaRhombusCount = PlayerPrefs.GetInt ("NinjaRhombusCount", 0);
				NinjaSquareCount = PlayerPrefs.GetInt ("NinjaSquareCount", 0);
				NinjaWhiteStarCount = PlayerPrefs.GetInt ("NinjaWhiteStarCount", 0);
			}
			NinjaGoldenStarText.text = NinjaGoldenStarCount.ToString ();
			NinjaRhombusText.text = NinjaRhombusCount.ToString ();
			NinjaSquareText.text = NinjaSquareCount.ToString ();
			NinjaWhiteStarText.text = NinjaWhiteStarCount.ToString ();
		}

		public void NinjaRhombusPowerUp ()
		{
			if (!isShopedPowerUpActive) {
				if (NinjaRhombusCount >= 1) {
					NinjaRhombus = true;
					StartCoroutine (SpawnPurchsedPowerup (PowerUp [0]));
				} else {
					var GameScreen = NinjaFootieGameScreen.GetComponent<NinjaFootie.NinjaScreenManager> ();
					GameScreen.OnClickPause ();
					GameScreen.OpenInAppItemsShop ();
				}
			} else if (isShopedPowerUpActive && NinjaRhombusCount == 0) {
				
				var GameScreen = NinjaFootieGameScreen.GetComponent<NinjaFootie.NinjaScreenManager> ();
				GameScreen.OnClickPause ();
				GameScreen.OpenInAppItemsShop ();

			}
		}

		public void NinjaWhiteStarPowerUp ()
		{
			if (!isShopedPowerUpActive) {
				if (NinjaWhiteStarCount >= 1) {
					NinjaWhiteStar = true;
					StartCoroutine (SpawnPurchsedPowerup (PowerUp [1]));
				} else {
					var GameScreen = NinjaFootieGameScreen.GetComponent<NinjaFootie.NinjaScreenManager> ();
					GameScreen.OnClickPause ();
					GameScreen.OpenInAppItemsShop ();
				}
			} else if (isShopedPowerUpActive && NinjaWhiteStarCount == 0) {
				
				var GameScreen = NinjaFootieGameScreen.GetComponent<NinjaFootie.NinjaScreenManager> ();
				GameScreen.OnClickPause ();
				GameScreen.OpenInAppItemsShop ();

			}
		}

		public void NinjaSquarePowerUp ()
		{
			if (!isShopedPowerUpActive) {
				if (NinjaSquareCount >= 1) {
					NinjaSquare = true;
					StartCoroutine (SpawnPurchsedPowerup (PowerUp [2]));
				} else {
					var GameScreen = NinjaFootieGameScreen.GetComponent<NinjaFootie.NinjaScreenManager> ();
					GameScreen.OnClickPause ();
					GameScreen.OpenInAppItemsShop ();
				}
			} else if (isShopedPowerUpActive && NinjaSquareCount == 0) {
				
				var GameScreen = NinjaFootieGameScreen.GetComponent<NinjaFootie.NinjaScreenManager> ();
				GameScreen.OnClickPause ();
				GameScreen.OpenInAppItemsShop ();

			}
		}

		public void NinjaGoldenStarPowerUp ()
		{
			if (!isShopedPowerUpActive) {
				if (NinjaGoldenStarCount >= 1) {
					NinjaGoldenStar = true;
					StartCoroutine (SpawnPurchsedPowerup (PowerUp [3]));
				} else {
					var GameScreen = NinjaFootieGameScreen.GetComponent<NinjaFootie.NinjaScreenManager> ();
					GameScreen.OnClickPause ();
					GameScreen.OpenInAppItemsShop ();
				}
			} else if (isShopedPowerUpActive && NinjaGoldenStarCount == 0) {
				
				var GameScreen = NinjaFootieGameScreen.GetComponent<NinjaFootie.NinjaScreenManager> ();
				GameScreen.OnClickPause ();
				GameScreen.OpenInAppItemsShop ();

			}
		}

		public IEnumerator SpawnPurchsedPowerup (GameObject powerUp)
		{
			yield return new WaitForSeconds (0.0f);

			if (NinjaGoldenStar) {
				NinjaGoldenStarCount--;
				if (HartPlayerRegistration.Isplayforpaid) {
					PlayerPrefs.SetInt ("NinjaGoldenStarCountForPaid", NinjaGoldenStarCount);
				} else {
					PlayerPrefs.SetInt ("NinjaGoldenStarCount", NinjaGoldenStarCount);
				}
				NinjaGoldenStarText.text = NinjaGoldenStarCount.ToString ();
				NinjaGoldenStar = false;
			}
			if (NinjaRhombus) {
				NinjaRhombusCount--;
				if (HartPlayerRegistration.Isplayforpaid) {
					PlayerPrefs.SetInt ("NinjaRhombusCountForPaid", NinjaRhombusCount);
				} else {
					PlayerPrefs.SetInt ("NinjaRhombusCount", NinjaRhombusCount);
				}
				NinjaRhombusText.text = NinjaRhombusCount.ToString ();
				NinjaRhombus = false;
			}
			if (NinjaSquare) {
				NinjaSquareCount--;
				if (HartPlayerRegistration.Isplayforpaid) {
					PlayerPrefs.SetInt ("NinjaSquareCountForPaid", NinjaSquareCount);
				} else {
					PlayerPrefs.SetInt ("NinjaSquareCount", NinjaSquareCount);
				}
				NinjaSquareText.text = NinjaSquareCount.ToString ();
				NinjaSquare = false;
			}
			if (NinjaWhiteStar) {
				NinjaWhiteStarCount--;
				if (HartPlayerRegistration.Isplayforpaid) {
					PlayerPrefs.SetInt ("NinjaWhiteStarCountForPaid", NinjaWhiteStarCount);
				} else {
					PlayerPrefs.SetInt ("NinjaWhiteStarCount", NinjaWhiteStarCount);
				}
				NinjaWhiteStarText.text = NinjaWhiteStarCount.ToString ();
				NinjaWhiteStar = false;
			}

			PurchasedpowerUpGameObject = Instantiate (powerUp, Vector3.zero, Quaternion.identity) as GameObject;
			isShopedPowerUpActive = true;	

			PurchasedpowerUpGameObject.transform.position = new Vector3 (Random.Range (-ResctritedX, ResctritedX), Random.Range (-ResctritedY, ResctritedY), 0f);
			Invoke ("FiveSecondTimerForShopedItem", 5f);
		}


		public IEnumerator SpawnPowerup (GameObject powerUp)
		{
			yield return new WaitForSeconds (Random.Range (10.00f, 20.00f));

			powerUpGameObject = Instantiate (powerUp, Vector3.zero, Quaternion.identity) as GameObject;
			isPowerUpActive = true;	

			powerUpGameObject.transform.position = new Vector3 (Random.Range (-ResctritedX, ResctritedX), Random.Range (-ResctritedY, ResctritedY), 0f);
			Invoke ("FiveSecondTimer", 5f);
		}

		void FiveSecondTimer ()
		{
			if (powerUpGameObject && !isPowerUpTaken) {
				DeactivatePowerUpGameObject (powerUpGameObject);
				Destroy (powerUpGameObject);
			}
		}

		void FiveSecondTimerForShopedItem ()
		{
			if (PurchasedpowerUpGameObject && !isShopedPowerUpTaken) {
				DeactivatePowerUpGameObjectShopedItem (PurchasedpowerUpGameObject);
				Destroy (PurchasedpowerUpGameObject);
			}
		}

		/// <summary>
		/// Which Power Up is available according to boolean.
		/// 1. isRohmbusAvailable
		/// 2. isSquareAvailable
		/// 3. isWhiteStarAvailable
		/// 4. isGoldenStarAvailable
		/// </summary>
		public void PowerupCount (bool isRohmbusAvailable, bool isWhiteStarAvailable, bool isSquareAvailable, bool isGoldenStarAvailable)
		{
			if (isRohmbusAvailable) {
				PowerUpsList.Add (PowerUp [0]);
			}
			if (isWhiteStarAvailable) {
				PowerUpsList.Add (PowerUp [1]);		
			}
			if (isSquareAvailable) {
				PowerUpsList.Add (PowerUp [2]);
			}
			if (isGoldenStarAvailable) {
				PowerUpsList.Add (PowerUp [3]);
			}
				

			if (PowerUpsList.Count >= 1) {	
				SelectTypeOfPowerup ();
			}

		}

		void SelectTypeOfPowerup ()
		{
			var Go = GetNextPowerUpGameObject ();
			PowerUpsList.Remove (Go);
			StartCoroutine (SpawnPowerup (Go));	
		}


		public void DeactivatePowerUpGameObject (GameObject powerUp)
		{
			isPowerUpActive = false;	
			powerUpGameObject = null;
			powerUp.GetComponent <SpriteRenderer> ().enabled = false;
			powerUp.GetComponent <BoxCollider2D> ().enabled = false;

			// Timed Next PowerUp if there is more than one powerUp possibilities in same level
			if (PowerUpsList.Count >= 1) {	
				SelectTypeOfPowerup ();
			}
		}

		public void DeactivatePowerUpGameObjectShopedItem (GameObject powerUp)
		{
			isShopedPowerUpActive = false;	
			PurchasedpowerUpGameObject = null;
			powerUp.GetComponent <SpriteRenderer> ().enabled = false;
			powerUp.GetComponent <BoxCollider2D> ().enabled = false;


		}

		GameObject GetNextPowerUpGameObject ()
		{
			return PowerUpsList [Random.Range (0, PowerUpsList.Count)];
		}
	}
}
