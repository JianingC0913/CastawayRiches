using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;

public class HookBehavior : MonoBehaviour
{

    public FishingLine fishingLine;
    private Transform fishingLineBottom;


    // Start is called before the first frame update
    void Start()
    {
        fishingLineBottom = transform.root.Find("SwingingLine/FishingLine/FishingLineBottom");


        if (fishingLineBottom == null)
        {
            UnityEngine.Debug.LogError("Fishing line bottom transform not found!");
        }
        else
        {
            // Move the hook to the bottom of the fishing line
            transform.position = fishingLineBottom.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = new Vector3(transform.position.x, , transform.position.z);
        if (fishingLineBottom != null)
        {
            transform.position = fishingLineBottom.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FishingLine fishingLine = GetComponentInParent<FishingLine>(); // Get the FishingLine script
        if (fishingLine != null)
        {
            // Debug.Log("hook hit fish");
            fishingLine.SetIsSpaceKeyPressed(false); // Stop extending the fishing line
        }

    }
}