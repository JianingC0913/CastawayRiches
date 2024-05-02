using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePower : MonoBehaviour
{

    // get timer class
    public Transform timerText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hook"))
        {
            // get timer script using GetComponent
            Timer timer = timerText.GetComponent<Timer>();
            Debug.Log("time power up hit");

            // call func to increase time
            timer.AddTimePowerup();

            Destroy(gameObject);
        }
    }
}
