using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveTime = 0.5f;
    public float gridSize = 350f;
    public float followRange = 600f;


    private Rigidbody2D rb;
    private Transform player;


    private Vector2 targetPosition;
    private bool isMoving = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        // Find player - ensure your Player object is actually named "Player"
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null) player = playerObj.transform;


        // CRITICAL: Snap the enemy to the grid immediately on start
        SnapToGrid();
        targetPosition = transform.position;
    }


    void Update()
    {
        if (isMoving || player == null) return;


        float distance = Vector2.Distance(transform.position, player.position);


        // Only move if player is within range
        if (distance > followRange) return;


        // Only move if the player is at least one grid space away
        // (prevents the enemy from constantly trying to "overlap" the player)
        if (Vector2.Distance(transform.position, player.position) < gridSize * 0.5f) return;


        Vector2 currentPos = transform.position;
        Vector2 playerPos = player.position;


        Vector2 diff = playerPos - currentPos;
        Vector2 moveDir = Vector2.zero;


        // Strict Grid Logic: Determine which axis has the greatest distance
        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            // Move horizontally
            moveDir = new Vector2(Mathf.Sign(diff.x), 0);
        }
        else
        {
            // Move vertically
            moveDir = new Vector2(0, Mathf.Sign(diff.y));
        }


        Vector2 newTarget = currentPos + (moveDir * gridSize);
       
        StartCoroutine(MoveTo(newTarget));
    }


    IEnumerator MoveTo(Vector2 destination)
    {
        isMoving = true;


        Vector2 start = transform.position;
        float elapsed = 0f;


        while (elapsed < moveTime)
        {
            // Smoothly slide to the next grid square
            Vector2 nextPos = Vector2.Lerp(start, destination, elapsed / moveTime);
            rb.MovePosition(nextPos);
            elapsed += Time.deltaTime;
            yield return null;
        }


        // Finalize position at exactly the destination
        rb.MovePosition(destination);
        targetPosition = destination;


        isMoving = false;
    }


    // Helper to ensure the enemy starts aligned to your 160px grid
    void SnapToGrid()
    {
        float snappedX = Mathf.Round(transform.position.x / gridSize) * gridSize;
        float snappedY = Mathf.Round(transform.position.y / gridSize) * gridSize;
        transform.position = new Vector2(snappedX, snappedY);
    }

}
