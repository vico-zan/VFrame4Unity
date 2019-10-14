using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using VFrame;

public enum LocalizationType {
    ChineseS = 1,
    English,

    MAX,
}

public class RFLocalize : MonoBehaviour {
    //localazition
    protected static LocalizationType language = LocalizationType.English;
    protected static Dictionary<string, string> m_localizationStrings = new Dictionary<string, string>();

    public string m_key;
    protected object[] m_paramlist;

    void Awake() {
        if (m_localizationStrings.Count == 0) {
            reloadLocalizationStrings();
        }

        UpdateContent();
    }

    static void reloadLocalizationStrings() {
        m_localizationStrings.Clear();
        DataAssetLocalization items = Resources.Load<DataAssetLocalization>("Table/DataLocalization");
        if (items == null) return;
        foreach (LocalizationItem item in items.items) {
            m_localizationStrings.Add(item.key, item.contents[(int)language - 1]);
        }
    }

    public void SetContent(string key, params object[] paramlist) {
        m_key = key;
        m_paramlist = paramlist;

        UpdateContent();
    }

    public virtual void UpdateContent() { }

    public static void SetLanguage(LocalizationType lan) {
        if (language != lan) {
            language = lan;
            reloadLocalizationStrings();
            RFManager.Ins.DoCommand("ChangeLanguage");
        }
    }

    public static void SetLanguageByOrder(bool positive) {
        if (positive) {
            if ((int)language + 1 < (int)LocalizationType.MAX)
                SetLanguage((LocalizationType)((int)language + 1));
            else
                SetLanguage((LocalizationType)1);
        }
        else {
            if ((int)language - 1 > 0)
                SetLanguage((LocalizationType)((int)language - 1));
            else
                SetLanguage((LocalizationType)((int)LocalizationType.MAX - 1));
        }
    }

    public static string GetLocalText(string key, params object[] paramlist) {
        if (m_localizationStrings.Count == 0) {
            reloadLocalizationStrings();
        }

        try {
            //if (!m_localizationStrings.ContainsKey (key)) {
            //    return "Error:" + key;
            //}

            string str = m_localizationStrings[key];
            if (paramlist != null) {
                for (int i = 1; i <= paramlist.Length; i++) {
                    str = str.Replace("&" + i, paramlist[i - 1].ToString());
                }
            }
            return str;
        }
        catch (System.Exception exp) {
            Debug.LogError(key + " not exite");
            throw exp;
        }
    }

}
