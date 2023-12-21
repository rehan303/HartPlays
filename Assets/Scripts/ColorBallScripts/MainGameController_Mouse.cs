using UnityEngine;
using System.Collections;

public class MainGameController_Mouse : MonoBehaviour
{

	private float Z_Dist;
	public static bool dragging = false;
	private Vector3 offset;
	private Vector3 GameObject_WorldPosition;
	//	private Vector3 TouchPosition_GameObject;
	public static GameObject GameObject_toDrag;

	public static bool bombCollidedWithPot = false;
	public static bool ballCollidedWithPots = false;

	public Transform ballRestrictedPositionX;
	public Transform ballRestrictedPositionY;

	 
	void Update ()
	{
//		TouchPosition_GameObject = Input.mousePosition;
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
				if (hit.collider.gameObject.layer == 8 || hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 10 || hit.collider.gameObject.layer == 11 || hit.collider.gameObject.layer == 20 || hit.collider.gameObject.layer == 21 || hit.collider.gameObject.layer == 22 || hit.collider.gameObject.layer == 23 || hit.collider.gameObject.layer == 17 || hit.collider.gameObject.layer == 18 || hit.collider.gameObject.layer == 19) {
					GameObject_toDrag = hit.collider.gameObject;
					GameObject_toDrag.GetComponent<Movement_RandomInstantiatedBalls> ().enabled = false;
					GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (0.65f, 0.65f);
					Z_Dist = hit.transform.position.z - Camera.main.transform.position.z;
					GameObject_WorldPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Z_Dist);
					GameObject_WorldPosition = Camera.main.ScreenToWorldPoint (GameObject_WorldPosition);
					offset = GameObject_toDrag.transform.position - GameObject_WorldPosition;
					dragging = true;
				} else if (hit.collider.gameObject.name == "PauseButton_GameObject") {
//					Debug.Log ("Pause button Hitted");
				} else if (hit.collider.gameObject.layer == 16) {
					GameObject_toDrag = hit.collider.gameObject;
					dragging = true;
				}
			} 
		}

		if (dragging && !ballCollidedWithPots) {
			GameObject_WorldPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Z_Dist);
			GameObject_WorldPosition = Camera.main.ScreenToWorldPoint (GameObject_WorldPosition);
			if (GameObject_toDrag) {
				GameObject_toDrag.transform.position = GameObject_WorldPosition + offset;
				Vector3 restricted_Position = GameObject_toDrag.transform.position;
//				restricted_Position.x = Mathf.Clamp (restricted_Position.x, -ballRestrictedPositionX.position.x, ballRestrictedPositionX.position.x);
//				restricted_Position.y = Mathf.Clamp (restricted_Position.y, -ballRestrictedPositionY.position.y, ballRestrictedPositionY.position.y);
				//Changed By Rehan
				restricted_Position.x = Mathf.Clamp (restricted_Position.x, -2.5125f, 2.5125f);
				restricted_Position.y = Mathf.Clamp (restricted_Position.y, -4.7f, 4.7f);
				restricted_Position.z = Mathf.Clamp (restricted_Position.z, 0, 0);
				GameObject_toDrag.transform.position = restricted_Position;
			} else {
				if (dragging) {
					dragging = false;
					if (GameObject_toDrag && GameObject_toDrag.layer != 16) {
						ballCollidedWithPots = false;
						GameObject_toDrag.GetComponent<Movement_RandomInstantiatedBalls> ().enabled = true;
						GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (0.9f, 0.9f);
						GameObject_toDrag = null;
					} else if (GameObject_toDrag && GameObject_toDrag.layer == 16) {
						GameObject_toDrag = null;
					}
				}
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			if (dragging) {
				dragging = false;
				if (GameObject_toDrag && GameObject_toDrag.layer != 16) {
					ballCollidedWithPots = false;
					GameObject_toDrag.GetComponent<Movement_RandomInstantiatedBalls> ().enabled = true;
					GameObject_toDrag.GetComponent<BoxCollider2D> ().size = new Vector2 (0.9f, 0.9f);
					GameObject_toDrag = null;
				} else if (GameObject_toDrag && GameObject_toDrag.layer == 16) {
					GameObject_toDrag = null;
				}
			} else {
				return;
			}
		}
	}
}
