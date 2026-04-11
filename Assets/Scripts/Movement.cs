using UnityEngine;
using UnityEngine.InputSystem; // NEW

public class Movement : MonoBehaviour
{
    public float speed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input = Vector2.zero;

        // Horizontal
        if (Keyboard.current.aKey.isPressed)
            input.x = -1;
        if (Keyboard.current.dKey.isPressed)
            input.x = 1;

        // Vertical
        if (Keyboard.current.sKey.isPressed)
            input.y = -1;
        if (Keyboard.current.wKey.isPressed)
            input.y = 1;

        input.Normalize();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }
}
