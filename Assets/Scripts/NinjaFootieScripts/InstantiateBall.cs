using UnityEngine;
using System.Collections;
namespace NinjaFootie
{
	public class InstantiateBall : MonoBehaviour 
	{
		public GameObject ball;

		void Start () {
		
		}
		
		void Update () 
		{
			if(NinjaBall.ballInstance == null)
			{
	//			yield return new WaitForSeconds (0.5f);
				Instantiate (ball , Vector3.zero, Quaternion.identity);
			}
		
		}
	}
}