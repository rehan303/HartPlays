using UnityEngine;
using System.Collections;

namespace NinjaFootie
{
	public class NinjaWhiteStarPowerUp : MonoBehaviour
	{
		GameObject Player;

		float IntialReactionTimeOfOpponent = 0;
		float InitialSpeedofOpponent = 0;

		NinjaPowerUpController ninjaPowerUpController;
		AudioSource audioSource;
		public AudioClip PowerUpActive;
		public AudioClip PowerUpDeactive;

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
						Player = other.gameObject;
						ninjaPowerUpController.isShopedPowerUpActive = false;
						other.GetComponent<NinjaFootiePlayer> ().gotPowerUp = true;
						ninjaPowerUpController.isPowerUpTaken = true;
						ninjaPowerUpController.isShopedPowerUpTaken = true;
						transform.parent = Player.transform;
					}
				}
			}

			if (other.gameObject.layer == 23 && Player) {
				ninjaPowerUpController.isShopedPowerUpActive = false;
				ninjaPowerUpController.isPowerUpTaken = false;
				ninjaPowerUpController.isShopedPowerUpTaken = false;
				ApplyPowerUpEffect ();
			}
			
		}

		void ApplyPowerUpEffect ()
		{
			BackgroundMusicManger.instance.PlaySoundEffect (audioSource, PowerUpActive);
			IntialReactionTimeOfOpponent = AI_NinjaFootie.AiReactionTime;
			InitialSpeedofOpponent = AI_NinjaFootie.SpeedofMovement;

			AI_NinjaFootie.AiReactionTime = IntialReactionTimeOfOpponent * 1.5f;
			AI_NinjaFootie.SpeedofMovement = InitialSpeedofOpponent * 0.6f;

			StartCoroutine (DeactivatePowerUp (10f));
			ninjaPowerUpController.DeactivatePowerUpGameObject (gameObject);
		}

		IEnumerator DeactivatePowerUp (float time)
		{
			// Deactivate or Apply Normals Again..
			Player.GetComponent<NinjaFootiePlayer> ().gotPowerUp = false;
			Player = null;
			yield return new WaitForSeconds (time);
			print ("Deactivate or Apply Normals Again");
			BackgroundMusicManger.instance.PlaySoundEffect (audioSource, PowerUpDeactive);
			AI_NinjaFootie.AiReactionTime = IntialReactionTimeOfOpponent;
			AI_NinjaFootie.SpeedofMovement = InitialSpeedofOpponent;
			Destroy (gameObject, 0.1f);
		}
	}
}