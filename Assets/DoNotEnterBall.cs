using UnityEngine;
using System.Collections;

public class DoNotEnterBall : MonoBehaviour
{
	AudioSource audioSource;
	public AudioClip ballPostClip;
	public AudioClip ballInWrongPostClip;

	//	When Color Balls Collided with Pots
	private GameObject BallCollidedWithWrongPot;

	private float SpeedOfBallWhenPutInWrongPot = 1f;

	void OnEnable ()
	{
		audioSource = GetComponent<AudioSource> ();
		if (this.gameObject.transform.parent.GetComponent<SpriteRenderer> ().enabled)
			this.gameObject.SetActive (true);
		else
			this.gameObject.SetActive (false);
		
	}


	// Use this for initialization
	void OnTriggerEnter2D (Collider2D coll)
	{
//		if (coll.gameObject.tag == "Red_Draggable" || coll.gameObject.tag == "Blue_Draggable" || coll.gameObject.tag == "Green_Draggable" || coll.gameObject.tag == "White_Draggable"
//		    || coll.gameObject.tag == "Yellow_Draggable" || coll.gameObject.tag == "Gray_Draggable" || coll.gameObject.tag == "Pink_Draggable" || coll.gameObject.tag == "Brown_Draggable"
//		    || coll.gameObject.tag == "Parpel_Draggable" || coll.gameObject.tag == "SkyBlue_Draggable") {
//			if (this.gameObject.transform.parent.GetComponent<SpriteRenderer> ().enabled) {
//				this.gameObject.tag = "Red_Draggable";
//				BallCollidedWithWrongPot = coll.gameObject;
//				ApplyForceWhenWrongBallCollidedWithPot ();
//			}
//		}
	}

	void ApplyForceWhenWrongBallCollidedWithPot ()
	{
		Handheld.Vibrate ();
		BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballInWrongPostClip);

		BallCollidedWithWrongPot.GetComponent<Movement_RandomInstantiatedBalls> ().enabled = false;
		//		BallCollidedWithWrongPot.GetComponent<BoxCollider2D> ().enabled = false;
		if (this.gameObject.transform.parent.gameObject.layer == 12) {

			if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.LeftCenter)
				//Left
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (2, 5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.RightCenter)
				//Right
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-2, -5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.BottomCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (2, 5)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.TopCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (-2, -5)) * SpeedOfBallWhenPutInWrongPot;
		} else if (this.gameObject.transform.parent.gameObject.layer == 13) {
			//			BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-2, -5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.LeftCenter)
				//Left
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (2, 5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.RightCenter)
				//Right
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-2, -5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.BottomCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (2, 5)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.TopCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (-2, -5)) * SpeedOfBallWhenPutInWrongPot;
		} else if (this.gameObject.transform.parent.gameObject.layer == 14) {
			//			BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (2, 5)) * SpeedOfBallWhenPutInWrongPot;
			if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.LeftCenter)
				//Left
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (2, 5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.RightCenter)
				//Right
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-2, -5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.BottomCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (2, 5)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.TopCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (-2, -5)) * SpeedOfBallWhenPutInWrongPot;
		} else if (this.gameObject.transform.parent.gameObject.layer == 15) {
			//			BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (-2, -5)) * SpeedOfBallWhenPutInWrongPot;
			if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.LeftCenter)
				//Left
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (2, 5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.RightCenter)
				//Right
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-2, -5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.BottomCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (2, 5)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.transform.parent.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.TopCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (-2, -5)) * SpeedOfBallWhenPutInWrongPot;
		}
		BallCollidedWithWrongPot = null;
	}
}
