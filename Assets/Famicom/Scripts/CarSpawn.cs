using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarSpawn : MonoBehaviour
{
    public List <GameObject> cars, trucks;
    public GameObject spawnVehicle;
    public float timerMin, timerMax, timeRemaining;
    public bool isActive;
    public GameObject manager;
    Quaternion carRotation = Quaternion.LookRotation(Vector3.forward, Vector3.down);
    public enum spawnDir { North, East, South, West };
    public spawnDir Direction;
    int numCars, numTrucks;

    // Start is called before the first frame update
    void Start()
    {
        cars = Resources.LoadAll<GameObject>("CarPrefabs/Cars").ToList();
        trucks = Resources.LoadAll<GameObject>("CarPrefabs/Trucks").ToList();
        numCars = cars.Count;
        numTrucks = trucks.Count;
        timeRemaining = timerMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                GameObject car;
                Vector3 impreciseSpawn = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                if(Random.value > 0.75f)
                    car = Instantiate(trucks[Random.Range(0, numTrucks)], transform.position + impreciseSpawn, carRotation);
                else
                    car = Instantiate(cars[Random.Range(0, numCars)], transform.position + impreciseSpawn, carRotation);
                car.transform.Rotate(new Vector3(0, 0, 180 - 90 * (int)Direction));
                car.GetComponent<CarMovement>().Direction = (CarMovement.spawnDir)Direction;
                car.GetComponent<CarMovement>().speed = Random.Range(3, 10);
                timeRemaining = Random.Range(timerMin, timerMax);
            }
        }
        else
        {
            isActive = manager.GetComponent<CarSpawnManager>().gameRunning;
        }
    }

    public void rotationSystem(bool clockwise, int Dir)
    {
        transform.RotateAround(manager.transform.position, Vector3.forward, 90 * (clockwise ? -1:1));
        Direction = (spawnDir)Dir;
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
