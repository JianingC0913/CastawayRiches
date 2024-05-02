using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float horizontal = 0.0f;
    public float playerMoveSpeed = 4.0f;
    private bool isFacingRight = true;

    private float timer = 5f;
    public bool powerupActive = false;
    public GameObject speedPowerIndicator;

    // [SerializeField] private SpriteRenderer player;
    [SerializeField] private SpriteRenderer boat;

    [SerializeField] private Rigidbody2D rb; // player rb

    void Start()
    {

    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * playerMoveSpeed;

        if ((isFacingRight && horizontal < 0.0f) || (!isFacingRight && horizontal > 0.0f))
        {
            isFacingRight = !isFacingRight;
        }

        if (powerupActive)
        {
            // Debug.Log("power up active");
            timer -= Time.deltaTime;
            // Debug.Log("timer:" + timer);

            if (timer <= 0)
            {
                powerupActive = false;
                playerMoveSpeed /= 2;
                speedPowerIndicator.SetActive(false);
                timer = 5f;
                Debug.Log("deactivated power up, player speed lowered");
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (horizontal<0 && screenPosition.x > - 525 || horizontal>0 && screenPosition.x < Screen.width - 580)
            rb.velocity = new Vector2(horizontal, rb.velocity.y); // update horizontal velocity
        else 
            rb.velocity = new Vector2(0, rb.velocity.y);

        // sprite flipping
        boat.flipX = !isFacingRight;
    }

    // double player's speed for ~10s
    public void speedPowerUp()
    {
        if (!powerupActive)
        {
            powerupActive = true;
            playerMoveSpeed *= 2;
            Debug.Log("player speed doubled");
        }
    }
}