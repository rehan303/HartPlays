using UnityEngine;
using System.Collections;

public class Collision_Pots : MonoBehaviour
{
	AudioSource audioSource;
	public AudioClip ballPostClip;
	public AudioClip ballInWrongPostClip;
	
	//	When Color Balls Collided with Pots
	private GameObject BallCollidedWithWrongPot;

	private float SpeedOfBallWhenPutInWrongPot = 5f;

	private static bool PotAssignedRedColor;
	private static bool PotAssignedBlueColor;
	private static bool PotAssignedGreenColor;
	private static bool PotAssignedWhiteColor;
	private static bool PotAssignedYellowColor;
	private static bool PotAssignedGrayColor;
	private static bool PotAssignedPinkColor;
	private static bool PotAssignedSkyBlueColor;
	private static bool PotAssignedPurpelColor;
	private static bool PotAssignedBrownColor;

	public Pots potsComponent;

	void OnEnable ()
	{
		PotAssignedRedColor = false;
		PotAssignedBlueColor = false;
		PotAssignedGreenColor = false;
		PotAssignedWhiteColor = false;
		PotAssignedYellowColor = false;
		PotAssignedGrayColor = false;
		PotAssignedPinkColor = false;
		PotAssignedSkyBlueColor = false;
		PotAssignedPurpelColor = false;
		PotAssignedBrownColor = false;

		potsComponent = this.gameObject.GetComponent<Pots> ();

		audioSource = GetComponent<AudioSource> ();
	}


	#if UNITY_EDITOR

	void OnTriggerEnter2D (Collider2D coll)
	{

		if (potsComponent.PotFilled (10)) {
			// RED BALL COLLISION WITH POTS
			if (coll.gameObject.tag == "Red_Draggable" && !PotAssignedRedColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "Untagged") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
//					print (this.gameObject.name);
//					potsComponent.BallInsidePots ();
					PotAssignedRedColor = true;
					this.gameObject.tag = "RedBalls_Pot";
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			} else if (coll.gameObject.tag == "Red_Draggable" && PotAssignedRedColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "RedBalls_Pot") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					GameManager.TotalPointsOrScores += 3;
					print (this.gameObject.name);
//					potsComponent.BallInsidePots ();
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			}

			// BLUE BALL COLLISION WITH POTS
			if (coll.gameObject.tag == "Blue_Draggable" && !PotAssignedBlueColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "Untagged") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					PotAssignedBlueColor = true;
					this.gameObject.tag = "BlueBalls_Pot";
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);

					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
//					potsComponent.BallInsidePots ();

					Destroy (coll.gameObject);
				} else {
					
					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			} else if (coll.gameObject.tag == "Blue_Draggable" && PotAssignedBlueColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "BlueBalls_Pot") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
//					potsComponent.BallInsidePots ();
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			}

			// GREEN BALL COLLISION WITH POTS
			if (coll.gameObject.tag == "Green_Draggable" && !PotAssignedGreenColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "Untagged") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					PotAssignedGreenColor = true;
					this.gameObject.tag = "GreenBalls_Pot";
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
//					potsComponent.BallInsidePots ();
					Destroy (coll.gameObject);
				} else {
					
					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			} else if (coll.gameObject.tag == "Green_Draggable" && PotAssignedGreenColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "GreenBalls_Pot") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
