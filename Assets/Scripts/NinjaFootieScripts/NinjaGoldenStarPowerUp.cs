using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NinjaFootie
{
	public class NinjaGoldenStarPowerUp : MonoBehaviour
	{
		public List<Sprite>	Colors;
		float ChangingRate = 1f;
		SpriteRenderer spriteRenderer;
		bool isColorChanging = true;
		List<Sprite> Tempcolors = new List<Sprite> ();

		GameObject Player;

		NinjaPowerUpController ninjaPowerUpController;

		AudioSource audioSource;

		void Start ()
		{
			audioSource = GetComponent<AudioSource> ();
			ninjaPowerUpController = GameObject.Find ("NinjaPowerUpController").GetComponent<NinjaPowerUpController> ();
		
			isColorChanging = true;

			spriteRenderer = GetComponent <SpriteRenderer> ();
			for (int i = 0; i < Colors.Count; i++) {
				Tempcolors.Add (Colors [i]);
			}

			StartCoroutine (PickRandomSprite ());
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
					isColorChanging = false;
				}
			}

			if (other.gameObject.layer == 23 && Player) {
				ninjaPowerUpController.isShopedPowerUpActive = false;
				ninjaPowerUpController.isPowerUpTaken = false;
				ninjaPowerUpController.isShopedPowerUpTaken = false;
				string OtherSpriteName = other.GetComponent<SpriteRenderer> ().sprite.name;
				if (OtherSpriteName == spriteRenderer.sprite.name.Split ('_') [1]) {
					ApplyPowerUpEffect ();
				} else {
					//Destroy PowerUp without doing anything.....	
					ninjaPowerUpController.DeactivatePowerUpGameObject (gameObject);
					StartCoroutine (DeactivatePowerUp (5f));
					Handheld.Vibrate ();
//					print("Putted in Wrong Net.............................................");
				}
			}
		}

		void ApplyPowerUpEffect ()
		{
			//apply effects 
			NinjaFootiePlayer.PlayerScore += 6;
			ninjaPowerUpController.DeactivatePowerUpGameObject (gameObject);
			StartCoroutine (DeactivatePowerUp (5f));
			BackgroundMusicManger.instance.PlaySound (audioSource);
		}

		IEnumerator DeactivatePowerUp (float time)
		{
			Player.GetComponent<NinjaFootiePlayer> ().gotPowerUp = false;
			Player = null;
			// remove and Apply normals...
			yield return new WaitForSeconds (time);
			Destroy (gameObject);
		}


		IEnumerator PickRandomSprite ()
		{
			if (isColorChanging) {
				if (spriteRenderer.sprite == null) {
					Sprite selected = Tempcolors [Random.Range (0, Tempcolors.Count)];
					spriteRenderer.sprite = selected;
					Tempcolors.Remove (selected);
				} else {
					Sprite selected = Tempcolors [Random.Range (0, Tempcolors.Count)];
					Tempcolors.Add (spriteRenderer.sprite);
					spriteRenderer.sprite = selected;
					Tempcolors.Remove (selected);
				}
				yield return new WaitForSeconds (ChangingRate);
			} else {
				yield return new WaitUntil (() => isColorChanging);
			}

			StartCoroutine (PickRandomSprite ());
		}
	}
}
