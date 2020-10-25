using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    public GameObject vehicleHolder;

    private Animator anim;
    private GameObject vehicle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void EndRamp()
    {
        vehicle.transform.SetParent(null);
        vehicle.GetComponent<PlayerController>().enabled = true;
        vehicle = null;
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
        }
    }
}
