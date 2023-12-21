using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Reminder_Bomb : MonoBehaviour
{
	[SerializeField]
	private List<Color32> materialSprite = new List<Color32> ();

	//	public List<Sprite> Textures_Animations;
	public List<GameObject> activePots = new List<GameObject> ();
	public List<GameObject> activePots_Textures = new List<GameObject> ();
	public Dictionary<string, Color32> ReminderBomb = new Dictionary<string, Color32> ();

	private GameManager gameManagerComponent;
	AudioSource m_AudioSource;

	void OnEnable ()
	{
		gameManagerComponent = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		PopulatingTheColors ();
		//ActivePotsTextureColor ();
		m_AudioSource = GetComponent <AudioSource> ();

	}

	void PopulatingTheColors ()
	{
		ReminderBomb.Clear ();
		ReminderBomb.Add ("BlueBalls", materialSprite [0]);
		ReminderBomb.Add ("RedBalls", materialSprite [1]);
		ReminderBomb.Add ("GreenBalls", materialSprite [2]);
		ReminderBomb.Add ("YellowBalls", materialSprite [3]);
		ReminderBomb.Add ("WhiteBalls", materialSprite [4]);
		ReminderBomb.Add ("GrayBalls", materialSprite [5]);
		ReminderBomb.Add ("BrownBalls", materialSprite [6]);
		ReminderBomb.Add ("PinkBalls", materialSprite [7]);
		ReminderBomb.Add ("SkyBlueBalls", materialSprite [8]);
		ReminderBomb.Add ("ParpelBalls", materialSprite [9]);

	}

	void ActivePots ()
	{
		int count = PlayerPrefs.GetInt ("NumberOfBallsInCurrentLevel");

		for (int i = 0; i <= count - 1; i++) {
			activePots.Add (gameManagerComponent.potList [i]);
			activePots_Textures.Add (gameManagerComponent.potList_Textures [i]);
		}
	}

	void ActivePotsTextureColor ()
	{
		ActivePots ();
		string[] tempNameObj;

		for (int i = 0; i < activePots.Count; i++) {

			if (activePots [i].tag != "Untagged") {
				tempNameObj = activePots [i].tag.Split ('_');
				AddSpriteToObject (tempNameObj [0], i);
			}
		}
	}

	void  AddSpriteToObject (string spriteName, int count)
	{
		activePots_Textures [count].GetComponent<Image> ().color = GetColorFromDictionary (spriteName);

	}

	Color32 GetColorFromDictionary (string colorname)
	{
		return ReminderBomb [colorname];
	}

	IEnumerator Timer (List<GameObject> tempSprite)
	{
		yield return new WaitForSeconds (5.0f);
		PowerUpContoller.isPowerUpAlive = false;
		PowerUpContoller.isShopedPowerUpAlive = false;
		foreach (GameObject obj in activePots_Textures) {
			obj.GetComponent<Image> ().color = Color.black;
		}

		Renderstatus (false);

		this.gameObject.transform.position = new Vector3 (0, 0, 0);

	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.layer == 12 || coll.gameObject.layer == 13 || coll.gameObject.layer == 14 || coll.gameObject.layer == 15) {
			if (this.gameObject.layer == 16) {
				PowerUpContoller.isShopedPowerUpAlive = false;
				ActivePotsTextureColor ();
				StartCoroutine ("Timer", activePots);
				Renderstatus (false);
				PowerUpContoller.PowerStayTime = 0.0f;
				this.gameObject.transform.position = new Vector3 (0, 0, 0);
				BackgroundMusicManger.instance.PlaySound (m_AudioSource);
			}
		}
	}

	public void ActiveReminderPowerUp ()
	{
		PowerUpContoller.isShopedPowerUpAlive = false;
		ActivePotsTextureColor ();
		StartCoroutine ("Timer", activePots);
		Renderstatus (false);
		PowerUpContoller.PowerStayTime = 0.0f;
		this.gameObject.transform.position = new Vector3 (0, 0, 0);
		BackgroundMusicManger.instance.PlaySound (m_AudioSource);
	}

	public void Renderstatus (bool status)
	{
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = status; 
		this.gameObject.GetComponent<BoxCollider2D> ().enabled = status;
	}

	//	 IEnumerator PlayAnimationOfReminder()
	//	{
	//		Debug.Log ("Play Animation");
	//		for (int i = 0; i < Textures_Animations.Count; i++)
	//		{
	//			GetComponent<SpriteRenderer> ().sprite = Textures_Animations [i];
	//			yield return new WaitForSeconds (0.1f);
	//		}
	//		yield return new WaitForSeconds (0.6f);
	//
	//		GetComponent<SpriteRenderer> ().enabled = false;
	////		transform.position = new Vector3 (0,0,0);
	//	}
}
