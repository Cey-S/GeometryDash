using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private Text attemptText;
    [SerializeField] private Text OutOfCanvasPoint;
    [SerializeField] private float moveSpeed;
    private Vector3 originalTextPos;

    private void Start()
    {
        originalTextPos = attemptText.rectTransform.position;
    }

    private void MoveLeft()
    {
        attemptText.rectTransform.position = originalTextPos;
        LeanTween.move(attemptText.gameObject, OutOfCanvasPoint.rectTransform, moveSpeed);
    }

    private void RefreshAttemptText(int attempt)
    {
        attemptText.text = $"Attempt {attempt}";
        MoveLeft();
    }

    private void OnEnable()
    {
        GameManager.RefreshUI += RefreshAttemptText;
    }

    private void OnDisable()
    {
        GameManager.RefreshUI -= RefreshAttemptText;
    }
}
