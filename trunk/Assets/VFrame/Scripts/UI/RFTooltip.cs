using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace VFrame {
    public class RFTooltip : MonoBehaviour {

        public string m_text;

        private void OnTooltip (bool show) {
            if (show) {
                RFManager.Ins.UpdatePage (NPage.Tooltip, UIMSG.Enable, transform.position);
                RFManager.Ins.UpdatePage (NPage.Tooltip, UIMSG.SetTipText, m_text);
            }
            else {
                RFManager.Ins.UpdatePage (NPage.Tooltip, UIMSG.Disable);
            }
        }
    }
}