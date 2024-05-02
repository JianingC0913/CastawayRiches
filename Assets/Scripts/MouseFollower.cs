using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    //private Rigidbody2D rb;
    //public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePresent)
        {
            Vector3 mousePos = Input.mousePosition;

            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;

            transform.position = mousePos;
        }
        float verticalInput = Input.GetAxis("Vertical");

        //float move = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(move * 5f, verticalInput * 5f);
    }
}
