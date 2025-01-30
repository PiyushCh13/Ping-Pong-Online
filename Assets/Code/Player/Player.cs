using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private GameManager gameManager;

    [Header("Player Attributes")]
    public float moveSpeed;
    public float touchmoveSpeed;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        rb = GetComponent<Rigidbody2D>();
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

        if(!gameManager.isBallMoving && Input.anyKeyDown || Input.touchCount > 0) 
        {
            gameManager.StartGameAction?.Invoke();
        }

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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); 
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.x = transform.position.x; 
            touchPosition.z = 0f; 

            rb.MovePosition(Vector2.Lerp(transform.position, touchPosition, touchmoveSpeed * Time.deltaTime));
        }
    }

}
