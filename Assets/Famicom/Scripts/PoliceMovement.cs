using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovement : CarMovement
{
    public GameObject player;
    public bool test;
    bool leftLight, rightLight;
    Light[] lightList;
    public float lightSwap = 10f, lightIntensity = 8f;
    float lightTimer;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Direction = manager.Direction;
        speed = manager.speed * 1.5f;
        player = GameObject.Find("Player");

        lightList = gameObject.GetComponentsInChildren<Light>();
        lightList[0].intensity = Random.Range(0,5f);
        lightTimer = lightList[0].intensity/lightSwap;
        lightList[1].intensity = 5f - lightList[0].intensity;
        leftLight = Random.value > 0.5f ? true:false;
        rightLight = !leftLight;

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += movement(Direction) + moveToPlayer(player) * Time.deltaTime + tempAddSpeed;
        lightList[0].intensity += Time.deltaTime * (leftLight ? lightIntensity * lightSwap : lightIntensity * -lightSwap);
        if (lightList[0].intensity >= lightIntensity)
            leftLight = false;
        else if (lightList[0].intensity == 0)
            leftLight = true;
        lightList[1].intensity += Time.deltaTime * (rightLight ? lightIntensity * lightSwap : lightIntensity * -lightSwap);
        if (lightList[1].intensity >= lightIntensity)
            rightLight = false;
        else if (lightList[1].intensity == 0)
            rightLight = true;
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
        return hunt;
    }
}
