using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarSpawn : MonoBehaviour
{
    private GameManager gameManager;
    List <GameObject> cars, trucks, police;
    public float timerMin, timerMax, timeRemaining;
    public bool isActive;
    public GameObject manager;
    Quaternion carRotation = Quaternion.LookRotation(Vector3.forward, Vector3.down);
    int numCars, numTrucks, numPolice;
    public bool forcePolice;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cars = Resources.LoadAll<GameObject>("CarPrefabs/Cars").ToList();
        trucks = Resources.LoadAll<GameObject>("CarPrefabs/Trucks").ToList();
        police = Resources.LoadAll<GameObject>("CarPrefabs/Police").ToList();
        numCars = cars.Count;
        numTrucks = trucks.Count;
        numPolice = police.Count;
        timeRemaining = timerMax;
    }

    // Update is called once per frame
    void Update()
    {
        isActive = gameManager.spawnCars;
        if (isActive)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                GameObject car;
                Vector3 impreciseSpawn = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
                if(forcePolice || Random.value >= 0.9f && !manager.GetComponent<CarSpawnManager>().policeSpawn)
                {
                    car = Instantiate(police[Random.Range(0, numPolice)], transform.position + impreciseSpawn, carRotation);
                    manager.GetComponent<CarSpawnManager>().police = car;
                }
                else if(Random.value >= 0.60f)
                    car = Instantiate(trucks[Random.Range(0, numTrucks)], transform.position + impreciseSpawn, carRotation);
                else
                    car = Instantiate(cars[Random.Range(0, numCars)], transform.position + impreciseSpawn, carRotation);
                car.transform.Rotate(new Vector3(0, 0, 180 - 90 * (1+(int)gameManager.moveDir)));
                //car.GetComponent<CarMovement>().speed = Random.Range(3, 10);
                timeRemaining = Random.Range(timerMin, timerMax);
            }
        }
        else
        {
            isActive = manager.GetComponent<CarSpawnManager>().gameRunning;
        }
    }

    public void rotationSystem(bool clockwise, int slot, GameManager.spawnDir dir)
    {
        transform.Rotate(Vector3.forward, 90 * (clockwise ? -1:1));
        Vector3 pos;
        switch (dir)
        {
            case GameManager.spawnDir.North:
                pos = new Vector3(slot * 0.85f - 2.15f, 12, 0);
                break;
            case GameManager.spawnDir.South:
                pos = new Vector3(2.15f - slot * .85f, -12, 0);
                break;
            case GameManager.spawnDir.East:
                pos = new Vector3(12, slot * 0.85f - 2.15f, 0);
                break;
            case GameManager.spawnDir.West:
                pos = new Vector3(-12, 2.15f - slot * 0.85f, 0);
                break;
            default:
                Debug.Log("Broken?");
                Debug.Break();
                return;
        }
        transform.position = pos;
    }

    public void externalTimerSet(float min, float max)
    {
        timerMin = min;
        timerMax = max;
    }

    public void changeMax(float time)
    {
        timerMax += time;
    }

    public void changeMin(float time)
    {
        timerMin += time;
    }
}
