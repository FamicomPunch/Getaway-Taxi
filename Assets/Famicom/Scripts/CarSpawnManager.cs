using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnManager : MonoBehaviour
{
    public GameObject spawner;
    public int highwayLanes = 5;
    public GameObject[] spawns;
    public enum spawnDir { North, East, South, West };
    public spawnDir Direction;
    public bool gameRunning;

    // Start is called before the first frame update
    void Start()
    {
        spawns = new GameObject[highwayLanes];
        for (int i = 0; i < highwayLanes; i++)
        {
            spawns[i] = Instantiate(spawner, new Vector3(-6 + (3 * i), 10, 0), Quaternion.LookRotation(Vector3.down, Vector3.back));
            spawns[i].GetComponent<CarSpawn>().externalTimerSet(((float)i + 0.5f), ((float)i + 1.5f));
            spawns[i].GetComponent<CarSpawn>().manager = gameObject;
            spawns[i].GetComponent<CarSpawn>().Direction = CarSpawn.spawnDir.North;
        }
        Direction = spawnDir.North;
        Debug.Log(spawns.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameRunning)
            gameRunning = true; //TEMPORARY!!
    }

    public int facingDirection() //Not used yet
    {
        return (int)Direction;
    }

    public void rotateSpawn(bool clockwise) //Rotating the map clockwise?
    {

        if (clockwise)
        {
            if (Direction.Equals(spawnDir.West))
                Direction = spawnDir.North;
            else Direction++;            
        }
        else
        {
            if (Direction.Equals(spawnDir.North))
                Direction = spawnDir.West;
            else Direction--;
        }

        for (int i = 0; i < highwayLanes; i++)
        {
            spawns[i].GetComponent<CarSpawn>().rotationSystem(clockwise,(int)Direction);
        }
    }
}
