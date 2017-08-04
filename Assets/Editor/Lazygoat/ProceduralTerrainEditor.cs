
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProceduralTerrain))]
public class ProceduralTerrainEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var pTerrain = target as ProceduralTerrain;

        if(GUILayout.Button("Add Noise Terrain Generator"))
        {
            if (pTerrain.GetComponent<NoiseBasedTerrainGen>() != null)
                Debug.LogWarning("This terrain has already one Noise Terrain Generator");
            else pTerrain.gameObject.AddComponent<NoiseBasedTerrainGen>();
        }

        if (GUILayout.Button("Add Island Generator"))
        {
            if (pTerrain.GetComponent<IslandGen>() != null)
                Debug.LogWarning("This terrain has already one Island Generator");
            else pTerrain.gameObject.AddComponent<IslandGen>();
        }

        if (GUILayout.Button("Add Terrace Generator"))
        {
            if (pTerrain.GetComponent<TerraceGen>() != null)
                Debug.LogWarning("This terrain has already one Terrace Generator");
            else pTerrain.gameObject.AddComponent<TerraceGen>();
        }

        if (GUILayout.Button("Add Noise Biome Generator"))
        {
            if (pTerrain.GetComponent<NoiseBasedBiomeGen>() != null)
                Debug.LogWarning("This terrain has already one Noise Biome Generator");
            else pTerrain.gameObject.AddComponent<NoiseBasedBiomeGen>();
        }
        /*

        EditorGUILayout.LabelField("Terraces Properties", EditorStyles.boldLabel);
        pTerrain.Terraces = EditorGUILayout.Toggle("Enable Terraces", pTerrain.Terraces);
        if (pTerrain.terraces)
        {
            pTerrain.TerraceSteps = EditorGUILayout.IntSlider("Terrace Steps", pTerrain.TerraceSteps, 1, 30);
        }

        //GUILayout.BeginArea(new Rect(0, this., 30, 100), "Batata");
        EditorGUILayout.LabelField("Biome Properties", EditorStyles.boldLabel);*/


        if (pTerrain.enabled)
            pTerrain.SmartUpdate();
    }
}
