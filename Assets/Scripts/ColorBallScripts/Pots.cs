using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Pots : MonoBehaviour
{
	public int ballsEnteredPot;

	private int potsLimit;
	//	private ScreenManager screenManagerComponent;
	private GameManager gameManager_Component;


	void OnEnable ()
	{
		ballsEnteredPot = 0;
//		screenManagerComponent =  GameObject.Find ("ScreenManager").GetComponent<ScreenManager>();
		gameManager_Component = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
		
	//Calculating Ball Enterede in the pots
	public void BallInsidePots ()
	{
		ballsEnteredPot++;
		if (ballsEnteredPot >= 10) {
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			this.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			Invoke ("PotReset", 1.30f);
		}
	}


	public bool PotFilled (int potLimit)
	{
		if (ballsEnteredPot >= potLimit) {
			return false;
		} else {
			return true;
		}
	}

	void PotReset ()
	{
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		this.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		ballsEnteredPot = 0;
	}

	public void FillingOfTexturesIntoPot (string nameOfPot)
	{
		BallInsidePots ();
		for (int i = 0; i < gameManager_Component.potList.Count; i++) {
			if (gameManager_Component.potList [i].name == nameOfPot) {
				if (gameManager_Component.potList_Textures [i].GetComponent<Image> ().fillAmount <= 0.9f)
					gameManager_Component.potList_Textures [i].GetComponent<Image> ().fillAmount += 0.1f;
				else
					gameManager_Component.potList_Textures [i].GetComponent<Image> ().fillAmount = 0.0f;				
				break;
			}
		}
	}
}