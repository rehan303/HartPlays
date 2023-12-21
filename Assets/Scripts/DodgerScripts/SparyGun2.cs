using UnityEngine;
using System.Collections;

public class SparyGun2 : Guns 
{		

	void Start()
	{
		InvokeRepeating ("SprayBullets", 0.5f, Random.Range (3,6));
	}

	void Update () 
	{
		RotateGunsTowardsPlayer (1f);
		TranslateGuns ();
	}

	void SprayBullets()
	{	
		StartCoroutine(SprayMultipleBulltes (bullet,Random.Range (3,7)));
	}

	IEnumerator SprayMultipleBulltes(GameObject Go, int I)
	{	
		
		Bullets BulletsComponentInstance = Go.GetComponent <Bullets> ();

		BulletsComponentInstance.side = this.Face == FaceSide.Left ? Bullets.Side.left : Bullets.Side.right;

		for (int i = 0 ; i < I; i++) 
		{			
			ChangeSpeedAccordingToAngleOfGun (BulletsComponentInstance);
			BulletsComponentInstance.movementAlloted = 4;
			GameObject temp =Instantiate (Go,BulletsSpwanpoint[0].position,Quaternion.identity)as GameObject;
			temp.transform.eulerAngles = transform.eulerAngles;
			temp.transform.eulerAngles += new Vector3 (0, 0, Random.Range (-15f, -10f)* (int) BulletsComponentInstance.side);
			instaintiatedBulletsOnScreen.Add (temp);
			yield return new WaitForSeconds (Random.Range (0.1f, 0.4f));
		}
	}

	void ChangeSpeedAccordingToAngleOfGun(Bullets bulletComponent)
	{
		if (this.transform.eulerAngles.z < 345 && this.transform.eulerAngles.z > 270 && Face == FaceSide.Right) 
		{ 
			bulletComponent.BulletsSpeedOffset = 1.5f;
		}
		else if (this.transform.eulerAngles.z > 15 && this.transform.eulerAngles.z < 90 && Face == FaceSide.Left) 
		{
			bulletComponent.BulletsSpeedOffset = 1.5f;
		}
		else 
		{
			bulletComponent.BulletsSpeedOffset = 1f;
		}
	}
}
