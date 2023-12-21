using UnityEngine;
using System.Collections;

public class DrawGizmos : MonoBehaviour {
	public Transform resX;
	public Transform resY;
	public GameObject button;
	void OnDrawGizmos()
	{
		Vector3 verticalUpHalf = new Vector3 (0, resY.position.y+1, 0);
		Vector3 verticalDownHalf = new Vector3 (0, -resY.position.y-1, 0);
		Gizmos.color = Color.red;
		Gizmos.DrawLine (verticalUpHalf,verticalDownHalf);

		Vector3 HorUpHalf = new Vector3 (resX.position.x+1, 0f, 0f);
		Vector3 HorDownHalf = new Vector3 (-resX.position.x-1,0f , 0f);

		Gizmos.color = Color.green;
		Gizmos.DrawLine (HorUpHalf,HorDownHalf);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
