using System;
using UnityEngine;
using UnityEngine.UI;
using VFrame;

public class PageSample : RFPage {

    enum Widgets {
        Tips,
        Button,
    }

    public override void Init(Type et) {
        base.Init(typeof(Widgets));

        //bind button event
        GetWidget<Button>(Widgets.Button).onClick.AddListener(
            delegate () {
                OnBtTest(GetWidget<Button>(Widgets.Button).gameObject);
            });
    }

    public void OnBtTest(GameObject sender) {
        Debug.Log(sender.name);
        GetWidget<Text>(Widgets.Tips).text = "Changed";
        //ManagerSound.Ins.PlaySound("error");
        //ManagerSound.Ins.PlayMusic("error");
        //RFLocalize.SetLanguage (LocalizationType.English);
    }
}
