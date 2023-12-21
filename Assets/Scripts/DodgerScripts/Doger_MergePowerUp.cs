using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Doger_MergePowerUp : MonoBehaviour
{
	public Animator CharacterAnimator;
	DogerPowerUpController powerUpController;
	DogerGameManager gameManager;
	public GameObject SecondPlayer;
	public Image TimerIndicator;
	AudioSource audioSource;

	void Start ()
	{		
		audioSource = GetComponent<AudioSource> ();
		gameManager = DogerGameManager.gameManger;
		powerUpController = transform.parent.GetComponent<DogerPowerUpController> ();
		CharacterAnimator.SetBool ("IsJoined", false);
	}

	void Update ()
	{
		if (DogerGameManager.isSliptEnabled && SecondPlayer == null) {
			SecondPlayer = GameObject.Find ("SecondPlayer");
		}
	}

	//	void OnGUI()
	//	{
	//		if(SecondPlayer != null)
	//		if (GUI.Button (new Rect(0,0,100,50),"MergePlayers"))
	//		{
	//			Test ();
	//		}
	//	}


	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player") && DogerGameManager.isSliptEnabled) {
			if (SecondPlayer != null) {
				print ("OnTriggerEnter   ______ Merge");
//				powerUpController.UsingBuyPowerUp = false;
				BackgroundMusicManger.instance.PlaySound (audioSource);
				StartCoroutine (MergeTwoCharacters (SecondPlayer.transform.position, CharacterAnimator.gameObject.transform.position, 0.5f));

				StartCoroutine (DeactivatePowerUp (10));
				powerUpController.DeactivateInstnatiatedPowerUp (gameObject);//only sets the power up gameobject deactive 
			}
		}
	}

	public void ActiveMergePowerUp ()
	{
		if (DogerGameManager.isSliptEnabled) {
			if (SecondPlayer != null) {
				print ("OnTriggerEnter   ______ Merge");
//				powerUpController.UsingBuyPowerUp = false;
				BackgroundMusicManger.instance.PlaySound (audioSource);
				StartCoroutine (MergeTwoCharacters (SecondPlayer.transform.position, CharacterAnimator.gameObject.transform.position, 0.5f));

				StartCoroutine (DeactivatePowerUp (10));
				powerUpController.DeactivateInstnatiatedPowerUp (gameObject);//only sets the power up gameobject deactive 
			}
		}
	}

	IEnumerator MergeTwoCharacters (Vector3  startPos, Vector3  endPos, float time)
	{
		float i = 0.0f;
		float rate = 1.0f / time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			SecondPlayer.transform.position = Vector3.Lerp (startPos, endPos, i);
			yield return null;
		}

		Destroy (SecondPlayer);
		CharacterAnimator.SetBool ("IsJoined", true);
	}

	IEnumerator DeactivatePowerUp (int time)
	{	
		TimerIndicator.sprite = GetComponent<SpriteRenderer> ().sprite;
		TimerIndicator.gameObject.SetActive (true);
		TimerIndicator.fillAmount = 1;

		for (int i = time; i > 0; i--) {
			yield return new WaitForSeconds (1f);
			TimerIndicator.fillAmount -= 0.1f;		
		}
		TimerIndicator.gameObject.SetActive (false);


		CharacterAnimator.SetBool ("IsJoined", false);
		gameManager.OnEnableSplit ();		//		split agian
	}
}
