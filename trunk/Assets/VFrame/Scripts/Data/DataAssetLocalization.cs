using UnityEngine;
using System.Collections;

[System.Serializable]
public class LocalizationItem {
	public string key;
	public string[] contents;
}

[System.Serializable]
public class DataAssetLocalization : ScriptableObject {
	public LocalizationItem[] items;
}
