using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

namespace VFrame {
    public class RFManager : MonoBehaviour {

        public static RFManager Ins;

        public static NScene CurScene;
        public static NScene PreScene;
        public static string Command;

        public AudioClip m_audio;
        public float m_audioDelay = 0;

        protected Transform m_trans;
        protected Transform m_uiTrans;
        protected Transform m_ui3DTrans;
        Dictionary<string, NPage> _dicEnum = new Dictionary<string, NPage>();
        protected Dictionary<NPage, RFPage> m_pages = new Dictionary<NPage, RFPage>();

        //some Action must execute in main thread.
        private Queue<Action> _actions = new Queue<Action>();

        protected virtual void Awake() {
            Ins = this;
            m_trans = transform;
            if (GameObject.Find("Canvas") != null)
                m_uiTrans = GameObject.Find("Canvas").transform;
            if (GameObject.Find("Canvas3D") != null)
                m_ui3DTrans = GameObject.Find("Canvas3D").transform;
            Time.timeScale = 1;

            string[] s = Enum.GetNames(typeof(NPage));
            for (int i = 0; i < s.Length; ++i) {
                _dicEnum.Add(s[i], (NPage)i);
            }
            foreach (RFPage child in GetComponentsInChildren<RFPage>(true)) {
                m_pages[_dicEnum[child.name]] = child;
                child.Init(null);
                //child.gameObject.SetActive (true);
            }

            foreach (RFPage page in m_pages.Values) {
                page.transform.SetSiblingIndex(page.m_showLayer);
                //Debug.Log(page.name+curPage.transform.GetSiblingIndex ());
            }
        }

        protected virtual void Start() {
            if (RFManager.Command != null) {
                DoCommand(RFManager.Command);
            }

            if (m_audio != null)
                this.StartCoroutine(musicWait(m_audioDelay));
        }

        protected virtual void Update() {
            lock (_actions) {
                while (_actions.Count > 0) {
                    _actions.Dequeue()();
                }
            }
        }

        protected virtual void OnApplicationFocus(bool focusStatus) {
            //		Debug.Log("Phone Focus State: "+focusStatus.ToString());
        }

        protected virtual void OnApplicationPause(bool pauseStatus) {
            //		Debug.Log("Phone Pause State: "+pauseStatus.ToString());
        }

        public virtual void DoCommand(string command, params object[] param) {
            switch (command) {
                case "ChangeLanguage":
                    foreach (RFLocalize child in GetComponentsInChildren(typeof(RFLocalize), true)) {
                        child.UpdateContent();
                    }
                    break;
            }
        }

        public bool IsPageActive(NPage page) {
            if (m_pages.ContainsKey(page)) {
                return m_pages[page].gameObject.activeSelf;
            }
            else {
                return false;
            }
        }

        public NPage GetPageEnmu(string name) {
            return _dicEnum[name];
        }

        public void UpdatePage(string page, UIMSG cmd, params object[] param) {
            //if (_dicEnum.ContainsKey (page))
            updatePage(_dicEnum[page], cmd, param);
        }

        public void UpdatePage(NPage page, UIMSG cmd, params object[] param) {
            updatePage(page, cmd, param);
        }

        private void updatePage(NPage page, UIMSG cmd, params object[] param) {
            if (m_pages.ContainsKey(page)) {
                if (m_pages[page] != null) {
                    m_pages[page].UpdateUI(cmd, param);
                }
            }
            else {
                GameObject obj = Resources.Load("Prefab/UI/Page/" + page) as GameObject;
                    Debug.Log(CurScene);
                if (obj != null) {
                    RFPage objC = obj.GetComponent<RFPage>();
                    if ((int)objC.m_sceneMask == -1 || (objC.m_sceneMask & CurScene) > 0) {
                        m_pages[page] = ((GameObject)Instantiate(obj)).GetComponent<RFPage>();
                        m_pages[page].gameObject.name = page.ToString();
                        m_pages[page].Init(null);

                        RectTransform rt = m_pages[page].GetComponent<RectTransform>();
                        if (m_uiTrans != null) {
                            rt.SetParent(m_uiTrans);
                            rt.anchorMin = Vector2.zero;
                            rt.anchorMax = Vector2.one;
                            rt.sizeDelta = Vector2.zero;
                            rt.localPosition = Vector3.zero;
                            rt.localScale = Vector3.one;

                            m_pages[page].UpdateUI(cmd, param);
                        }
                    }
                }
            }
        }

        public void EnablePage(RFPage curPage) {
            curPage.gameObject.SetActive(true);
            //foreach (RFPage page in m_pages.Values) {
            //    Debug.Log (page.name + curPage.transform.GetSiblingIndex ());
            //}
            //curPage.transform.SetAsLastSibling ();
            if (curPage.m_showType == RFPage.ShowType.Exclude) {
                foreach (RFPage page in m_pages.Values) {
                    if (page.gameObject.activeSelf
                        && curPage != page
                        && page.m_showType == RFPage.ShowType.Exclude
                        && curPage.m_showPrior >= page.m_showPrior) {
                        page.UpdateUI(UIMSG.Disable);
                        //page.gameObject.SetActive (false);
                    }
                }
            }
        }

        public T GetWidget<T>(NPage page, string widgetName) {
            if (m_pages.ContainsKey(page)) {
                return (T)m_pages[page].GetWidget<T>(widgetName);
            }
            else {
                return default(T);
            }
        }

        //	public void AddPage (string name, RFPage page) {
        //		m_pages[name] = page;
        //	}

        public GameObject GetPage(NPage page) {
            if (m_pages.ContainsKey(page)) {
                return m_pages[page].gameObject;
            }
            return null;
        }

        public void LoadScene(NScene scene, string cmd = null) {
            RFManager.PreScene = RFManager.CurScene;
            RFManager.CurScene = scene;
            RFManager.Command = cmd;
            SceneManager.LoadScene(NScene.Load.ToString());
        }

        IEnumerator musicWait(float t) {
            yield return new WaitForSeconds(t);
            ManagerSound.Ins.PlayMusic(m_audio);
        }

        public void PushAction(Action act) {
            lock (_actions) {
                _actions.Enqueue(act);
            }
        }
    }
}