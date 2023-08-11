using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private GameObject levelDesign;

    private int attempt;

    private WaitForSeconds waitForRestart;

    public delegate void OnAttemptChange(int attempt);
    public static event OnAttemptChange RefreshUI;

    private void Start()
    {
        waitForRestart = new WaitForSeconds(2.0f);
        
        attempt = 1;
        RefreshUI?.Invoke(attempt);
    }

    private IEnumerator GoBackToBeginning()
    {
        yield return waitForRestart;

        ResetGame();
    }

    private void ResetGame()
    {
        levelDesign.transform.position = Vector3.zero;
        levelDesign.GetComponent<MoveLeft>().StartMovement();
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
