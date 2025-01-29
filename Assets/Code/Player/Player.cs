using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Player Attributes")]
    public float moveSpeed;
    public float touchmoveSpeed;
    private Vector2 lastTouchPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

#if UNITY_ANDROID
        Screen.orientation = ScreenOrientation.LandscapeLeft;
#endif
    }
    void Start()
    {

    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        MovePlayer();
#endif

#if UNITY_ANDROID
        TouchMovePlayer();
#endif

    }


    public void MovePlayer()
    {
        Vector2 vectical = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            vectical = Vector2.up;

        if (Input.GetKey(KeyCode.S))
            vectical = Vector2.down;

        Vector2 movement = vectical * moveSpeed;
        rb.velocity = movement;
    }

    public void TouchMovePlayer()
    {
        if (Input.touchCount > 0) // Check if screen is touched
        {
            Touch touch = Input.GetTouch(0); // Get first touch

            // Convert touch position to world space
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.x = transform.position.x; // Keep X position fixed
            touchPosition.z = 0f; // Ensure no depth changes

            // Move paddle using Rigidbody2D (smooth movement)
            rb.MovePosition(Vector2.Lerp(transform.position, touchPosition, touchmoveSpeed * Time.deltaTime));
        }
    }

}
