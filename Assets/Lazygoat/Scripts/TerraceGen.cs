using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraceGen : MonoBehaviour, TerrainGen
{
    public bool terraces = false;
    public int steps = 30;
    public float scale = 1f;
    private bool changed = true;

    public bool Terraces
    {
        get { return terraces; }
        set { if (value != terraces) { terraces = value; changed = true; } }
    }

    public int Steps
    {
        get { return steps; }
        set { if (value != steps) { steps = value; changed = true; } }
    }
    public float Scale
    {
        get { return scale; }
        set { if (value != scale) { scale = value; changed = true; } }
    }

    public bool IsDirty
    {
        get
        {
            return changed;
        }
    }

    public void CleanupDirtyFlag()
    {
        changed = false;
    }

    public float Generate(float previousValue, float nx, float ny)
    {
        if (terraces)
            previousValue = Mathf.Round(previousValue * (steps / scale)) / (steps / scale);
        return previousValue;
    }
}
