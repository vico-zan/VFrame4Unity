using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPS : MonoBehaviour {
    private Text m_fps;
    public float updateInterval = 0.5f;
    private float lastInterval; // Last interval end time
    private int frames = 0; // Frames over current interval
    public static float fps; // Current FPS
    public bool showFPS = true;

    float timeNow;

    // Use this for initialization
    void Awake () {
        m_fps = this.GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update () {
        if (!showFPS) return;
        timeNow = Time.realtimeSinceStartup;
        ++frames;

        if (timeNow > lastInterval + updateInterval) {
            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;
            m_fps.text = ((int)(Mathf.Round (fps * 100) / 100)).ToString ();
        }

    }

}