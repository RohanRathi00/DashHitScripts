using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomout : MonoBehaviour
{
    [SerializeField] float zoomFactor = 4.0f;
    [SerializeField] float zoomSpeed = 3.0f;
    private float originalSize = 6.0f;
    Camera mainCamera;
    PlayerMovement playerMovement;
    float targetSize;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        mainCamera = GetComponent<Camera>();
        originalSize = mainCamera.orthographicSize;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space) && playerMovement.zoom)
        {
            Zoom();
        }
        if (playerMovement.zoom == false)
        {
            ResetZoom();
        }
    }

    void Zoom()
    {
        if(playerMovement.zoom)
        {
            targetSize = originalSize + zoomFactor;
        }
        mainCamera.orthographicSize = Mathf.Lerp(originalSize, targetSize, Time.deltaTime * zoomSpeed);
    }

    void ResetZoom()
    {
        targetSize = originalSize;

        mainCamera.orthographicSize = Mathf.Lerp(originalSize, targetSize, Time.deltaTime * zoomSpeed);
    }
}
