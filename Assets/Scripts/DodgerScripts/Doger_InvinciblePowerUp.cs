using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Doger_InvinciblePowerUp : MonoBehaviour
{
	bool CharacterRendererEnabled;
	DogerPowerUpController powerUpController;
	public Image TimerIndicator;
	public static bool isPowerUpEffectGoing = false;
	AudioSource audioSource;

	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
		powerUpController = transform.parent.GetComponent<DogerPowerUpController> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			BackgroundMusicManger.instance.PlaySound (audioSource);
			isPowerUpEffectGoing = true;
			powerUpController.UsingBuyPowerUp = false;
//			Player_GameObject.transform.GetChild (0).gameObject.SetActive (true);
			DogerPlayerController.isInvincibleFromBeingHit = true;
			StartCoroutine (DeactivatePowerUp (5));
			StartCoroutine (ApplyInvincibleEffect ());

			powerUpController.DeactivateInstnatiatedPowerUp (gameObject);

		}
	}

	public void ActiveInvinciblePowerUp ()
	{
		BackgroundMusicManger.instance.PlaySound (audioSource);
		isPowerUpEffectGoing = true;
//		powerUpController.UsingBuyPowerUp = false;
		//			Player_GameObject.transform.GetChild (0).gameObject.SetActive (true);
		DogerPlayerController.isInvincibleFromBeingHit = true;
		StartCoroutine (DeactivatePowerUp (5));
		StartCoroutine (ApplyInvincibleEffect ());

		powerUpController.DeactivateInstnatiatedPowerUp (gameObject);
	}

	IEnumerator DeactivatePowerUp (int time)
	{	
		TimerIndicator.sprite = GetComponent<SpriteRenderer> ().sprite;
		TimerIndicator.gameObject.SetActive (true);
		TimerIndicator.fillAmount = 1;

		for (int i = time; i > 0; i--) {
			yield return new WaitForSeconds (1f);
			TimerIndicator.fillAmount -= 0.2f;
		}

		TimerIndicator.gameObject.SetActive (false);
		isPowerUpEffectGoing = false;
//		Player_GameObject.transform.GetChild (0).gameObject.SetActive (false);
		DogerPlayerController.isInvincibleFromBeingHit = false;
		foreach (var player in  GameObject.FindGameObjectsWithTag ("Player")) {
			player.GetComponent<SpriteRenderer> ().enabled = true;
		}	
	}


	IEnumerator ApplyInvincibleEffect ()
	{
		while (isPowerUpEffectGoing) {
			CharacterRendererEnabled = !CharacterRendererEnabled;
			GameObject[] Players = GameObject.FindGameObjectsWithTag ("Player");
			foreach (var player in Players) {
				player.GetComponent<SpriteRenderer> ().enabled = CharacterRendererEnabled;
			}
			yield return new WaitForSeconds (0.2f);
		}

	}
}
