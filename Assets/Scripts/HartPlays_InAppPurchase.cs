using UnityEngine;
using System.Collections;



public class HartPlays_InAppPurchase : MonoBehaviour
{
	public static HartPlays_InAppPurchase Instance = null;
	SmartIAPAgent smartIapAgent;

	[Header ("Play For Free Product")]
	public string[] proId = {
		"com.Hartplays.game_colorball_bomb",
		"com.Hartplays.game_colorball_freeze",
		"com.Hartplays.game_colorball_reminder",
		"com.Hartplays.game_dodger_invincible",
		"com.Hartplays.game_dodger_merge",
		"com.Hartplays.game_dodger_bomb",
		"com.Hartplays.game_dodger_slow_down",
		"com.Hartplays.game_ninja_footie_golden_star",
		"com.Hartplays.game_ninja_footie_rhombus",
		"com.Hartplays.game_ninja_footie_square",
		"com.Hartplays.game_ninja_footie_white_star",
		"HartPlays_PlaytoWin_Subscribe"
	};
	[Header ("Play For Win Subscription Plan")]
	public string[] Subscription  ={
		"com.Hartplays.game_10Plays_Subscription",
		"com.Hartplays.game_20Plays_Subscription",
		"com.Hartplays.game_30Plays_Subscription",
		"com.Hartplays.game_24Hours_Play_Subscription",
		"com.Hartplays.game_7Days_Play_Subscription"
	};

	[Header ("Color Ball Game Lives")]
	public string[] ColorBallGameLives = {
		"com.Hartplays.game_ColorBall_5Lives",
		"com.Hartplays.game_ColorBall_10Lives"
	};

	[Header ("Dodger Game Lives")]
	public string[] DodgerGameLives = {
		"com.Hartplays.game_Dodger_5Lives",
		"com.Hartplays.game_Dodger_10Lives"
	};

	public string[] dummy;
	// Use this for initialization

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (this.gameObject);
		}
	}

	void Start ()
	{
		DontDestroyOnLoad (this.gameObject);
//		SmartIAP.Instance ().Initialize (SmartIAPStoreSelection.AutoDetect, "", "", 
//			false, true, proId, 
//			dummy, dummy, dummy,
//			false, true, "");
	}

	
	// Update is called once per frame
	public void PurchaseHartPlaysProduct (int ProductId)
	{
		SmartIAP.Instance ().Purchase (Subscription [ProductId]);		
	}

	public void PurchaseLivesForColorBall (int ProductId)
	{
		SmartIAP.Instance ().Purchase (ColorBallGameLives [ProductId]);	
	}

	public void PurchaseLivesForDodger (int ProductId)
	{
		SmartIAP.Instance ().Purchase (DodgerGameLives [ProductId]);	
	}

}
