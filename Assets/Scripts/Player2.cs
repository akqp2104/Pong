using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private float ybound = 8.45f;
    private void Update()
    {
        int direction = InputManager.GetInstance().Player2GetDirection();

        Vector2 paddlePosition = transform.position;
        paddlePosition.y = Mathf.Clamp(paddlePosition.y + direction * speed * Time.deltaTime, -ybound, ybound);
        transform.position = paddlePosition;
    }
}
