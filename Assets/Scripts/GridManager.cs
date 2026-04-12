using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    // Tracks which world-space grid positions are occupied
    private HashSet<Vector2Int> occupiedCells = new HashSet<Vector2Int>();
    private HashSet<Vector2Int> wallCells = new HashSet<Vector2Int>();

    public float gridSize = 350f; // ONE shared grid size for everything

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    // Call this after all walls are placed in Start()
    public void RegisterWall(Vector2 worldPos)
    {
        wallCells.Add(WorldToCell(worldPos));
    }

    public void OccupyCell(Vector2 worldPos)   => occupiedCells.Add(WorldToCell(worldPos));
    public void FreeCell(Vector2 worldPos)     => occupiedCells.Remove(WorldToCell(worldPos));

    // Returns true if the cell is free (no wall, no occupant)
    public bool IsCellFree(Vector2 worldPos)
    {
        Vector2Int cell = WorldToCell(worldPos);
        return !wallCells.Contains(cell) && !occupiedCells.Contains(cell);
    }

    public Vector2 SnapToGrid(Vector2 worldPos)
    {
        float x = Mathf.Round(worldPos.x / gridSize) * gridSize;
        float y = Mathf.Round(worldPos.y / gridSize) * gridSize;
        return new Vector2(x, y);
    }

    public Vector2Int WorldToCell(Vector2 worldPos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(worldPos.x / gridSize),
            Mathf.RoundToInt(worldPos.y / gridSize)
        );
    }
}