using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int speed;
    private float velocityMultiplayer = 1.05f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }

    public void Launch()
    {
        int velocityX = Random.Range(0, 2) == 0 ? 1 : -1;
        int velocityY = Random.Range(0, 2) == 0 ? 1 : -1;
        StartCoroutine(InitializeVelocity(velocityX, velocityY));
    }

    private IEnumerator InitializeVelocity(int velocityX, int velocityY)
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        rb.velocity = new Vector2(velocityX, velocityY) * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            rb.velocity *= velocityMultiplayer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal1"))
        {
            PointScored(2);
        }
        else if (collision.gameObject.CompareTag("Goal2"))
        {
            PointScored(1);
        }
    }

    private void PointScored(int player)
    {
        GameManager.GetInstance().AddScore(player);
    }
}
