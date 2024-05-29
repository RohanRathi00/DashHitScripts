using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePowerup : MonoBehaviour
{
    public GameObject enemy;
    public EnemyController enemyController;
    public float powerUpDuration = 5.0f;
    public GameObject particleEffect;

    private void Start()
    {
        enemy = GameObject.Find("enemyGameObj");
        enemyController = enemy.GetComponent<EnemyController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(ActivatePowerup());
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public IEnumerator ActivatePowerup()
    {
        ExplosionEffect();

        foreach (Rigidbody2D rb in enemy.GetComponentsInChildren<Rigidbody2D>())
        {
            enemyController.stopMovement = true;
            rb.velocity = Vector2.zero;
        }

        yield return new WaitForSeconds(5f);

        enemyController.stopMovement = false;

        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void ExplosionEffect()
    {
        if (particleEffect != null)
        {
            GameObject explosion = Instantiate(particleEffect, transform.position, Quaternion.identity);

            Destroy(explosion, 0.6f);
        }
    }
}
