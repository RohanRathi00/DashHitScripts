using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D player;
    public float moveSpeed = 3.0f;
    public Vector2 direction;
    public bool stopMovement;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        stopMovement = false;
    }

    private void Update()
    {
        foreach (Rigidbody2D rb in transform.GetComponentsInChildren<Rigidbody2D>())
        {
            if (player == null)
                return;

            direction = (player.position - rb.position).normalized;

            rb.velocity = direction * moveSpeed;

            if (stopMovement) 
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
