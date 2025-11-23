using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private float speedIncrement = 0.25f;
    private float ybound = 8.45f;

    private void Start()
    {
        StartCoroutine(IncreaseSpeed());
    }

    private IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(10);
        speed += speedIncrement;
    }

    private void Update()
    {
        int direction = InputManager.GetInstance().Player1GetDirection();

        Vector2 paddlePosition = transform.position;
        paddlePosition.y = Mathf.Clamp(paddlePosition.y + direction * speed * Time.deltaTime, -ybound, ybound);
        transform.position = paddlePosition;
    }
}
