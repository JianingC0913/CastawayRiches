using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SwingingLine : MonoBehaviour
{

    //public float speed = 1.5f;
    //public float angleLim = 55f;
    //private float random = 0;

    //void Awake()
    //{
    //    random = Random.Range(0f, 1f);
    //}

    //void Update()
    //{
    //    float angle = angleLim * Mathf.Sin(Time.time + random * speed);
    //    transform.localRotation = Quaternion.Euler(0, 0, angle);
    //}

    public float swingSpeed = 65f; // Speed of swinging in degrees per second
    public float swingAmplitude = 40f; // Amplitude of swinging in degrees
    private FishingLine fishingLine; // Reference to the FishingLine component

    public bool isSwinging = true; // Flag to control swinging behavior

    // Start is called before the first frame update
    void Start()
    {
        fishingLine = GetComponentInChildren<FishingLine>(); // Get the FishingLine component
        if (PlayerPrefs.GetString("Mode") == "Swinging")
        {
            isSwinging = true;
        }
        else
        {
            isSwinging = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the swing line if swinging is enabled
        if (isSwinging)
        {
            float swingAngle = Mathf.Sin(Time.time * swingSpeed * Mathf.Deg2Rad) * swingAmplitude;
            transform.rotation = UnityEngine.Quaternion.Euler(0f, 0f, swingAngle);

        }

        // Handle extending the line when the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Stop swinging
            isSwinging = false;

            // Extend the line
            if (fishingLine != null)
            {
                fishingLine.Fishing();
            }
        }

    }

    // Method to reset swinging behavior
    public void ResetSwing()
    {
        isSwinging = true;
    }
}