using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public Player player;

    private void PlayerDeath()
    {
        explosionParticle.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
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
