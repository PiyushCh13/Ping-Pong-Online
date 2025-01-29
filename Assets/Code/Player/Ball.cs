using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    public float ballSpeed;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Begin();
    }

    void Update()
    {
        
    }

    public void Begin()
    {
        var bounceRight = Random.value > 0.5f;
        var xDir = bounceRight ? 1f : -1f;
        var yDir = Random.Range(-1f, 1f);

        rb.velocity = new Vector3(xDir, yDir) * ballSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var hitpoint = (collision.contacts[0].point.y - collision.transform.position.y) / collision.transform.localScale.y; 
            float angle = hitpoint > 0f ? Random.Range(27f, 45f): Random.Range(-45f, -27f);
            float radians = angle * Mathf.Deg2Rad;
            Vector2 newDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            rb.AddForce(newDirection , ForceMode2D.Impulse);
        }
    }
}
