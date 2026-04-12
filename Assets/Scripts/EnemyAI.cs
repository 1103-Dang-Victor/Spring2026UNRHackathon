using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemyAI : MonoBehaviour
{
    public float moveTime = 0.5f;
    public float followRange = 2000f;
    public float stunDuration = 1.5f;

    private Rigidbody2D rb;
    private Movement playerMovement;
    private Vector2 currentGridPos;
    private bool isMoving = false;
    private float stunTimer = 0f;
    public static event Action<int> DamageTaken;

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
        if (playerMovement == null)
        {
            GameObject p = GameObject.Find("Player");
            if (p != null) playerMovement = p.GetComponent<Movement>();
            return;
        }

        if (GridManager.Instance == null) return;

        if (stunTimer > 0) { stunTimer -= Time.deltaTime; return; }
        if (isMoving) return;

        Vector2 playerPos = playerMovement.GetGridPos();
        float dist = Vector2.Distance(currentGridPos, playerPos);
        float gs = GridManager.Instance.gridSize;

        if (dist > followRange) return;

        // If adjacent to player, deal damage and stun self
        if (dist < gs * 1.1f)
        {
            Debug.Log("im stun");
            stunTimer = stunDuration;
            DamageTaken?.Invoke(10);
            return;
        }

        Vector2 diff = playerPos - currentGridPos;
        Vector2 moveDir = Mathf.Abs(diff.x) > Mathf.Abs(diff.y)
            ? new Vector2(Mathf.Sign(diff.x), 0)
            : new Vector2(0, Mathf.Sign(diff.y));

        Vector2 destination = GridManager.Instance.SnapToGrid(currentGridPos + moveDir * gs);

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

    void SnapToGrid()
    {
        currentGridPos = GridManager.Instance.SnapToGrid(transform.position);
        rb.MovePosition(currentGridPos);
    }

    void OnDestroy()
    {
        if (GridManager.Instance != null)
            GridManager.Instance.FreeCell(currentGridPos);
    }
}