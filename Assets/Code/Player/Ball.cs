using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    public float ballSpeed;
    public float collisionForceMultiplier = 0.1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        GameManager.Instance.isBallMoving = false;
        GameManager.Instance.ResetPositionAction = ResetPosition;
        GameManager.Instance.StartGameAction = StartingForce;
    }

    void Update()
    {
        if(!GameManager.Instance.isBallMoving) 
        {
            rb.Sleep();
        }
        else 
        {
            rb.WakeUp();
        }
    }

    public void ResetPosition() 
    {
        transform.position = Vector3.zero;
    }

    public void StartingForce()
    {
        GameManager.Instance.isBallMoving = true;

        var bounceRight = Random.value > 0.5f;
        var xDir = bounceRight ? 1f : -1f;
        var yDir = Random.Range(-1f, 1f);

        rb.velocity = new Vector3(xDir, yDir) * ballSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SFXManager.Instance.PlaySound(SFXManager.Instance.collisionSound);
            //var hitpoint = (collision.contacts[0].point.y - collision.transform.position.y) / collision.transform.localScale.y; 
            //float angle = hitpoint > 0f ? Random.Range(0, 45f): Random.Range(-45f, 0);

            float angle = Random.Range(-45f, 46f);
            float radians = angle * Mathf.Deg2Rad;
            Vector2 newDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            rb.AddForce(newDirection);
        }

        if (collision.gameObject.CompareTag("Bounds"))
        {
            
        }

    }
}
