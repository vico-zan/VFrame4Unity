using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LocalizeRawImage : RFLocalize {

    public Texture texture {
        get { return GetComponent<RawImage> ().texture; }
        set { GetComponent<RawImage> ().texture = value; }
    }

    public override void UpdateContent () {
        if (m_key != "")
            this.GetComponent<RawImage> ().texture = Resources.Load<Texture> ("UI/Localize/RawImage/" + GetLocalText (m_key));
    }

}
