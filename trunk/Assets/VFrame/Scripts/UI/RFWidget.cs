using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RFWidget : MonoBehaviour {
    public enum EType {
        //system component
        Transform = 0,
        RectTransform,

        //ugui component
        Text = 100,
        RawImage,
        Image,
        Button,
        Toggle,
        InputField,
        Slider,
        Scrollbar,
        ScrollRect,
        GridLayoutGroup,
        LocalizeText,
        LocalizeImage,
        LocalizeRawImage,
        ToggleGroup,
        LoopListView2,
        LoopGridView,

        ParticleSystem,
    }

    public EType m_type;
    public bool m_defaultActived = true;

    void Awake () {

        gameObject.SetActive (m_defaultActived);
    }

}
