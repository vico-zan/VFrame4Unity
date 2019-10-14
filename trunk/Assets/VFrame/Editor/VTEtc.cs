using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System.Collections;

namespace VioTools {

public class VTEtc : EditorWindow {

		[MenuItem("GameObject/UI/Localize Text")]
		public static void CreateLocalizeText() {
			GameObject obj = new GameObject ();
			obj.name = "TextL";
			obj.AddComponent<RectTransform>();
			obj.AddComponent<Text>();
			obj.GetComponent<Text> ().text = "Localize Text";
			obj.AddComponent<LocalizeText> ();
			obj.transform.parent = Selection.activeGameObject.transform;
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localScale = Vector3.one;
		}

		[MenuItem("GameObject/UI/Localize Image")]
		public static void CreateLocalizeImage() {
			GameObject obj = new GameObject ();
			obj.name = "ImageL";
			obj.AddComponent<RectTransform>();
			obj.AddComponent<Image>();
			obj.AddComponent<LocalizeImage> ();
			obj.transform.parent = Selection.activeGameObject.transform;
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localScale = Vector3.one;
		}

		[MenuItem("GameObject/UI/Localize RawImage")]
		public static void CreateLocalizeRawImage() {
			GameObject obj = new GameObject ();
			obj.name = "RawImageL";
			obj.AddComponent<RectTransform>();
			obj.AddComponent<RawImage>();
			obj.AddComponent<LocalizeRawImage> ();
			obj.transform.parent = Selection.activeGameObject.transform;
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localScale = Vector3.one;
		}
}

}