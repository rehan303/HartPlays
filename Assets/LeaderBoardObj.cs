using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeaderBoardObj : MonoBehaviour {

	public Text UserName;
	public Text Score;
	public Text HighScoreGameName;
	public Text Prize;
	public Image Icon;
	public LeaderBoardAtribute playerRecored;
	public Sprite[] GameIcon;

	public void SetLeaderBoardData(LeaderBoardAtribute Recored)
	{
		playerRecored = Recored;

		UserName.text = Recored.Name;
		Score.text = Recored.TotalHighScore.ToString ();
		if(Recored.TotalHighScore == 0)
		{
			Icon.sprite = null;
		}else if(Recored.TotalHighScore == Recored.ColorBallHighScore)
		{
			Icon.sprite = GameIcon [0];
		}else if(Recored.TotalHighScore == Recored.DodgerHighScore)
		{
			Icon.sprite = GameIcon [1];
		}else if(Recored.TotalHighScore != Recored.DodgerHighScore)
		{
			
			Icon.sprite = null;
		}else if(Recored.TotalHighScore != Recored.ColorBallHighScore)
		{
			Icon.sprite = null;
		}
		HighScoreGameName.text = Recored.Rank.ToString ();
		Prize.text ="$"+ Recored.Prize.ToString ();
	}
}
