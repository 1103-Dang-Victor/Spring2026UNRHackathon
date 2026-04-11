using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // NEW

public class Movement : MonoBehaviour
{
    //made with help of chatgpt to speed up dev time 
    public float moveTime = 0.15f;
    private float gridSize = 160f;
    private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 targetPosition;
    private bool isMoving = false;
    private Coroutine moveTask;
    public Player_Combat player_Combat;



    void Start()
    {
        targetPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isMoving) return;
        input = Vector2.zero;

        // Horizontal
        if (Keyboard.current.aKey.wasPressedThisFrame)
            input = Vector2.left;
        else if (Keyboard.current.dKey.wasPressedThisFrame)
            input = Vector2.right;
        else if (Keyboard.current.sKey.wasPressedThisFrame) //vertical
            input = Vector2.down;
        else if (Keyboard.current.wKey.wasPressedThisFrame)
            input = Vector2.up;

        input.Normalize();
        if (input != Vector2.zero)
        {
            Vector2 newTarget = targetPosition + input * gridSize;
            moveTask = StartCoroutine(MoveTo(newTarget));
        }
        if (Keyboard.current != null && Keyboard.current[Key.Space].wasPressedThisFrame){ 
            player_Combat.Attack(); 
        }
    }

    void FixedUpdate()
    {
        //rb.linearVelocity = input * speed;
    }

    IEnumerator MoveTo(Vector2 destination)
    {
        isMoving = true;

        Vector2 start = transform.position;
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            Vector2 nextPos = Vector2.Lerp(start, destination, elapsed / moveTime);
            elapsed += Time.deltaTime;
            //new
            rb.MovePosition(nextPos);
            yield return null;
        }

        
        //transform.position = destination;
        targetPosition = destination;
        rb.MovePosition(targetPosition);

        isMoving = false;
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (moveTask != null)
        {
            StopCoroutine(moveTask);
            moveTask = null;
            Debug.Log("I HIT SOMETHING");
        }
    }*/
}
