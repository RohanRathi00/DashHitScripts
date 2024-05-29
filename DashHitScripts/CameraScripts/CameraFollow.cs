using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed;
    public Transform target;

    public AudioSource audioSource;
    public AudioClip BgMusicAudioClip;

    private void Start()
    {
        audioSource.PlayOneShot(BgMusicAudioClip);
    }

    private void FixedUpdate()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
