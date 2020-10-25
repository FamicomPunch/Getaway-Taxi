using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gm;
    private GameManager.spawnDir Dir;
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
        Dir = gm.Direction;
        switch (Dir)
        {
            case GameManager.spawnDir.East:
                setSpeed(true, false);
                forceRotation(-90);
                break;
            case GameManager.spawnDir.North:
                setSpeed(false, false);
                forceRotation(0);
                break;
            case GameManager.spawnDir.South:
                setSpeed(false, true);
                forceRotation(180);
                break;
            case GameManager.spawnDir.West:
                setSpeed(true, true);
                forceRotation(90);
                break;
            default:
                Debug.Log("Broke Car Movement...");
                Debug.Break();
                return;
        }


        /*if (transform.position.x >= 1.50f)
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
        }*/
    }

    private void forceRotation(int Rotation)
    {
        Quaternion q = new Quaternion();
        //q.SetFromToRotation(transform.rotation.eulerAngles, Vector3.left * Rotation);
        transform.rotation = Quaternion.Euler(Vector3.forward * Rotation);
        //transform.Rotate(Vector3.back, Rotation);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    
    private void setSpeed(bool xAxis, bool inverse)
    {
        float min = -1.5f, max = 1.5f;
        if(!inverse)
        {
            if (xAxis)
            {
                if (transform.position.x >= max)
                {
                    speedMultiplier = (transform.position.x - 1) / 2;
                    gm.speed = gm.defaultSpeed + speedMultiplier;
                    return;
                }
                else if (transform.position.x <= min)
                {
                    speedMultiplier = Mathf.Abs(transform.position.x - 1) / 8;
                    gm.speed = gm.defaultSpeed - speedMultiplier;
                    return;
                }
            }
            else
            {
                if (transform.position.y >= max)
                {
                    speedMultiplier = (transform.position.y - 1) / 2;
                    gm.speed = gm.defaultSpeed + speedMultiplier;
                    return;
                }
                else if (transform.position.y <= min)
                {
                    speedMultiplier = Mathf.Abs(transform.position.y - 1) / 8;
                    gm.speed = gm.defaultSpeed - speedMultiplier;
                    return;
                }
            }
        }
        else
        {
            if (xAxis)
            {
                if (transform.position.x <= min)
                {
                    speedMultiplier = Mathf.Abs(transform.position.x + 1) / 2;
                    gm.speed = gm.defaultSpeed + speedMultiplier;
                    return;
                }
                else if (transform.position.x >= max)
                {
                    speedMultiplier = (transform.position.x + 1) / 8;
                    gm.speed = gm.defaultSpeed - speedMultiplier;
                    return;
                }
            }
            else
            {
                if (transform.position.y <= min)
                {
                    speedMultiplier = Mathf.Abs(transform.position.y + 1) / 2;
                    gm.speed = gm.defaultSpeed + speedMultiplier;
                    return;
                }
                else if (transform.position.y >= max)
                {
                    speedMultiplier = (transform.position.y + 1) / 8;
                    gm.speed = gm.defaultSpeed - speedMultiplier;
                    return;
                }
            }
        }
        gm.speed = gm.defaultSpeed;
    }
}
