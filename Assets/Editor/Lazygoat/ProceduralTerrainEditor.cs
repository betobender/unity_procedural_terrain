
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProceduralTerrain))]
public class ProceduralTerrainEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var pTerrain = target as ProceduralTerrain;

        EditorGUILayout.LabelField("Sub-components", EditorStyles.boldLabel);

        if (pTerrain.GetComponent<NoiseBasedTerrainGen>() == null)
            if (GUILayout.Button("Add Noise Terrain Generator"))
                pTerrain.gameObject.AddComponent<NoiseBasedTerrainGen>();

        if (pTerrain.GetComponent<IslandGen>() == null)
            if (GUILayout.Button("Add Island Generator"))
                pTerrain.gameObject.AddComponent<IslandGen>();

        if (pTerrain.GetComponent<TerraceGen>() == null)
            if (GUILayout.Button("Add Terrace Generator"))
                pTerrain.gameObject.AddComponent<TerraceGen>();

        if (pTerrain.GetComponent<NoiseBasedBiomeGen>() == null)
            if (GUILayout.Button("Add Noise Biome Generator"))
                pTerrain.gameObject.AddComponent<NoiseBasedBiomeGen>();

        EditorGUILayout.LabelField("Current Pipelines", EditorStyles.helpBox);
        EditorGUILayout.LabelField("Terrain:", EditorStyles.boldLabel);
        foreach (var gen in pTerrain.TerrainGenerators)
            EditorGUILayout.LabelField(" > " + gen.ToString());
        EditorGUILayout.LabelField("Biome:", EditorStyles.boldLabel);
        foreach (var gen in pTerrain.BiomeGenerators)
            EditorGUILayout.LabelField(" > " + gen.ToString());


        EditorGUILayout.LabelField("Manual actions", EditorStyles.boldLabel);

        if (GUILayout.Button("Invalidate Everything"))
            pTerrain.Invalidate();

        if (GUILayout.Button("Invalidate Generators"))
        {
            pTerrain.InvalidateBiomeGenerators();
            pTerrain.InvalidateTerrainGenerators();
        }


        if (pTerrain.enabled)
            pTerrain.SmartUpdate();
    }
}
