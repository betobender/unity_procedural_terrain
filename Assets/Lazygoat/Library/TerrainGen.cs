using System;

public interface TerrainGen
{
    bool IsDirty { get; }
    void CleanupDirtyFlag();
    float Generate(float previousValue, float nx, float ny);
    
}
