using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NoiseBasedTerrainGen))]
public class NoiseBasedTerrainGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var gen = target as NoiseBasedTerrainGen;

        gen.TerrainNoiseOffset = EditorGUILayout.Slider("Offset", gen.TerrainNoiseOffset, -1000, 1000);
        gen.TerrainNoiseScale = EditorGUILayout.Slider("Scale", gen.TerrainNoiseScale, 0.01f, 2);
        gen.TerrainExp = EditorGUILayout.Slider("Redistribution", gen.TerrainExp, 0.01f, 5f);
        gen.TerrainFreq1 = EditorGUILayout.Slider("Frequency 1", gen.TerrainFreq1, 0, 1);
        gen.TerrainFreq2 = EditorGUILayout.Slider("Frequency 2", gen.TerrainFreq2, 0, 1);
        gen.TerrainFreq3 = EditorGUILayout.Slider("Frequency 3", gen.TerrainFreq3, 0, 1);
        gen.TerrainFreq4 = EditorGUILayout.Slider("Frequency 4", gen.TerrainFreq4, 0, 1);
        gen.TerrainFreq5 = EditorGUILayout.Slider("Frequency 5", gen.TerrainFreq5, 0, 1);
        gen.TerrainFreq6 = EditorGUILayout.Slider("Frequency 6", gen.TerrainFreq6, 0, 1);
    }
}
