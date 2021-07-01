using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateassetBundle : MonoBehaviour
{
    [MenuItem("Assets/Build Asset")]
    // Start is called before the first frame update
   static void BuildassetBundle()
    {
        string AssetBundleDirectory = "Assets/AssetBundle";
        if (!Directory.Exists(AssetBundleDirectory))
        {
            Directory.CreateDirectory(AssetBundleDirectory);
        }
#if  UNITY_EDITOR
        BuildPipeline.BuildAssetBundles(AssetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget. StandaloneWindows);
#endif
#if UNITY_ANDROID 
        BuildPipeline.BuildAssetBundles(AssetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget. Android);
#endif

    }
}
