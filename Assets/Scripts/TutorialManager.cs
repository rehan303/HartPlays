using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

	[Header ("Color Ball Game Tutorial")]
	//ColorBall Tutorial
	public List<Sprite> ColorBallTutList = new List<Sprite> ();
	public Image ColorBallImageHolder;
	public int Cnum = 0;
	public GameObject CNextButton;
	public GameObject CBackButton;
	public GameObject ColorBallTutPanel;


	[Header ("Dodger Game Tutorial")]
	//Dodger Tutorial
	public List<Sprite> DodgerTutList = new List<Sprite> ();
	public Image DodgerImageHolder;
	public int Dnum = 0;
	public GameObject DNextButton;
	public GameObject DBackButton;
	public GameObject DodgerTutPanel;

	void Start ()
	{

		ColorBallTutPanel.transform.localScale = Vector3.zero;
		DodgerTutPanel.transform.localScale = Vector3.zero;
//		ColorBall
		Cnum++;
		ColorBallImageHolder.sprite = ColorBallTutList [Cnum];
		if (Cnum == 3) {
			CNextButton.SetActive (true);
			CBackButton.SetActive (true);
		} else if (Cnum == 1) {
			CNextButton.SetActive (true);
			CBackButton.SetActive (false);
		} else if (Cnum == 2) {
			CNextButton.SetActive (true);
			CBackButton.SetActive (true);
		}
		else if (Cnum == 4) {
			CNextButton.SetActive (false);
			CBackButton.SetActive (true);
		}
		//Dodger 
		Dnum++;
		DodgerImageHolder.sprite = DodgerTutList [Dnum];
		if (Dnum == 3) {
			DNextButton.SetActive (true);
			DBackButton.SetActive (true);
		} else if (Dnum == 1) {
			DNextButton.SetActive (true);
			DBackButton.SetActive (false);
		} else if (Dnum == 2) {
			DNextButton.SetActive (true);
			DBackButton.SetActive (true);
		} else if (Dnum == 4) {
			DNextButton.SetActive (false);
			DBackButton.SetActive (true);
		}
	}

	public void ChangeTutImageForward ()
	{
		
		Cnum++;
		ColorBallImageHolder.sprite = ColorBallTutList [Cnum];
		if (Cnum == 3) {
			CNextButton.SetActive (true);
			CBackButton.SetActive (true);
		} else if (Cnum == 1) {
			CNextButton.SetActive (true);
			CBackButton.SetActive (false);
		} else if (Cnum == 2) {
			CNextButton.SetActive (true);
			CBackButton.SetActive (true);
		}	else if (Cnum == 4) {
			CNextButton.SetActive (false);
			CBackButton.SetActive (true);
		}
	}

	public void ChangeTutImageBackward ()
	{
		Cnum--;
		ColorBallImageHolder.sprite = ColorBallTutList [Cnum];
		if (Cnum == 3) {
			CNextButton.SetActive (true);
			CBackButton.SetActive (true);
		} else if (Cnum == 1) {
			CNextButton.SetActive (true);
			CBackButton.SetActive (false);
		} else if (Cnum == 2) {
			CNextButton.SetActive (true);
			CBackButton.SetActive (true);
		}	else if (Cnum == 4) {
			CNextButton.SetActive (false);
			CBackButton.SetActive (true);
		}
	}

	public void ChangeDogerTutImageForward ()
	{

		Dnum++;
		DodgerImageHolder.sprite = DodgerTutList [Dnum];
		if (Dnum == 3) {
			DNextButton.SetActive (true);
			DBackButton.SetActive (true);
		} else if (Dnum == 1) {
			DNextButton.SetActive (true);
			DBackButton.SetActive (false);
		} else if (Dnum == 2 || Dnum == 3) {
			DNextButton.SetActive (true);
			DBackButton.SetActive (true);
		}if (Dnum == 4) {
			DNextButton.SetActive (true);
			DBackButton.SetActive (true);
		}if (Dnum == 5) {
			DNextButton.SetActive (false);
			DBackButton.SetActive (true);
		}
	}

	public void ChangeDogerTutImageBackward ()
	{
		Dnum--;
		DodgerImageHolder.sprite = DodgerTutList [Dnum];
		if (Dnum == 3) {
			DNextButton.SetActive (true);
			DBackButton.SetActive (true);
		} else if (Dnum == 1) {
			DNextButton.SetActive (true);
			DBackButton.SetActive (false);
		} else if (Dnum == 2 || Dnum == 3) {
			DNextButton.SetActive (true);
			DBackButton.SetActive (true);
		}if (Dnum == 4) {
			DNextButton.SetActive (true);
			DBackButton.SetActive (true);
		}if (Dnum == 5) {
			DNextButton.SetActive (false);
			DBackButton.SetActive (true);
		}
	}

	public void OnpenCloseColorBallTut ()
	{
		if (ColorBallTutPanel.transform.localScale == Vector3.one) {
			ColorBallTutPanel.transform.localScale = Vector3.zero;
		} else
			ColorBallTutPanel.transform.localScale = Vector3.one;
	}

	public void OnpenCloseDodgerTut ()
	{
		if (DodgerTutPanel.transform.localScale == Vector3.one) {
			DodgerTutPanel.transform.localScale = Vector3.zero;
		} else
			DodgerTutPanel.transform.localScale = Vector3.one;
	}

}
