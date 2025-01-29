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

    //[Header("Player Controls")]

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
       
    }

    void Update()
    {
        MovePlayer();
    }

    public Vector2 CheckInput()
    {
        float vectical = 0f;

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKey(KeyCode.W))
            vectical = 1f;

        if (Input.GetKey(KeyCode.S))
            vectical = -1f;
#endif

#if UNITY_ANDROID
        if (Input.acceleration.y > 0f)
            vectical = 1f;

        if (Input.acceleration.y < 0f)
            vectical = -1f;

#endif
        return new Vector2(0f, vectical);
    }

    public void MovePlayer() 
    {
        Vector2 movement = CheckInput() * moveSpeed;
        rb.velocity = movement;
    }
}
