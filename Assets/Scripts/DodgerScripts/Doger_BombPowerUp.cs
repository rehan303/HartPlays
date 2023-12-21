using UnityEngine;
using System.Collections;

public class Doger_BombPowerUp : MonoBehaviour
{
	DogerPowerUpController powerUpController;
	public GameObject BlastObject;
	AudioSource audioSource;

	void Start ()
	{
		audioSource = GetComponent <AudioSource> ();
		powerUpController = transform.parent.GetComponent<DogerPowerUpController> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			powerUpController.UsingBuyPowerUp = false;
			BackgroundMusicManger.instance.PlaySound (audioSource);
			foreach (GameObject bullet in Guns.instaintiatedBulletsOnScreen) {
				GameObject TempBlastObj = Instantiate (BlastObject, bullet.transform.position, Quaternion.identity)as GameObject;
				this.gameObject.transform.position = new Vector3 (0, 0, 0);
				Destroy (bullet.gameObject);
				DogerPowerUpController.PowerUpAnimations.Add (TempBlastObj);
				StartCoroutine (DestroyBlastObjects (TempBlastObj));
			}
			Guns.instaintiatedBulletsOnScreen.Clear ();

			foreach (GameObject guns in RandomInstantiationOfGuns_Dodger.instantiatedGuns_Runtime) {
				GameObject TempBlastObj = Instantiate (BlastObject, guns.transform.position, Quaternion.identity)as GameObject;
				this.gameObject.transform.position = new Vector3 (0, 0, 0);
				Destroy (guns.gameObject);
				DogerPowerUpController.PowerUpAnimations.Add (TempBlastObj);
				StartCoroutine (DestroyBlastObjects (TempBlastObj));
			}
			RandomInstantiationOfGuns_Dodger.instantiatedGuns_Runtime.Clear ();
			powerUpController.DeactivateInstnatiatedPowerUp (gameObject);
		}
	}

	public void ActiveBombPoweUp ()
	{
//		powerUpController.UsingBuyPowerUp = false;
		BackgroundMusicManger.instance.PlaySound (audioSource);
		foreach (GameObject bullet in Guns.instaintiatedBulletsOnScreen) {
			GameObject TempBlastObj = Instantiate (BlastObject, bullet.transform.position, Quaternion.identity)as GameObject;
			this.gameObject.transform.position = new Vector3 (0, 0, 0);
			Destroy (bullet.gameObject);
			DogerPowerUpController.PowerUpAnimations.Add (TempBlastObj);
			StartCoroutine (DestroyBlastObjects (TempBlastObj));
		}
		Guns.instaintiatedBulletsOnScreen.Clear ();

		foreach (GameObject guns in RandomInstantiationOfGuns_Dodger.instantiatedGuns_Runtime) {
			GameObject TempBlastObj = Instantiate (BlastObject, guns.transform.position, Quaternion.identity)as GameObject;
			this.gameObject.transform.position = new Vector3 (0, 0, 0);
			Destroy (guns.gameObject);
			DogerPowerUpController.PowerUpAnimations.Add (TempBlastObj);
			StartCoroutine (DestroyBlastObjects (TempBlastObj));
		}
		RandomInstantiationOfGuns_Dodger.instantiatedGuns_Runtime.Clear ();
		powerUpController.DeactivateInstnatiatedPowerUp (gameObject);
	}

	IEnumerator DestroyBlastObjects (GameObject go)
	{
		float time = go.GetComponent<Animator> ().GetCurrentAnimatorClipInfo (0).Length;

		yield return new WaitForSeconds (time);
		GameObject.Destroy (go);
		DogerPowerUpController.PowerUpAnimations.Remove (go);
	}
}
