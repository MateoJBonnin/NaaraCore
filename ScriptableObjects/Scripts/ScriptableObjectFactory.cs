using UnityEditor;
using UnityEngine;

public static class ScriptableObjectFactory
{
    public static void Create<T>() where T : ScriptableObject
    {
#if UNITY_EDITOR
        T asset = ScriptableObject.CreateInstance<T>();
        string storagePath = AssetDatabase.GenerateUniqueAssetPath("Assets/" + typeof(T).ToString() + ".asset");
        AssetDatabase.CreateAsset(asset, storagePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
#endif
    }
}