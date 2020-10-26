using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    public GameObject vehicleHolder;
    private GameManager gameManager;
    private Animator anim;
    private GameObject vehicle = null;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }

    public void EndRamp()
    {
        vehicle.transform.SetParent(null);
        vehicle.GetComponent<PlayerController>().enabled = true;
        vehicle = null;
        gameManager.getOffRamp();
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("ON RAMP");
            vehicle = other.gameObject;
            vehicle.GetComponent<PlayerController>().enabled = false;
            vehicle.transform.SetParent(vehicleHolder.transform);
            vehicle.transform.position = vehicleHolder.transform.position;
            anim.SetTrigger("OnRamp");
            gameManager.getOnRamp();
        }
    }
}
