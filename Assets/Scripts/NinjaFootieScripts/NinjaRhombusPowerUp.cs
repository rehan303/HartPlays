using UnityEngine;
using System.Collections;

namespace NinjaFootie
{
	public class NinjaRhombusPowerUp : MonoBehaviour
	{
		GameObject Player;
		NinjaPowerUpController ninjaPowerUpController;
		NetColorPicker netColorPicker;

		AudioSource audioSource;

		void Start ()
		{
			audioSource = GetComponent<AudioSource> ();
			ninjaPowerUpController = GameObject.Find ("NinjaPowerUpController").GetComponent<NinjaPowerUpController> ();
			netColorPicker = GameObject.Find ("OpponentsNet").GetComponent<NetColorPicker> ();
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
				if (!other.GetComponent <NinjaFootiePlayer> ().gotball) {
					Player = other.gameObject;
					ninjaPowerUpController.isShopedPowerUpActive = false;
					other.GetComponent<NinjaFootiePlayer> ().gotPowerUp = true;
					ninjaPowerUpController.isPowerUpTaken = true;
					ninjaPowerUpController.isShopedPowerUpTaken = true;
					transform.parent = Player.transform;
				}
			}

			if (other.gameObject.layer == 23 && Player) {
				ninjaPowerUpController.isShopedPowerUpActive = false;
				ninjaPowerUpController.isPowerUpTaken = false;
				ninjaPowerUpController.isPowerUpTaken = false;
				ApplyPowerUpEffect ();
			}
		}

		void ApplyPowerUpEffect ()
		{
			BackgroundMusicManger.instance.PlaySound (audioSource);
			netColorPicker.isChanging = false;
			ninjaPowerUpController.DeactivatePowerUpGameObject (gameObject);
			StartCoroutine (DeactivatePowerUp (5f));
		}

		IEnumerator DeactivatePowerUp (float time)
		{
			Player.GetComponent<NinjaFootiePlayer> ().gotPowerUp = false;
			Player = null;
			yield return new WaitForSeconds (time);
			netColorPicker.isChanging = true;
			Destroy (gameObject, 1f);
		}
	}
}
