using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardShoulder : MonoBehaviour
{
    private ScreenShake shaker;

    private void Start()
    {
        shaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //if(!shaker.onShoulder)
            //{
                shaker.onShoulder = true;
                StartCoroutine(shaker.Shake(0.4f, 0.9f));
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //shaker.EndShake();
            //shaker.onShoulder = false;
        }
    }
}
