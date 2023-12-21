using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DogerPowerUpController : MonoBehaviour
{
	public GameObject DodgerGameScreen;
	public static List<GameObject> PowerUpAnimations = new List<GameObject> ();

	public GameObject[] PowerUpsArray;

	public bool isPowerUpActive;
	public float speed = 0.5f;

	public GameObject powerUpGameObject;

	// to be private Only....

	float ResctritedX = 2f;
	float ResctritedY = 5f;


	[Header ("Dodger Power Ups Count")]
	public int DodgerBombCount;
	public int DodgerInvincibleCount;
	public int DodgerSlowCount;
	public int DodgerMergeCount;
	public GameObject PurchasedPowerUp;

	[Header ("Dodger Power ups count GUI text")]
	public Text BombText;
	public Text InvincibleText;
	public Text SlowText;
	public Text MergeText;

	[Header ("DodgerPowerUpBool")]
	bool DodgerBomb;
	bool DodgerInvincible;
	bool DodgerSlow;
	bool DodgerMerge;
	public bool UsingBuyPowerUp;

	void Start ()
	{
		//Update Power up count from PlayerPrefs
		UpdatePowerUpCount ();

		PowerUpAnimations.Clear ();
		//this will stop the power up ability in game play ++++++ Cahnges done by Rehan as per client requirment
//		StartCoroutine (SpawnPowerup (GetNextPowerUpGameObject ()));	
	}

	void Update ()
	{
		if (isPowerUpActive && powerUpGameObject != null) {
			powerUpGameObject.transform.Translate (Vector3.down * Time.deltaTime * (Guns.GunMovement_Speed + speed));	
		
			if (powerUpGameObject.transform.position.y < -6) {
				DeactivateInstnatiatedPowerUp (powerUpGameObject);
			}
		}
//		if (UsingBuyPowerUp && PurchasedPowerUp != null) {
//			PurchasedPowerUp.transform.Translate (Vector3.down * Time.deltaTime * (Guns.GunMovement_Speed + speed));
//			if (PurchasedPowerUp.transform.position.y < -6) {
//				DeactivateBuyInstnatiatedPowerUp (PurchasedPowerUp);
//			}
//		}
	}

	public void InvinciblePowerUp ()
	{
		if (!HartPlayerRegistration.Isplayforpaid) {
			if (!UsingBuyPowerUp) {
				if (DodgerInvincibleCount >= 1) {
					DodgerInvincible = true;
					UsingBuyPowerUp = true;
					StartCoroutine (DeactivePowerUp ());
					StartCoroutine (UseSpawnPowerup (PowerUpsArray [0]));
				} else if (!UsingBuyPowerUp) {
					var GameScreen = DodgerGameScreen.GetComponent<DogerGameManager> ();
					GameScreen.OnClickPause ();
					GameScreen.BuyInAppItems ();
				}
			} else if (UsingBuyPowerUp && DodgerInvincibleCount == 0) {
				if (!UsingBuyPowerUp) {
					var GameScreen = DodgerGameScreen.GetComponent<DogerGameManager> ();
					GameScreen.OnClickPause ();
					GameScreen.BuyInAppItems ();
				}
			}
		}else
		{
			if (!UsingBuyPowerUp) {
				if (DodgerInvincibleCount >= 1) {
					DodgerInvincible = true;
					UsingBuyPowerUp = true;
					StartCoroutine (DeactivePowerUp ());
					StartCoroutine (UseSpawnPowerup (PowerUpsArray [0]));
				} 
			}
		}

		
	}

	public void BombPowerUp ()
	{
		if (!HartPlayerRegistration.Isplayforpaid) {
			
			if (!UsingBuyPowerUp) {
				if (DodgerBombCount >= 1) {
					DodgerBomb = true;
					UsingBuyPowerUp = true;
					StartCoroutine (DeactiveBombPowerUp ());
					StartCoroutine (UseSpawnPowerup (PowerUpsArray [1]));
				} else if (!UsingBuyPowerUp) {
					var GameScreen = DodgerGameScreen.GetComponent<DogerGameManager> ();
					GameScreen.OnClickPause ();
					GameScreen.BuyInAppItems ();
				}
			} else if (UsingBuyPowerUp && DodgerBombCount == 0) {
				if (!UsingBuyPowerUp) {
					var GameScreen = DodgerGameScreen.GetComponent<DogerGameManager> ();
					GameScreen.OnClickPause ();
					GameScreen.BuyInAppItems ();
				}

			}
		}else
		{
			if (!UsingBuyPowerUp) {
				if (DodgerBombCount >= 1) {
					DodgerBomb = true;
					UsingBuyPowerUp = true;
					StartCoroutine (DeactiveBombPowerUp ());
					StartCoroutine (UseSpawnPowerup (PowerUpsArray [1]));
				} 
			}
		}


	}

	public void SlowDownPowerUp ()
	{
		if (!HartPlayerRegistration.Isplayforpaid) {
			
			if (!UsingBuyPowerUp) {
				if (DodgerSlowCount >= 1) {
					DodgerSlow = true;
					UsingBuyPowerUp = true;
					StartCoroutine (DeactivePowerUp ());
					StartCoroutine (UseSpawnPowerup (PowerUpsArray [2]));
				} else if (!UsingBuyPowerUp) {
					var GameScreen = DodgerGameScreen.GetComponent<DogerGameManager> ();
					GameScreen.OnClickPause ();
					GameScreen.BuyInAppItems ();
				}
			} else if (UsingBuyPowerUp && DodgerSlowCount == 0) {
				if (!UsingBuyPowerUp) {
					var GameScreen = DodgerGameScreen.GetComponent<DogerGameManager> ();
					GameScreen.OnClickPause ();
					GameScreen.BuyInAppItems ();
				}

			}
		}else
		{
			if (!UsingBuyPowerUp) {
				if (DodgerSlowCount >= 1) {
					DodgerSlow = true;
					UsingBuyPowerUp = true;
					StartCoroutine (DeactivePowerUp ());
					StartCoroutine (UseSpawnPowerup (PowerUpsArray [2]));
				} 
			}
			
		}


	}

	public void MergePowerUp ()
	{
		if (!HartPlayerRegistration.Isplayforpaid) {
			if (!UsingBuyPowerUp) {
				if (DodgerMergeCount >= 1) {
					DodgerMerge = true;
					UsingBuyPowerUp = true;
					StartCoroutine (DeactivePowerUp ());
					StartCoroutine (UseSpawnPowerup (PowerUpsArray [3]));
				} else if (!UsingBuyPowerUp) {
					var GameScreen = DodgerGameScreen.GetComponent<DogerGameManager> ();
					GameScreen.OnClickPause ();
					GameScreen.BuyInAppItems ();
				}
			} else if (UsingBuyPowerUp && DodgerMergeCount == 0) {
				if (!UsingBuyPowerUp) {
					var GameScreen = DodgerGameScreen.GetComponent<DogerGameManager> ();
					GameScreen.OnClickPause ();
					GameScreen.BuyInAppItems ();
				}
			}
		}else
		{
			if (!UsingBuyPowerUp) {
				if (DodgerMergeCount >= 1) {
					DodgerMerge = true;
					UsingBuyPowerUp = true;
					StartCoroutine (DeactivePowerUp ());
					StartCoroutine (UseSpawnPowerup (PowerUpsArray [3]));
				} 
			}
		}

	}

	IEnumerator DeactivePowerUp ()
	{
		yield return new WaitForSeconds (5f);
		UsingBuyPowerUp = false;
	}
	IEnumerator DeactiveBombPowerUp ()
	{
		yield return new WaitForSeconds (2f);
		UsingBuyPowerUp = false;
	}

	public void UpdatePowerUpCount ()
	{
		if (HartPlayerRegistration.Isplayforpaid) {
			DodgerBombCount = PlayerPrefs.GetInt ("DodgerBombCountForPaid", 0);
			DodgerInvincibleCount = PlayerPrefs.GetInt ("DodgerInvincibleCountForPaid", 0);
			DodgerSlowCount = PlayerPrefs.GetInt ("DodgerSlowCountForPaid", 0);
			DodgerMergeCount = PlayerPrefs.GetInt ("DodgerMergeCountForPaid", 0);	
		} else {
			DodgerBombCount = PlayerPrefs.GetInt ("DodgerBombCount", 0);
			DodgerInvincibleCount = PlayerPrefs.GetInt ("DodgerInvincibleCount", 0);
			DodgerSlowCount = PlayerPrefs.GetInt ("DodgerSlowCount", 0);
			DodgerMergeCount = PlayerPrefs.GetInt ("DodgerMergeCount", 0);	
		}
		BombText.text = DodgerBombCount.ToString ();
		InvincibleText.text = DodgerInvincibleCount.ToString ();
		SlowText.text = DodgerSlowCount.ToString ();
		MergeText.text = DodgerMergeCount.ToString ();

	}


	public IEnumerator SpawnPowerup (GameObject powerUp)
	{
		print ("Next powerUp will be ==>>>>" + powerUp.name);
		if (DogerGameManager.DistenceCovered < 130f) {
			yield return new WaitForSeconds (Random.Range (20.00f, 40.00f));
		} else if (DogerGameManager.DistenceCovered > 130f && DogerGameManager.DistenceCovered < 250f) {
			yield return new WaitForSeconds (Random.Range (40.00f, 58.00f));	
		} else if (DogerGameManager.DistenceCovered > 250f && DogerGameManager.DistenceCovered < 400f) {
			yield return new WaitForSeconds (Random.Range (59.00f, 80.00f));	
		}
		powerUpGameObject = powerUp;
		powerUp.transform.position = new Vector3 (Random.Range (-ResctritedX, ResctritedX), ResctritedY, 0f);

		PowerUpRendererStatus (powerUp, true);	
	}

	public IEnumerator UseSpawnPowerup (GameObject CurntpowerUp)
	{
		print ("Next powerUp will be ==>>>>" + CurntpowerUp.name);

		if (DodgerInvincible) {
			DodgerInvincibleCount--;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("DodgerInvincibleCountForPaid", DodgerInvincibleCount);
			} else {
				PlayerPrefs.SetInt ("DodgerInvincibleCount", DodgerInvincibleCount);
			}
			InvincibleText.text = DodgerInvincibleCount.ToString ();
			DodgerInvincible = false;
			CurntpowerUp.GetComponent<Doger_InvinciblePowerUp> ().ActiveInvinciblePowerUp ();
		}
		if (DodgerBomb) {
			DodgerBombCount--;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("DodgerBombCountForPaid", DodgerBombCount);
			} else {
				PlayerPrefs.SetInt ("DodgerBombCount", DodgerBombCount);
			}
			BombText.text = DodgerBombCount.ToString ();
			DodgerBomb = false;
			CurntpowerUp.GetComponent<Doger_BombPowerUp> ().ActiveBombPoweUp ();
		}
		if (DodgerSlow) {
			DodgerSlowCount--;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("DodgerSlowCountForPaid", DodgerSlowCount);
			} else {
				PlayerPrefs.SetInt ("DodgerSlowCount", DodgerSlowCount);
			}
			SlowText.text = DodgerSlowCount.ToString ();
			DodgerSlow = false;
			CurntpowerUp.GetComponent<Doger_SlowPowerUp> ().ActiveReversePowerUp ();
		}
		if (DodgerMerge) {
			DodgerMergeCount--;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("DodgerMergeCountForPaid", DodgerMergeCount);
			} else {
				PlayerPrefs.SetInt ("DodgerMergeCount", DodgerMergeCount);
			}
			MergeText.text = DodgerMergeCount.ToString ();
			DodgerMerge = false;
			CurntpowerUp.GetComponent<Doger_MergePowerUp> ().ActiveMergePowerUp ();
		}


