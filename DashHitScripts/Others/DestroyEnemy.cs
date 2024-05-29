using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    PlayerMovement pm;
    int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI points;
    public GameObject gameOverUI;
    public GameObject scoreUI;
    public GameObject enemyExplosionPrefab;
    public GameObject playerExplosionPrefab;

    public AudioSource audioSource;
    public AudioClip destroyAudioClip;

    public TextMeshProUGUI highScore;

    //public Rigidbody2D enemyRb;

    private void Start()
    {
        gameOverUI.SetActive(false);
        pm = GetComponent<PlayerMovement>();
        CheckHighScore();
        UpdateHighScore();
        //enemyRb = GameObject.Find("Enemey").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        points.text = $"Score : {score.ToString()}";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (pm.isDashing)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                PlayEnemyExplosionEffect();
                collision.gameObject.SetActive(false);
                score += 50;

                CheckHighScore();
                UpdateHighScore();

                audioSource.PlayOneShot(destroyAudioClip);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                audioSource.PlayOneShot(destroyAudioClip);

                PlayPlayerExplosionEffect();
                gameObject.SetActive(false);

                GameOver();

                scoreUI.SetActive(false);
                gameOverUI.SetActive(true);
            }
            //else if (collision.gameObject.CompareTag("PowerUp"))
            //{
            //    gameObject.SetActive(false);

            //    enemyRb.constraints = RigidbodyConstraints2D.FreezePosition;
            //}
        }
    }

    private void PlayEnemyExplosionEffect()
    {
        if (enemyExplosionPrefab != null)
        {
            GameObject explosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);

            Destroy(explosion, 0.6f);
        }
    }

    private void PlayPlayerExplosionEffect()
    {
        if (enemyExplosionPrefab != null)
        {
            GameObject explosion = Instantiate(playerExplosionPrefab, transform.position, Quaternion.identity);

            Destroy(explosion, 0.6f);
        }
    }

    void GameOver()
    {
        Time.timeScale = 1f;
    }



    void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    void UpdateHighScore()
    {
        highScore.text = $"High Score : {PlayerPrefs.GetInt("HighScore", 0)}"; 
    }
}
