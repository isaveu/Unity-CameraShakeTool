using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

[CustomEditor(typeof(CameraShakeTool))]
public class CameraShakeToolEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create AnimationClip"))
        {
            CreateAnimationClip();
        }
    }

    void CreateAnimationClip()
    {
        CameraShakeTool tool = target as CameraShakeTool;

        List<Keyframe> keyframeList = new List<Keyframe>();

        foreach (Vector2 v in tool.value)
        {
            Keyframe k = new Keyframe(v.x, v.y);
            keyframeList.Add(k);
        }

        AnimationClip clip = new AnimationClip();
    #if UNITY_5
        clip.legacy = true;
    #endif
        clip.wrapMode = WrapMode.Once;
        
        clip.SetCurve("", typeof(Transform), "localPosition.x", new AnimationCurve(keyframeList.ToArray()));

        string activePath = AssetDatabase.GetAssetPath(Selection.activeObject);
        string directory = Path.GetDirectoryName(activePath);
        string filename = Path.GetFileNameWithoutExtension(activePath);
        string path = directory + "/" + filename + ".anim";
        string clipPath = AssetDatabase.GenerateUniqueAssetPath(path);

        AssetDatabase.CreateAsset(clip, clipPath);
    }
     
    [MenuItem("Assets/Create/CameraShakeTool")]
    public static void CreaeteCameraShakeTool()
    {
        CustomAssetUtility.CreateAsset<CameraShakeTool>();
    }
}
