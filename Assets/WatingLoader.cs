using UnityEngine;
using System.Collections;

public class WatingLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.GetChild (0).transform.Rotate (0f, 0f, -80* Time.deltaTime);
	}
}
