using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseBasedBiomeGen : MonoBehaviour, BiomeGen
{
    [Serializable]
    public class BiomeProperty
    {
        public int textureId;
        public float minHeight = 0;
        public float maxHeight = 1;
        public float maxIntensity = 1f;
    }

    public BiomeProperty[] biomeProperties;

    private bool changed = true;

    public bool IsDirty
    {
        get
        {
            return changed;
        }
        set
        {
            changed = value;
        }
    }

    public void CleanupDirtyFlag()
    {
        changed = false;
    }

    public void Generate(float terrainHeight, int terrainX, int terrainY, float nx, float ny, float[,,] alphamaps)
    {
        for (int textureId = 0; textureId < alphamaps.GetLength(2); ++textureId)
            alphamaps[terrainY, terrainX, textureId] = 0;

        foreach (var prop in biomeProperties)
        {
            if(terrainHeight >= prop.minHeight && terrainHeight <= prop.maxHeight)
            {
                for (int textureId = 0; textureId < alphamaps.GetLength(2); ++textureId)
                {
                    if(prop.textureId == textureId)
                        alphamaps[terrainY, terrainX, textureId] = prop.maxIntensity;
                }
            }
        }

    }
}
