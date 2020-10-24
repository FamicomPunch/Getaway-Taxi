using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovement : CarMovement
{
    public GameObject player;
    bool leftLightDim;
    Light[] lightList;

    // Start is called before the first frame update
    void Start()
    {
        Direction = GameObject.Find("GameManager").GetComponent<GameManager>().Direction;
        Destroy(gameObject, 5);
        speed *= 1.5f;
        player = GameObject.Find("Player");

        lightList = gameObject.GetComponentsInChildren<Light>();
        lightList[0].intensity = Random.value;
        lightList[1].intensity = 1f - lightList[0].intensity;
        leftLightDim = (Random.value > 0.5f) ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += movement(Direction) + moveToPlayer(player) * Time.deltaTime;
        lightList[0].intensity += Time.fixedDeltaTime * (leftLightDim ? -1 : 1);
        lightList[1].intensity += Time.fixedDeltaTime * (leftLightDim ? 1 : -1);
        leftLightDim = (lightList[0].intensity > 1 || lightList[0].intensity < 0) ? !leftLightDim : leftLightDim;
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
