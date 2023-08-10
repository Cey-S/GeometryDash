using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public delegate void OnPlayerCollide();
    public static event OnPlayerCollide playerCollided;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollided?.Invoke();
        }
    }
}
