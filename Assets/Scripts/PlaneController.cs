using System;
using Cinemachine;
using TMPro;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public CinemachineVirtualCamera sideScrollCamera, fpsCamera;
    public GameObject gameOverScreen;
    public TMP_Text coinCounter, bestCoinScore;
    public Rigidbody body;
    public float moveSpeed;
    public float rotateSpeed;


    private int coinCount;
    private bool isFpsCamera;
    
    
    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleCamera();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            gameOverScreen.SetActive(true);
            int oldBestCoinScore = PlayerPrefs.GetInt("Best_Coin_Score", 0);
            
            if (coinCount > oldBestCoinScore)
            {
                PlayerPrefs.SetInt("Best_Coin_Score", coinCount);
                bestCoinScore.text = "Best Coin Score: " + coinCount.ToString();
            }
            
            else
            {
                bestCoinScore.text = "Best Coin Score: " + oldBestCoinScore.ToString();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinCount++;
            coinCounter.text = "Coin:" + coinCount.ToString();
        }
    }


    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            body.velocity = transform.forward * moveSpeed;
        }
        
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            body.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.velocity = -transform.forward * moveSpeed;
        }
        
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            body.velocity = Vector3.zero;
        }
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            body.angularVelocity = -transform.right * (rotateSpeed * Time.deltaTime);
        }
        
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            body.angularVelocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            body.angularVelocity = transform.right * (rotateSpeed * Time.deltaTime);
        }
        
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            body.angularVelocity = Vector3.zero;
        }
    }

    private void HandleCamera()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFpsCamera = !isFpsCamera;

            if (isFpsCamera)
            {
                fpsCamera.Priority = 1;
                sideScrollCamera.Priority = 0;
            }

            else
            {
                fpsCamera.Priority = 0;
                sideScrollCamera.Priority = 1;
            }
        }
    }
}