//					potsComponent.BallInsidePots ();
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			}

			// WHITE BALL COLLISION WITH POTS
			if (coll.gameObject.tag == "White_Draggable" && !PotAssignedWhiteColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "Untagged") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					PotAssignedWhiteColor = true;
					this.gameObject.tag = "WhiteBalls_Pot";
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
//					potsComponent.BallInsidePots ();
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			} else if (coll.gameObject.tag == "White_Draggable" && PotAssignedWhiteColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "WhiteBalls_Pot") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
//					potsComponent.BallInsidePots ();
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			}

			// YELLOW BALL COLLISION WITH POTS
			if (coll.gameObject.tag == "Yellow_Draggable" && !PotAssignedYellowColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "Untagged") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					PotAssignedYellowColor = true;
					this.gameObject.tag = "YellowBalls_Pot";
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			} else if (coll.gameObject.tag == "Yellow_Draggable" && PotAssignedYellowColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "YellowBalls_Pot") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
//					potsComponent.BallInsidePots ();
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			}
			//Gray Ball Collision With Pot
			if (coll.gameObject.tag == "Gray_Draggable" && !PotAssignedGrayColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "Untagged") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					PotAssignedGrayColor = true;
					this.gameObject.tag = "GrayBalls_Pot";
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			} else if (coll.gameObject.tag == "Gray_Draggable" && PotAssignedGrayColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "GrayBalls_Pot") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
//					potsComponent.BallInsidePots ();
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			}
			//Pink Ball Collision With Pot
			if (coll.gameObject.tag == "Pink_Draggable" && !PotAssignedPinkColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "Untagged") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					PotAssignedPinkColor = true;
					this.gameObject.tag = "PinkBalls_Pot";
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			} else if (coll.gameObject.tag == "Pink_Draggable" && PotAssignedPinkColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "PinkBalls_Pot") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
//					potsComponent.BallInsidePots ();
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			}
			//Brown Ball Collision With Pot
			if (coll.gameObject.tag == "Brown_Draggable" && !PotAssignedBrownColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "Untagged") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					PotAssignedBrownColor = true;
					this.gameObject.tag = "BrownBalls_Pot";
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			} else if (coll.gameObject.tag == "Brown_Draggable" && PotAssignedBrownColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "BrownBalls_Pot") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
//					potsComponent.BallInsidePots ();
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			}
			//Purpel Ball Collision With Pot
			if (coll.gameObject.tag == "Parpel_Draggable" && !PotAssignedPurpelColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "Untagged") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					PotAssignedPurpelColor = true;
					this.gameObject.tag = "ParpelBalls_Pot";
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
					Destroy (coll.gameObject);
				} else {					

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			} else if (coll.gameObject.tag == "Parpel_Draggable" && PotAssignedPurpelColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "ParpelBalls_Pot") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
//					potsComponent.BallInsidePots ();
					Destroy (coll.gameObject);
				} else {				

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			}
			//SkyBlue Ball Collision With Pot
			if (coll.gameObject.tag == "SkyBlue_Draggable" && !PotAssignedSkyBlueColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "Untagged") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					PotAssignedSkyBlueColor = true;
					this.gameObject.tag = "SkyBlueBalls_Pot";
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
					Destroy (coll.gameObject);
				} else {	

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			} else if (coll.gameObject.tag == "SkyBlue_Draggable" && PotAssignedSkyBlueColor && coll.gameObject == MainGameController_Mouse.GameObject_toDrag) {
				if (this.gameObject.tag == "SkyBlueBalls_Pot") {
					BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
					GameManager.TotalPointsOrScores += 3;
					potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
					RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
//					potsComponent.BallInsidePots ();
					Destroy (coll.gameObject);
				} else {				

					// Push a Ball Out of screen
					MainGameController_Mouse.ballCollidedWithPots = true;
					BallCollidedWithWrongPot = coll.gameObject;
					ApplyForceWhenWrongBallCollidedWithPot ();
				}
			}
		} 
	}

	#elif UNITY_IPHONE
		void OnTriggerEnter2D(Collider2D coll)
		{
	
	
			if (potsComponent.PotFilled (10))
			{
	
				// RED BALL COLLISION WITH POTS
				if (coll.gameObject.tag == "Red_Draggable" && !PotAssignedRedColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "Untagged")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
	
						//potsComponent.BallInsidePots ();
	
	
						PotAssignedRedColor = true;
						this.gameObject.tag = "RedBalls_Pot";
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
				else if(coll.gameObject.tag == "Red_Draggable" && PotAssignedRedColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "RedBalls_Pot")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
	
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						print (this.gameObject.name);
						//potsComponent.BallInsidePots ();
	
	
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
	
				// BLUE BALL COLLISION WITH POTS
				if (coll.gameObject.tag == "Blue_Draggable" && !PotAssignedBlueColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "Untagged")
					{
						//potsComponent.BallInsidePots ();
	
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
	
						PotAssignedBlueColor = true;
						this.gameObject.tag = "BlueBalls_Pot";
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
				else if(coll.gameObject.tag == "Blue_Draggable" && PotAssignedBlueColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "BlueBalls_Pot")
					{
						//potsComponent.BallInsidePots ();
	
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
	
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
	
				// GREEN BALL COLLISION WITH POTS
				if (coll.gameObject.tag == "Green_Draggable" && !PotAssignedGreenColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "Untagged")
					{
						//potsComponent.BallInsidePots ();
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						PotAssignedGreenColor = true;
						this.gameObject.tag = "GreenBalls_Pot";
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
				else if(coll.gameObject.tag == "Green_Draggable" && PotAssignedGreenColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "GreenBalls_Pot")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
	
				// WHITE BALL COLLISION WITH POTS
				if (coll.gameObject.tag == "White_Draggable" && !PotAssignedWhiteColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "Untagged")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						PotAssignedWhiteColor = true;
						this.gameObject.tag = "WhiteBalls_Pot";
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
				else if(coll.gameObject.tag == "White_Draggable" && PotAssignedWhiteColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "WhiteBalls_Pot")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
	
				// YELLOW BALL COLLISION WITH POTS
				if (coll.gameObject.tag == "Yellow_Draggable" && !PotAssignedYellowColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "Untagged")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						PotAssignedYellowColor = true;
						this.gameObject.tag = "YellowBalls_Pot";
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
				else if(coll.gameObject.tag == "Yellow_Draggable" && PotAssignedYellowColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "YellowBalls_Pot")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
					// Gray BALL COLLISION WITH POTS
				if (coll.gameObject.tag == "Gray_Draggable" && !PotAssignedGrayColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "Untagged")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						PotAssignedGrayColor = true;
						this.gameObject.tag = "GrayBalls_Pot";
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
				else if(coll.gameObject.tag == "Gray_Draggable" && PotAssignedGrayColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "GrayBalls_Pot")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
				// Pink BALL COLLISION WITH POTS
				if (coll.gameObject.tag == "Pink_Draggable" && !PotAssignedPinkColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "Untagged")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						PotAssignedPinkColor = true;
						this.gameObject.tag = "PinkBalls_Pot";
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
				else if(coll.gameObject.tag == "Pink_Draggable" && PotAssignedPinkColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "PinkBalls_Pot")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
						// Brown BALL COLLISION WITH POTS
				if (coll.gameObject.tag == "Brown_Draggable" && !PotAssignedBrownColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "Untagged")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						PotAssignedBrownColor = true;
						this.gameObject.tag = "BrownBalls_Pot";
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
				else if(coll.gameObject.tag == "Brown_Draggable" && PotAssignedBrownColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "BrownBalls_Pot")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
				// SkyBlue BALL COLLISION WITH POTS
				if (coll.gameObject.tag == "SkyBlue_Draggable" && !PotAssignedSkyBlueColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "Untagged")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						PotAssignedSkyBlueColor = true;
						this.gameObject.tag = "SkyBlueBalls_Pot";
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					}
					else
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
				else if(coll.gameObject.tag == "SkyBlue_Draggable" && PotAssignedSkyBlueColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "SkyBlueBalls_Pot")
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
	//					potsComponent.BallInsidePots ();
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					} 
					else 
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
				// Purpel BALL COLLISION WITH POTS
				if (coll.gameObject.tag == "Parpel_Draggable" && !PotAssignedPurpelColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag) 
				{
					if (this.gameObject.tag == "Untagged") 
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						PotAssignedPurpelColor = true;
						this.gameObject.tag = "ParpelBalls_Pot";
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					} 
					else 
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
				else if(coll.gameObject.tag == "Parpel_Draggable" && PotAssignedPurpelColor && coll.gameObject == MainGameController_Touch.GameObject_toDrag)
				{
					if (this.gameObject.tag == "ParpelBalls_Pot") 
					{
						BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballPostClip);
						//potsComponent.BallInsidePots ();
						GameManager.TotalPointsOrScores += 3;
						potsComponent.FillingOfTexturesIntoPot (this.gameObject.name);
						RandomInstantiationOfBalls.instantiatedBalls_Runtime.Remove (coll.gameObject);
						Destroy (coll.gameObject);
					} 
					else 
					{
						// Push a Ball Out of screen
						MainGameController_Touch.ballCollidedWithPots = true;
						BallCollidedWithWrongPot = coll.gameObject;
						ApplyForceWhenWrongBallCollidedWithPot();
					}
				}
			}
		}
	
	
		#endif

	void ApplyForceWhenWrongBallCollidedWithPot ()
	{
		Handheld.Vibrate ();
		BackgroundMusicManger.instance.PlaySoundEffect (audioSource, ballInWrongPostClip);

		BallCollidedWithWrongPot.GetComponent<Movement_RandomInstantiatedBalls> ().enabled = false;
//		BallCollidedWithWrongPot.GetComponent<BoxCollider2D> ().enabled = false;
		if (this.gameObject.layer == 12) {
			
			if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.LeftCenter)
				//Left
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (2, 5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.RightCenter)
				//Right
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-2, -5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.BottomCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (2, 5)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.TopCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (-2, -5)) * SpeedOfBallWhenPutInWrongPot;
		} else if (this.gameObject.layer == 13) {
//			BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-2, -5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.LeftCenter)
				//Left
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (2, 5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.RightCenter)
				//Right
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-2, -5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.BottomCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (2, 5)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.TopCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (-2, -5)) * SpeedOfBallWhenPutInWrongPot;
		} else if (this.gameObject.layer == 14) {
//			BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (2, 5)) * SpeedOfBallWhenPutInWrongPot;
			if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.LeftCenter)
				//Left
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (2, 5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.RightCenter)
				//Right
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-2, -5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.BottomCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (2, 5)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.TopCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (-2, -5)) * SpeedOfBallWhenPutInWrongPot;
		} else if (this.gameObject.layer == 15) {
//			BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (-2, -5)) * SpeedOfBallWhenPutInWrongPot;
			if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.LeftCenter)
				//Left
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (2, 5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.RightCenter)
				//Right
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-2, -5), Random.Range (-3f, 3f)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.BottomCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (2, 5)) * SpeedOfBallWhenPutInWrongPot;
			else if (this.gameObject.GetComponent<CameraAnchor> ().anchorType == CameraAnchor.AnchorType.TopCenter)
				// Down
				BallCollidedWithWrongPot.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (-2, -5)) * SpeedOfBallWhenPutInWrongPot;
		}
		BallCollidedWithWrongPot = null;
	}
}
