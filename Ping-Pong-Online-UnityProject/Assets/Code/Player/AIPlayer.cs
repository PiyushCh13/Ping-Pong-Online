using UnityEngine;

public class AIPaddle : PlayerBaseClass
{
    public Transform ball;
    private float aiEfficiency;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        // You can add additional AI behaviors here if needed
    }

    protected override void MovePlayer()
    {
        float ballSpeedX = ball.GetComponent<Rigidbody2D>().linearVelocity.x;

        // If the ball is moving toward the AI (positive x-direction), be more responsive
        if (ballSpeedX > 0.5f)
        {
            // AI reacts quicker when the ball is approaching
            aiEfficiency = Random.Range(0.2f, 0.5f);
        }
        else
        {
            // AI reacts slower when the ball is moving away or slowly
            aiEfficiency = Random.Range(0.3f, 0.6f);
        }

        BallFollow(aiEfficiency);
    }


    private void BallFollow(float efficiency)
    {
        float targetY = ball.position.y;

        // Use efficiency to dynamically adjust the smoothTime (lower value = faster response)
        float smoothTime = Mathf.Lerp(0.05f, 0.3f, 1 - efficiency); // Invert efficiency for smoothing

        float smoothY = Mathf.SmoothDamp(transform.position.y, targetY, ref velocity.y, smoothTime, moveSpeed, Time.deltaTime);

        transform.position = new Vector3(transform.position.x, smoothY, transform.position.z);
    }


    // SmoothDamp requires a velocity reference to track smooth movement
    private Vector2 velocity = Vector2.zero;
}
