using UnityEngine;
using System.Collections;

///<summary>
/// This is Scriptable class for storing and creating GameVolumeSettings,
///To get asset of this ScriptableObject do this--->>
///GameVolumeSettings gameVolumeSettings = AssetDatabase.LoadAssetAtPath<GameVolumeSettings> ("Assets/ScriptableObjects/GameVolumeSettings/GameVolumeSettings.asset");
///</summary>
public class GameVolumeSettings : ScriptableObject 
{
//	[SerializeField]
//	Color color;
	[SerializeField]
	bool isMusicOn = false;

	[SerializeField]
	bool isSfxOn = false;

	[SerializeField][Range(0,1)]
	float musicVolume = 0;

	[SerializeField][Range(0,1)]
	float sfxVolume = 0;

	public bool MusicState 
	{
		get
		{ 
			return isMusicOn;
		}
		set
		{
			isMusicOn = value;
		}
	}

	public bool SfxState 
	{
		get
		{ 
			return isSfxOn;
		}
		set
		{
			isSfxOn = value;
		}
	}

	public float MusicVolume
	{ 
		get
		{ 
			if (isMusicOn)
				return musicVolume;
			else
				return 0f;
		}
		set
		{
			musicVolume = value;
		}
	}

	public float SFXVolume
	{
		get
		{ 
			if (isSfxOn)
				return sfxVolume;
			else
				return 0f;
		}
		set
		{
			sfxVolume = value;
		}
	}
}
