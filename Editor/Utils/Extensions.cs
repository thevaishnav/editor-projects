#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using AnimatorController = UnityEditor.Animations.AnimatorController;
using Object = UnityEngine.Object;

namespace Omnix.Utils.EditorUtils
{
    public static class EditorExtensions
    {
        public static Object GetFirstAsset(this Object resource)
        {
            string path = AssetDatabase.GetAssetPath(resource);
            if (!Directory.Exists(path)) return resource;

            foreach (string subPath in Directory.GetDirectories(path))
            {
                if (!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(subPath)))
                    return AssetDatabase.LoadAssetAtPath<Object>(subPath);
            }

            foreach (string subPath in Directory.GetFiles(path))
            {
                if (!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(subPath)))
                    return AssetDatabase.LoadAssetAtPath<Object>(subPath);
            }

            return resource;
        }

        public static Texture GetIcon(this Object resource)
        {
            Type type = resource.GetType();

            if (Directory.Exists(AssetDatabase.GetAssetPath(resource))) return EditorGUIUtility.IconContent("Folder Icon").image;

            if (typeof(GameObject).IsAssignableFrom(type)) return PrefabUtility.GetIconForGameObject((GameObject)resource);
            if (typeof(ScriptableObject).IsAssignableFrom(type)) return EditorGUIUtility.IconContent("ScriptableObject Icon").image;

            if (type == typeof(SceneAsset)) return EditorGUIUtility.IconContent("UnityLogo").image;
            if (type == typeof(TextAsset)) return EditorGUIUtility.IconContent("TextAsset Icon").image;
            if (type == typeof(MonoScript)) return EditorGUIUtility.IconContent("cs Script Icon").image;
            if (type == typeof(AnimatorController)) return EditorGUIUtility.IconContent("AnimatorController Icon").image;
            if (type == typeof(LightingDataAsset)) return EditorGUIUtility.IconContent("SceneviewLighting").image;
            if (type == typeof(Cubemap)) return EditorGUIUtility.IconContent("PreMatCube").image;
            if (type == typeof(Shader)) return EditorGUIUtility.IconContent("Shader Icon").image;
            if (type == typeof(Texture2D)) return EditorGUIUtility.IconContent("PreTextureRGB").image;
            return null;
        }
    }
}
#endif