using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem explosionParticle;

    private int attempt;

    private WaitForSeconds waitForRestart;

    public delegate void OnResetLevel();
    public static event OnResetLevel ResetLevelDesign;

    public delegate void OnAttemptChange(int attempt);
    public static event OnAttemptChange RefreshUI;

    private void Start()
    {
        waitForRestart = new WaitForSeconds(2.0f);

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
        RefreshUI?.Invoke(attempt);
    }

    private IEnumerator GoBackToBeginning()
    {
        yield return waitForRestart;

        ResetGame();
    }

    private void ResetGame()
    {
        ResetLevelDesign?.Invoke();

        explosionParticle.gameObject.SetActive(false);
        player.gameObject.SetActive(true);

        attempt++;
        RefreshUI?.Invoke(attempt);
    }

    private void PlayerDeath()
    {
        explosionParticle.gameObject.SetActive(true);
        player.gameObject.SetActive(false);

        StartCoroutine(GoBackToBeginning());
    }

    private void OnEnable()
    {
        Obstacle.playerCollided += PlayerDeath;
    }

    private void OnDisable()
    {
        Obstacle.playerCollided -= PlayerDeath;
    }
}
