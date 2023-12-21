using UnityEngine;
using System.Collections;

namespace NinjaFootie
{ 
	public class NinjaGameManager : MonoBehaviour {

		public static NinjaGameManager Instance;

		public static int AiScores;
		public static int PlayerScore;
		NinjaPowerUpController PowerUpController;
		void Awake () 
		{
			if (Instance == null)
				Instance = this;
			else if (Instance != this)
				Destroy(Instance);
			PowerUpController = GameObject.Find ("NinjaPowerUpController").GetComponent<NinjaPowerUpController> ();
		}

		void Start()
		{
			LoadLevel (GetCurrentLevel ());
		}

		/// <summary>
		/// Loads the level according to given level int.
		/// </summary>
		/// <param name="levelNumber">Level number.</param>
		public void LoadLevel(int levelNumber)
		{
			switch (levelNumber)
			{
		
			case 1:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 3f;
				NinjaBall.BallpositionChangingRate = 5f;
				NetColorPicker.NetColorChangeRate = 3f;
				AI_NinjaFootie.SpeedofMovement = 4f;
				AI_NinjaFootie.AiReactionTime = 5f;

				PowerUpController.PowerupCount (false,false,false,false);
				//TODO other level settings of each level;
				break;
			case 2:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 2.9f;
				NinjaBall.BallpositionChangingRate = 4.8f;
				NetColorPicker.NetColorChangeRate = 2.9f;
				AI_NinjaFootie.SpeedofMovement = 4.4f;
				AI_NinjaFootie.AiReactionTime = 4.75f;

				PowerUpController.PowerupCount (false,false,false,false);
				break;
			case 3:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 2.8f;
				NinjaBall.BallpositionChangingRate = 4.6f;
				NetColorPicker.NetColorChangeRate = 2.8f;
				AI_NinjaFootie.SpeedofMovement = 4.8f;
				AI_NinjaFootie.AiReactionTime = 4.5f;

				PowerUpController.PowerupCount (false,false,false,false);
				break;
			case 4:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 2.7f;
				NinjaBall.BallpositionChangingRate = 4.4f;
				NetColorPicker.NetColorChangeRate = 2.7f;
				AI_NinjaFootie.SpeedofMovement = 5.2f;
				AI_NinjaFootie.AiReactionTime = 4.25f;

				PowerUpController.PowerupCount (false,false,false,false);
				break;

			case 5:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 2.6f;
				NinjaBall.BallpositionChangingRate = 4.2f;
				NetColorPicker.NetColorChangeRate = 2.6f;
				AI_NinjaFootie.SpeedofMovement = 5.6f;
				AI_NinjaFootie.AiReactionTime = 4.0f;

				PowerUpController.PowerupCount (true,false,false,false);
				break;
			case 6:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 2.5f;
				NinjaBall.BallpositionChangingRate = 4.0f;
				NetColorPicker.NetColorChangeRate = 2.5f;
				AI_NinjaFootie.SpeedofMovement = 6.0f;
				AI_NinjaFootie.AiReactionTime = 3.75f;

				PowerUpController.PowerupCount (false,true,false,false);
				break;
			case 7:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 2.4f;
				NinjaBall.BallpositionChangingRate = 3.8f;
				NetColorPicker.NetColorChangeRate = 2.4f;
				AI_NinjaFootie.SpeedofMovement = 6.4f;
				AI_NinjaFootie.AiReactionTime = 3.50f;

				PowerUpController.PowerupCount (false,false,true,false);
				break;
			case 8:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 2.3f;
				NinjaBall.BallpositionChangingRate = 3.6f;
				NetColorPicker.NetColorChangeRate = 2.3f;
				AI_NinjaFootie.SpeedofMovement = 6.8f;
				AI_NinjaFootie.AiReactionTime = 3.25f;

				PowerUpController.PowerupCount (false,false,false,true);
				break;
			case 9:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 2.2f;
				NinjaBall.BallpositionChangingRate = 3.4f;
				NetColorPicker.NetColorChangeRate = 2.2f;
				AI_NinjaFootie.SpeedofMovement = 7.2f;
				AI_NinjaFootie.AiReactionTime = 3.0f;

				PowerUpController.PowerupCount (true,true,false,false);
				break;
			case 10:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 2.0f;
				NinjaBall.BallpositionChangingRate = 3.2f;
				NetColorPicker.NetColorChangeRate = 2.1f;
				AI_NinjaFootie.SpeedofMovement = 7.6f;
				AI_NinjaFootie.AiReactionTime = 2.75f;

				PowerUpController.PowerupCount (false,true,true,false);
				break;
			case 11:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 1.75f;
				NinjaBall.BallpositionChangingRate = 3.0f;
				NetColorPicker.NetColorChangeRate = 2.0f;
				AI_NinjaFootie.SpeedofMovement = 8.0f;
				AI_NinjaFootie.AiReactionTime = 2.50f;

				PowerUpController.PowerupCount (false,false,true,true);
				break;
			case 12:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 1.50f;
				NinjaBall.BallpositionChangingRate = 2.8f;
				NetColorPicker.NetColorChangeRate = 1.75f;
				AI_NinjaFootie.SpeedofMovement = 8.4f;
				AI_NinjaFootie.AiReactionTime = 2.25f;

				PowerUpController.PowerupCount (true,false,true,false);
				break;
			case 13:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 1.25f;
				NinjaBall.BallpositionChangingRate = 2.6f;
				NetColorPicker.NetColorChangeRate = 1.50f;
				AI_NinjaFootie.SpeedofMovement = 8.8f;
				AI_NinjaFootie.AiReactionTime = 2.0f;

				PowerUpController.PowerupCount (false,true,false,true);
				break;
			case 14:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 1.0f;
				NinjaBall.BallpositionChangingRate = 2.4f;
				NetColorPicker.NetColorChangeRate = 1.25f;
				AI_NinjaFootie.SpeedofMovement = 9.2f;
				AI_NinjaFootie.AiReactionTime = 1.75f;

				PowerUpController.PowerupCount (true,true,true,false);
				break;
			case 15:
				NinjaScreenManager.timeOfLevel = 60;
				NinjaBall.BallcolorChangingRate = 0.8f;
				NinjaBall.BallpositionChangingRate = 2f;
				NetColorPicker.NetColorChangeRate = 1f;
				AI_NinjaFootie.SpeedofMovement = 9.6f;
				AI_NinjaFootie.AiReactionTime = 1.5f;

				PowerUpController.PowerupCount (true,true,false,true);
				break;

			}
		}
		public static int GetCurrentLevel()
		{
			return int.Parse (PlayerPrefs.GetString ("NinjaLevelSelected"));
		} 
	}
}