using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    public float moveTime = 0.15f;
    public float gridSize = 350f;
    public float stunDuration = 0.4f;


    private Rigidbody2D rb;
    private Vector2 currentGridPos;
    private bool isMoving = false;
    private float stunTimer = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;


        SnapToGrid();
    }


    void Update()
    {
        if (stunTimer > 0) { stunTimer -= Time.deltaTime; return; }
        if (isMoving) return;


        Vector2 input = Vector2.zero;
        if (Keyboard.current.aKey.wasPressedThisFrame)      input = Vector2.left;
        else if (Keyboard.current.dKey.wasPressedThisFrame) input = Vector2.right;
        else if (Keyboard.current.sKey.wasPressedThisFrame) input = Vector2.down;
        else if (Keyboard.current.wKey.wasPressedThisFrame) input = Vector2.up;


        if (input != Vector2.zero)
            StartCoroutine(MoveTo(currentGridPos + (input * gridSize)));
    }


    IEnumerator MoveTo(Vector2 destination)
    {
        isMoving = true;
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


    public void OnHitByEnemy()
    {
        // Ignore if already stunned — prevents multiple enemies stacking stuns
        // on the same frame
        if (stunTimer > 0) return;


        StopAllCoroutines();
        isMoving = false;
        stunTimer = stunDuration;
        rb.MovePosition(currentGridPos);
    }


    public Vector2 GetGridPos() => currentGridPos;


    void SnapToGrid()
    {
        float x = Mathf.Round(transform.position.x / gridSize) * gridSize;
        float y = Mathf.Round(transform.position.y / gridSize) * gridSize;
        currentGridPos = new Vector2(x, y);
        rb.MovePosition(currentGridPos);
    }
}
