using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gm;

    public float moveSpeed = 5f;
    public float speedMultiplier = 0f;

    public Rigidbody2D rb;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(transform.position.x >= 1.50f)
        {
            speedMultiplier = (transform.position.x - 1) / 2;
            gm.speed = gm.defaultSpeed + speedMultiplier;
        }
        else if (transform.position.x <= -1.50f)
        {
            speedMultiplier = Mathf.Abs(transform.position.x - 1) / 8;
            gm.speed = gm.defaultSpeed - speedMultiplier;
        }
        else
        {
            gm.speed = gm.defaultSpeed;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
