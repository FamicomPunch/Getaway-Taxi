using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovement : CarMovement
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
        speed *= 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += movement(Direction) + moveToPlayer(player) * Time.deltaTime;
    }

    Vector3 moveToPlayer(GameObject player)
    {
        Vector3 hunt = (player.transform.position - gameObject.transform.position).normalized;
        hunt.x *= 4;
        return hunt;
    }
}
