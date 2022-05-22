using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;
    public int lives = 3;
    public int score = 0;
    public Text scoreText;
    public Text livesText;
    public AudioSource explosionAudioSource;

    public void AsteroidDestroyed(Asteroid asteroid) {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();
        explosionAudioSource.Play();

        if (asteroid.size < 0.75f) {
            this.score += 100;
            scoreText.text = score.ToString();
        } else if (asteroid.size < 1.2f) {
            this.score += 50;
            scoreText.text = score.ToString();
        } else {
            this.score += 25;
            scoreText.text = score.ToString();
        }
    }

    public void PlayerDied() {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        explosionAudioSource.Play();
        
        this.lives--;
        livesText.text = lives.ToString();


        if (this.lives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn() {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);
        
        Invoke(nameof(TurnOnCollisions), this.respawnInvulnerabilityTime);
    }

    private void TurnOnCollisions() {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver() {
        this.lives = 3;
        livesText.text = lives.ToString();
        this.score = 0;
        scoreText.text = score.ToString();

        Invoke(nameof(Respawn), this.respawnTime);
    }

}
