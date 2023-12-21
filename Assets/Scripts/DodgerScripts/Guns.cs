using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guns : MonoBehaviour
{
	public static List<GameObject> instaintiatedBulletsOnScreen = new List<GameObject> ();

	public static float GunsRotationSpeed;
	public static float GunMovement_Speed = 2f;

	public static float NumberOfBulletsMovementAllowed;

	public static float NextSpawnTimeOfBullet;

	public Transform[] BulletsSpwanpoint;

	private Transform targetTransform;

	private float timer = 0.0f;
	private float nextTime = 1.0f;
	private bool BulletsInstantiated;

	int movementType;
	public GameObject bullet;
	Quaternion quaternion;
	int num = 0;

	public enum FaceSide
	{
		Right = 0,
		Left = 180,
	}

	public FaceSide Face;

	void OnEnable ()
	{
		BulletsInstantiated = false;
		targetTransform = GameObject.Find ("MainPlayer").transform;

		bullet = Face == FaceSide.Left ? GameObject.Find ("BulletRight") : GameObject.Find ("BulletLeft");										
	
	}

	void Update ()
	{
		RotateGunsTowardsPlayer (1);
		TranslateGuns ();

		timer += Time.deltaTime;
		if (num == 0) {
			if (!BulletsInstantiated && timer > 0) {
				BulletsInstantiated = true;
				FireBullets ();
				num = 1;
			}
		} else if (num == 1) {
			if (!BulletsInstantiated && timer > nextTime) {
				BulletsInstantiated = true;
				FireBullets ();
				num = 1;
			}
		}

	}

	public void TranslateGuns ()
	{
		transform.Translate (Vector3.up * -1 * GunMovement_Speed * Time.deltaTime, Space.World);

		if (transform.position.y < -5.4f) {
			Destroy (gameObject);
			RandomInstantiationOfGuns_Dodger.instantiatedGuns_Runtime.Remove (gameObject);
		} 
	}

	public void RotateGunsTowardsPlayer (float Speedoffset)
	{
		if (!HartPlayerRegistration.Isplayforpaid) {
			if (DogerGameManager.DistenceCovered > 80f) {
				Vector3 vectorToTarget = (targetTransform.position - transform.position).normalized;
				float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
				angle -= (float)Face;
				quaternion = Quaternion.AngleAxis (angle, Vector3.forward);
				transform.rotation = Quaternion.Slerp (transform.rotation, quaternion, Time.deltaTime * GunsRotationSpeed * Speedoffset);
			}
		}else if(HartPlayerRegistration.Isplayforpaid)
		{			
				Vector3 vectorToTarget = (targetTransform.position - transform.position).normalized;
				float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
				angle -= (float)Face;
				quaternion = Quaternion.AngleAxis (angle, Vector3.forward);
				transform.rotation = Quaternion.Slerp (transform.rotation, quaternion, Time.deltaTime * GunsRotationSpeed * Speedoffset);
		}
	}

	void FireBullets ()
	{	
		movementType = Random.Range (0, Mathf.FloorToInt (NumberOfBulletsMovementAllowed));
	
		Bullets bulletComponent = bullet.GetComponent<Bullets> ();
		ChangeSpeedofBullet (bulletComponent);

		bulletComponent.movementAlloted = movementType;
		GameObject temp = Instantiate (bullet, BulletsSpwanpoint [0].position, Quaternion.identity)as GameObject;
		if (DogerGameManager.DistenceCovered > 80f)
			temp.transform.localEulerAngles = transform.eulerAngles;
		else {
			if (temp.gameObject.name.Contains ("BulletRight"))
				temp.transform.localEulerAngles = new Vector3 (0f, 0f, 50f);
			else
				temp.transform.localEulerAngles = new Vector3 (0f, 0f, -50f);
		}
	
		instaintiatedBulletsOnScreen.Add (temp);
		timer = 0.0f;
		nextTime = NextSpawnTimeOfBullet;
		BulletsInstantiated = false;


	}

	void ChangeSpeedofBullet (Bullets bulletComponent)
	{
	
		if (this.transform.eulerAngles.z < 345 && this.transform.eulerAngles.z > 270 && Face == FaceSide.Right) { 
			bulletComponent.BulletsSpeedOffset = 1.5f;
		} else if (this.transform.eulerAngles.z > 15 && this.transform.eulerAngles.z < 90 && Face == FaceSide.Left) {
			bulletComponent.BulletsSpeedOffset = 1.5f;
		} else {
			bulletComponent.BulletsSpeedOffset = 1f;
		}
	}
}