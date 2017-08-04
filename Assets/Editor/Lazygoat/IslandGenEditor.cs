using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IslandGen))]
public class IslandGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var gen = target as IslandGen;

        gen.Apply = EditorGUILayout.Toggle("Apply", gen.Apply);
        if (gen.Apply)
        {
            gen.IslandsA = EditorGUILayout.Slider("Islands Coef A", gen.IslandsA, 0, 1);
            gen.IslandsB = EditorGUILayout.Slider("Islands Coef B", gen.IslandsB, 0, 2);
            gen.IslandsC = EditorGUILayout.Slider("Islands Coef C", gen.IslandsC, 0, 10);
        }

    }
}
