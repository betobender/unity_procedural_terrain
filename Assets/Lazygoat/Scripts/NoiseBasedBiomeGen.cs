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
        //float v = Mathf.PerlinNoise(pTerrain.offset + nx * pTerrain.frequency * 10, pTerrain.offset + ny * pTerrain.frequency * 10);
        //v += Mathf.PerlinNoise(pTerrain.offset + nx * pTerrain.frequency * 2, pTerrain.offset + ny * pTerrain.frequency * 2);
        //v += Mathf.PerlinNoise(pTerrain.offset + nx * pTerrain.frequency * 4, pTerrain.offset + ny * pTerrain.frequency * 4);


        /*if (h <= 0.09f)
        {
            alphamaps[y, x, 0] = 1 - v;
            alphamaps[y, x, 1] = 0;
            alphamaps[y, x, 2] = v;
        }
        else
        {
            alphamaps[y, x, 0] = h * (1 - v);
            alphamaps[y, x, 1] = 1 - h;
            alphamaps[y, x, 2] = h * v;
        }*/

        for (int textureId = 0; textureId < alphamaps.GetLength(2); ++textureId)
            alphamaps[terrainY, terrainX, textureId] = 0;

        foreach (var prop in biomeProperties)
        {
            if(terrainHeight >= prop.minHeight && terrainHeight <= prop.maxHeight)
            {
                for (int textureId = 0; textureId < alphamaps.GetLength(2); ++textureId)
                {
                    if(prop.textureId == textureId)
                        alphamaps[terrainY, terrainX, textureId] = 1;
                    else alphamaps[terrainY, terrainX, textureId] = 0;
                }
            }
        }

    }
}
