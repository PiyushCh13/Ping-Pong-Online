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
        rb.linearVelocity = Vector2.zero;
        currentballSpeed = oldBallSpeed;
        gameManager.isBallMoving = false;
        FindFirstObjectByType<Player>().hasStartedGame = false;
    }

    public void StartingForce()
    {
        Debug.Log("StartingForce");
        GameManager.Instance.isBallMoving = true;

        var bounceRight = Random.value > 0.5f;
        float xDir = bounceRight ? 1f : -1f;
        float yDir = 0f;

        Vector2 initialDirection = new Vector2(xDir, yDir).normalized;
        rb.linearVelocity = initialDirection * currentballSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SFXManager.Instance.PlaySound(SFXManager.Instance.collisionSound);

            float hitPoint = (transform.position.y - collision.transform.position.y) / collision.gameObject.transform.localScale.y;


            float bounceAngleMin = hitPoint * Random.Range(35f, 45f);
            float bounceAngleMax = hitPoint * Random.Range(-45f, -35f);

            float angle = hitPoint * 45f; // Max angle
            angle = Mathf.Clamp(angle, bounceAngleMin, bounceAngleMax);

            float radians = angle * Mathf.Deg2Rad;
            Vector2 newDirection = new Vector2(Mathf.Sign(rb.linearVelocity.x), Mathf.Sin(radians)).normalized;

            currentballSpeed += currentballSpeed * speedMultiplier;

            rb.linearVelocity = newDirection * currentballSpeed;


        }

        if (collision.gameObject.CompareTag("Bounds"))
        {
            rb.linearVelocity += rb.linearVelocity.normalized * -rb.linearVelocity.y * speedMultiplier * Time.deltaTime;
        }
    }
}
