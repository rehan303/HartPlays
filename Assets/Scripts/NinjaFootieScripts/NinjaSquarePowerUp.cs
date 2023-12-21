using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace NinjaFootie
{
	public class NinjaSquarePowerUp : MonoBehaviour
	{
		float RealWaitTime = 0f;
		public Sprite RedColorBall;
		GameObject Player;
		public Sprite[] OtherTwoColors;
		SpriteRenderer Ball_spriteRenderer;
	
		NinjaPowerUpController ninjaPowerUpController;
		AudioSource audioSource;

		void Start ()
		{
			audioSource = GetComponent<AudioSource> ();
			ninjaPowerUpController = GameObject.Find ("NinjaPowerUpController").GetComponent<NinjaPowerUpController> ();
		}

		void Update ()
		{
			if (Player) {
				transform.localPosition = new Vector2 (0f, 0.9f);
			}
		}

		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.gameObject.CompareTag ("Player")) {
				if (other.gameObject.CompareTag ("Player")) {	
					if (!other.GetComponent <NinjaFootiePlayer> ().gotball) {
						ninjaPowerUpController.isShopedPowerUpActive = false;
						Player = other.gameObject;
						other.GetComponent<NinjaFootiePlayer> ().gotPowerUp = true;
						transform.parent = Player.transform;
						ninjaPowerUpController.isPowerUpTaken = true;
						ninjaPowerUpController.isShopedPowerUpTaken = true;
					}
				}
			}

			if (other.gameObject.layer == 23 && Player) {
				ninjaPowerUpController.isShopedPowerUpActive = false;
				StartCoroutine (TimerWait ());
				ninjaPowerUpController.isPowerUpTaken = false;
				ninjaPowerUpController.isShopedPowerUpTaken = false;
				ninjaPowerUpController.DeactivatePowerUpGameObject (gameObject);
				BackgroundMusicManger.instance.PlaySound (audioSource);
			}
		}

		IEnumerator DeactivatePowerUp (float time)
		{
			yield return new WaitForSeconds (time);
			// Deactivate or Apply Normals Again..
//			NinjaBall.ballInstance.isColorChanging = true;
			Destroy (gameObject, 0.1f);

		}

		IEnumerator TimerWait ()
		{ 
			yield return new WaitUntil (() => NinjaBall.ballInstance);
	
			Ball_spriteRenderer = NinjaBall.ballInstance.GetComponent<SpriteRenderer> ();
			Ball_spriteRenderer.sprite = OtherTwoColors [Random.Range (0, 2)];
			Player.GetComponent<NinjaFootiePlayer> ().gotPowerUp = false;
			Player = null;
			NinjaBall.ballInstance.isColorChanging = false;
			int Time = Mathf.CeilToInt (NinjaBall.BallcolorChangingRate * 2);
			StartCoroutine (RunTimerEverySecond (Time));

			for (int i = 0; i <= 1; i++) {
				ChangeColorOfBall ();
				yield return new WaitForSeconds (NinjaBall.BallcolorChangingRate);
			}
			ApplyPowerUpEffect ();
		

		}

		void ApplyPowerUpEffect ()
		{
			Ball_spriteRenderer = NinjaBall.ballInstance.GetComponent<SpriteRenderer> ();

			if (Ball_spriteRenderer.sprite != RedColorBall) {			
				Ball_spriteRenderer.sprite = RedColorBall;
			}
			ninjaPowerUpController.TimerText.gameObject.SetActive (false);		

			StartCoroutine (DeactivatePowerUp (NinjaBall.BallcolorChangingRate));
		}

		void ChangeColorOfBall ()//changes ball color...
		{
			Ball_spriteRenderer = NinjaBall.ballInstance.GetComponent<SpriteRenderer> ();

			if (Ball_spriteRenderer.sprite == OtherTwoColors [0]) {
				Ball_spriteRenderer.sprite = OtherTwoColors [1];
			} else {
				Ball_spriteRenderer.sprite = OtherTwoColors [0];
			}			
		}

		IEnumerator RunTimerEverySecond (int time)// shows timer only 
		{	
			ninjaPowerUpController.TimerText.gameObject.SetActive (true);
			RealWaitTime = time;

			for (int i = 0; i < time; i++) {
				RealWaitTime--;
				ninjaPowerUpController.TimerText.text = RealWaitTime.ToString ();
				yield return new WaitForSeconds (1f);
			}
		}
	}
}