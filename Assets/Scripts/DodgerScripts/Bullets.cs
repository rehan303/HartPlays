using UnityEngine;
using System.Collections;

//using System;

public class Bullets : MonoBehaviour
{
	public static float BulletMovement_Speed;
	public float BulletsSpeedOffset;
	public int movementAlloted = 0;
	float frequency = 8.0f;
	float magnitude = 0.5f;
	//	public Sprite[] BulletsSprites;
	Camera cam;
	float tempSpeed;

	void OnEnable ()
	{
		typeOfMovementEnum = (TypeOfMovement)movementAlloted;

//		transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = BulletsSprites[(int)typeOfMovementEnum];
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
	
	}

	public enum Side
	{
		left = -1,
		right = 1
	}

	public Side side;

	enum TypeOfMovement
	{
		Straight = 0,
		ZigZag = 1,
		Curve = 2,
		Straight_2 = 3,
	}

	TypeOfMovement typeOfMovementEnum;

	void Update ()
	{
		switch (typeOfMovementEnum) {
		case TypeOfMovement.ZigZag:			
			transform.Translate (Vector2.right * Time.deltaTime * BulletMovement_Speed * (int)side * BulletsSpeedOffset, Space.Self);
			if (transform.GetChild (0) != null)
				transform.GetChild (0).localPosition = new Vector2 (0, Mathf.Sin (Time.time * frequency) * magnitude);
			break;

		case TypeOfMovement.Curve:
			transform.Translate (Vector2.right * BulletMovement_Speed * Time.deltaTime * (int)side * BulletsSpeedOffset, Space.Self);
			transform.Rotate (0, 0, frequency * BulletMovement_Speed * 2 * Time.deltaTime * (int)side);
			break;

		default:			
			transform.Translate (Vector2.right * BulletMovement_Speed * Time.deltaTime * (int)side * BulletsSpeedOffset, Space.Self);		
			break;
		}
		if (Doger_SlowPowerUp.ReversePowerUp)
			transform.localEulerAngles = new Vector3 (0f, 0f, 0f);	

	}
}
