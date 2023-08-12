using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource crashSound;

    private Vector3 playerStartPos;
    private int attempt;

    private WaitForSeconds waitForRestart;

    public enum GameMode
    {
        Running,
        Surfing
    }

    public static GameMode CurrentGameMode { get; private set; }
    public static bool IsGameRunning { get; private set; }

    public delegate void OnResetLevel();
    public static event OnResetLevel ResetLevelDesign;

    public delegate void OnUIChange(int attempt);
    public static event OnUIChange RefreshAttemptText;
    public static event OnUIChange DisplayFinishPanel;

    private void Start()
    {
        IsGameRunning = false;
        CurrentGameMode = GameMode.Running;

        waitForRestart = new WaitForSeconds(2.0f);

        playerStartPos = player.transform.position;
        attempt = 1;

        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        float lerpSpeed = 10.0f;

        Vector3 endPos = player.transform.position;
        Vector3 startPos = endPos + Vector3.left * 10.0f;
        float journeyLenght = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLenght;

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLenght;
            player.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }

        ResetLevelDesign?.Invoke();
        RefreshAttemptText?.Invoke(attempt);

        IsGameRunning = true;
    }

    private IEnumerator GoBackToBeginning()
    {
        yield return waitForRestart;

        ResetGame();
        IsGameRunning = true;
    }

    private void ResetGame()
    {
        ResetLevelDesign?.Invoke();

        explosionParticle.gameObject.SetActive(false);

        CurrentGameMode = GameMode.Running;
        player.GetComponent<Rigidbody2D>().gravityScale = Player.Gravity;
        player.transform.position = playerStartPos;
        player.gameObject.SetActive(true);

        bgMusic.Stop();
        bgMusic.Play();

        attempt++;
        RefreshAttemptText?.Invoke(attempt);
    }

    private void PlayerDeath()
    {
        IsGameRunning = false;

        crashSound.Play();
        explosionParticle.transform.position = player.transform.position;
        explosionParticle.gameObject.SetActive(true);
        player.gameObject.SetActive(false);

        StartCoroutine(GoBackToBeginning());
    }

    private void FinishGame()
    {
        IsGameRunning = false;
        DisplayFinishPanel?.Invoke(attempt);
    }

    private void ChangeGameMode()
    {
        switch (CurrentGameMode)
        {
            case GameMode.Running:
                CurrentGameMode = GameMode.Surfing;
                break;
            case GameMode.Surfing:
                CurrentGameMode = GameMode.Running;
                player.GetComponent<Rigidbody2D>().gravityScale = Player.Gravity;
                break;
        }
    }

    private void OnEnable()
    {
        Obstacle.playerCollided += PlayerDeath;
        Portal.portalEntered += ChangeGameMode;
        FinishLine.GameFinished += FinishGame;
    }

    private void OnDisable()
    {
        Obstacle.playerCollided -= PlayerDeath;
        Portal.portalEntered -= ChangeGameMode;
        FinishLine.GameFinished -= FinishGame;
    }
}
