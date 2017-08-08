using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
[ExecuteInEditMode()]
public class ProceduralTerrain : MonoBehaviour
{
    private int currTX;
    private int currTY;
    private int currAX;
    private int currAY;
    private float[,] currHeights;
    private float[,,] currAlphamaps;
    private List<TerrainGen> terrainGenerators;
    private List<BiomeGen> biomeGenerators;
    private bool isDirty = true;

    private const int linesToRender = 10;

    private void OnEnable()
    {
        UnityEditor.EditorApplication.update += OnEditorUpdate;
    }

    private void OnDisable()
    {
        UnityEditor.EditorApplication.update -= OnEditorUpdate;
    }

    private void OnEditorUpdate()
    {
        RefreshTerrainData();
    }

    public void Invalidate()
    {
        InvalidateBiomeGenerators();
        InvalidateTerrainGenerators();
        currHeights = null;
        currAlphamaps = null;
        currTX = currTY = 0;
        currAX = currAY = 0;
    }

    public void InvalidateTerrainGenerators()
    {
        terrainGenerators = null;
        isDirty = true;
    }

    public void InvalidateBiomeGenerators()
    {
        biomeGenerators = null;
        isDirty = true;
    }

    public bool IsTerrainGenDataDirty
    {
        get
        {
            foreach (var gen in TerrainGenerators)
                if (gen.IsDirty)
                    return true;
            return isDirty;
        }
    }

    public bool IsBiomeGenDataDirty
    {
        get
        {
            foreach (var gen in BiomeGenerators)
                if (gen.IsDirty)
                    return true;
            return isDirty;
        }
    }

    private void CleanupGeneratorsDirtyFlag()
    {
        foreach (var gen in TerrainGenerators)
            gen.CleanupDirtyFlag();
        foreach (var gen in BiomeGenerators)
            gen.CleanupDirtyFlag();

        isDirty = false;
    }

    public void SmartUpdate()
    {
        var terrain = GetComponent<Terrain>();
        if(currHeights != null && (terrain.terrainData.heightmapHeight != currHeights.GetLength(0) || terrain.terrainData.heightmapWidth != currHeights.GetLength(1)))
        {
            currHeights = null;
            currAlphamaps = null;
        }

        if (IsTerrainGenDataDirty || IsBiomeGenDataDirty)
        {
            currTX = currTY = 0;
            currAX = currAY = 0;
        }

        CleanupGeneratorsDirtyFlag();
    }

    internal float ComputeHeight(float nx, float ny)
    {
        float e = 0;
        foreach (var gen in TerrainGenerators)
            e = gen.Generate(e, nx, ny);
        return e;
    }

    internal void ComputeAlphamap(float terrainHeight, int terrainX, int terrainY, float nx, float ny, float[,,] alphamaps)
    {
        foreach (var gen in BiomeGenerators)
            gen.Generate(terrainHeight, terrainX, terrainY, nx, ny, alphamaps);
    }

    public List<TerrainGen> TerrainGenerators
    {
        get
        {
            if(terrainGenerators == null)
            {
                terrainGenerators = new List<TerrainGen>();
                foreach (var c in GetComponents<Component>())
                {
                    var gen = c as TerrainGen;
                    if (gen != null) terrainGenerators.Add(gen);
                }
            }

            return terrainGenerators;
        }
    }

    public List<BiomeGen> BiomeGenerators
    {
        get
        {
            if (biomeGenerators == null)
            {
                biomeGenerators = new List<BiomeGen>();
                foreach (var c in GetComponents<Component>())
                {
                    var gen = c as BiomeGen;
                    if (gen != null) biomeGenerators.Add(gen);
                }
            }

            return biomeGenerators;
        }
    }

    public void RefreshTerrainData()
    {
        var pTerrain = this;
        var terrain = pTerrain.GetComponent<Terrain>();
        var terrainData = terrain.terrainData;
        var linesRendered = 0;
        var changed = false;

        if (currHeights == null)
            currHeights = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);

        while (currTY < terrainData.heightmapHeight)
        {
            while (currTX < terrainData.heightmapWidth)
            {
                float nx = (float)currTX / terrainData.heightmapWidth - 0.5f, ny = (float)currTY / terrainData.heightmapHeight - 0.5f;
                currHeights[currTY, currTX] = ComputeHeight(nx, ny);
                currTX++;
            }
            changed = true;
            currTX = 0;
            currTY++;
            if(++linesRendered >= linesToRender)
                return;
        }

        if(changed)
            terrainData.SetHeights(0, 0, currHeights);

        linesRendered = 0;
        changed = false;

        if (currAlphamaps == null)
            currAlphamaps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);


        while (currAY < terrainData.alphamapHeight)
        {
            var sx = (float)terrainData.alphamapWidth / terrainData.heightmapWidth;
            var sy = (float)terrainData.alphamapHeight / terrainData.heightmapHeight;

            while (currAX < terrainData.alphamapWidth)
            {
                var h = currHeights[(int)(currAY / sy), (int)(currAX / sx)];
                float nx = (float)currAX / terrainData.alphamapWidth - 0.5f, ny = (float)currAY / terrainData.alphamapHeight - 0.5f;
                ComputeAlphamap(h, currAX, currAY, nx, ny, currAlphamaps);
                currAX++;
            }
            currAX = 0;
            currAY++;
            changed = true;
            if (++linesRendered >= linesToRender)
                return;
        }

        if(changed)
            terrainData.SetAlphamaps(0, 0, currAlphamaps);


    }
}
