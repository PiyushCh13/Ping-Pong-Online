using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public Transform ball;        // Assign the ball in the Inspector
    public Rigidbody2D ballRb;    // Reference to ball’s Rigidbody2D
    public float speed = 5f;      // AI paddle speed

    void Update()
    {
        if (ball == null || ballRb == null) return;

        // AI paddle's right direction (facing left in a typical Pong game)
        Vector2 aiDirection = Vector2.right; // Assuming AI is on the right side

        // Dot product between AI direction and ball velocity
        float dot = Vector2.Dot(aiDirection, ballRb.velocity);

        //Debug.Log(dot);

        // Move AI only if the ball is moving towards it
        if (dot > 0)
        {
            float targetY = ball.position.y;
            float newY = Mathf.Lerp(transform.position.y, targetY, speed * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
