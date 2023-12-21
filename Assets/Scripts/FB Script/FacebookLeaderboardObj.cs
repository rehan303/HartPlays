using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FacebookLeaderboardObj : MonoBehaviour
{

	public Text Rank;
	public Image ProfileView;
	public Text PlayerName;
	public Text Score;
	public string PlayerFbId;

	public void SetLeaderBoard (Text rank, Image profile, Text Name, Text score)
	{
		Rank = rank;
		ProfileView = profile;
		PlayerName = Name;
		Score = score;
	}
}
