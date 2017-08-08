using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NoiseBasedBiomeGen))]
public class NoiseBasedBiomeGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var gen = target as NoiseBasedBiomeGen;

        if (GUILayout.Button("Add Texture"))
            AddProp();

        if (GUILayout.Button("Update Biome"))
            gen.IsDirty = true;

        if(gen.biomeProperties.Length > 0)
            EditorGUILayout.LabelField("Texture List", EditorStyles.boldLabel);

        foreach (var prop in gen.biomeProperties)
        {
            prop.textureId = EditorGUILayout.IntField("Texture Index", prop.textureId);
            prop.minHeight = EditorGUILayout.Slider("Min Height", prop.minHeight, 0, prop.maxHeight);
            prop.maxHeight = EditorGUILayout.Slider("Max Height", prop.maxHeight, prop.minHeight, 1f);
            prop.maxIntensity = EditorGUILayout.Slider("Max Intensity", prop.maxIntensity, prop.minHeight, 1f);
            EditorGUILayout.LabelField("Height Range: [" + prop.minHeight + ";" + prop.maxHeight + "]", EditorStyles.boldLabel);
            
            if (GUILayout.Button("Remove Texture"))
                DeleteProp(prop);

            GUILayout.Space(10);
        }
    }

    private void AddProp()
    {
        var gen = target as NoiseBasedBiomeGen;
        var list = new List<NoiseBasedBiomeGen.BiomeProperty>(gen.biomeProperties);
        list.Add(new NoiseBasedBiomeGen.BiomeProperty());
        gen.biomeProperties = list.ToArray();
    }

    private void DeleteProp(NoiseBasedBiomeGen.BiomeProperty prop)
    {
        var gen = target as NoiseBasedBiomeGen;
        var list = new List<NoiseBasedBiomeGen.BiomeProperty>(gen.biomeProperties);
        list.Remove(prop);
        gen.biomeProperties = list.ToArray();
    }
}
