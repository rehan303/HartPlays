using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class PowerUpBomb_All : MonoBehaviour
{
	public Sprite[] BombTextures_Animations;
	public GameObject BombAnimation_GameObject;
	public GameObject BlastObject;
	public static List<GameObject> BombAnimation = new List<GameObject> ();
	//	public Animator BallsBlastAnimator;

	AudioSource m_AudioSource;

	void Start ()
	{
		m_AudioSource = GetComponent <AudioSource> ();
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.layer == 12 || coll.gameObject.layer == 13 || coll.gameObject.layer == 14 || coll.gameObject.layer == 15) {
			BombAnimation_GameObject.GetComponent<SpriteRenderer> ().enabled = true;
			BombAnimation_GameObject.transform.position = coll.gameObject.transform.position;
			PowerUpContoller.isShopedPowerUpAlive = false;
			StartCoroutine ("PlayAnimationOfBomb");
	
			foreach (GameObject obj in RandomInstantiationOfBalls.instantiatedBalls_Runtime) {
				GameManager.TotalPointsOrScores += 3;
				Destroy (obj);
				GameObject TempBlastObj = Instantiate (BlastObject, obj.transform.position, Quaternion.identity)as GameObject;
				Renderstatus (false);
				BombAnimation.Add (TempBlastObj);
				this.gameObject.transform.position = new Vector3 (0, 0, 0);

				StartCoroutine (DestroyBlastObjects (TempBlastObj));
				BackgroundMusicManger.instance.PlaySound (m_AudioSource);
				PowerUpContoller.PowerStayTime = 0.0f;
			}
		
			RandomInstantiationOfBalls.instantiatedBalls_Runtime.Clear ();
		}
	}

	public void ActiveBombPowerUp ()
	{
		BombAnimation_GameObject.GetComponent<SpriteRenderer> ().enabled = true;
		var gamemanager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		int rendomNum = Random.Range (0, 9);
		BombAnimation_GameObject.transform.position = gamemanager.potList [rendomNum].transform.position;
			
		PowerUpContoller.isShopedPowerUpAlive = false;
		StartCoroutine ("PlayAnimationOfBomb");

		foreach (GameObject obj in RandomInstantiationOfBalls.instantiatedBalls_Runtime) {
			GameManager.TotalPointsOrScores += 3;
			Destroy (obj);
			GameObject TempBlastObj = Instantiate (BlastObject, obj.transform.position, Quaternion.identity)as GameObject;
			Renderstatus (false);
			BombAnimation.Add (TempBlastObj);
			this.gameObject.transform.position = new Vector3 (0, 0, 0);

			StartCoroutine (DestroyBlastObjects (TempBlastObj));
			BackgroundMusicManger.instance.PlaySound (m_AudioSource);
			PowerUpContoller.PowerStayTime = 0.0f;
		}

		RandomInstantiationOfBalls.instantiatedBalls_Runtime.Clear ();
	}

	IEnumerator DestroyBlastObjects (GameObject GO)
	{
		float time = GO.GetComponent<Animator> ().GetCurrentAnimatorClipInfo (0).Length;

		yield return new WaitForSeconds (time);
		GameObject.Destroy (GO);
//		GO.SetActive (false);
		BombAnimation.Clear ();
	}

	IEnumerator PlayAnimationOfBomb ()
	{

		print ("Play Animation");
		for (int i = 0; i < BombTextures_Animations.Length; i++) {
			BombAnimation_GameObject.GetComponent<SpriteRenderer> ().sprite = BombTextures_Animations [i];
			yield return new WaitForSeconds (0.15f);

		}
		yield return new WaitForSeconds (0.15f);
		BombAnimation_GameObject.GetComponent<SpriteRenderer> ().enabled = false;
		BombAnimation_GameObject.transform.position = new Vector3 (0, 0, 0);
	}

	IEnumerator DeactivateBombAfterSometime ()
	{ 
		yield return new WaitForSeconds (5.0f);
		PowerUpContoller.isPowerUpAlive = false;
		PowerUpContoller.isShopedPowerUpAlive = false;

		Renderstatus (false);

		this.gameObject.transform.position = new Vector3 (0, 0, 0);
	}

	public void Renderstatus (bool status)
	{
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = status; 
		this.gameObject.GetComponent<BoxCollider2D> ().enabled = status;
	}
}
