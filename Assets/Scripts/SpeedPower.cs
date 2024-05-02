using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPower : MonoBehaviour
{
    public Transform player;
    public GameObject speedPowerIndicator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hook"))
        {
            PlayerMovement movement = player.GetComponent<PlayerMovement>();
            Debug.Log("speed power up hit");

            movement.speedPowerUp();
            speedPowerIndicator.SetActive(true);

            Destroy(gameObject);
        }
    }

}

