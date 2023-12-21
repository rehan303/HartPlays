using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomInstantiationOfGuns_Dodger : MonoBehaviour 
{

	public GameObject[] GunsInstantiation_Array;
	public Transform leftPosition;
	public Transform rightPosition;

	private GameObject RandomGameObjectInstantiate_Runtime;
	private int RandomGunsInstantiated = 0;

	public static float NextSpawnTimeOfGun = 2f;
	public static int maxGunsAllowed;
	private float timer = 0.0f;
	private float nextTime = 1.0f;

	private bool randomGunsInstantiated_Boolean;

	public static List<GameObject> instantiatedGuns_Runtime = new List<GameObject> ();

	void Start () 
	{
		
		RandomGunsInstantiated = 0;
		randomGunsInstantiated_Boolean = false;
		instantiatedGuns_Runtime.Clear ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if (!randomGunsInstantiated_Boolean && timer > nextTime) 
		{
			randomGunsInstantiated_Boolean = true;
			if(Random.Range (0,2)<0.5f)
				RandomGunsInstantiated = Random.Range (0,2);
			else
			RandomGunsInstantiated = Random.Range (0,maxGunsAllowed);
			StartCoroutine ("InstantiateGunsOnTheScreen");
		}
	}

	void InstantiateGunsOnTheScreen()
	{
		switch (RandomGunsInstantiated) 
		{
			case 0:
					RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (GunsInstantiation_Array [RandomGunsInstantiated], leftPosition.position, Quaternion.identity);
					instantiatedGuns_Runtime.Add (RandomGameObjectInstantiate_Runtime);
					break;
			case 1:
					RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (GunsInstantiation_Array [RandomGunsInstantiated], rightPosition.position, Quaternion.identity);
					instantiatedGuns_Runtime.Add (RandomGameObjectInstantiate_Runtime);
					break;
			case 2:
					RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (GunsInstantiation_Array [RandomGunsInstantiated], leftPosition.position, Quaternion.identity);
					instantiatedGuns_Runtime.Add (RandomGameObjectInstantiate_Runtime);
					break;
			case 3:
					RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (GunsInstantiation_Array [RandomGunsInstantiated], rightPosition.position, Quaternion.identity);
					instantiatedGuns_Runtime.Add (RandomGameObjectInstantiate_Runtime);
					break;
		}
		timer = 0.0f;
		nextTime = NextSpawnTimeOfGun;
		randomGunsInstantiated_Boolean = false;
	}
}
