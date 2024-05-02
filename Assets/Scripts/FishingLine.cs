using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class FishingLine : MonoBehaviour
{

    public float maxLength = 35f;
    public float minLength = 6f;
    public float currentLength = 5f;
    private bool isSpaceKeyPressed = false;
    private SwingingLine swingingline;

    // proportion of increaseSpeed to downwardsSpeed should ALWAYS be 10 : 1 (increaseSpeed / 10 = downwardsSpeed)
    public float increaseSpeed = 8f; // speed line is scaled
    public float downwardsSpeed = .8f;

    // save these to return to later
    private float originalYScale;

    [SerializeField] private Transform fishingLineBottom;

    public float timer = 5f;
    public bool reelPowerActive = false;
    public GameObject reelPowerIndicator;

    private System.Action swingFunction;

    // Start is called before the first frame update
    void Start()
    {
        swingFunction = GetSwingFunction();
        originalYScale = transform.localScale.y;
        swingingline = GetComponentInParent<SwingingLine>();
    }

    public void SetIsSpaceKeyPressed(bool value)
    {
        isSpaceKeyPressed = value;
    }

    public void Fishing()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpaceKeyPressed = true;
        }

        if (isSpaceKeyPressed)
        {
            // increase the length of the line
            currentLength += increaseSpeed * Time.deltaTime;
            transform.localScale = new UnityEngine.Vector3(transform.localScale.x, currentLength, transform.localScale.z);

            // change position to form illusion of line moving downwards
            transform.position += new UnityEngine.Vector3(0, -downwardsSpeed * Time.deltaTime, 0);

            // Move the fishing line bottom along with the fishing line
            //if (fishingLineBottom != null)
            //{
            //    UnityEngine.Vector3 bottomPosition = transform.position;
            //    bottomPosition.y -= currentLength / 2f; // Adjust this calculation based on the origin of the fishing line
            //    fishingLineBottom.position = bottomPosition;
            //}

            // stop expanding
            if (currentLength >= maxLength)
            {
                isSpaceKeyPressed = false;
            }

        }

        // reel back in
        if (!isSpaceKeyPressed)
        {
            if (currentLength > originalYScale)
            {
                currentLength -= increaseSpeed * Time.deltaTime;
                transform.localScale = new UnityEngine.Vector3(transform.localScale.x, currentLength, transform.localScale.z);

                transform.position += new UnityEngine.Vector3(0, downwardsSpeed * Time.deltaTime, 0);



                // Move the fishing line bottom along with the fishing line
                //if (fishingLineBottom != null)
                //{
                //    UnityEngine.Vector3 bottomPosition = transform.position;
                //    bottomPosition.y -= currentLength / 2f; // Adjust this calculation based on the origin of the fishing line
                //    fishingLineBottom.position = bottomPosition;
                //}
            }
            swingFunction();
            //if (currentLength < minLength)
            //{
            //    swingingline.ResetSwing();
            //    // UnityEngine.Debug.Log("swing reset");
            //}
            //if (currentLength == originalYScale)
            //{
            //    swingingline.ResetSwing();
            //    UnityEngine.Debug.Log("swing reset");
            //}

        }
    }

    private System.Action GetSwingFunction()
    {
        if (PlayerPrefs.GetString("Mode") == "Swinging")
        {
            return () => {
                if (currentLength < minLength)
                {
                    swingingline.ResetSwing();
                }
            };
        }
        else
        {
            return () => { };
        }
    }

        // Update is called once per frame
        void Update()
    {
        Fishing();

        //if (!isfishing)
        //{
        //    swingingline.ResetSwing();
        //    UnityEngine.Debug.Log("swing reset");
        //}

        if (reelPowerActive)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                reelPowerActive = false;
                increaseSpeed /= 2;
                downwardsSpeed /= 2;
                reelPowerIndicator.SetActive(false);
                timer = 5f;
                Debug.Log("reel speed power deactivated, back to /= 2");
            }
        }
    }

    public void SpeedReelPower()
    {
        if (!reelPowerActive)
        {
            reelPowerActive = true;
            increaseSpeed *= 2;
            downwardsSpeed *= 2;

            Debug.Log("reeling doubled");
        }
    }
}