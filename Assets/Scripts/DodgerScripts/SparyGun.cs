using UnityEngine;
using System.Collections;

public class SparyGun : Guns 
{
		

	void Start()
	{
		InvokeRepeating ("SprayBullets", 0.5f, Random.Range(2,5));
	}

	void Update () 
	{
		RotateGunsTowardsPlayer (0.5f);
		TranslateGuns ();
	}

	void SprayBullets()
	{	
		StartCoroutine(SprayMultipleBulltes (bullet,3));
	}

	IEnumerator SprayMultipleBulltes(GameObject Go, int I)
	{	
		GameObject Parent = new GameObject ("BullteSpray");
		Parent.transform.position = transform.position;
		Parent.transform.eulerAngles = transform.eulerAngles;
		
		Bullets BulletsComponentInstance = Parent.AddComponent <Bullets> ();

		BulletsComponentInstance.side = this.Face == FaceSide.Left ? Bullets.Side.left : Bullets.Side.right;

//		ChangeSpeedofBullet (BulletsComponentInstance);
		for (int i = 0 ; i < I; i++) 
		{	
			GameObject Sub_Parent = new GameObject ("Sub Parent " + i);
			Sub_Parent.transform.SetParent (Parent.transform);
			Sub_Parent.transform.localPosition = Vector3.zero;
			Sub_Parent.transform.localRotation = transform.localRotation;
		
			for(int j = 0; j<I- i; j++)
			{			
				BulletsComponentInstance.movementAlloted = 4;

				GameObject temp =Instantiate (Go.transform.GetChild (0).gameObject,BulletsSpwanpoint[j].position,Quaternion.identity)as GameObject;

				temp.transform.SetParent (Sub_Parent.transform);
//				temp.transform.localPosition = new Vector3(j * 0.5f,i * 0.25f,0);
				instaintiatedBulletsOnScreen.Add (temp);
			}

			Sub_Parent.transform.localPosition =new Vector2 ((3 - i)*0.25f* (int) BulletsComponentInstance.side,i *-0.15f);
//			Sub_Parent.transform.localPosition = Vector3.zero;
//			Sub_Parent.transform.localEulerAngles = transform.localEulerAngles;
			yield return null;
//			yield return new WaitForSeconds (waitTime);
		}
	}
}
