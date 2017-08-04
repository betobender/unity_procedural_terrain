using System;
interface BiomeGen
{
    bool IsDirty { get; }
    void CleanupDirtyFlag();

    void Generate(float terrainHeight, int terrainX, int terrainY, float nx, float ny, float[,,] alphamaps);
     
}
