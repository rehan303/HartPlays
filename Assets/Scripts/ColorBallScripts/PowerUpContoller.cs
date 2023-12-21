using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PowerUpContoller : MonoBehaviour
{

	#region FIELDS

	[SerializeField]
	private GameObject[] powerupsObject;

	[SerializeField]
	private GameObject[] PurchsedPowerupsObject;

	public GameObject powerUp_GameObject;
	public List<GameObject> powerUps = new List<GameObject> ();

	public static bool isPowerUpAlive;
	public static bool isShopedPowerUpAlive;

	private float MinX, MaxX, MinY, MaxY;
	private MainGameController_Touch mainGameControllerTouch;
	private float offset = 2.0f;
	public static float PowerStayTime;

	[Header ("Power Ups Count")]
	public int BombCount;
	public int FreezeCount;
	public int ReminderCount;

	[Header ("Power ups count GUI text")]
	public Text BombText;
	public Text FreezeText;
	public Text ReminderText;

	[Header ("PowerUpBool")]
	bool Bomb;
	bool Freeze;
	bool Reminder;
	public GameObject PurchasedpowerUp_GameObject;


	#endregion

	void Awake ()
	{
		mainGameControllerTouch = GameObject.Find ("MainGameController").GetComponent<MainGameController_Touch> ();

		//print( "Counts :"+PowerupCount ( true, true, true, false ) );
		//PowerupCount ( true, true, true, true );
	}

	void OnEnable ()
	{
		MinX = -mainGameControllerTouch.ballRestrictedPositionX.position.x + offset;
		MaxX = mainGameControllerTouch.ballRestrictedPositionX.position.x - offset;
		MinY = -mainGameControllerTouch.ballRestrictedPositionY.position.y + offset;
		MaxY = mainGameControllerTouch.ballRestrictedPositionX.position.y - offset;
		isPowerUpAlive = false;
		isShopedPowerUpAlive = false;

		// PowerUps Count on GUI Display
		UpdatePowerUpCount ();

	}



	public void ResumeGameFormShop ()
	{
		mainGameControllerTouch.PauseButtonClicked ();
	}

	public void BombPowerUp ()
	{
		if (!HartPlayerRegistration.Isplayforpaid) {
			if (!isPowerUpAlive) {
				if (BombCount >= 1) {
					Bomb = true;
					isPowerUpAlive = true;
					Invoke ("DecativatePowerUp", 0.2f);
					StartCoroutine (UseBuySpawnPowerup (PurchsedPowerupsObject [0]));
				} 
			}
		}else
		{
			if (!isPowerUpAlive) {
				if (BombCount >= 1) {
					Bomb = true;
					isPowerUpAlive = true;
					Invoke ("DecativatePowerUp", 0.2f);
					StartCoroutine (UseBuySpawnPowerup (PurchsedPowerupsObject [0]));
				} 
			}
		}

	}

	public void FreezePowerUp ()
	{
		if (!HartPlayerRegistration.Isplayforpaid) {
			if (!isPowerUpAlive) {
				if (FreezeCount >= 1) {
					Freeze = true;
					isPowerUpAlive = true;
					Invoke ("DecativatePowerUp", 4f);
					StartCoroutine (UseBuySpawnPowerup (PurchsedPowerupsObject [1]));
				} else {
					mainGameControllerTouch.PauseButtonClicked ();
					mainGameControllerTouch.OnShopPanleClick ();
				}
			}
		}else
		{
			if (FreezeCount >= 1) {
				Freeze = true;
				isPowerUpAlive = true;
				Invoke ("DecativatePowerUp", 4f);
				StartCoroutine (UseBuySpawnPowerup (PurchsedPowerupsObject [1]));
			} 
		
		}
	}

	public void ReminderPowerUp ()
	{
		if (!HartPlayerRegistration.Isplayforpaid) {
			if (!isPowerUpAlive) {
				if (ReminderCount >= 1) {
					Reminder = true;
					isPowerUpAlive = true;
					Invoke ("DecativatePowerUp", 4f);
					StartCoroutine (UseBuySpawnPowerup (PurchsedPowerupsObject [2]));
				} else {
					mainGameControllerTouch.PauseButtonClicked ();
					mainGameControllerTouch.OnShopPanleClick ();

				}
			}
		}else
		{
			if (!isPowerUpAlive) {
				if (ReminderCount >= 1) {
					Reminder = true;
					isPowerUpAlive = true;
					Invoke ("DecativatePowerUp", 4f);
					StartCoroutine (UseBuySpawnPowerup (PurchsedPowerupsObject [2]));
				}
		
			}
		}
	}

	void DecativatePowerUp ()
	{
		isPowerUpAlive = false;
	}

	public void UpdatePowerUpCount ()
	{
		if (HartPlayerRegistration.Isplayforpaid) {

			BombCount = PlayerPrefs.GetInt ("BombCountForPaid", 0);
			FreezeCount = PlayerPrefs.GetInt ("FreezeCountForPaid", 0);
			ReminderCount = PlayerPrefs.GetInt ("ReminderCountForPaid", 0);

		} else {

			BombCount = PlayerPrefs.GetInt ("BombCount", 0);
			FreezeCount = PlayerPrefs.GetInt ("FreezeCount", 0);
			ReminderCount = PlayerPrefs.GetInt ("ReminderCount", 0);
		}
		BombText.text = BombCount.ToString ();
		FreezeText.text = FreezeCount.ToString ();
		ReminderText.text = ReminderCount.ToString ();
	}



	private void TypeOfPowerup ()
	{
		int tempValue = Random.Range (0, powerUps.Count);
		//StartCoroutine(SpawnpowerUps [tempValue] );

		StartCoroutine (SpawnPowerup (powerUps [tempValue]));
	}

	public IEnumerator SpawnPowerup (GameObject powerUp)
	{

		isPowerUpAlive = true;
		yield return new WaitForSeconds (SpawnPowerUpTimer (20.00f, 30.00f));
		powerUp_GameObject = powerUp;	
		powerUp_GameObject.transform.position = new Vector3 (Random.Range (MinX, MaxX), Random.Range (MinY, MaxY), 0f);
		powerUpRenderStatus (true);	

		StartCoroutine (DeactivateThePowerUpWhichIsInstnatiated ());
		//	yield return null;
	}

	public IEnumerator UseBuySpawnPowerup (GameObject powerUp)
	{

		// To check which power up is active
		//	if (Bomb) {
		//	BombCount--;
		//
		//	if (HartPlayerRegistration.Isplayforpaid) {
		//	PlayerPrefs.SetInt ("BombCountForPaid", BombCount);
		//	} else {
		//	PlayerPrefs.SetInt ("BombCount", BombCount);
		//	}
		//	BombText.text = BombCount.ToString ();
		//	Bomb = false;
		//	}
		//	if (Freeze) {
		//	FreezeCount--;
		//	if (HartPlayerRegistration.Isplayforpaid) {
		//	PlayerPrefs.SetInt ("FreezeCountForPaid", FreezeCount);
		//	} else {
		//	PlayerPrefs.SetInt ("FreezeCount", FreezeCount);
		//	}
		//	FreezeText.text = FreezeCount.ToString ();
		//	Freeze = false;
		//	}
		//	if (Reminder) {
		//	ReminderCount--;
		//
		//	if (HartPlayerRegistration.Isplayforpaid) {
		//	PlayerPrefs.SetInt ("ReminderCountForPaid", ReminderCount);
		//	} else {
		//	PlayerPrefs.SetInt ("ReminderCount", ReminderCount);
		//	}
		//	ReminderText.text = ReminderCount.ToString ();
		//	Reminder = false;
		//	}
		//	
		//	isShopedPowerUpAlive = true;
		//	if (PurchasedpowerUp_GameObject != null) {
		//	ShopedpowerUpRenderStatus (false);
		//	PurchasedpowerUp_GameObject = null;
		//	}
		//
		//	PurchasedpowerUp_GameObject = powerUp;
		//	PurchasedpowerUp_GameObject.transform.position = new Vector3 (Random.Range (MinX, MaxX), Random.Range (MinY, MaxY), 0f);
		//	ShopedpowerUpRenderStatus (true);	
		//	yield return new WaitForSeconds (8.0f);
		//	StartCoroutine (DeactivateTheBuyPowerUpWhichIsInstnatiated ());
		//	yield return null;

		// To check which power up is active
		if (Bomb) {
			BombCount--;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("BombCountForPaid", BombCount);
			} else {
				PlayerPrefs.SetInt ("BombCount", BombCount);
			}
			BombText.text = BombCount.ToString ();
			Bomb = false;
			powerUp.GetComponent<PowerUpBomb_All> ().ActiveBombPowerUp ();
		}
		if (Freeze) {
			FreezeCount--;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("FreezeCountForPaid", FreezeCount);
			} else {
				PlayerPrefs.SetInt ("FreezeCount", FreezeCount);
			}
			FreezeText.text = FreezeCount.ToString ();
			Freeze = false;
			powerUp.GetComponent<PowerUp_Freeze> ().ActiveFreezePowerUp ();
		}
		if (Reminder) {
			ReminderCount--;
			if (HartPlayerRegistration.Isplayforpaid) {
				PlayerPrefs.SetInt ("ReminderCountForPaid", ReminderCount);
			} else {
				PlayerPrefs.SetInt ("ReminderCount", ReminderCount);
			}
			ReminderText.text = ReminderCount.ToString ();
			Reminder = false;
			powerUp.GetComponent<Reminder_Bomb> ().ActiveReminderPowerUp ();
		}

		isShopedPowerUpAlive = true;
		if (PurchasedpowerUp_GameObject != null) {
			ShopedpowerUpRenderStatus (false);
			PurchasedpowerUp_GameObject = null;
		}

		//	PurchasedpowerUp_GameObject = powerUp;
		//	PurchasedpowerUp_GameObject.transform.position = new Vector3 (Random.Range (MinX, MaxX), Random.Range (MinY, MaxY), 0f);
		//Do not render and show on the screen as per client requrment 
		//	ShopedpowerUpRenderStatus (true);

		yield return new WaitForSeconds (0.0f);
		StopAllCoroutines ();
		StartCoroutine (DeactivateTheBuyPowerUpWhichIsInstnatiated ());
	}


	public float SpawnPowerUpTimer (float minTime, float maxTime)
	{
		float timer = Random.Range (minTime, maxTime);
		return timer;
	}

	public void PowerupCount (bool isBombAvailable, bool isFreezeAvailable, bool isReminderAvailable, bool isSlowUpAvailable)
	{
		if (isBombAvailable) {
			powerUps.Add (powerupsObject [0]);
		}
		if (isFreezeAvailable) {
			powerUps.Add (powerupsObject [1]);
		}
		if (isReminderAvailable) {
			powerUps.Add (powerupsObject [2]);	
		}
		if (isSlowUpAvailable) {
			powerUps.Add (powerupsObject [3]);
		}
		if (powerUps.Count >= 1) {	
			TypeOfPowerup ();
		}
	}

	public IEnumerator DeactivateThePowerUpWhichIsInstnatiated ()
	{
		PowerStayTime = 5.0f;
		//	powerUp_GameObject.GetComponent<Reminder_Bomb> (). PlayAnimation ();
		yield return new WaitForSeconds (PowerStayTime);

		isPowerUpAlive = false;

		powerUpRenderStatus (false);
		PowerStayTime = 0.0f;
	}

	public IEnumerator DeactivateTheBuyPowerUpWhichIsInstnatiated ()
	{
		//	powerUp_GameObject.GetComponent<Reminder_Bomb> (). PlayAnimation ();
		PowerStayTime = 8.0f;

		yield return new WaitForSeconds (PowerStayTime);

		isShopedPowerUpAlive = false;

		ShopedpowerUpRenderStatus (false);
		PurchasedpowerUp_GameObject = null;
		PowerStayTime = 0.0f;

	}


	public void powerUpRenderStatus (bool status)
	{
		powerUp_GameObject.GetComponent<SpriteRenderer> ().enabled = status;
		powerUp_GameObject.GetComponent<BoxCollider2D> ().enabled = status;
		Animator anim = powerUp_GameObject.GetComponent<Animator> ();

		if (anim != null) {
			anim.enabled = status;
			//	anim.Play();
		}
	}

	public void ShopedpowerUpRenderStatus (bool status)
	{
		if (PurchasedpowerUp_GameObject != null) {
			PurchasedpowerUp_GameObject.GetComponent<SpriteRenderer> ().enabled = status;
			PurchasedpowerUp_GameObject.GetComponent<BoxCollider2D> ().enabled = status;
			//	if (PurchasedpowerUp_GameObject.GetComponent<PowerUp_Freeze> ()) {
			//	PurchasedpowerUp_GameObject.GetComponent<PowerUp_Freeze> ().enabled = status;
			//	} else if (PurchasedpowerUp_GameObject.GetComponent<Reminder_Bomb> ()) {
			//	PurchasedpowerUp_GameObject.GetComponent<Reminder_Bomb> ().enabled = status;
			//	} else {
			//	PurchasedpowerUp_GameObject.GetComponent<PowerUpBomb_All> ().enabled = status;
			//	}	
			Animator anim = PurchasedpowerUp_GameObject.GetComponent<Animator> ();
			if (anim != null) {
				anim.enabled = status;
				//	anim.Play();
			}
		}

	}
}
