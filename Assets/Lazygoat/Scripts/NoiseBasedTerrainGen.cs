using System;
using UnityEditor;
using UnityEngine;

public class NoiseBasedTerrainGen 
    : MonoBehaviour
    , TerrainGen
{
    public float terrainNoiseOffset = 0;
    public float terrainNoiseScale = 1f;
    public float terrainExp = 1f;
    public float terrainFreq1 = 1f;
    public float terrainFreq2 = 0f;
    public float terrainFreq3 = 0f;
    public float terrainFreq4 = 0f;
    public float terrainFreq5 = 0f;
    public float terrainFreq6 = 0f;
    private bool changed = true;

    public float TerrainNoiseOffset
    {
        get { return terrainNoiseOffset; }
        set { if (value != terrainNoiseOffset) { terrainNoiseOffset = value; changed = true; } }
    }

    public float TerrainNoiseScale
    {
        get { return terrainNoiseScale; }
        set { if (value != terrainNoiseScale) { terrainNoiseScale = value; changed = true; } }
    }

    public float TerrainExp
    {
        get { return terrainExp; }
        set { if (value != terrainExp) { terrainExp = value; changed = true; } }
    }

    public float TerrainFreq1
    {
        get { return terrainFreq1; }
        set { if (value != terrainFreq1) { terrainFreq1 = value; changed = true; } }
    }

    public float TerrainFreq2
    {
        get { return terrainFreq2; }
        set { if (value != terrainFreq2) { terrainFreq2 = value; changed = true; } }
    }

    public float TerrainFreq3
    {
        get { return terrainFreq3; }
        set { if (value != terrainFreq3) { terrainFreq3 = value; changed = true; } }
    }

    public float TerrainFreq4
    {
        get { return terrainFreq4; }
        set { if (value != terrainFreq4) { terrainFreq4 = value; changed = true; } }
    }

    public float TerrainFreq5
    {
        get { return terrainFreq5; }
        set { if (value != terrainFreq5) { terrainFreq5 = value; changed = true; } }
    }

    public float TerrainFreq6
    {
        get { return terrainFreq6; }
        set { if (value != terrainFreq6) { terrainFreq6 = value; changed = true; } }
    }

    protected float TerrainNoise(float nx, float ny)
    {
        return Mathf.PerlinNoise(terrainNoiseOffset + nx * terrainNoiseScale, terrainNoiseOffset + ny * terrainNoiseScale);
    }

    public bool IsDirty
    {
        get
        {
            return changed;
        }
    }

    public float Generate(float previousValue, float nx, float ny)
    {
        var e = previousValue;

        e += terrainFreq1 * TerrainNoise(1 * nx, 1 * ny);
        e += terrainFreq2 * TerrainNoise(2 * nx, 2 * ny);
        e += terrainFreq3 * TerrainNoise(4 * nx, 4 * ny);
        e += terrainFreq4 * TerrainNoise(8 * nx, 8 * ny);
        e += terrainFreq5 * TerrainNoise(16 * nx, 16 * ny);
        e += terrainFreq6 * TerrainNoise(32 * nx, 32 * ny);

        e /= (terrainFreq1 + terrainFreq2 + terrainFreq3 + terrainFreq4 + terrainFreq5 + terrainFreq6);

        e = Mathf.Pow(e, terrainExp);

        return e;
    }

    public void CleanupDirtyFlag()
    {
        changed = false;
    }

    private void OnEnable()
    {
        InvalidateGenerators();
    }

    private void OnDisable()
    {
        InvalidateGenerators();
    }

    private void InvalidateGenerators()
    {
        var siblingProceduralTerrain = GetComponent<ProceduralTerrain>();
        if (siblingProceduralTerrain != null)
            siblingProceduralTerrain.InvalidateTerrainGenerators();
    }
}
