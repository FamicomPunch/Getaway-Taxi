using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    public GameObject vehicleHolder;
    private GameManager gameManager;
    private Animator anim;
    private GameObject vehicle = null;
    private bool animRunning = false, canRun = false, overlap = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }

    public void EndRamp()
    {
        Debug.Log("EndRampForCar");
        vehicle.transform.SetParent(null);
        vehicle.GetComponent<PlayerController>().enabled = true;
        vehicle = null;
        gameManager.getOffRamp();
    }

    void Update()
    {
        if (canRun && !overlap)
        {
            vehicle.transform.position = Vector3.MoveTowards(vehicle.transform.position, vehicleHolder.transform.position, gameManager.speed / 2 * Time.deltaTime);
            if (Mathf.Abs(gameManager.speed - gameManager.defaultSpeed) > 0.1f)
                gameManager.speed += (gameManager.speed > gameManager.defaultSpeed ? Time.deltaTime : -Time.deltaTime);
            if (Vector3.Magnitude(vehicle.transform.position - vehicleHolder.transform.position) < 0.0001f)
            {
                Debug.Log("Overlapped!");
                overlap = true;
                vehicle.transform.position = vehicleHolder.transform.position;
                gameManager.speed = gameManager.defaultSpeed;
            }
            else
                Debug.Log("DistLeft: " + Vector3.Magnitude(vehicle.transform.position - vehicleHolder.transform.position));
        }
        if (canRun && !animRunning)
        {
            switch (gameManager.moveDir)
            {
                case GameManager.spawnDir.North: // North
                    {
                        if (vehicleHolder.transform.position.y <= 1f)
                            playAnim();
                        break;
                    }
                case GameManager.spawnDir.East: // East
                    {
                        if (vehicleHolder.transform.position.x <= 1f)
                            playAnim();
                        break;
                    }
                case GameManager.spawnDir.South: // South
                    {
                        if (vehicleHolder.transform.position.y >= -1f)
                            playAnim();
                        break;
                    }
                case GameManager.spawnDir.West: // West
                    {
                        if (vehicleHolder.transform.position.x >= -1f)
                            playAnim();
                        break;
                    }
                default:
                    Debug.Log("Anim broke");
                    return;
            }
        }
    }

    void playAnim()
    {
        anim.SetTrigger("OnRamp");
        animRunning = true;
        Debug.Break();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("ON RAMP");
            vehicle = other.gameObject;
            vehicle.GetComponent<PlayerController>().enabled = false;
            vehicle.transform.SetParent(vehicleHolder.transform);
            //vehicle.transform.position = vehicleHolder.transform.position;
            canRun = true;
            gameManager.getOnRamp();
            Debug.Log("StartDistLeft: " + Vector3.Magnitude(vehicle.transform.position - vehicleHolder.transform.position));
        }
    }
}
