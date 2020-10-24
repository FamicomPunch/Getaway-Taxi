using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    public GameObject[] vehicles;
    public GameObject spawnVehicle;
    public float timerMin, timerMax, timeRemaining;
    public bool isActive;
    public GameObject manager;
    Quaternion carRotation = Quaternion.LookRotation(Vector3.forward, Vector3.down);
    public enum spawnDir { North, East, South, West };
    public spawnDir Direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                GameObject car = Instantiate(spawnVehicle, transform.position, carRotation);
                car.transform.Rotate(new Vector3(0, 0, -90 * (int)Direction));
                car.GetComponent<CarMovement>().Direction = (CarMovement.spawnDir)Direction;
                car.GetComponent<CarMovement>().speed = Random.Range(3, 10);
                timeRemaining = Random.Range(timerMin, timerMax);
            }
        }
    }

    public void rotationSystem(bool clockwise, int Dir)
    {
        transform.RotateAround(manager.transform.position, Vector3.forward, 90);
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
