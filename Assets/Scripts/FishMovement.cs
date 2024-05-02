using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 2f;
    public float baseSpeed = 2f;
    public float minSpeed = 1.95f;
    public float maxSpeed = 2.05f;
    public bool isRight = true;
    private Vector2 direction = Vector2.right;
    public float size = 1f;
    public float baseSize = 1f;
    public float minSize = 0.80f;
    public float maxSize = 1.20f;
    public float points = 100f;
    public float pointsMultiplier = 0.2f;
    public bool isSwitchHalfway = false;
    public bool isWavy = false;
    public float waveFrequency = 1.5f;
    public float waveAmplitude = 0.01f;
    private float waveOffset;

    private System.Action movementFunction;

    public Timer timer;

    public string fishType = "null";

    // Start is called before the first frame update
    void Start()
    {
        movementFunction = GetMovementFunction();

        if (isRight)
            transform.localScale = new Vector3(size, size, transform.localScale.z);
        else
            transform.localScale = new Vector3(-size, size, transform.localScale.z);
        fishType = "movement";
    }

    public void DeterminePoints()
    {
        float sizeRatio = (size - baseSize) / (maxSize - minSize);
        float speedRatio = (speed - baseSpeed) / (maxSpeed - minSpeed);
        points += (sizeRatio + speedRatio) * pointsMultiplier * points;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            // Move the fish horizontally
            movementFunction();

            // Check if the fish moves out of the screen
            if (IsOutOfScreen())
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collision detected");
        if (collision.gameObject.CompareTag("Hook"))
        {
            timer.AddPoints(points, transform.position);
            Destroy(gameObject);
        }
    }

    private System.Action GetMovementFunction()
    {
        direction = isRight ? Vector2.right : Vector2.left;
        if (isSwitchHalfway)
        {
            System.Action linePattern = GetLinePatternFunction();
            if (isRight)
                return () =>
                {
                    // GetLinePatternFunction()();
                    linePattern();
                    SwitchLeft();
                };
            else
                return () =>
                {
                    linePattern();
                    SwitchRight();
                };
        }
        // return () => transform.Translate(direction * speed * Time.deltaTime);
        return GetLinePatternFunction();
    }

    private System.Action GetLinePatternFunction()
    {
        // Debug.Log("isWavy: " + isWavy);
        if (isWavy)
            return () => MoveWithWave();
        else
            return () => transform.Translate(direction * speed * Time.deltaTime);
    }

    public void generateOffset()
    {
        waveOffset = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
    }

    void MoveWithWave()
    {
        float time = Time.time * waveFrequency + waveOffset;
        // Calculate vertical offset using sine wave function
        float yOffset = Mathf.Sin(time) * waveAmplitude;

        // Combine horizontal and vertical movement
        Vector2 movement = direction * speed * Time.deltaTime;
        movement.y += yOffset * Time.deltaTime;

        transform.Translate(movement);
    }

    void SwitchLeft()
    {
        //transform.Translate(Vector2.right * speed * Time.deltaTime);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x > Screen.width / 2)
        {
            isRight = false;
            isSwitchHalfway = false;
            transform.localScale = new Vector3(-size, size, transform.localScale.z);
            movementFunction = GetMovementFunction();
        }
    }

    void SwitchRight()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x < Screen.width / 2)
        {
            isRight = true;
            isSwitchHalfway = false;
            transform.localScale = new Vector3(size, size, transform.localScale.z);
            movementFunction = GetMovementFunction();
        }
    }

    bool IsOutOfScreen()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return screenPosition.x < -100 || screenPosition.x > Screen.width+100;
    }

}
