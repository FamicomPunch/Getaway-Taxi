using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovement : CarMovement
{
    public GameObject player;
    public bool test;
    bool leftLight, rightLight;
    Light[] lightList;
    float lightSwap;

    // Start is called before the first frame update
    void Start()
    {
        Direction = GameObject.Find("GameManager").GetComponent<GameManager>().Direction;
        Destroy(gameObject, 5);
        speed *= 1.5f;
        player = GameObject.Find("Player");

        lightList = gameObject.GetComponentsInChildren<Light>();
        lightList[0].intensity = Random.Range(0,1f);
        lightSwap = lightList[0].intensity/5;
        lightList[1].intensity = 1f - lightList[0].intensity;
        Debug.Log(lightList[0].intensity);
        Debug.Log(lightList[1].intensity);
        leftLight = true;
        rightLight = false;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += movement(Direction) + moveToPlayer(player) * Time.deltaTime;
        lightList[0].intensity += Time.deltaTime * (leftLight ? -5 : 5);
        lightList[1].intensity += Time.deltaTime * (rightLight ? -5 : 5);
        lightSwap -= Time.deltaTime;
        if(lightSwap <= 0)
        {
            leftLight = !leftLight;
            rightLight = !rightLight;
            lightSwap = 0.20f;
        }
        if (test)
            gameObject.transform.position = Vector3.zero;
    }

    Vector3 moveToPlayer(GameObject player)
    {
        Vector3 hunt = (player.transform.position - gameObject.transform.position).normalized;
        if (Direction.Equals(GameManager.spawnDir.East) || Direction.Equals(GameManager.spawnDir.West))
            hunt.y *= 4;
        else
            hunt.x *= 4;
        Debug.Log(hunt);
        return hunt;
    }
}
