using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Notifications : MonoBehaviour {

	public Notification notification;
	// Use this for initialization
	public Text Name;
	public Text msg;
	void Start () {
	
	}
	
	public void SetDataToThisNotification(Notification thisnotifction)
	{
		notification = thisnotifction;
		var tt = notification.Msg;
		string t = "";
		if(tt.Length >1){
			t = tt.Remove (1);
			Name.text = t;
		}else
			Name.text = tt;	

		msg.text = notification.Msg;
	}
}
