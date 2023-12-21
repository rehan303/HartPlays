using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CountdownTimer : MonoBehaviour
{
	public static float timeOfLevel;
	public float timeLeft = 300.0f;
	public bool stop = true;

	private float minutes;
	static public float seconds;

	public Text text;
	public ScreenManager screenManagerComponent;

	void OnEnable ()
	{
		startTimer (timeOfLevel);
		screenManagerComponent = GameObject.Find ("ScreenManager").GetComponent<ScreenManager> ();
	}

	public void startTimer (float from)
	{
		stop = false;
		timeLeft = from;
//		Update();
		StartCoroutine (updateCoroutine ());
	}

	void Update ()
	{
		if (stop)
			return;

		timeLeft -= Time.deltaTime;
		minutes = Mathf.Floor (timeLeft / 60);
		seconds = timeLeft % 60;
		if (seconds > 59) {
			seconds = 59;
		}
		//Used when clock showing less than 0 minutes 
		if (minutes < 0) {
			stop = true;
			minutes = 0;
			seconds = 0;
		}
	}

	private IEnumerator updateCoroutine ()
	{
		while (!stop) {
			text.text = string.Format ("{0:0}:{1:00}", minutes, seconds);
			yield return new WaitForSeconds (0.0f);

			//Notifying User about 30 second Left in the clock
			if (minutes <= 0 && seconds <= 30) {
				TimerBlinker ();
				yield return new WaitForSeconds (0.1f);
			}
			if (minutes <= 0 && seconds <= 0 && GameManager.TotalPointsOrScores >= GameManager.TotalScoresToBeAchieved) {
				//Winning Condition
				screenManagerComponent.UnlockingOfNextLevel ();
				screenManagerComponent.LevelClearScreen ();
			} else if (minutes <= 0 && seconds <= 0 && GameManager.TotalPointsOrScores < GameManager.TotalScoresToBeAchieved) {
				screenManagerComponent.LevelOverScreen ();
			}
		}
	}

	private void TimerBlinker ()
	{
		text.color = Color.red;

//		text.enabled = !text.enabled; 
	}
}