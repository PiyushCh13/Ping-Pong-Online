using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public Transform ball;
    public float speed = 5f;
    public Rigidbody2D ballRb;  // Reference to ball's Rigidbody
    public float followThreshold = 0.2f;

    void Update()
    {
        if (ball == null || ballRb == null) return;

        // If ball is moving towards AI, predict where it will hit
        float targetY = ball.position.y;

        if (ballRb.velocity.x > 0)  // Ball moving towards AI
        {
            float timeToReachPaddle = Mathf.Abs((transform.position.x - ball.position.x) / ballRb.velocity.x);
            targetY = ball.position.y + (ballRb.velocity.y * timeToReachPaddle);
        }


        // Smooth movement towards the predicted position
        float newY = Mathf.Lerp(transform.position.y, targetY, speed * Time.deltaTime);

        if (Mathf.Abs(targetY - transform.position.y) > followThreshold)
        {
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
