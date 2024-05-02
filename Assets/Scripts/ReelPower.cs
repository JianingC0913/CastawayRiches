using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelPower : MonoBehaviour
{

    public Transform fishingLine;
    public GameObject reelPowerIndicator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hook"))
        {
            // get fishingline script using GetComponent
            FishingLine playerLine = fishingLine.GetComponent<FishingLine>();

            Debug.Log("reel power up hit");

            // call func to increase reel speed
            playerLine.SpeedReelPower();
            reelPowerIndicator.SetActive(true);

            Destroy(gameObject);
        }
    }
}
