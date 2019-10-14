using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

namespace VicoTools {

    public class VTImage : EditorWindow {

        /// <summary>
        /// Vico Tools. Used to Unpack some slice, save it to PNG.
        /// </summary>
        [MenuItem ("VT/Slice")]
        static public void VT_DoSlice () {
            string pathName = AssetDatabase.GetAssetPath (Selection.activeObject);
            Debug.Log (pathName);
            TextureImporter ti = AssetImporter.GetAtPath (pathName) as TextureImporter;
            ti.isReadable = true;
            AssetDatabase.ImportAsset (pathName);
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D> (pathName) as Texture2D;
            foreach (SpriteMetaData d in ti.spritesheet) {
                int w = (int)d.rect.width;
                int h = (int)d.rect.height;
                Texture2D t = new Texture2D (w, h);
                t.SetPixels (texture.GetPixels ((int)d.rect.x, (int)d.rect.y, w, h));
                t.Apply (false);

                byte[] bytes = t.EncodeToPNG ();
                File.WriteAllBytes (Application.dataPath + "/" + d.name + ".png", bytes);
            }

            AssetDatabase.Refresh ();
        }

        [MenuItem ("VT/AtlasMaker")]
        static private void MakeAtlas () {
            DirectoryInfo rootDirInfo = new DirectoryInfo (Application.dataPath + "/Main/UI/Src/");
            foreach (DirectoryInfo dirInfo in rootDirInfo.GetDirectories ()) {
                Debug.Log (dirInfo.FullName);
                string pathSuffix = dirInfo.FullName.Substring (dirInfo.FullName.LastIndexOf ('\\') + 1);
                string spriteDir = Application.dataPath + "/Main/Resources/UI/" + pathSuffix;
                if (!Directory.Exists (spriteDir)) {
                    Directory.CreateDirectory (spriteDir);
                }

                foreach (FileInfo pngFile in dirInfo.GetFiles ("*.png", SearchOption.AllDirectories)) {
                    string allPath = pngFile.FullName;
                    string assetPath = allPath.Substring (allPath.IndexOf ("Assets"));
                    Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite> (assetPath);
                    GameObject go = new GameObject (sprite.name);
                    go.AddComponent<SpriteRenderer> ().sprite = sprite;
                    allPath = spriteDir + "/" + sprite.name + ".prefab";
                    string prefabPath = allPath.Substring (allPath.IndexOf ("Assets"));
                    PrefabUtility.SaveAsPrefabAsset (go, prefabPath);
                    GameObject.DestroyImmediate (go);
                }
            }
            AssetDatabase.Refresh ();
        }

    }

}