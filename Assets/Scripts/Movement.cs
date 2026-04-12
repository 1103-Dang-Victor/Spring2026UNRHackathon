using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float moveTime = 0.15f;

    private Rigidbody2D rb;
    private Vector2 currentGridPos;
    private bool isMoving = false;
    private float stunTimer = 0f;

    public Player_Combat player_Combat;

    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        SnapToGrid();
        GridManager.Instance.OccupyCell(currentGridPos);
    }

    void Update()
    {
        if (isMoving) return;

        Vector2 input = Vector2.zero;
        if      (Keyboard.current.aKey.wasPressedThisFrame) input = Vector2.left;
        else if (Keyboard.current.dKey.wasPressedThisFrame) input = Vector2.right;
        else if (Keyboard.current.sKey.wasPressedThisFrame) input = Vector2.down;
        else if (Keyboard.current.wKey.wasPressedThisFrame) input = Vector2.up;
        if(Keyboard.current.spaceKey.wasPressedThisFrame){
            player_Combat.Attack();
        }
        if (input == Vector2.zero) return;

        float gs = GridManager.Instance.gridSize;
        Vector2 destination = GridManager.Instance.SnapToGrid(currentGridPos + input * gs);

        if (GridManager.Instance.IsCellFree(destination))
            StartCoroutine(MoveTo(destination));
    }

    IEnumerator MoveTo(Vector2 destination)
    {
        isMoving = true;

        GridManager.Instance.FreeCell(currentGridPos);
        GridManager.Instance.OccupyCell(destination);

        float elapsed = 0f;
        Vector2 startPos = currentGridPos;

        while (elapsed < moveTime)
        {
            rb.MovePosition(Vector2.Lerp(startPos, destination, elapsed / moveTime));
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(destination);
        currentGridPos = destination;
        isMoving = false;
    }

    public Vector2 GetGridPos() => currentGridPos;

    void SnapToGrid()
    {
        currentGridPos = GridManager.Instance.SnapToGrid(transform.position);
        rb.MovePosition(currentGridPos);
    }
}