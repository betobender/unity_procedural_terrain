using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerraceGen))]
public class TerraceGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var gen = target as TerraceGen;

        gen.Terraces = EditorGUILayout.Toggle("Enable", gen.Terraces);
        if (gen.terraces)
        {
            gen.Steps = EditorGUILayout.IntSlider("Steps", gen.Steps, 1, 30);
            gen.Scale = EditorGUILayout.Slider("Scale", gen.Scale, 0, 5);
        }
    }
}
