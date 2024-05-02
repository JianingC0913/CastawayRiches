using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SharkMovement : MonoBehaviour
{
    public float speed = 2f;
    public float baseSpeed = 2f;
    public float minSpeed = 1.95f;
    public float maxSpeed = 2.05f;
    public bool isRight = true;
    private Vector2 direction = Vector2.right;
    public float size = 1f;
    public float baseSize = 4f;
    public float minSize = 4f;
    public float maxSize = 4f;
    public float points = 200f;
    public float pointsMultiplier = 0.2f;
    public bool isSwitchHalfway = false;
    public bool isWavy = false;
    public float waveFrequency = 1.5f;
    public float waveAmplitude = 0.01f;

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

    public void DeterminePoints(float points)
    {
        this.points = points;
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
            timer.AddPoints(-points, transform.position);
            //Destroy(gameObject);
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

    private float waveTimer;

    void MoveWithWave()
    {
        // Calculate vertical offset using sine wave function
        waveTimer++;
        float yOffset = Mathf.Sin(waveTimer * waveFrequency * 0.05f) * waveAmplitude * 2f;

        // Combine horizontal and vertical movement
        Vector2 movement = direction * speed * Time.deltaTime;
        movement.y += yOffset;

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
        return screenPosition.x < -100 || screenPosition.x > Screen.width + 100;
    }

}
