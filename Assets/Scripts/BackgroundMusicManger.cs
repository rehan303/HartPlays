using UnityEngine;
using System.Collections;

public class BackgroundMusicManger : MonoBehaviour
{

	public static BackgroundMusicManger instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	public bool isSfxOn = true;
	public bool isMusicOn = true;
	public AudioClip MainMenuClip;
	public AudioClip ColorBallClip;
	public AudioClip DogerClip;
	public AudioClip NinjaFootieClip;


	AudioSource ThisAuioSource;                     

	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		} 
		else if (instance != this) 
		{
			Destroy (gameObject);
		}

		ThisAuioSource = GetComponent <AudioSource>();
		DontDestroyOnLoad (gameObject);

		if (!PlayerPrefs.HasKey ("isMusicOn"))
			PlayerPrefs.SetInt ("isMusicOn", 0);

		if (!PlayerPrefs.HasKey ("isSfxOn"))
			PlayerPrefs.SetInt ("isSfxOn", 0);
		
		if (PlayerPrefs.GetInt ("isMusicOn") == 1)
			isMusicOn = false;
		else
			isMusicOn = true;
		
		if (PlayerPrefs.GetInt ("isSfxOn") == 1)
			isSfxOn = false;
		else
			isSfxOn = true;
	}

	void OnLevelWasLoaded (int levelNo)
	{
		switch(levelNo)
		{
		case 0:
			if (ThisAuioSource.clip != MainMenuClip) 
			{
				ThisAuioSource.clip = MainMenuClip;
				ThisAuioSource.Play ();
			}
			break;
		case 1:
			if (ThisAuioSource.clip != MainMenuClip) 
			{
				ThisAuioSource.clip = MainMenuClip;
				ThisAuioSource.Play ();
			}
			break;
		case 2:
			if (ThisAuioSource.clip != ColorBallClip) 
			{
				ThisAuioSource.clip = ColorBallClip;
				ThisAuioSource.Play ();
			}	

			break;
		case 3:
			if (ThisAuioSource.clip != DogerClip) {
				ThisAuioSource.clip = DogerClip;
				ThisAuioSource.Play ();
			}
			break;
		case 4:
			if (ThisAuioSource.clip != NinjaFootieClip) {
				ThisAuioSource.clip = NinjaFootieClip;
				ThisAuioSource.Play ();
			}
			break;
		case 5:
			if (ThisAuioSource.clip != MainMenuClip) {
				ThisAuioSource.clip = MainMenuClip;
				ThisAuioSource.Play ();
			}
			break;
		default:
			
			break;

		}
	}
	void Update()
	{
		ThisAuioSource.mute = !isMusicOn;
	}

	public void PlaySoundEffect(AudioSource audioSource, AudioClip audioClip)
	{
		if(isSfxOn)
		{
			audioSource.PlayOneShot (audioClip);
		}
	}
	public void PlaySound(AudioSource _audioSource)
	{
		if(isSfxOn)
		{
			_audioSource.Play ();
		}
	}
}
