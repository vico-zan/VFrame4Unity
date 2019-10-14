using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

//if needed, open this code
//[InitializeOnLoad]
class VFInstaller {
    static VFInstaller () {
        //Debug.Log ("Init Extend"); 

        if (!Directory.Exists (Application.dataPath + "/Main")) {
            Directory.CreateDirectory (Application.dataPath + "/Excel");
            Directory.CreateDirectory (Application.dataPath + "/Main");
            Directory.CreateDirectory (Application.dataPath + "/Main/UI");
            Directory.CreateDirectory (Application.dataPath + "/Main/UI/Src");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources/Table");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources/Sound");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources/DataAsset");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources/Font");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources/Prefab");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources/Prefab/UI");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources/Prefab/Other");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources/UI");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources/UI/Sprite");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources/UI/RawImage");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources/UI/Localize");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources/UI/Localize/Sprite");
            Directory.CreateDirectory (Application.dataPath + "/Main/Resources/UI/Localize/RawImage");
            Directory.CreateDirectory (Application.dataPath + "/Main/Scene");
            Directory.CreateDirectory (Application.dataPath + "/Main/Script");
            Directory.CreateDirectory (Application.dataPath + "/Main/Script/DataClass");
            Directory.CreateDirectory (Application.dataPath + "/Main/Script/Core");
            Directory.CreateDirectory (Application.dataPath + "/Main/Script/Core/Manager");
            Directory.CreateDirectory (Application.dataPath + "/Main/Script/Core/Page");
            Directory.CreateDirectory (Application.dataPath + "/StreamingAssets");

            if (!File.Exists (Application.dataPath + "/Main/Script/Core/NDefine.cs")) {
                File.Move (Application.dataPath + "/VFrame/Template/NDefine.cs",
                           Application.dataPath + "/Main/Script/Core/NDefine.cs");
            }

            if (!File.Exists (Application.dataPath + "/Main/Script/DataClass/UserData.cs")) {
                File.Copy (Application.dataPath + "/VFrame/Template/UserData.txt",
                           Application.dataPath + "/Main/Script/DataClass/UserData.cs");
            }

            if (!File.Exists (Application.dataPath + "/Main/Scene/Load.unity")) {
                File.Copy (Application.dataPath + "/VFrame/Template/Load.unity",
                           Application.dataPath + "/Main/Scene/Load.unity");
            }

        }

        AssetDatabase.Refresh(ImportAssetOptions.Default);
    }
}