using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    
    public float moveTime = 0.2f;
    public float gridSize = 160f;
    public float followRange = 600f;
    public float stunDuration = 0.5f;


    private Rigidbody2D rb;
    private Transform player;
    private Vector2 startOfMovePos;
    private bool isMoving = false;
    private float stunTimer = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        
        GameObject p = GameObject.Find("Player");
        if (p) player = p.transform;


        SnapToInitialGrid();
    }


    void Update()
    {
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
            return;
        }


        if (isMoving || player == null) return;


        float dist = Vector2.Distance(transform.position, player.position);
        if (dist > followRange || dist < gridSize * 0.8f) return;


        Vector2 diff = (Vector2)player.position - (Vector2)transform.position;
        Vector2 moveDir = Mathf.Abs(diff.x) > Mathf.Abs(diff.y) 
            ? new Vector2(Mathf.Sign(diff.x), 0) 
            : new Vector2(0, Mathf.Sign(diff.y));


        startOfMovePos = transform.position;
        StartCoroutine(MoveTo((Vector2)transform.position + (moveDir * gridSize)));
    }


    IEnumerator MoveTo(Vector2 destination)
    {
        isMoving = true;
        float elapsed = 0f;
        Vector2 startPos = transform.position;


        while (elapsed < moveTime)
        {
            rb.MovePosition(Vector2.Lerp(startPos, destination, elapsed / moveTime));
            elapsed += Time.deltaTime;
            yield return null;
        }


        rb.MovePosition(destination);
        isMoving = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (stunTimer > 0) return;


        if (collision.gameObject.CompareTag("Player") || collision.gameObject.name == "Player")
        {
            StopAllCoroutines();
            isMoving = false;
            stunTimer = stunDuration;


            // Teleport back to original square to resolve the overlap
            transform.position = startOfMovePos;
            rb.linearVelocity = Vector2.zero;
        }
    }


    void SnapToInitialGrid()
    {
        float x = Mathf.Round(transform.position.x / gridSize) * gridSize;
        float y = Mathf.Round(transform.position.y / gridSize) * gridSize;
        transform.position = new Vector2(x, y);
    }


}
