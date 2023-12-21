using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace NinjaFootie
{

	public class AI_NinjaFootie : MonoBehaviour 
	{	
		public List<SpriteRenderer> MyPosts = new List<SpriteRenderer>();

		public static float SpeedofMovement;
		public static float AiReactionTime;
		public static bool isAi_Active = true;
		public static int AiScores= 0;

		public TextMesh AIScores;

		GameObject BallTarget;
		Sprite BallColor;
		public bool finding = false;
		public bool gotball = false;

		Vector3 StartPos = new Vector3(0f,2.0f,0f);
		Transform GotoTransform;

		public enum AiState
		{Find,Chase,Get,Return}

		public AiState state;
		public bool AIStartFollowing;

		AudioSource audioSource;
		public AudioClip BallPostSound;
		public AudioClip WrongBallPostSound;

		void Start()
		{
			audioSource = GetComponent<AudioSource> ();
			isAi_Active = true;
			AiScores= 0;	
			finding = false;
			gotball = false;
			AIStartFollowing = false;
			state = AiState.Find;
		}

		void Update ()
		{
			AIScores.text ="OPPONENT: "+ AiScores.ToString ();

			if(state == AiState.Find && BallTarget == null && isAi_Active)
			{
				if(!finding)
				{
					GotoTransform = null;
					StartCoroutine (FindGameObjectAfterWait ("NinjaBall"));
				}
			}
			else if (state == AiState.Chase && finding && isAi_Active && BallTarget)
			{
				if (!AIStartFollowing) 
				{
					AIStartFollowing = true;
					FindStateOfBallByAIOpponent ();
				} 
				else 
				{
					Vector3 ChasePos = BallTarget.transform.position;
					RotateTowardsPosition (ChasePos);
					if (Vector3.Distance (transform.position, ChasePos) > 1.5f) 
					{	
						this.transform.position = Vector3.Lerp (this.transform.position, ChasePos, SpeedofMovement * 0.4f * Time.deltaTime);
					} 
					else 
					{
						AIStartFollowing = false;
					}
				}
			}
			else if(state == AiState.Get && !gotball && BallTarget && isAi_Active)
			{	

				MoveToPosition (BallTarget.transform.position);
				RotateTowardsPosition (BallTarget.transform.position);
				if (gotball)
					state = AiState.Return;

				if (BallTarget.GetComponent <SpriteRenderer> ().sprite.name != "Red") 
				{				
					finding = false;
					gotball = false;
					isAi_Active = true;
//					print ("Problem is here");
					state = AiState.Find;
					BallTarget = null;
				}
			}

			else if (state == AiState.Return && gotball && BallTarget && isAi_Active)
			{
				GotoTransform = FindCorrectPost ();
				if (GotoTransform)
				{
					MoveToPosition (GotoTransform.position);
					RotateTowardsPosition (GotoTransform.position);
				}
				else
				{
					MoveToPosition (StartPos);
					RotateTowardsPosition (StartPos);				
				}
				 if(BallTarget.GetComponent<SpriteRenderer> ().sprite.name != "Red")
				{
					gotball = false;
					finding = false;
					BallTarget = null;
					BallColor = null;
					state = AiState.Find;
				}	
			
			}
			if(BallTarget == null)
			{
				state = AiState.Find;
			}
		}

		void FindStateOfBallByAIOpponent()
		{
			Vector3 ChasePos = BallTarget.transform.position;
			RotateTowardsPosition (ChasePos);
		}
		void OnTriggerEnter2D(Collider2D other)
		{
			/// If Ai Hits the ball 
			if (other.gameObject.layer == 21 && !gotball && state == AiState.Get && isAi_Active)// here ball target is to ensure that ball is red if(!red){BallTarget == null};
			{
				if (BallTarget.GetComponent <SpriteRenderer> ().sprite.name == "Red") 
				{
					gotball = true;
					other.GetComponent <NinjaBall> ().isMoveAllowed = false;
					other.GetComponent <NinjaBall> ().isColorChanging = false;
					other.GetComponent <NinjaBall> ().Player = this.gameObject;
					other.GetComponent <NinjaBall> ().IsCaughtByAi = true;

					other.transform.parent = this.transform;
					other.transform.position = this.transform.position;
					state = AiState.Return;
					other.transform.localPosition = Vector3.zero;
				}
			}
			// If Ai Hits the Post with ball in hand
			if (other.gameObject.layer == 22 && gotball && isAi_Active)
			{
				// If color is same
				if(other.GetComponent <SpriteRenderer>().sprite.name == BallColor.name) 
				{			
					gotball = false;
					finding = false;
					Destroy (BallTarget);
					BallColor = null;
					state = AiState.Find;
					//TODO Update Ai scores...
					AiScores += 3;	
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, BallPostSound);
				}
				else
				{
					print ("Move ball out of screen");//TODO Move ball out of screen
				}
			}
		}
		void OnTriggerStay2D(Collider2D other)
		{
			if (other.gameObject.layer == 21 && !gotball && state == AiState.Get && isAi_Active)
			{
				if (BallTarget.GetComponent <SpriteRenderer> ().sprite.name == "Red") 
				{
					gotball = true;
					other.GetComponent <NinjaBall> ().isMoveAllowed = false;
					other.GetComponent <NinjaBall> ().isColorChanging = false;
					other.GetComponent <NinjaBall> ().Player = this.gameObject;
					other.GetComponent <NinjaBall> ().IsCaughtByAi = true;
					print ("OnTriggerStay.....>>>>>>>>>>>>> ");

					other.transform.parent = this.transform;
					other.transform.position = this.transform.position;
					state = AiState.Return;
					other.transform.localPosition = Vector3.zero;
				}
			}
		}

		void OnTriggerExit2D(Collider2D other)
		{
			/// If Ai Hits the ball 
			if (other.gameObject.layer == 21 && gotball && isAi_Active)
			{
				gotball = false;
				finding = false;
				BallTarget = null;
				BallColor = null;
				state = AiState.Find;
			}
		}

		IEnumerator FindGameObjectAfterWait(string name)
		{
			finding = true;
			yield return new WaitForSeconds (AiReactionTime*0.2f);
			GameObject Go = GameObject.FindGameObjectWithTag (name);

			if(Go != null)
			{
				BallTarget = Go;
				state = AiState.Chase;
				while(Go.GetComponent <SpriteRenderer> ().sprite.name !="Red")
				{ 
					if (Go.GetComponent <SpriteRenderer> ().sprite.name != "Red") 
					{
	//					print ("Waiting");
						yield return new WaitForSeconds (AiReactionTime*0.2f);
					}
					else
						break;
				}

				yield return new WaitForSeconds (AiReactionTime*0.2f);
				if (Go) {
					BallColor = Go.GetComponent <SpriteRenderer> ().sprite;
					state = AiState.Get;
					AIStartFollowing = false;
					finding = false;
					yield return null;
				} 
				else
				{
					AIStartFollowing = false;
					finding = false;
					BallTarget = null;
					state = AiState.Find;
//					print ("Problem is not here");

				}
				//			yield return new WaitForSeconds (AiTimeToFindBall*0.2f);
			}
		}

		void ChaseBall(Vector3 target, float time)
		{
			this.transform.position = Vector3.Lerp (this.transform.position, target, time * Time.deltaTime);
		}

		void MoveToPosition(Vector3 endPos)
		{		
			transform.position = Vector3.MoveTowards( transform.position, endPos, Time.deltaTime* SpeedofMovement);	//TODO swere aa ke 
		}

		Transform FindCorrectPost()
		{

			foreach (var post in MyPosts) 
			{
				if(post.sprite.name == "Red")
				{
					return post.transform;
				}
			}

			return null;
		}

		public void RotateTowardsPosition(Vector3 Pos)
		{		
			Vector3 vectorToTarget = (Pos - transform.position).normalized;
			float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			angle+=90f;
			Quaternion quaternion = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, quaternion, Time.deltaTime*10f);
		}
	}
}