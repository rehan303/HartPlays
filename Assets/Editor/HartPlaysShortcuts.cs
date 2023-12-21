using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

public class HartPlaysShortcuts : MonoBehaviour
{
	///<summary>
	/// This is Genric Scriptable class for Creating any kind of scriptableObjects
	///</summary>
	public static T CreateScriptacbleObject<T>(string path) where T: ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance <T> ();
		string assetpath;
		if (AssetDatabase.IsValidFolder (path+"/"+asset.GetType ()))
		{
			assetpath = path+"/"+asset.GetType ()+"/"+asset.GetType ()+".asset";
		}
		else
		{
			string Guid  =AssetDatabase.CreateFolder (path, asset.GetType ().ToString ());
			assetpath = AssetDatabase.GUIDToAssetPath (Guid)+ "/"+asset.GetType ()+".asset";
		}

		AssetDatabase.CreateAsset (asset, assetpath);
		AssetDatabase.SaveAssets ();

		EditorUtility.FocusProjectWindow ();

		Selection.activeObject = asset;

		return asset;
	}


//	[MenuItem("Assets/Create/ScriptableObjects/MusicSettings")]
//	public static void CreatMusicSetting()
//	{
//		HartPlaysShortcuts.CreateScriptacbleObject<GameVolumeSettings> ("Assets");
//	}

	[MenuItem("HartPlays/Scenes/OpenLoginScene %F1")]
	public static void OpenLoginScene()
	{
		EditorSceneManager.OpenScene ("Assets/Scenes/ColorBallHartPlayer/02_LoginScene.unity");
	}

	[MenuItem("HartPlays/Scenes/Open_MainMenuScene %F2")]
	public static void Open_MenuScene()
	{
		EditorSceneManager.OpenScene ("Assets/Scenes/ColorBallHartPlayer/00_MenuScreen.unity");
//		SceneManager.LoadScene ("00_MenuScreen");
	}

	[MenuItem("HartPlays/Scenes/OpenColorBallScene %F3")]
	public static void OpenColorBallScene()
	{
		EditorSceneManager.OpenScene ("Assets/Scenes/ColorBallHartPlayer/01_MainGamePlay.unity");
	}


	[MenuItem("HartPlays/Scenes/OpenDogerScene %F4")]
	public static void OpenDogerScene()
	{
		EditorSceneManager.OpenScene ("Assets/Scenes/DodgerGame/03_DogerGamePlay.unity");
	}

	[MenuItem("HartPlays/Scenes/OpenNinjaLevelSelectionScene %F6")]
	public static void OpenNinjaLevelScene()
	{
		EditorSceneManager.OpenScene ("Assets/Scenes/NinjaFootieGame/05_NinjaLevelSelectionScene.unity");
	}

	[MenuItem("HartPlays/Scenes/OpenNinjaFootieScene %F7")]
	public static void OpenNinjaScene()
	{
		EditorSceneManager.OpenScene ("Assets/Scenes/NinjaFootieGame/04_NinjaFootie.unity");
	}
}
