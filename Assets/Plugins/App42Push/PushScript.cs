using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.pushNotification;
using System;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp.user;

public class PushScript : MonoBehaviour
{
//	const string api_key = "4f12bbd8cb82fae4390afc6812ed5b13e3c32b70aab936fce559eaa37da1e111";
//	const string secret_key = "9a6004b76dff695d15f14daf212e381a870e7b98f35965370dc2375373c04f0f";
//
//	PushResponse callBack = new PushResponse ();
//
//	[System.Runtime.InteropServices.DllImport ("__Internal")]
//	extern static public void registerForRemoteNotifications ();
//
//	[System.Runtime.InteropServices.DllImport ("__Internal")]
//	extern static public void setListenerGameObject (string listenerName);
//	
//	// Use this for initialization
//	void Start ()
//	{
//		DontDestroyOnLoad (this);
//		App42Log.SetDebug (true);
//		App42API.Initialize (api_key, secret_key); 
////		Debug.Log ("Start called -----" + this.gameObject.name);
////		#if !UNITY_EDITOR
//		setListenerGameObject (this.gameObject.name);// sets the name of the game object as a listener to which this script is assigned.
//		//		#endif
//
//	}
//
//    public static void CreateNewUserOnRegistration (string userName, string pwd, string emailId)
//	{
//		UserService user = App42API.BuildUserService ();  
//		user.CreateUser (userName, pwd, emailId, new UnityCallBack ()); 
////		Debug.Log ("Create new user on Registration of Push Services");
//	}
//	
//	//Sent when the application successfully registered with Apple Push Notification Service (APNS).
//	void onDidRegisterForRemoteNotificationsWithDeviceToken (string deviceToken)
//	{
//		Debug.Log ("deviceToken" + deviceToken);
//		if (deviceToken != null && deviceToken.Length != 0) {
////			Debug.Log ("Register device into App42 Push Notifications");
//			registerDeviceTokenToApp42PushNotificationService (deviceToken, PlayerPrefs.GetString ("UserName"));
//		}           
//	}
//	
//	//Sent when the application failed to be registered with Apple Push Notification Service (APNS).
//	void onDidFailToRegisterForRemoteNotificcallBackationsWithError (string error)
//	{
////		Debug.Log ("failed to register -->>>>" + error);
//	}
//	
//	//Sent when the application Receives a push notification
//	void onPushNotificationsReceived (string pushMessageString)
//	{
////		Console.WriteLine ("onPushNotificationsReceived....Called");
//		var JsonString = Simple_JSON.JSON.Parse (pushMessageString);
//
//		_MessageString = JsonString ["aps"] ["alert"].ToString ().Trim ('\"');
//
//		Invoke ("OpenScreenAccordingToMessage", 1.0f);	
//	}
//
//	string _MessageString = "";
//
//	
//	//Registers a user with the given device token to APP42 push notification service
//	void registerDeviceTokenToApp42PushNotificationService (string devToken, string userName)
//	{
////		Debug.Log ("registerDeviceTokenToApp42PushNotificationService   Called");
//		ServiceAPI serviceAPI = new ServiceAPI (api_key, secret_key);	
//		PushNotificationService pushService = serviceAPI.BuildPushNotificationService ();
//		pushService.StoreDeviceToken (userName, devToken, "iOS", callBack);
//	}
//
//		
//	//	Sends push to a given user
//	public void SendPushToUser (string userName, string message)
//	{
////		Debug.Log ("SendPushToUser Called");
//		ServiceAPI serviceAPI = new ServiceAPI (api_key, secret_key);
//		PushNotificationService pushService = serviceAPI.BuildPushNotificationService ();
//		pushService.SendPushMessageToUser (userName, message, callBack);
//	
//	}
//
//	void onDidRegisterUserNotificationSettings (string setting)
//	{
//		Debug.Log ("setting" + setting);
//	}
}

//public class UnityCallBack : App42CallBack
//{
//	public void OnSuccess (object response)
//	{  
//		User user = (User)response;  
//		App42Log.Console ("userName is " + user.GetUserName ()); 
//		App42Log.Console ("emailId is " + user.GetEmail ());
//	}
//
//	public void OnException (Exception e)
//	{  
//		App42Log.Console ("Exception : " + e);  
//	}
//}
