using UnityEngine;
using System.Collections;

public class Movement_RandomInstantiatedBalls : MonoBehaviour
{
	// Use this for initialization
	public static float SpeedOfBallToMove;
	private Vector3 ballDirection;
	private bool isBallDirectionSet = false;

	public static bool isDigonalMovementOn;
	
	// Update is called once per frame
	void Update ()
	{
		if (!isBallDirectionSet) {
			BallPositions ();
//			print ("BallMovement"+ isDigonalMovementOn );
			isBallDirectionSet = true;
		}
			
		if (!PowerUp_Freeze.isFreezeOn)
//			this.gameObject.transform.Translate (ballDirection );
			this.gameObject.transform.Translate (ballDirection * Time.deltaTime * SpeedOfBallToMove, Space.World);
	}


	//	void BallPositions ()
	//	{
	//		//Left Random Digonal Move
	//		if (this.gameObject.layer == 20) {
	//			//Left Top Side
	//			Vector3[] Pos = new Vector3[2];
	//			Pos [0] = new Vector3 (Time.deltaTime * SpeedOfBallToMove, 0f, 0f);
	//			Pos [1] = new Vector3 (Time.deltaTime * SpeedOfBallToMove, Time.deltaTime * SpeedOfBallToMove * -1f, 0f);// bottom
	//			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
	//		} else if (this.gameObject.layer == 8) {
	//			//Left Mid
	//			Vector3[] Pos = new Vector3[3];
	//			Pos [0] = new Vector3 (Time.deltaTime * SpeedOfBallToMove, 0f, 0f);
	//			Pos [1] = new Vector3 (Time.deltaTime * SpeedOfBallToMove, Time.deltaTime * SpeedOfBallToMove, 0f);
	//			Pos [2] = new Vector3 (Time.deltaTime * SpeedOfBallToMove, Time.deltaTime * SpeedOfBallToMove * -1f, 0f);
	//			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
	//		} else if (this.gameObject.layer == 21) {
	//			//Left Bottom Side
	//			Vector3[] Pos = new Vector3[2];
	//			Pos [0] = new Vector3 (Time.deltaTime * SpeedOfBallToMove, 0f, 0f);
	//			Pos [1] = new Vector3 (Time.deltaTime * SpeedOfBallToMove, Time.deltaTime * SpeedOfBallToMove * 1f, 0f);// up
	//			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
	//		}
	//		//Right Random Digonal Move
	//		else if (this.gameObject.layer == 22) {
	//			//Right Top Side
	//			Vector3[] Pos = new Vector3[2];
	//			Pos [0] = new Vector3 (Time.deltaTime * SpeedOfBallToMove * -1, 0f, 0f);
	//			Pos [1] = new Vector3 (Time.deltaTime * SpeedOfBallToMove * -1, Time.deltaTime * SpeedOfBallToMove * -1f, 0f);//bottom
	//			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
	//		} else if (this.gameObject.layer == 9) {
	//			// Right Mid
	//			Vector3[] Pos = new Vector3[3];
	//			Pos [0] = new Vector3 (Time.deltaTime * SpeedOfBallToMove * -1, 0f, 0f);
	//			Pos [1] = new Vector3 (Time.deltaTime * SpeedOfBallToMove * -1, Time.deltaTime * SpeedOfBallToMove, 0f);
	//			Pos [2] = new Vector3 (Time.deltaTime * SpeedOfBallToMove * -1, Time.deltaTime * SpeedOfBallToMove * -1f, 0f);
	//			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
	//		} else if (this.gameObject.layer == 23) {
	//			//Right Bottom Side
	//			Vector3[] Pos = new Vector3[2];
	//			Pos [0] = new Vector3 (Time.deltaTime * SpeedOfBallToMove * -1, 0f, 0f);
	//			Pos [1] = new Vector3 (Time.deltaTime * SpeedOfBallToMove * -1, Time.deltaTime * SpeedOfBallToMove * 1f, 0f);//up
	//			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
	//		}
	//		// Upper Random Digonal Move
	//		else if (this.gameObject.layer == 10) {
	//			// Up Left
	//			Vector3[] Pos = new Vector3[3];
	//			Pos [0] = new Vector3 (0f, Time.deltaTime * SpeedOfBallToMove * -1f, 0f);
	//			Pos [1] = new Vector3 (Time.deltaTime * SpeedOfBallToMove, Time.deltaTime * SpeedOfBallToMove * -1, 0f);
	//			Pos [2] = new Vector3 (Time.deltaTime * SpeedOfBallToMove * -1f, Time.deltaTime * SpeedOfBallToMove * -1f, 0f);//Right bottom
	//			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
	//		} else if (this.gameObject.layer == 19) {
	//			//Up Mid
	//			Vector3[] Pos = new Vector3[2];
	//			Pos [0] = new Vector3 (0f, Time.deltaTime * SpeedOfBallToMove * -1f, 0f);
	//			Pos [1] = new Vector3 (Time.deltaTime * SpeedOfBallToMove, Time.deltaTime * SpeedOfBallToMove * -1, 0f);
	//			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
	//		}
	//		// Bottom Random Digonal Move
	//		else if (this.gameObject.layer == 11) {
	//			// Bottom Right
	//			Vector3[] Pos = new Vector3[2];
	//			Pos [0] = new Vector3 (0f, Time.deltaTime * SpeedOfBallToMove, 0f);
	//			Pos [1] = new Vector3 (Time.deltaTime * SpeedOfBallToMove * -1f, Time.deltaTime * SpeedOfBallToMove, 0f);
	//			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
	//		} else if (this.gameObject.layer == 17) {
	//			//Bottom Mid
	//			Vector3[] Pos = new Vector3[3];
	//			Pos [0] = new Vector3 (0f, Time.deltaTime * SpeedOfBallToMove, 0f);
	//			Pos [1] = new Vector3 (Time.deltaTime * SpeedOfBallToMove, Time.deltaTime * SpeedOfBallToMove, 0f);
	//			Pos [2] = new Vector3 (Time.deltaTime * SpeedOfBallToMove * -1f, Time.deltaTime * SpeedOfBallToMove, 0f);
	//			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
	//		} else if (this.gameObject.layer == 18) {
	//			//Bottom Left Side
	//			Vector3[] Pos = new Vector3[2];
	//			Pos [0] = new Vector3 (0f, Time.deltaTime * SpeedOfBallToMove, 0f);// Stragiht
	//			Pos [1] = new Vector3 (Time.deltaTime * SpeedOfBallToMove * 1f, Time.deltaTime * SpeedOfBallToMove, 0f);// left for right spwoner
	//			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
	//		}
	//	}

