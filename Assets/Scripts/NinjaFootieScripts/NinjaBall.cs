using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NinjaFootie
{
	public class NinjaBall : MonoBehaviour 
	{
		public List<Sprite> Colors = new List<Sprite>();
		public GameObject Player;
		public bool IsCaughtByAi = false;
		public static float BallcolorChangingRate;
		public static float BallpositionChangingRate;

		public float MaxX= 6f;
		public float MaxY= 3f;

		public bool isMoveAllowed = true;
		public bool isColorChanging = true;

		SpriteRenderer spriteRenderer;
		public List<Sprite> Tempcolors = new List<Sprite>();

		public static NinjaBall ballInstance;

		void Awake()
		{
			if (ballInstance == null)
				ballInstance = this;
			else if (ballInstance != this)
				Destroy(ballInstance);
			
		}

		void Start () 
		{
			spriteRenderer = GetComponent <SpriteRenderer> ();

			for(int i = 0; i < Colors.Count; i++)
			{
				Tempcolors.Add (Colors[i]);
			}

			StartCoroutine (MoveTargetToPosition (PickRandomPosition (), 1f));
			StartCoroutine (PickRandomSprite ());
		}

		void Update()
		{
			if(!isMoveAllowed)
			{
				if (IsCaughtByAi) 
				{
					transform.localPosition = new Vector3 (0f, -0.8f, 0f);
				}// apply offset when Ai/Player is dragging ball to post 
				else
				{
					transform.localPosition = new Vector3 (0f, 0.8f, 0f);
				}
			}
		}

		IEnumerator MoveTargetToPosition(Vector3 target, float time)
		{
			if (isMoveAllowed) {
				float i = 0.0f;
				float rate = 1.0f / time;
				while (i < 1.0f) 
				{
					i += Time.deltaTime * rate;
					this.transform.position = Vector3.Lerp (transform.position, target, time * Time.deltaTime);
					yield return null;
				}
				yield return new WaitForSeconds (BallpositionChangingRate*0.1f);

				StartCoroutine (MoveTargetToPosition (PickRandomPosition (), 1f));
			}
			else
			{
				yield return new WaitUntil (() => isMoveAllowed);			
			}
		}

		Vector2 PickRandomPosition()
		{
			
			Vector2 newpos = new Vector2 (Random.Range (-MaxX, MaxX), Random.Range (-MaxY, MaxY));
			while (Vector2.Distance (transform.position, newpos) < 3) 
			{
				if (Vector2.Distance (transform.position, newpos) < 3) 
				{
					newpos = new Vector2 (Random.Range (-MaxX, MaxX), Random.Range (-MaxY, MaxY));
				} 
				else 
				{
					break;
				}
			}
				return newpos;
		}

		IEnumerator PickRandomSprite()
		{
			if (isColorChanging) 
			{
				if (spriteRenderer.sprite == null) 
				{
					Sprite selected = Tempcolors [Random.Range (0, Tempcolors.Count)];
					spriteRenderer.sprite = selected;
					Tempcolors.Remove (selected);
				}
				else
				{
					Sprite selected = Tempcolors [Random.Range (0, Tempcolors.Count)];
					Tempcolors.Add (spriteRenderer.sprite);
					spriteRenderer.sprite = selected;
					Tempcolors.Remove (selected);
				}
				yield return new WaitForSeconds (BallcolorChangingRate);
			}

			else
			{
				yield return new WaitUntil (() => isColorChanging);
			}
				
			StartCoroutine (PickRandomSprite ());
		}
		void OnTriggerEnter2D(Collider2D other)
		{
	//		if (other.gameObject.CompareTag ("Player")) 
	//		{
	//			Player=	other.gameObject;
	//		}
		}
	}
}