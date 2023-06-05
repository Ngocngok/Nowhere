#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEditor;

public static class ConfigHelper
{
    public static string GetContainingFolder(Object asset)
    {
        string containingFolder = Directory.GetParent(AssetDatabase.GetAssetPath(asset)).FullName.Replace("\\", "/");

        if (containingFolder.StartsWith(Application.dataPath))
        {
            containingFolder = $"Assets{containingFolder.Substring(Application.dataPath.Length)}";
        }

        return containingFolder;
    }

    public static string ToUnityAssetPath(string path)
    {
        path = path.Replace("\\", "/");
        if (path.StartsWith(Application.dataPath))
        {
            return $"Assets{path.Substring(Application.dataPath.Length)}";
        }

        return path;
    }
}
#endif
