using UnityEngine;
using System.Collections;

public class PotDirectionChange : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.GetComponent<Pots> ()) {
			if (coll.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.LeftCenter) {
				
			}		
		}	
	}
}
