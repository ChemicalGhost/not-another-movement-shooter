using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class FlyerScript : MonoBehaviour
{

    [Header("Flight Settings")]
    [SerializeField] private bool isFlying = false;
    [SerializeField] private bool isHovering = false;
    [SerializeField] private float flySpeed = 5.0f;
    [SerializeField] private float flyAscendSpeed = 4.0f;
    [SerializeField] private float flyDescendSpeed = 3.0f;
    [SerializeField] private float hoverAmplitude = 0.3f;
    [SerializeField] private float hoverFrequency = 1.0f;
    [SerializeField] private float groundCheckDistance = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    private float hoverTime = 0f;
    private Vector3 lastHoverPosition;

    [SerializeField] Transform player;
    [SerializeField] Transform playerCamera;
    [SerializeField] CinemachineFreeLook cineCamera;
    [SerializeField] float jumpPower = 5.0f;

    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] bool isRunner = false;
    [SerializeField] bool isFlyer = false;



    void LateUpdate()
    {

        MouseRotatePlayer();
    }



    private void MouseRotatePlayer()
    {
        Vector3 cameraForward = playerCamera.forward;
        Vector3 upVec = playerCamera.up;
        cameraForward.y = 0;
        cameraForward.Normalize();
        Quaternion playerQuat = Quaternion.LookRotation(cameraForward);

        //Can be applied somewhere else Later DO NOT CLEAN YET
        // Quaternion playerQuat = Quaternion.LookRotation(cameraForward, new Vector3(0, 1, 0).normalized);

        player.rotation = playerQuat;

    }

    // Call this in your LateUpdate method
    public void HandleFlying()
    {
        // Toggle flying mode on/off with F key
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlying = !isFlying;

            if (isFlying)
            {
                // Enter flying mode
                lastHoverPosition = player.position;
                isHovering = true;
            }
            else
            {
                // Exit flying mode and land
                isHovering = false;
                Land();
            }
        }

        if (isFlying)
        {
            // Get vertical flight input (could reuse jump axis or use a new one)
            float verticalFlight = 0f;
            if (Input.GetKey(KeyCode.Space))
            {
                verticalFlight = flyAscendSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                verticalFlight = -flyDescendSpeed * Time.deltaTime;
            }

            // Apply vertical flight movement
            player.position += Vector3.up * verticalFlight;

            // Detect if player is moving horizontally
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            bool isMovingHorizontally = (Mathf.Abs(moveX) > 0.1f || Mathf.Abs(moveY) > 0.1f);

            // Update hovering state
            if (!isMovingHorizontally && isFlying)
            {
                if (!isHovering)
                {
                    isHovering = true;
                    lastHoverPosition = player.position;
                }

                // Apply hover effect with sin wave
                hoverTime += Time.deltaTime;
                float hoverOffset = Mathf.Sin(hoverTime * hoverFrequency * Mathf.PI) * hoverAmplitude;

                // Store the current position, apply hover effect to y component only
                Vector3 currentPos = player.position;
                currentPos.y = lastHoverPosition.y + hoverOffset;
                player.position = currentPos;
            }
            else if (isMovingHorizontally)
            {
                isHovering = false;
                lastHoverPosition = player.position;
            }

            // Check if player should automatically land when close to ground
            if (Input.GetKeyDown(KeyCode.G))
            {
                Land();
                isFlying = false;
                isHovering = false;
            }
        }
    }

    private void Land()
    {
        // Cast ray downward to find ground
        RaycastHit hit;
        if (Physics.Raycast(player.position, Vector3.down, out hit, 100f, groundLayer))
        {
            // Smoothly move player to ground position
            StartCoroutine(SmoothLand(hit.point + Vector3.up * 0.1f)); // Small offset to prevent clipping
        }
    }

    private IEnumerator SmoothLand(Vector3 targetPosition)
    {
        Vector3 startPosition = player.position;
        float duration = 0.5f; // Landing animation duration
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            player.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        player.position = targetPosition;
    }

    // Modify your MovePlayer function to account for flying mode
    private void MovePlayer()
    {
        // Your existing movement code...

        // If in flying mode, use different movement logic
        if (isFlying)
        {
            float moveX = GetMouseInput("Horizontal");
            float moveY = GetMouseInput("Vertical");

            // Create movement vector and normalize if necessary
            Vector3 moveDirection = (player.transform.forward * moveY) + (player.transform.right * moveX);
            if (moveDirection.magnitude > 1.0f)
            {
                moveDirection.Normalize();
            }

            // Apply movement with fly speed
            player.position += moveDirection * flySpeed * Time.deltaTime;

            // Skip the rest of the normal movement code
            return;
        }

        // Rest of your existing movement code for when not flying...
    }


    ///////////////////
    //HELPER FUNCTIONS
    /////////////////

    private float GetMouseInput(string axis)
    {
        return Input.GetAxis(axis) * playerSpeed * Time.deltaTime;
    }


}
