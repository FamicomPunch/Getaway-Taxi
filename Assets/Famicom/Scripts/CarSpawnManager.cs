using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    GameManager.spawnDir Direction;
    public GameObject spawner;
    public int highwayLanes = 6;
    public GameObject[] spawns;
    public bool gameRunning, policeSpawn;
    public GameObject police;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawns = new GameObject[highwayLanes];
        for (int i = 0; i < highwayLanes; i++)
        {
            spawns[i] = Instantiate(spawner, new Vector3(10, i * 0.85f - 2.15f, 0), Quaternion.LookRotation(Vector3.left, Vector3.back));
            spawns[i].GetComponent<CarSpawn>().externalTimerSet((i + .5f), (i + 1.5f));
            spawns[i].GetComponent<CarSpawn>().manager = gameObject;
        }
        Direction = gameManager.moveDir;
        gameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (police)
            policeSpawn = true;
        else
            policeSpawn = false;

        Direction = gameManager.moveDir;
    }

    int facingDirection() //Not used yet
    {
        return (int)Direction;
    }

    public void rotateSpawn(bool clockwise, GameManager.spawnDir dir) //Rotating the map clockwise?
    {
        //Debug.Log(dir);
        //Debug.Break();
        for (int i = 0; i < highwayLanes; i++)
        {
            spawns[i].GetComponent<CarSpawn>().rotationSystem(clockwise, i, dir);
        }
    }
}
