using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGen : MonoBehaviour, TerrainGen
{
    public bool apply = false;
    public float islandsA = 0f;
    public float islandsB = 0f;
    public float islandsC = 0f;
    private bool changed = true;

    public bool Apply
    {
        get { return apply; }
        set { if (value != apply) { apply = value; changed = true; } }
    }

    public float IslandsA
    {
        get { return islandsA; }
        set { if (value != islandsA) { islandsA = value; changed = true; } }
    }

    public float IslandsB
    {
        get { return islandsB; }
        set { if (value != islandsB) { islandsB = value; changed = true; } }
    }

    public float IslandsC
    {
        get { return islandsC; }
        set { if (value != islandsC) { islandsC = value; changed = true; } }
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
        if (apply)
        {
            var d = 2 * Mathf.Sqrt(nx * nx + ny * ny);
            previousValue = (previousValue + islandsA) * (1 - islandsB * Mathf.Pow(d, islandsC));
        }

        return previousValue;

    }
}
