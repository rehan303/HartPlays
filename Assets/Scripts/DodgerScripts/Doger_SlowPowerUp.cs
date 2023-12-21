using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Doger_SlowPowerUp : MonoBehaviour
{
	float InitialBulletSpeed;
	public Image TimerIndicator;

	DogerPowerUpController powerUpController;
	AudioSource audioSource;
	public AudioClip PowerUpActive;
	public AudioClip PowerUpDeactive;
	public static bool ReversePowerUp = false;

	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
		TimerIndicator.gameObject.SetActive (false);
		powerUpController = transform.parent.GetComponent<DogerPowerUpController> ();
		ReversePowerUp = false;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
//			powerUpController.UsingBuyPowerUp = false;
			InitialBulletSpeed = Bullets.BulletMovement_Speed;
			Bullets.BulletMovement_Speed = InitialBulletSpeed * 0.6f;
			BackgroundMusicManger.instance.PlaySoundEffect (audioSource, PowerUpActive);
			ReversePowerUp = true;
			StartCoroutine (DeactivatePowerUp (5));
			powerUpController.DeactivateInstnatiatedPowerUp (gameObject);//only sets the powerUp gameobject deactive
		}
	}

	public void ActiveReversePowerUp ()
	{
		InitialBulletSpeed = Bullets.BulletMovement_Speed;
		Bullets.BulletMovement_Speed = -InitialBulletSpeed * 1.38f;
		BackgroundMusicManger.instance.PlaySoundEffect (audioSource, PowerUpActive);
		ReversePowerUp = true;

		StartCoroutine (DeactivatePowerUp (5));
		powerUpController.DeactivateInstnatiatedPowerUp (gameObject);//only sets the powerUp gameobject deactive
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
		BackgroundMusicManger.instance.PlaySoundEffect (audioSource, PowerUpDeactive);
		TimerIndicator.gameObject.SetActive (false);
		Bullets.BulletMovement_Speed = InitialBulletSpeed;
		ReversePowerUp = false;
	
	}
}
