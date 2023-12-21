using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WheelofFortune : MonoBehaviour {

	public List<string> GameMode;
	public static string SpinResult;
	public List <Text> ButtonText;
	public List <Sprite> BoxImage;
	public List <Button> btnList;

	List <string> tempGameMode;
	List <Button> tempBtnlist;
	public MenuScreen_Controller levelControler;
	int LevelSelection;

	public Button BackButton;
	// Use this for initialization

	void Start () {
		levelControler = GameObject.Find ("Main Camera").GetComponent<MenuScreen_Controller> ();
		BackButton.interactable = true;
	}	
	public void SelectGameMode(int num)
	{
		BackButton.interactable = false;
		int randomVal = Random.Range (0, 8);
		if (randomVal >= 0 && randomVal <= 1)
			randomVal = 0;
		else if (randomVal >= 2 && randomVal <= 3)
			randomVal = 1;		
		else if (randomVal >= 4 && randomVal <= 5)
			randomVal = 2;		
		else if (randomVal >= 6 && randomVal <= 8)
			randomVal = 3;		
		print (randomVal.ToString ());
		SpinResult = GameMode [randomVal];
		if(num == 0)
		{			
			btnList [num].gameObject.GetComponent <Image> ().sprite = BoxImage [1];
			btnList [num].transform.GetChild (0).GetComponent <Text> ().text = SpinResult;
			tempGameMode = GameMode;
			tempBtnlist = btnList;
			for(int i=0; i<tempBtnlist.Count; i++)
			{
				tempBtnlist [i].interactable = false;
			}
			print (SpinResult);
		} else if(num == 1)
		{
			btnList [num].gameObject.GetComponent <Image> ().sprite = BoxImage [0];
			btnList [num].transform.GetChild (0).GetComponent <Text> ().text = SpinResult;
			tempGameMode = GameMode;
			tempBtnlist = btnList;
			for(int i=0; i<tempBtnlist.Count; i++)
			{
				tempBtnlist [i].interactable = false;
			}
			print (SpinResult);
		} else if(num == 2)
		{
			btnList [num].gameObject.GetComponent <Image> ().sprite = BoxImage [2];
			btnList [num].transform.GetChild (0).GetComponent <Text> ().text = SpinResult;
			tempGameMode = GameMode;
			tempBtnlist = btnList;
			for(int i=0; i<tempBtnlist.Count; i++)
			{
				tempBtnlist [i].interactable = false;
			}
			print (SpinResult);
		} else if(num == 3)
		{
			btnList [num].gameObject.GetComponent <Image> ().sprite = BoxImage [3];
			btnList [num].transform.GetChild (0).GetComponent <Text> ().text = SpinResult;
			tempGameMode = GameMode;
			tempBtnlist = btnList;
			for(int i=0; i<tempBtnlist.Count; i++)
			{
				tempBtnlist [i].interactable = false;
			}
			print (SpinResult);
		} 

		tempGameMode.RemoveAt (randomVal);
		tempBtnlist.RemoveAt (num);
		for (int i =0; i< tempGameMode.Count ; i++)
		{
			if(tempGameMode[i] != SpinResult)
			{
				tempBtnlist[i].transform.GetChild (0).GetComponent <Text> ().text = tempGameMode[i];
			}
			
		}
		// Set button interactable false
		for (int k = 0; k< btnList.Count; k++)
		{
			btnList [k].interactable = false;
		}	
		StartCoroutine (PlayGame());
	}

	IEnumerator PlayGame()
	{
		yield return new WaitForSeconds (1.5f);
		print (SpinResult);
		LevelSelection = 0;
		StartCoroutine (GameSaveState.Instance.GetUserStatus (true));	

		if(SpinResult.Contains ("EASY"))
		{
			if (MenuScreen_Controller.GameSelectionName.Contains ("ColorGame")) {
				LevelSelection = Random.Range (1, 5);
				levelControler.LevelSelectionButtonClickedFromWheelOfFortutne (LevelSelection);
			} else if(MenuScreen_Controller.GameSelectionName.Contains ("DodgerGame"))
			{
				SceneManager.LoadScene ("03_DogerGamePlay");
			}
		}else if(SpinResult.Contains ("MEDIUM"))
		{
			if (MenuScreen_Controller.GameSelectionName.Contains ("ColorGame")) {
				LevelSelection = Random.Range (6, 10);
				levelControler.LevelSelectionButtonClickedFromWheelOfFortutne (LevelSelection);
			}else if(MenuScreen_Controller.GameSelectionName.Contains ("DodgerGame"))
			{
				SceneManager.LoadScene ("03_DogerGamePlay");
			}
		}else if(SpinResult.Contains ("HARD"))
		{
			if (MenuScreen_Controller.GameSelectionName.Contains ("ColorGame")) {
				LevelSelection = Random.Range (11, 15);
				levelControler.LevelSelectionButtonClickedFromWheelOfFortutne (LevelSelection);
			}else if(MenuScreen_Controller.GameSelectionName.Contains ("DodgerGame"))
			{
				SceneManager.LoadScene ("03_DogerGamePlay");
			}
		}else if(SpinResult.Contains ("EXTREME"))
		{
			if (MenuScreen_Controller.GameSelectionName.Contains ("ColorGame")) {
				LevelSelection = Random.Range (15, 20);
				levelControler.LevelSelectionButtonClickedFromWheelOfFortutne (LevelSelection);
			}else if(MenuScreen_Controller.GameSelectionName.Contains ("DodgerGame"))
			{
				SceneManager.LoadScene ("03_DogerGamePlay");
			}
		}

	}
}
