using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Text))]
public class LocalizeText : RFLocalize {

    public string text {
        get { return GetComponent<Text> ().text; }
        set { GetComponent<Text> ().text = value; }
    }

    public override void UpdateContent () {
        if (m_key != "")
            this.GetComponent<Text> ().text = GetLocalText (m_key, m_paramlist);
    }

}
