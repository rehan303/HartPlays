using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NinjaFootie
{
	public class NinjaLevelSelection : MonoBehaviour
	{
		public GameObject[] LevelSelectionButtons;

		public Sprite LevelClear_Image;
		public Sprite LevelLocked_Image;
		public  int CurrentLevel;
		public static bool PlayForFree = true;

		void Awake ()
		{
			SetScreenOrientation ();
		}

		void Start ()
		{		// TODO
//			PlayerPrefs.DeleteKey ("NinjaLevelUnlockedFree");
			SetScreenOrientation ();
			SetLevelButtons ();
		}

		void Update ()
		{
			if (Screen.orientation == ScreenOrientation.Portrait) {
				SetScreenOrientation ();
			}
		}

		void SetScreenOrientation ()
		{
			Screen.autorotateToLandscapeLeft = true;
			Screen.autorotateToLandscapeRight = true;
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			Screen.autorotateToPortrait = false;
			Screen.autorotateToPortraitUpsideDown = false;		

		}

		void SetLevelButtons ()
		{
			//To be Deleted
//			PlayerPrefs.SetInt ("NinjaLevelUnlockedFree", 15);
			//
			if (!HartPlayerRegistration.Isplayforpaid) {
				if (!PlayerPrefs.HasKey ("NinjaLevelUnlockedFree")) {
					PlayerPrefs.SetInt ("NinjaLevelUnlockedFree", 1);
				}

				for (int i = 0; i < LevelSelectionButtons.Length; i++) {
					//				print ("unlocked levels are --- " + PlayerPrefs.GetInt ("NinjaLevelUnlockedFree"));
					if (i < PlayerPrefs.GetInt ("NinjaLevelUnlockedFree")) {
						LevelSelectionButtons [i].GetComponent<Image> ().sprite = LevelClear_Image;
						LevelSelectionButtons [i].transform.GetChild (0).gameObject.SetActive (true);
					} else {
						LevelSelectionButtons [i].GetComponent<Image> ().sprite = LevelLocked_Image;
						LevelSelectionButtons [i].transform.GetChild (0).gameObject.SetActive (false);
					}
				}
			} else if (HartPlayerRegistration.Isplayforpaid) {
				if (!PlayerPrefs.HasKey ("NinjaLevelUnlockedPaid")) {
					PlayerPrefs.SetInt ("NinjaLevelUnlockedPaid", 1);
				}

				for (int i = 0; i < LevelSelectionButtons.Length; i++) {
					if (i == PlayerPrefs.GetInt ("NinjaLevelUnlockedPaid") - 1) {
						LevelSelectionButtons [i].GetComponent<Image> ().sprite = LevelClear_Image;
						LevelSelectionButtons [i].transform.GetChild (0).gameObject.SetActive (true);
					} else {
						LevelSelectionButtons [i].GetComponent<Image> ().sprite = LevelLocked_Image;
						LevelSelectionButtons [i].transform.GetChild (0).gameObject.SetActive (false);
					}
				}
			}
			print ("CurrentLevel is -->>" + CurrentLevel);

		}

		public void OnClickedMainMenuButton ()
		{
			SceneManager.LoadScene ("00_MenuScreen");
		}

		public void LevelSelectionButtonClicked ()
		{
			//		Debug.Log ("Button Clicked is ---->>>>" + EventSystem.current.currentSelectedGameObject.name);
			if (EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite == LevelClear_Image) {
				PlayerPrefs.SetString ("NinjaLevelSelected", EventSystem.current.currentSelectedGameObject.name.Split ('_') [1].ToString ());
				print ("LevelSelected is -->>" + PlayerPrefs.GetString ("NinjaLevelSelected"));
				CurrentLevel = int.Parse (PlayerPrefs.GetString ("NinjaLevelSelected"));
				SceneManager.LoadScene ("04_NinjaFootie");

			}
		}
	}
}