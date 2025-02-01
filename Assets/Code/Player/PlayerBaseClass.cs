using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseClass : MonoBehaviour
{
    [Header("Components")]
    protected Rigidbody2D rb;
    protected GameManager gameManager;

    [Header("Player Attributes")]
    public float moveSpeed;
    public float minBound;
    public float maxBound;

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update() 
    {
        RestrictMovement();
        MovePlayer();
    }

    protected virtual void MovePlayer() { }

    public void RestrictMovement()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minBound, maxBound), transform.position.z);
    }
}
