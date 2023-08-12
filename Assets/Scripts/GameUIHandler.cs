using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private Text finishText;
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private Text attemptText;
    [SerializeField] private Text OutOfCanvasPoint;
    [SerializeField] private float moveSpeed;
    private Vector3 originalTextPos;

    private bool isPaused;

    private void Start()
    {
        pauseButton.SetActive(true);
        attemptText.gameObject.SetActive(true);
        originalTextPos = attemptText.rectTransform.position;

        finishPanel.SetActive(false);
        settingsPanel.SetActive(false);
        isPaused = false;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ToggleSettings()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            bgMusic.Play();
            settingsPanel.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            bgMusic.Pause();
            settingsPanel.SetActive(true);
            isPaused = true;
        }
    }

    private void DisplayFinishPanel(int attempt)
    {
        pauseButton.SetActive(false);
        finishPanel.SetActive(true);
        finishText.text = $"Attempts: {attempt}";
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
        GameManager.RefreshAttemptText += RefreshAttemptText;
        GameManager.DisplayFinishPanel += DisplayFinishPanel;
    }

    private void OnDisable()
    {
        GameManager.RefreshAttemptText -= RefreshAttemptText;
        GameManager.DisplayFinishPanel -= DisplayFinishPanel;
    }
}
