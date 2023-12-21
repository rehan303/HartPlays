using UnityEngine;
using System.Collections;

public class ParallaxScrolling : MonoBehaviour 
{

	private Transform thisGameObject_Transform;

	void Start () 
	{
		thisGameObject_Transform = this.gameObject.transform;
	}

	void Update () 
	{
		transform.Translate (Vector3.up * -1 * Guns.GunMovement_Speed * Time.deltaTime, Space.World);

		if (thisGameObject_Transform.localPosition.y <= -13f) 
		{
			Vector3 newPositionOfGameObject = thisGameObject_Transform.localPosition;
			newPositionOfGameObject.y = 26f;
			thisGameObject_Transform.localPosition = newPositionOfGameObject;
		}
	}
}
