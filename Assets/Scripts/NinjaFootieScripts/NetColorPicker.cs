using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NinjaFootie
{
	public class NetColorPicker : Singleton<NetColorPicker>
	{
		public List<SpriteRenderer> OpponentNets = new List<SpriteRenderer> ();

		public List<SpriteRenderer> PlayerNets = new List<SpriteRenderer> ();

		List<Sprite> colorsList = new List<Sprite>();

		public static float NetColorChangeRate;

		public bool isChanging = true;

		void Start () 
		{				
			isChanging = true;
			
//			InvokeRepeating ("PickColor", 2f, NetColorChangeRate); not working in pause
			
			foreach(var net in PlayerNets)
			{
				colorsList.Add (net.sprite); 
			}
			StartCoroutine (PickColor ());
		}

		IEnumerator PickColor()
		{
			if (isChanging)
			{
				foreach (var net in PlayerNets) 
				{
					var sprite = colorsList [Random.Range (0, colorsList.Count)];

					if (net.sprite != sprite) 
					{
						net.sprite = sprite;
					} 
					else 
					{
						sprite = colorsList [Random.Range (0, colorsList.Count)];

						if (net.sprite != sprite) 
						{
							net.sprite = sprite;

						} 
						else 
						{
							sprite = colorsList [Random.Range (0, colorsList.Count)];
							net.sprite = sprite;
						}
					}
					colorsList.Remove (net.sprite);
				}

				for (int i = 0; i < OpponentNets.Count; i++) 
				{
					OpponentNets [i].sprite = PlayerNets [i].sprite;
				}

				foreach (var net in PlayerNets) 
				{
					colorsList.Add (net.sprite); 
				}

				yield return new WaitForSeconds (NetColorChangeRate);
			}
			else
			{
				yield return new WaitUntil (() => isChanging);
				yield return new WaitForSeconds (1f);
			}

			StartCoroutine (PickColor ());
		}
	}
}
