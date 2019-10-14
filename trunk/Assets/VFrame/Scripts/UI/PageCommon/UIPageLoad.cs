using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using VFrame;

public class UIPageLoad : RFPage {

    public enum NWwidget {
        Progress,
        Tips,
    }

    AsyncOperation m_async;
    float m_loadProgress;

    // Use this for initialization
    public override void Init (Type et) {
        base.Init (typeof (NWwidget));
    }

    void Start () {
        StartCoroutine (loadScene ());
    }

    void Update () {
        GetWidget<Slider> (NWwidget.Progress).value = m_loadProgress;
        //		Debug.Log("pb: " + m_async.progress);
    }

    void OnEnable () {
    }

    void OnDisable () {
    }

    IEnumerator loadScene () {
        m_async = SceneManager.LoadSceneAsync (RFManager.CurScene.ToString ());
        m_async.allowSceneActivation = false;
        while (!m_async.isDone && m_async.progress < 0.9f) {
            m_loadProgress = m_async.progress;
            //			Debug.Log(m_loadProgress);
            if (m_async.progress >= 0.9f) {
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds (1);
        m_loadProgress = 1;
        yield return new WaitForSeconds (0.1f);
        m_async.allowSceneActivation = true;
        yield return m_async;
    }

}
