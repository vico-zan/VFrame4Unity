using UnityEngine;
using System.Collections;

public class ManagerDebug : MonoBehaviour {

	public static ManagerDebug Ins;

	private string content;
	//private string debugCommand = "";
	private bool showDebug;

	//private string ipAddress = "";
	private bool showEnter;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this);
		Ins = this;

//		ipAddress = PlayerPrefs.GetString("IP");
//		if (ipAddress == null)
//			ipAddress = "";

	}
	
	Vector2 scrollPosition = Vector2.zero;

	void OnGUI () {
//		if (!UserData.DebugMode) return;
//		
//		if (GUI.Button(new Rect (0, 30, 100, 30), "Debug")) {
//			showDebug = !showDebug;
//		}
//		if (showDebug) {
//			debugCommand = GUI.TextField (new Rect (0, 0, 200, 30), debugCommand, 10000);
//			if (GUI.Button(new Rect (220, 0, 100, 30), "send")) {
////				ManagerNet.GetIns().Send_DebugCommand(ManagerNet.GetIns().createXML());
//			}
//			
//			GUILayout.BeginHorizontal(GUI.skin.box);
//			scrollPosition = GUILayout.BeginScrollView(scrollPosition);
//			GUILayout.Label(content);
//			GUILayout.EndScrollView();
//			GUILayout.EndHorizontal();
//		}
//
//		if (GUI.Button(new Rect (100, 30, 100, 30), "Login")) {
//			ManagerNet.GetIns().Connect();
//		}
//		if (showEnter) {
//			ipAddress = GUI.TextField (new Rect (0, 0, 200, 30), ipAddress, 100);
//			if (GUI.Button(new Rect (220, 0, 100, 30), "Enter")) {
//				if (ipAddress != null) {
//					UserData.IPLogin = ipAddress;
//					PlayerPrefs.SetString("IP", UserData.IPLogin);
//					ManagerNet.GetIns().Login();
//				}
//			}
//		}
	}

	public void WebLog (string text) {
		content += "\n"+text;
		Debug.Log(text);
	}
}
