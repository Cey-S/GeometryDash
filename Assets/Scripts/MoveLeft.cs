using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;

    private void Update()
    {
        if (GameManager.IsGameRunning)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }

    private void ResetGameObject()
    {
        transform.position = Vector3.zero;
    }

    private void OnEnable()
    {
        GameManager.ResetLevelDesign += ResetGameObject;
    }

    private void OnDisable()
    {
        GameManager.ResetLevelDesign -= ResetGameObject;
    }
}
