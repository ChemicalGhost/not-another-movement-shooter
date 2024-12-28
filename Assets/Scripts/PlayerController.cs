using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] Transform playerCamera;
    [SerializeField] CinemachineFreeLook cineCamera;
    [SerializeField] float jumpPower = 5.0f;

    [SerializeField] float playerSpeed = 2.0f;

    void LateUpdate()
    {
        MovePlayer();
        MouseRotatePlayer();
    }

    private float GetMouseInput(string axis)
    {
        return Input.GetAxis(axis) * playerSpeed * Time.deltaTime;
    }
    // Start is called before the first frame update

    private void MovePlayer()
    {
        float moveX = GetMouseInput("Horizontal");
        float moveY = GetMouseInput("Vertical");
        float jump = GetMouseInput("Jump");
        // string keyCode = Input.inputString;
        // if (!string.IsNullOrEmpty(keyCode))
        // {
        //     Debug.Log("CODE" + keyCode);
        // }

        //Enable player Jump
        if (jump > 0)
        {
            Debug.Log(jump);
            player.position += player.transform.up * jumpPower;
        }

        float cameraFov = cineCamera.m_Lens.FieldOfView;
        //Adjust camera FOV to simulate sense of speed
        if (playerSpeed > 3.0f && cameraFov < 50.0f)
        {
            cineCamera.m_Lens.FieldOfView += 2.0f * Time.deltaTime;
        }
        if (playerSpeed < 10.0f && cameraFov >= 50.0f)
        {
            cineCamera.m_Lens.FieldOfView -= 2.0f * Time.deltaTime;
        }

        //This is for Up and Down Movement
        switch (moveY)
        {
            case > 0:
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        if (playerSpeed <= 20.0f)
                        {
                            playerSpeed += 10.0f * Time.deltaTime;

                        }
                    }
                    else
                    {
                        if (playerSpeed > 2.0f)
                        {
                            playerSpeed -= 6.0f * Time.deltaTime; //Decelerate player's top speed
                        }
                        if (playerSpeed < 2.0f)
                        {
                            playerSpeed = 2;
                        }
                    }

                    player.position += player.transform.forward * moveY;
                }
                break;
            case < 0:
                {
                    player.position += player.transform.forward * moveY;
                }
                break;
            case 0:
                {
                    playerSpeed = 2;
                }
                break;
        }
        //This is for Left and Right Movement
        switch (moveX)
        {
            case > 0:
                {
                    player.position += player.transform.right * moveX;
                }
                break;
            case < 0:
                {
                    player.position += player.transform.right * moveX;
                }
                break;
        }

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

}
