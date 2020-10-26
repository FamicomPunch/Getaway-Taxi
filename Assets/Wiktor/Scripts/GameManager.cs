﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TileManager tileManager;
    private CarSpawnManager carSpawnManager;

    public float speed = 5f;
    public float defaultSpeed = 5f;
    public int tileAmnt = 3;
    public enum spawnDir { East, South, West, North, Rotate };
    public Vector3[] dirVectors = { Vector2.right, Vector2.down, Vector2.left, Vector2.up };
    public Vector3 tempAddValue = Vector3.zero;
    public spawnDir Direction = spawnDir.East, moveDir = spawnDir.East;
    public spawnDir nextDirection, oldDirection = spawnDir.East;
    public int tilesUntilTurn;
    private bool clearTurn = true, onRamp = false;
    public bool isRotating = false, rotatingClockwise = false, hasShift = false, spawnCars = true;
    

    // Start is called before the first frame update
    void Start()
    {
        tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        carSpawnManager = GameObject.Find("SpawnManager").GetComponent<CarSpawnManager>();
        //speed = speed / 4;
        tilesUntilTurn = 5;
        Direction = spawnDir.East;
    }

    // Update is called once per frame
    void Update()
    {
        if(tileAmnt < 5)
        {
            tileManager.SpawnTile(Direction);
            tilesUntilTurn--;
            Debug.Log(tileAmnt);
        }
        if(tilesUntilTurn == 3 && clearTurn)
        {
            bool nextTurn = Random.value > 0.5f ? false : true; //Right or Left
            if (nextTurn)
            {
                if (Direction.Equals(spawnDir.North))
                    nextDirection = spawnDir.East;
                else nextDirection = Direction + 1;
                tileManager.SpawnTurnTile(Direction, false);
                rotatingClockwise = true;
            }
            else
            {
                if (Direction.Equals(spawnDir.East))
                    nextDirection = spawnDir.North;
                else nextDirection = Direction - 1;
                tileManager.SpawnTurnTile(Direction, true);
                rotatingClockwise = false;
            }
            Debug.Log("NextDir: "+nextDirection);
            //Debug.Break();
            startTurnIndicator(nextTurn, Direction);
            clearTurn = false;
            Direction = nextDirection;
        }
        else if(tilesUntilTurn == 0)
        {
            clearTurn = true;
            tilesUntilTurn = 5;
        }
        if(isRotating)
        {
            isRotating = !isRotating;
        }
    }

    void startTurnIndicator(bool turnLeft, spawnDir currDirection)
    {
        //Create arrow on UI
        //Boolean is for left or right turn, direction is the current travel direction
    }

    public void getOnRamp()
    {
        Debug.Log("NextDirFunc: "+nextDirection+", dirvector: "+ dirVectors[(int)nextDirection]);
        //moveDir = spawnDir.Rotate;
        onRamp = true;
        spawnCars = false;
        tempAddValue = dirVectors[(int)nextDirection] * speed / 10 * Time.deltaTime * ((int)nextDirection % 2 == 0 ? 1:-1);
        //speed *= Mathf.Sqrt(2);
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Vehicle");
        GameObject[] road = GameObject.FindGameObjectsWithTag("Road");
        foreach (GameObject a in cars)
        { a.GetComponent<CarMovement>().tempAddSpeed = tempAddValue; }
        foreach (GameObject a in road)
        { a.GetComponent<RoadMovement>().tempAddSpeed = tempAddValue; }
    }

    public void rampCentered(Vector3 shift)
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Vehicle");
        GameObject[] road = GameObject.FindGameObjectsWithTag("Road");
        tempAddValue = Vector3.zero;
        foreach(GameObject a in cars)
        {
            a.transform.position += shift;
            a.GetComponent<CarMovement>().tempAddSpeed = tempAddValue;
        }
        foreach(GameObject a in road)
        {
            a.transform.position += shift; 
            a.GetComponent<RoadMovement>().tempAddSpeed = tempAddValue;
        }
        hasShift = true;
        moveDir = nextDirection;
        //Debug.Break();
    }

    public void getOffRamp()
    {
        Direction = nextDirection;
        moveDir = Direction;
        onRamp = hasShift = false;
        spawnCars = true;
        isRotating = true;
        carSpawnManager.rotateSpawn(rotatingClockwise, Direction);
        //speed /= Mathf.Sqrt(2);
    }
}
