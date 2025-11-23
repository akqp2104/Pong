using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private float speedIncrement;
    private float ybound = 8.45f;

    [SerializeField] private GameObject ball;
    
    private void Start()
    {
        StartCoroutine(IncreaseSpeed());
    }

    private IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(5);
        speedIncrement = Random.Range(0f, 0.6f);
        speed += speedIncrement;
    }

    public void ResetSpeed()
    {
        speed = 10;
    }

    private void Update()
    {
        Vector2 currentPosition = transform.position;
        Vector2 ballPosition = ball.transform.position;

        if (currentPosition.y < ballPosition.y)
        {
            currentPosition.y = Mathf.Clamp(currentPosition.y + speed * Time.deltaTime, -ybound, ybound);
        }
        else if (currentPosition.y > ballPosition.y)
        {
            currentPosition.y = Mathf.Clamp(currentPosition.y - speed * Time.deltaTime, -ybound, ybound);
        }
        
        transform.position = currentPosition;
    }
}
