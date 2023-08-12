using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private bool isMoving;

    [SerializeField] private float moveSpeed = 10.0f;

    private void Start()
    {
        isMoving = false;    
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }

    private void ResetMovement()
    {
        transform.position = Vector3.zero;
        isMoving = true;
    }

    private void StopMovement()
    {
        isMoving = false;
    }

    private void OnEnable()
    {
        Obstacle.playerCollided += StopMovement;
        GameManager.ResetLevelDesign += ResetMovement;
    }

    private void OnDisable()
    {
        Obstacle.playerCollided -= StopMovement;
        GameManager.ResetLevelDesign -= ResetMovement;
    }
}
