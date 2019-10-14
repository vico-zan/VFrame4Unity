using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace VFrame {
    public abstract class RFPage : MonoBehaviour {
        public enum ShowType {
            Exclude,
            Coexist,
        }

        public enum TransitionType {
            Instanst,
            Alpha,
            Animator,
        }

        public class Command {
            public Command(UIMSG cmd, params object[] p) {
                command = cmd;
                param = p;
            }
            public UIMSG command;
            public object[] param;
        }


        [HideInInspector]
        public NScene m_sceneMask;
        [HideInInspector]
        public ShowType m_showType;
        [HideInInspector]
        public bool m_enableStack;
        [HideInInspector]
        public int m_showPrior;
        [HideInInspector]
        public int m_showLayer;
        [HideInInspector]
        public TransitionType m_tEnable;
        [HideInInspector]
        public TransitionType m_tDisable;

        protected NPage _pEnum;
        protected Transform m_transform;
        protected Animator m_ani;
        protected CanvasGroup m_panel;
        protected Dictionary<int, object> m_widgets = new Dictionary<int, object>();
        protected bool m_aniRunning;
        protected Queue<Command> m_cmds = new Queue<Command>();
        protected Command curCmd;

        protected bool _notchTop;
        protected bool _notchBottom;

        protected virtual void Awake() {
            if (Screen.height / (float)Screen.width > 2f) {
                float ch = Screen.height * GameUtility.CanvasWidth / (float)Screen.width;
                float h = ch * 88 / 2436f;
                if (_notchBottom)
                    ((RectTransform)transform).anchorMin = new Vector2(0, h / ch);
                if (_notchTop)
                    ((RectTransform)transform).anchorMax = new Vector2(1, 1 - h / ch);
            }
        }

        public virtual void Init(Type et) {
            _pEnum = RFManager.Ins.GetPageEnmu(this.name);
            m_transform = transform;
            m_ani = this.GetComponent<Animator>();
            m_panel = this.GetComponent<CanvasGroup>();

            if (et != null) {
                Dictionary<string, int> dic = new Dictionary<string, int>();
                string[] s = Enum.GetNames(et);
                for (int i = 0; i < s.Length; ++i) {
                    dic.Add(s[i], i);
                }

                foreach (RFWidget child in m_transform.GetComponentsInChildren(typeof(RFWidget), true)) {
                    if (dic.ContainsKey(child.name))
                        m_widgets[dic[child.name]] = child.GetComponent(child.m_type.ToString());
                }
            }
        }

        //	public ShowType SType {get; set;}
        public virtual void UpdateUI(UIMSG cmd, params object[] param) {
            switch (cmd) {
                case UIMSG.Enable:
                    RFManager.Ins.EnablePage(this);
                    switch (m_tEnable) {
                        case TransitionType.Alpha:
                            this.StartCoroutine(transitionAlpha(true));
                            m_aniRunning = true;
                            break;
                        case TransitionType.Animator:
                            if (m_ani != null) {
                                m_ani.SetTrigger("Enable");
                                m_aniRunning = true;
                            }
                            break;
                    }
                    break;
                case UIMSG.Disable:
                    switch (m_tDisable) {
                        case TransitionType.Alpha:
                            this.StartCoroutine(transitionAlpha(false));
                            break;
                        case TransitionType.Animator:
                            if (m_ani != null)
                                m_ani.SetTrigger("Disable");
                            break;
                        default:
                            gameObject.SetActive(false);
                            break;
                    }
                    break;
                case UIMSG.DisableComplete:
                    m_aniRunning = false;
                    if (m_cmds.Count > 0) {
                        exeCommand();
                    }
                    break;
                default:
                    if (m_enableStack) {
                        setCommand(cmd, param);
                        if (!m_aniRunning) {
                            exeCommand();
                        }
                    }
                    break;
            }
        }

        protected virtual void UpdateUIByStack(UIMSG cmd, params object[] param) {
        }

        protected void setCommand(UIMSG cmd, params object[] param) {
            m_cmds.Enqueue(new Command(cmd, param));
        }

        protected Command getCommand() {
            return (Command)m_cmds.Dequeue();
        }

        protected void exeCommand() {
            curCmd = getCommand();
            UpdateUIByStack(curCmd.command, curCmd.param);
        }

        private T getWidget<T>(int name) {
            if (m_widgets.ContainsKey(name))
                return (T)m_widgets[name];
            else
                return default(T);
        }

        public T GetWidget<T>(object name) {
            return getWidget<T>((int)name);
        }

        //public void AddWidget (string name, object widget) {
        //    if (m_widgets.ContainsKey (name))
        //        return;
        //    m_widgets.Add (name, widget);
        //}

        //public void RemoveWidget (string name) {
        //    if (m_widgets.ContainsKey (name))
        //        m_widgets.Remove (name);
        //}

        protected IEnumerator transitionAlpha(bool isShow) {
            if (m_panel != null) {
                if (isShow) {
                    float alpha = 0;
                    while (alpha < 1) {
                        m_panel.alpha = alpha;
                        yield return null;
                        alpha += 0.02f;
                    }
                }
                else {
                    float alpha = 1;
                    while (alpha > 0) {
                        m_panel.alpha = alpha;
                        yield return null;
                        alpha -= 0.02f;
                    }
                    gameObject.SetActive(false);
                    m_panel.alpha = 1;
                    RFManager.Ins.UpdatePage(_pEnum, UIMSG.DisableComplete);
                }
            }
            else
                yield return null;
        }

        public void OnComplete() {
            gameObject.SetActive(false);
            RFManager.Ins.UpdatePage(_pEnum, UIMSG.DisableComplete);
        }

    }
}