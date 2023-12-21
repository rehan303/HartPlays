/***
 * This script will anchor a GameObject to a relative screen position.
 * This script is intended to be used with CameraFit.cs by Marcel Căşvan, available here: http://gamedev.stackexchange.com/a/89973/50623
 * 
 * Note: For performance reasons it's currently assumed that the game resolution will not change after the game starts.
 * You could not make this assumption by periodically calling UpdateAnchor() in the Update() function or a coroutine, but is left as an exercise to the reader.
 */
/* The MIT License (MIT)

Copyright (c) 2015, Eliot Lash

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraAnchor : MonoBehaviour
{
	public enum AnchorType
	{
		TopLeft,
		TopCenter,
		TopRight,

		BottomLeft,
		BottomCenter,
		BottomRight,

		LeftTop,
		LeftCenter,
		LeftBottom,

		RightTop,
		RightCenter,
		RightBottom,

	};

	public AnchorType anchorType;
	public Vector3 anchorOffset;

	IEnumerator updateAnchorRoutine;
	//Coroutine handle so we don't start it if it's already running

	// Use this for initialization
	void Start ()
	{
		updateAnchorRoutine = UpdateAnchorAsync ();
		StartCoroutine (updateAnchorRoutine);
	}

	/// <summary>
	/// Coroutine to update the anchor only once CameraFit.Instance is not null.
	/// </summary>
	IEnumerator UpdateAnchorAsync ()
	{
		uint cameraWaitCycles = 0;
		while (CameraFit.Instance == null) {
			++cameraWaitCycles;
			yield return new WaitForEndOfFrame ();
		}
		if (cameraWaitCycles > 0) {
//			print(string.Format("CameraAnchor found CameraFit instance after waiting {0} frame(s). You might want to check that CameraFit has an earlie execution order.", cameraWaitCycles));
		}
		UpdateAnchor ();
		updateAnchorRoutine = null;
	}

	void UpdateAnchor ()
	{
		switch (anchorType) {
		case AnchorType.TopLeft:
			SetAnchor (CameraFit.Instance.TopLeft);
			break;
		case AnchorType.TopCenter:
			SetAnchor (CameraFit.Instance.TopCenter);
			break;
		case AnchorType.TopRight:
			SetAnchor (CameraFit.Instance.TopRight);
			break;
		case AnchorType.BottomLeft:
			SetAnchor (CameraFit.Instance.BottomLeft);
			break;
		case AnchorType.BottomCenter:
			SetAnchor (CameraFit.Instance.BottomCenter);
			break;
		case AnchorType.BottomRight:
			SetAnchor (CameraFit.Instance.BottomRight);
			break;
		case AnchorType.LeftTop:
			SetAnchor (CameraFit.Instance.LeftTop);
			break;
		case AnchorType.LeftCenter:
			SetAnchor (CameraFit.Instance.LeftCenter);
			break;
		case AnchorType.LeftBottom:
			SetAnchor (CameraFit.Instance.LeftBottom);
			break;
		case AnchorType.RightTop:
			SetAnchor (CameraFit.Instance.RightTop);
			break;
		case AnchorType.RightCenter:
			SetAnchor (CameraFit.Instance.RightCenter);
			break;
		case AnchorType.RightBottom:
			SetAnchor (CameraFit.Instance.RightBottom);
			break;
	
		}
	}

	void SetAnchor (Vector3 anchor)
	{
		Vector3 newPos = anchor + anchorOffset;
		if (!transform.position.Equals (newPos)) {
			transform.position = newPos;
		}
	}

	//	#if UNITY_EDITOR
	// Update is called once per frame
	void Update ()
	{
		if (updateAnchorRoutine == null) {
			updateAnchorRoutine = UpdateAnchorAsync ();
			StartCoroutine (updateAnchorRoutine);
		}
	}
	//	#endif
}
