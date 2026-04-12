using UnityEngine;

// Attach this to every wall GameObject.
// The wall must have a Collider2D so physics still blocks non-kinematic objects,
// but the grid registration is what actually blocks player/enemy movement.
public class WallRegistrar : MonoBehaviour
{
    void Start()
    {
        // Register every collider tile the wall occupies
        // For a single-cell wall, just register the center
        if (GridManager.Instance != null)
            GridManager.Instance.RegisterWall(transform.position);
    }
}