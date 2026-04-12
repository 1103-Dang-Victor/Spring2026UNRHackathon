using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Drag your Player Prefab here")]
    public GameObject playerPrefab;
    [Tooltip("Drag your scene's Grid object here")]
    public Grid mapGrid;
    
    [Header("Manual Spawn Points")]
    [Tooltip("Assign 4 empty GameObjects from your hierarchy here")]
    public Transform[] spawnPoints = new Transform[4];

    void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (spawnPoints.Length == 0 || playerPrefab == null) return;

        // 1. Pick a random point from your manually placed objects
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector3 rawPosition = spawnPoints[randomIndex].position;

        // 2. Snap position to the center of the nearest grid cell
        // Convert world to cell (int coordinates) then back to world (float coordinates)
        Vector3Int cellPosition = mapGrid.WorldToCell(rawPosition);
        Vector3 snappedPosition = mapGrid.GetCellCenterWorld(cellPosition);

        // 3. Instantiate the player at the snapped location
        playerPrefab.transform.position = snappedPosition;
    }
}
