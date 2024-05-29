using JetBrains.Annotations;
using NUnit.Framework.Internal.Filters;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    float xInput;
    float yInput;
    Vector2 moveDirection;
    //Vector2 mousePosition;

    public float moveSpeed = 5f;
    public float rotationSpeed;

    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    public bool isDashing;
    public bool canDash;
    public bool canUnlimitedDash;

    public Slider dashSlider;
    
    //float minValue = 0f;
    //float maxValue = 1.5f;
    //float currValue;
    
    public float progressRate = 0.1f;
    public float currentProgress = 0;

    public AudioSource audioSource;
    public AudioClip dashAudioClip;

    public GameObject powerupParticleEffect;

    public bool zoom;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        zoom = false;
        canUnlimitedDash = false;
        canDash = true;
    }

    private void Update()
    {
        if(isDashing)
        {
            return;
        }

        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        moveDirection = new Vector2(xInput, yInput).normalized;

        currentProgress = Mathf.Clamp(currentProgress, dashSlider.minValue, dashSlider.maxValue);
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (moveDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (dashSlider.value == 1f)
        {
            canDash = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
            audioSource.PlayOneShot(dashAudioClip);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canUnlimitedDash)
        {
            StartCoroutine(UnlimitedDash());
            audioSource.PlayOneShot(dashAudioClip);
        }

        DashProgressBar();
        //DashSlider();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        //Vector2 aimDirection = mousePosition - rb.position;
        //float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = aimAngle;
    }

    public IEnumerator Dash()
    {
        zoom = true;
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);

        currentProgress = Mathf.Lerp(dashSlider.value, dashSlider.minValue, 1f);

        //currentProgress = 0.0f;
        //currentProgress -= dashCooldown * Time.deltaTime;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        zoom = false;

        //yield return new WaitForSeconds(dashCooldown);
    }

    public IEnumerator UnlimitedDash()
    {
        isDashing = true;
        canDash = true;

        if (isDashing) 
        {
            rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);

            yield return new WaitForSeconds(0.25f);

            isDashing = false;
        }

        yield return new WaitForSeconds(1);

        canDash = false;
        canUnlimitedDash = false;
    }

    //void DashSlider()
    //{
    //    while(Time.deltaTime < 1.5f && canDash == true)
    //    {
    //        dashSlider.value += Time.deltaTime * 0.3f;
    //    }
    //}

    void DashProgressBar()
    {
       currentProgress += dashCooldown * Time.deltaTime;
       dashSlider.value = currentProgress;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            canUnlimitedDash = true;
            ExplosionEffect();
        }
    }

    private void ExplosionEffect()
    {
        if (powerupParticleEffect != null)
        {
            GameObject explosion = Instantiate(powerupParticleEffect, transform.position, Quaternion.identity);

            Destroy(explosion, 0.6f);
        }
    }
}