//		UsingBuyPowerUp = true;
//		PurchasedPowerUp = CurntpowerUp;
//		CurntpowerUp.transform.position = new Vector3 (Random.Range (-ResctritedX, ResctritedX), ResctritedY, 0f);

		PowerUpRendererStatusForBuyItem (CurntpowerUp, false);	
		yield return new WaitForSeconds (Random.Range (0.00f, 0.00f));

	}

	void PowerUpRendererStatus (GameObject powerUp, bool isActive)
	{
		powerUp.GetComponent <SpriteRenderer> ().enabled = isActive;
		powerUp.GetComponent <BoxCollider2D> ().enabled = isActive;

		if (powerUp.GetComponent<Animator> () != null) {
			powerUp.GetComponent<Animator> ().enabled = isActive;
		}

		isPowerUpActive = isActive;	
	}

	void PowerUpRendererStatusForBuyItem (GameObject powerUp, bool isActive)
	{
		powerUp.GetComponent <SpriteRenderer> ().enabled = isActive;
		powerUp.GetComponent <BoxCollider2D> ().enabled = isActive;

		if (powerUp.GetComponent<Animator> () != null) {
			powerUp.GetComponent<Animator> ().enabled = isActive;
		}

//		UsingBuyPowerUp = isActive;	
	}

	public void DeactivateBuyInstnatiatedPowerUp (GameObject powerUp)
	{
		PowerUpRendererStatusForBuyItem (powerUp, false);
		PurchasedPowerUp = null;


		// spwan next power up....
//		StartCoroutine (SpawnPowerup (GetNextPowerUpGameObject ()));
	}

	public void DeactivateInstnatiatedPowerUp (GameObject powerUp)
	{
		PowerUpRendererStatus (powerUp, false);
		powerUpGameObject = null;
		// spwan next power up....
//		StartCoroutine (SpawnPowerup (GetNextPowerUpGameObject ()));
	}

	GameObject GetNextPowerUpGameObject ()
	{
		if (DogerGameManager.isSliptEnabled) {
			print ("ArrayLength == >>> " + PowerUpsArray.Length);
			return PowerUpsArray [Random.Range (0, PowerUpsArray.Length)];
		} else {
			print ("ArrayLength -1 == >>> " + (PowerUpsArray.Length - 1));
			
			return PowerUpsArray [Random.Range (0, PowerUpsArray.Length - 1)];
		}
	}
}
