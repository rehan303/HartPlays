using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomInstantiationOfBalls : MonoBehaviour 
{
	public GameObject[] BallsInstantiation_Array;
	private GameObject RandomGameObjectInstantiate_Runtime;
	private int RandomBallInstantiated = 0;
	private int RandomSideOfScreenToInstantiateBall = 0;

	public static float NextSpawnTimeOfBall;
	private float timer = 0.0f;
	private float nextTime = 2.0f;

	private bool randomBallInstantiated_Boolean;

	public static List<GameObject> instantiatedBalls_Runtime = new List<GameObject> ();

	[SerializeField]
	private Transform[] leftPositions;
	[SerializeField]
	private Transform[] rightPositions;
	[SerializeField]
	private Transform[] upPositions;
	[SerializeField]
	private Transform[] downPositions;


	// Use this for initialization
	void Start () 
	{
		RandomBallInstantiated = 0;
		randomBallInstantiated_Boolean = false;
		instantiatedBalls_Runtime.Clear ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if (!randomBallInstantiated_Boolean && timer > nextTime) 
		{
			randomBallInstantiated_Boolean = true;
			RandomBallInstantiated = Random.Range (0,PlayerPrefs.GetInt("NumberOfBallsInCurrentLevel"));
			RandomSideOfScreenToInstantiateBall = Random.Range (0, 11);
			StartCoroutine ("InstantiateBallOnTheScreen");
		}
	}

	void InstantiateBallOnTheScreen()
	{
		switch (RandomSideOfScreenToInstantiateBall) 
		{
		//Left Randome Instantation
		//Left Top
		case 0:
			//				Debug.Log ("Case 0 is Called");
			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], leftPositions[0].position, Quaternion.identity);
			instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			RandomGameObjectInstantiate_Runtime.layer = 20;
			break;
			//Left Mid
		case 1:
			//				Debug.Log ("Case 1 is Called");
			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], leftPositions[ Random.Range( 1, 3 ) ].position, Quaternion.identity);
			instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			RandomGameObjectInstantiate_Runtime.layer = 8;
			break;
			//Left Bottom
		case 2:
			//				Debug.Log ("Case 2 is Called");
			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], leftPositions[3].position, Quaternion.identity);
			instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			RandomGameObjectInstantiate_Runtime.layer = 21;
			break;


			//Right Randome Instantation
			// Right Top
		case 3:
			//				Debug.Log("Case 3 is Called");
			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], rightPositions[0].position, Quaternion.identity);
			instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			RandomGameObjectInstantiate_Runtime.layer = 22;
			break;
			// Right Mid
		case 4:
			//				Debug.Log("Case 4 is Called");
			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], rightPositions[ Random.Range( 1, 3 ) ].position, Quaternion.identity);
			instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			RandomGameObjectInstantiate_Runtime.layer = 9;
			break;
			// Right Bottom
		case 5:
			//				Debug.Log("Case 5 is Called");
			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], rightPositions[3].position, Quaternion.identity);
			instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			RandomGameObjectInstantiate_Runtime.layer = 23;
			break;
			// Upper Randome Instantation
			//Up Left
		case 6:
			//				Debug.Log("Case 6 is Called");
			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], upPositions[0].position, Quaternion.identity);
			instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			RandomGameObjectInstantiate_Runtime.layer = 10;
			break;
			//Up Mid
		case 7:
			//				Debug.Log("Case 7 is Called");
			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], upPositions[ Random.Range( 1, 2 ) ].position, Quaternion.identity);
			instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			RandomGameObjectInstantiate_Runtime.layer = 19;
			break;
			// Bottom Random Instantation
			// Bottom Right
		case 8:
			//				Debug.Log("Case 8 is Called");
			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], downPositions[0].position, Quaternion.identity);
			instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			RandomGameObjectInstantiate_Runtime.layer = 11;
			break;
			// Bottom Mid
		case 9:
			//				Debug.Log("Case 9 is Called");
			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], downPositions[ Random.Range( 1, 2 ) ].position, Quaternion.identity);
			instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			RandomGameObjectInstantiate_Runtime.layer = 17;
			break;
			// Bottom Left
		case 10:
			//				Debug.Log("Case 10 is Called");
			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], downPositions[2].position, Quaternion.identity);
			instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			RandomGameObjectInstantiate_Runtime.layer = 18;
			break;


			//				//Top Side
			//			case 4:
			////				Debug.Log("Case 4 is Called");
			//			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], TopSidePositions[ Random.Range( 0, TopSidePositions.Length ) ].position, Quaternion.identity);
			//				instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			//				RandomGameObjectInstantiate_Runtime.layer = 17;
			//				break;
			//			// Bottom Right Side
			//			case 5:
			////				Debug.Log("Case 5 is Called");
			//			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], BottomRightLeftPositions[0].position, Quaternion.identity);
			//				instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			//				RandomGameObjectInstantiate_Runtime.layer = 18;
			//				break;
			//			// Left Side
			//			case 6:
			////				Debug.Log("Case 6 is Called");
			//			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], LeftTopBottomPositions[0].position, Quaternion.identity);
			//				instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			//				RandomGameObjectInstantiate_Runtime.layer = 19;
			//				break;
			//			// Right Side
			//			case 7:
			////				Debug.Log("Case 7 is Called");
			//			RandomGameObjectInstantiate_Runtime = (GameObject)Instantiate (BallsInstantiation_Array [RandomBallInstantiated], RightTopBottomPositions[0].position, Quaternion.identity);
			//				instantiatedBalls_Runtime.Add (RandomGameObjectInstantiate_Runtime);
			//				RandomGameObjectInstantiate_Runtime.layer = 20;
			//				break;
		}

		timer = 0.0f;
		nextTime = NextSpawnTimeOfBall;
		randomBallInstantiated_Boolean = false;
	}
}
