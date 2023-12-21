using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUp_Freeze : MonoBehaviour
{
	private List<GameObject> activeBalls = new List<GameObject> ();
	private GameObject randomInstantiationOfBalls;
	public GameObject gamecontrollerComponent;
	private CountdownTimer countDownTimer;
	static public bool isFreezeOn = false;
	public bool isFREEZEONTEST;
	public float _timer;
	float LeveltimeLeft;
	AudioSource m_AudioSource;

	void OnEnable ()
	{
		m_AudioSource = GetComponent <AudioSource> ();
		activeBalls = RandomInstantiationOfBalls.instantiatedBalls_Runtime;
		randomInstantiationOfBalls = GameObject.Find ("Colorballs_RandomInstantiation");
		//gamecontrollerComponent = GameObject.Find ("MainGameController");
		countDownTimer = GameObject.Find ("GameManager").GetComponent <CountdownTimer> ();
		isFreezeOn = false;
	}

	void Update ()
	{
		isFREEZEONTEST = isFreezeOn;
		if (_timer >= 0 && isFreezeOn) {
			_timer -= Time.deltaTime;
		}
	}

	//	isFREEZEONTEST = isFreezeOn;
	//	if (_timer >= 0 && isFreezeOn) {
	//		_timer -= Time.deltaTime;
	//	} else if (_timer <= 0 && isFreezeOn) {
	//		PowerUpContoller.isPowerUpAlive = false;
	//		PowerUpContoller.isShopedPowerUpAlive = false;
	//		FreezeActiveBalls (true);
	//
	//		//		Renderstatus (true);
	//
	//		isFreezeOn = false;
	//		countDownTimer.startTimer (LeveltimeLeft);
	//		this.gameObject.transform.position = new Vector3 (0, 0, 0);
	//	}

	public void Unfreeze ()
	{
		if (_timer <= 0 && isFreezeOn) {
			PowerUpContoller.isPowerUpAlive = false;
			PowerUpContoller.isShopedPowerUpAlive = false;
			FreezeActiveBalls (true);

			//        Renderstatus (true);

			isFreezeOn = false;
			LeveltimeLeft = countDownTimer.timeLeft;
			countDownTimer.startTimer (LeveltimeLeft);
			this.gameObject.transform.position = new Vector3 (0, 0, 0);
		}
	}

	public void FreezeActivated ()
	{
		LeveltimeLeft = countDownTimer.timeLeft;

		FreezeActiveBalls (false);
		isFreezeOn = true;
		//        StartCoroutine ("Timer");
		_timer = 5f;
		Invoke ("Unfreeze", 5.55f);
	}


	void FreezeActiveBalls (bool status)
	{
		countDownTimer.stop = !status;
		foreach (GameObject Obj in activeBalls) {
			FreezeStatus (Obj, status);
		}

		randomInstantiationOfBalls.SetActive (status);
	}

	void FreezeStatus (GameObject obj, bool status)
	{
		obj.GetComponent<Movement_RandomInstantiatedBalls> ().enabled = status;
		//gamecontrollerComponent.SetActive (status );
	}


	//	IEnumerator Timer ()
	//	{
	//		yield return new WaitForSeconds (5.0f);
	//		PowerUpContoller.isPowerUpAlive = false;
	//		PowerUpContoller.isShopedPowerUpAlive = false;
	//		FreezeActiveBalls (true);
	//
	////		Renderstatus (true);
	//
	//		isFreezeOn = false;
	//		countDownTimer.startTimer (LeveltimeLeft);
	//		this.gameObject.transform.position = new Vector3 (0, 0, 0);
	//	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.layer == 12 || coll.gameObject.layer == 13 || coll.gameObject.layer == 14 || coll.gameObject.layer == 15) {
			if (this.gameObject.layer == 16) {
				PowerUpContoller.isShopedPowerUpAlive = false;
				FreezeActivated ();

				isFreezeOn = true;
				BackgroundMusicManger.instance.PlaySound (m_AudioSource);

				Renderstatus (false);
				PowerUpContoller.PowerStayTime = 0.0f;
				this.gameObject.transform.position = new Vector3 (0, 0, 0);
			}
		}
	}

	public void ActiveFreezePowerUp ()
	{
		PowerUpContoller.isShopedPowerUpAlive = false;
		FreezeActivated ();

		isFreezeOn = true;
		BackgroundMusicManger.instance.PlaySound (m_AudioSource);

		Renderstatus (false);
		PowerUpContoller.PowerStayTime = 0.0f;
		this.gameObject.transform.position = new Vector3 (0, 0, 0);
	}

	public void Renderstatus (bool status)
	{
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = status; 
		this.gameObject.GetComponent<BoxCollider2D> ().enabled = status;
	}
}
