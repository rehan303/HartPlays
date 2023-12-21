using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace NinjaFootie
{
	public class NinjaFootiePlayer : Singleton<NinjaFootiePlayer> 
	{

		public bool gotball = false;
		public bool gotPowerUp = false;
		GameObject BallTarget;
		public int wrongBallHits = 0;
		public TextMesh ScoreText;
		public static int PlayerScore = 0;
		AudioSource audioSource;
		public AudioClip BallPostSound;
		public AudioClip WrongBallPostSound;
		// Use this for initialization
		void Start () 
		{
			audioSource = GetComponent <AudioSource> ();
			wrongBallHits = 0;
			PlayerScore = 0;
		}
		
		// Update is called once per frame
		void Update () 
		{
			ScoreText.text = "PLAYER: " + PlayerScore.ToString ();
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			/// If Ai Hits the ball 
			if (other.gameObject.layer == 21 && !gotball && !gotPowerUp)
			{
				if (other.GetComponent <SpriteRenderer> ().sprite.name == "Red") 
				{
					gotball = true;
					BallTarget = other.gameObject;
					other.GetComponent <NinjaBall> ().isMoveAllowed = false;
					other.GetComponent <NinjaBall> ().isColorChanging = false;
					other.GetComponent <NinjaBall> ().IsCaughtByAi = false;

					other.transform.parent = this.transform;
					other.transform.position = this.transform.position;
					other.transform.localPosition = Vector3.zero;
				}
				else 
				{
					wrongBallHits++;
					if(wrongBallHits>=3)
					{
						wrongBallHits = 0;
						AI_NinjaFootie.AiScores += 3;
						print ("Award a score to Ai Opponent");
					}
					Handheld.Vibrate ();
	//				print ("OtherThan Red ball picked----< Scores deducted> "+ wrongBallHits);
				}		
			}

			// If Player Hits the Post with ball in hand
			if (other.gameObject.layer == 23)
			{
				// If color is same
				if (BallTarget && gotball && !gotPowerUp ) 
				{
					if (other.GetComponent <SpriteRenderer> ().sprite.name == BallTarget.GetComponent<SpriteRenderer> ().sprite.name) 
					{
						Destroy (BallTarget);	
						gotball = false;
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, BallPostSound);
						PlayerScore += 3;
					}
					else if (BallTarget)
					{
						StartCoroutine (MoveBallOutofView ());
					}
				}
			}
			if (other.gameObject.layer == 22 && BallTarget)
			{
				StartCoroutine (MoveBallOutofView ());
			}
		}

		void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.layer == 21)
			{
				if (gotball && !gotPowerUp) 
				{
					gotball = false;
				}
			}
		}
		IEnumerator MoveBallOutofView()
		{
			BackgroundMusicManger.instance.PlaySoundEffect (audioSource, WrongBallPostSound);
			Handheld.Vibrate ();
			BallTarget.transform.parent = null;
			BallTarget.GetComponent <NinjaBall> ().enabled = false;
			BallTarget.GetComponent <BoxCollider2D> ().enabled = false;

			BallTarget.GetComponent<Rigidbody2D> ().velocity = new Vector2(Random.Range (-3f,3f),Random.Range (2,5)) * 5f;
			AI_NinjaFootie.isAi_Active = false;
			gotball = false;
			yield return new WaitForSeconds (1.5f);
			Destroy (BallTarget);		

			AI_NinjaFootie.isAi_Active = true;
		}
	}
}