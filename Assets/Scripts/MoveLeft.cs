using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private bool isMoving = true;

    [SerializeField] private float moveSpeed = 10.0f;

    private void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }

    public void StartMovement()
    {
        isMoving = true;
    }

    private void StopMovement()
    {
        isMoving = false;
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
