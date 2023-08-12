using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public delegate void OnGameFinish();
    public static event OnGameFinish GameFinished;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Player"))
        {
            GameFinished?.Invoke();
        }
    }
}
