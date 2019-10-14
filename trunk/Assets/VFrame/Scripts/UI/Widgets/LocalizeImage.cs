using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LocalizeImage : RFLocalize {

    public Sprite sprite {
        get { return GetComponent<Image> ().sprite; }
        set { GetComponent<Image> ().sprite = value; }
    }

    public override void UpdateContent () {
        if (m_key != "")
            this.GetComponent<Image> ().sprite = Resources.Load<SpriteRenderer> ("UI/Localize/Sprite/" + GetLocalText (m_key)).sprite;
    }

}