	//New
	void BallPositions ()
	{	
		//Left Random Digonal Move
		if (this.gameObject.layer == 20) {
			//Left Top Side
			Vector3[] Pos = new Vector3[2];
			Pos [0] = new Vector3 (1f, 0f, 0f);
			Pos [1] = new Vector3 (1f, -1f, 0f);// bottom
			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
		} else if (this.gameObject.layer == 8) {
			//Left Mid 
			Vector3[] Pos = new Vector3[3];
			Pos [0] = new Vector3 (1f, 0f, 0f);
			Pos [1] = new Vector3 (1f, 1f, 0f);
			Pos [2] = new Vector3 (1f, -1f, 0f);
			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
		} else if (this.gameObject.layer == 21) {
			//Left Bottom Side
			Vector3[] Pos = new Vector3[2];
			Pos [0] = new Vector3 (1f, 0f, 0f);
			Pos [1] = new Vector3 (1f, 1f, 0f);// up
			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
		}
		//Right Random Digonal Move
		else if (this.gameObject.layer == 22) {
			//Right Top Side
			Vector3[] Pos = new Vector3[2];
			Pos [0] = new Vector3 (-1f, 0f, 0f);
			Pos [1] = new Vector3 (-1f, -1f, 0f);//bottom
			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
		} else if (this.gameObject.layer == 9) {
			// Right Mid
			Vector3[] Pos = new Vector3[3];
			Pos [0] = new Vector3 (-1f, 0f, 0f);
			Pos [1] = new Vector3 (-1f, 1f, 0f);
			Pos [2] = new Vector3 (-1f, -1f, 0f);
			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
		} else if (this.gameObject.layer == 23) {
			//Right Bottom Side
			Vector3[] Pos = new Vector3[2];
			Pos [0] = new Vector3 (-1f, 0f, 0f);
			Pos [1] = new Vector3 (-1f, 1f, 0f);//up
			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
		}
		// Upper Random Digonal Move
		else if (this.gameObject.layer == 10) {
			// Up Left
			Vector3[] Pos = new Vector3[3];
			Pos [0] = new Vector3 (0f, -1f, 0f);
			Pos [1] = new Vector3 (1f, -1f, 0f);
			Pos [2] = new Vector3 (-1f, -1f, 0f);//Right bottom
			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
		} else if (this.gameObject.layer == 19) {
			//Up Mid
			Vector3[] Pos = new Vector3[2];
			Pos [0] = new Vector3 (0f, -1f, 0f);
			Pos [1] = new Vector3 (1, -1f, 0f);
			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
		}
		// Bottom Random Digonal Move
		else if (this.gameObject.layer == 11) {
			// Bottom Right
			Vector3[] Pos = new Vector3[2];
			Pos [0] = new Vector3 (0f, 1f, 0f);
			Pos [1] = new Vector3 (-1f, 1f, 0f);
			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
		} else if (this.gameObject.layer == 17) {
			//Bottom Mid
			Vector3[] Pos = new Vector3[3];
			Pos [0] = new Vector3 (0f, 1f, 0f);
			Pos [1] = new Vector3 (1f, 1f, 0f);
			Pos [2] = new Vector3 (-1f, 1f, 0f);
			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
		} else if (this.gameObject.layer == 18) {
			//Bottom Left Side
			Vector3[] Pos = new Vector3[2];
			Pos [0] = new Vector3 (0f, 1f, 0f);// Stragiht
			Pos [1] = new Vector3 (1f, 1f, 0f);// left for right spwoner
			ballDirection = (Pos [Random.Range (0, IsDigonalMovementOn (isDigonalMovementOn, Pos))]);
		} 
	}


	public  int  IsDigonalMovementOn (bool Status, Vector3[] Pos)
	{
		if (Status == false) {
			return 1;
		} else if (Status == true) {
			return  Pos.Length;
		}

		return 0;
	}
}
