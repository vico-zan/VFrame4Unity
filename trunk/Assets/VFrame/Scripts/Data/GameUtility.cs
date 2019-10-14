using UnityEngine;
using System.IO;

public class GameUtility {
    public static bool IsCacheFolder;
    public static int CanvasWidth = 768;
    public static int CanvasHeight = 1024;

    public static string GetPath(string file = "") {
        if (IsCacheFolder) {
            return
#if UNITY_EDITOR
        Application.streamingAssetsPath + "/Cache"
#elif UNITY_ANDROID
        Application.temporaryCachePath
#else
        Application.temporaryCachePath
#endif
        + "/" + file;
        }
        else {
            return
#if UNITY_EDITOR
        Application.streamingAssetsPath + "/Document"
#elif UNITY_ANDROID
        Application.persistentDataPath
#else
        Application.persistentDataPath
#endif
        + "/" + file;
        }
    }

    public static string FormatSecond2Time(float t) {
        int m = Mathf.FloorToInt(t / 60);
        int s = Mathf.FloorToInt(t % 60);
        int f = Mathf.FloorToInt((t % 1) * 1000);

        //return m + ":" + s + ":" + f;
        return string.Format("{0:D2}:{1:D2}:{2:D3}", m, s, f);
    }

    public static void CreateByteFile(string path, string name, byte[] info, int length) {
        Stream sw;
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists) {
            sw = t.Create();
        }
        else {
            return;
        }
        sw.Write(info, 0, length);
        sw.Close();
        sw.Dispose();
    }

}
