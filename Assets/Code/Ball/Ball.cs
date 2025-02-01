using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private GameManager gameManager;

    [Header("Attributes")]
    public float currentballSpeed;
    public float oldBallSpeed; 
    public float speedMultiplier = 0.1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        oldBallSpeed = currentballSpeed;
        gameManager = GameManager.Instance;
        gameManager.isBallMoving = false;
        gameManager.ResetPositionAction = ResetPosition;
        gameManager.StartGameAction = StartingForce;
    }

    void Update()
    {
        if (!gameManager.isBallMoving)
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
        rb.velocity = Vector2.zero;
        currentballSpeed = oldBallSpeed;
        
    }

    public void StartingForce()
    {
        GameManager.Instance.isBallMoving = true;

        var bounceRight = Random.value > 0.5f;
        float xDir = bounceRight ? 1f : -1f;
        float yDir = 0f;

        Vector2 initialDirection = new Vector2(xDir, yDir).normalized;
        rb.velocity = initialDirection * currentballSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SFXManager.Instance.PlaySound(SFXManager.Instance.collisionSound);

            float hitPoint = (transform.position.y - collision.transform.position.y) / collision.gameObject.transform.localScale.y;


            float bounceAngleMin = hitPoint * Random.Range(35f,45f);
            float bounceAngleMax = hitPoint * Random.Range(-45f,-35f);

            float radians = hitPoint >= 0.5f ? bounceAngleMax * Mathf.Deg2Rad : bounceAngleMin * Mathf.Deg2Rad;
            
            Vector2 newDirection = new Vector2(Mathf.Sign(rb.velocity.x), Mathf.Sin(radians)).normalized;

            currentballSpeed += currentballSpeed * speedMultiplier;

            rb.velocity = newDirection * currentballSpeed;


        }

        if (collision.gameObject.CompareTag("Bounds"))
        {
            rb.velocity += rb.velocity.normalized * -rb.velocity.y * speedMultiplier * Time.deltaTime;
        }
    }
}
