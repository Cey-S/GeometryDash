using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float moveSpeed;

    private void Start()
    {
        moveSpeed = 10.0f;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    private void StopMovement()
    {
        moveSpeed = 0;
    }

    private void OnEnable()
    {
        Obstacle.playerCollided += StopMovement;
    }

    private void OnDisable()
    {
        Obstacle.playerCollided -= StopMovement;
    }
}
