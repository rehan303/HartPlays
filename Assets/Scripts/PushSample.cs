using UnityEngine;
using System.Collections;
using System.IO;

using System;
using System.Net;
using AssemblyCSharpfirstpass;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.pushNotification;
using com.shephertz.app42.paas.sdk.csharp.user;

public class PushSample: MonoBehaviour
{
	public const string ApiKey ="d9e862713a27d070f5d64c18b1e7f923f3f5b5a57e3e44d675be000c0d4edfc8";
	public const string SecretKey="322aa109899b5d18e8115eeb8b10785e350de3afc8207249b0c9a640aabb481e";
	public const string GoogleProjectNo="";

//    PushResponse callBack = new PushResponse ();

    [System.Runtime.InteropServices.DllImport ("__Internal")]
    extern static public void registerForRemoteNotifications ();
//
    [System.Runtime.InteropServices.DllImport ("__Internal")]
    extern static public void setListenerGameObject (string listenerName);

	void Start (){
		DontDestroyOnLoad (transform.gameObject);
		App42Log.SetDebug (true);
		App42API.Initialize(ApiKey,SecretKey);

//		App42API.SetLoggedInUser(UserId);

        setListenerGameObject (this.gameObject.name);
		print ("setListenerGameObject Called on-" + gameObject.name);

		//Put Your Game Object Here
//		App42Push.setApp42PushListener (this);
		#if UNITY_ANDROID
		App42Push.registerForPush (GoogleProjectNo);
		message=App42Push.getLastPushMessage();
		#endif 
	}

    /// <summary>
    /// Creates the new user on registration. To be called at registeration time
    /// </summary>
    /// <param name="userName">User name.</param>
    /// <param name="pwd">Pwd.</param>
    /// <param name="emailId">Email identifier.</param>
    public static void CreateNewUserOnRegistration (string userName, string pwd, string emailId)
    {
		print (string.Format ("Create user called with {0}, {1}, {2}", userName, pwd, emailId));
        UserService user = App42API.BuildUserService ();  
        user.CreateUser (userName, pwd, emailId, new UnityCallBack ()); 
    }

    void onDidRegisterUserNotificationSettings (string setting)
    {
        Debug.Log ("setting" + setting);
    }

    #region Native_Callbacks
	public void onDidRegisterForRemoteNotificationsWithDeviceToken(String deviceToken){
		print ("Device token from native: "+deviceToken);
		String deviceType = "";
		#if UNITY_IPHONE
		deviceType = "iOS";
		#elif UNITY_ANDROID
		deviceType = "Android";
		#endif



		if(deviceType!=null&&deviceToken!=null&& deviceToken.Length!=0)
			App42API.BuildPushNotificationService().StoreDeviceToken(PlayerPrefs.GetString("UserName"),deviceToken,
                                                    deviceType,new Callback());
    }

	public void onPushNotificationsReceived(String pushMessageString){
        print("Message From native: "+pushMessageString);	
        //      Console.WriteLine ("onPushNotificationsReceived....Called");
        var JsonString = Simple_JSON.JSON.Parse (pushMessageString);

        var _MessageString = JsonString ["aps"] ["alert"].ToString ().Trim ('\"');

		StartCoroutine (GameSaveState.Instance.GetUserStatus(false));
	}
		
    public void onError(String error){
        print("Error From native: "+error);
		
	}
	public static void sendPushToUser(string userName,string msg){
		
		App42API.BuildPushNotificationService().SendPushMessageToUser(userName,msg,new Callback());		
	}
	public void sendPushToAll(string msg){
		
		App42API.BuildPushNotificationService().SendPushMessageToAll(msg,new Callback());		
	}

	void onDidFailToRegisterForRemoteNotificationsWithError (string error)
	{
		Debug.Log ("Failed to register -->>>>" + error);
	}
	
    #endregion
}

public class UnityCallBack : App42CallBack
{
    public void OnSuccess (object response)
    {  
        User user = (User)response;  
        App42Log.Console ("userName is " + user.GetUserName ()); 
        App42Log.Console ("emailId is " + user.GetEmail ());
    }

    public void OnException (Exception e)
    {  
        App42Log.Console ("Exception : " + e);  
    }
}

public class PushResponse :  App42CallBack {

    public void OnSuccess(object response)
    {
        if(response is PushNotification)
        {
            PushNotification pushNotification = (PushNotification)response;
            Debug.Log ("UserName : " + pushNotification.GetUserName()); 
            Debug.Log ("Expiery : " + pushNotification.GetExpiry());
            Debug.Log ("DeviceToken : " + pushNotification.GetDeviceToken());   
            Debug.Log ("pushNotification : " + pushNotification.GetMessage());  
            Debug.Log ("pushNotification : " + pushNotification.GetStrResponse());  
            Debug.Log ("pushNotification : " + pushNotification.GetTotalRecords()); 
            Debug.Log ("pushNotification : " + pushNotification.GetType()); 
            Debug.Log ("pushNotification : " + pushNotification.GetMessage ()); 
            //              for(int i = 0 ; i < pushNotification.GetChannelList)
            //              Debug.Log ("pushNotification : " + pushNotification.GetChannelList()[0].GetName()); 
            //              Debug.Log ("pushNotification : " + pushNotification.GetChannelList()[0].GetName()); 
            //              Debug.Log ("pushNotification : " + pushNotification.GetChannelList()[0].GetType()); 
        }
    }

    public void OnException(Exception e)
    {
        Debug.Log ("Exception--------- : " + e);
    }
}