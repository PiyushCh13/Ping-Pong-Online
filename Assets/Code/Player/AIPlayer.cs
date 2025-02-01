using UnityEngine;

public class AIPaddle : PlayerBaseClass
{
    public Transform ball;
    private float ai_Efficiency;

    protected override void Awake()
    {
        base.Awake();

    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void MovePlayer()
    {
        if (ball.GetComponent<Rigidbody2D>().velocity.x > 1)
        {
            ai_Efficiency = Random.Range(0.1f, 0.6f);
        }

        //else if (ball.GetComponent<Rigidbody2D>().velocity.x < -1f)
        //{
        //    ai_Efficiency = Random.Range(-0.7f, -0.3f);
        //}

        BallFollow(ai_Efficiency);
    }

    private void BallFollow(float efficiency) 
    {
        float targetY = ball.position.y;
        float newY = Mathf.Lerp(transform.position.y, targetY, moveSpeed * Time.deltaTime * efficiency);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
