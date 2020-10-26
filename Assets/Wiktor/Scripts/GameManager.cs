using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TileManager tileManager;

    public float speed = 14;
    public float defaultSpeed = 14f;
    public int tileAmnt = 5;
    public enum spawnDir { East, South, West, North, Rotate };
    public Vector3[] dirVectors = { Vector2.left, Vector2.down, Vector2.right, Vector2.up };
    public spawnDir Direction = spawnDir.East, moveDir = spawnDir.East;
    public spawnDir nextDirection, oldDirection = spawnDir.East;
    public int tilesUntilTurn;
    private bool clearTurn = true, onRamp = false;
    public bool isRotating = false, rotatingClockwise = false, hasShift = false, spawnCars = true;

    // Start is called before the first frame update
    void Start()
    {
        tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
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
            }
            else
            {
                if (Direction.Equals(spawnDir.East))
                    nextDirection = spawnDir.North;
                else nextDirection = Direction - 1;
                tileManager.SpawnTurnTile(Direction, true);
            }
            Debug.Log("NextDir: "+nextDirection);
            Debug.Break();
            startTurnIndicator(nextTurn, Direction);
            clearTurn = false;
            Direction = nextDirection;
        }
        else if(tilesUntilTurn == 0)
        {
            clearTurn = true;
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
        //moveDir = spawnDir.Rotate;
        onRamp = true;
        spawnCars = false;
        //speed *= Mathf.Sqrt(2);
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Vehicle");
        GameObject[] road = GameObject.FindGameObjectsWithTag("Road");
        foreach (GameObject a in cars)
        { a.GetComponent<CarMovement>().tempAddSpeed = dirVectors[(int)nextDirection] * speed * Time.deltaTime; }
        foreach (GameObject a in road)
        { a.GetComponent<RoadMovement>().tempAddSpeed = dirVectors[(int)nextDirection] * speed * Time.deltaTime; }
    }

    public void rampCentered(Vector3 shift)
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Vehicle");
        GameObject[] road = GameObject.FindGameObjectsWithTag("Road");
        foreach(GameObject a in cars)
        { a.transform.position += shift; a.GetComponent<CarMovement>().tempAddSpeed = Vector3.zero; }
        foreach(GameObject a in road)
        { a.transform.position += shift; a.GetComponent<RoadMovement>().tempAddSpeed = Vector3.zero; }
        hasShift = true;
        moveDir = nextDirection;
    }

    public void getOffRamp()
    {
        moveDir = Direction;
        onRamp = hasShift = false;
        spawnCars = true;
        //speed /= Mathf.Sqrt(2);
    }

    public void rotation(bool clockwise)
    {
        oldDirection = Direction;
        if(clockwise)
            Direction = (Direction.Equals(spawnDir.North) ? Direction = spawnDir.East : Direction+1);
        else
            Direction = (Direction.Equals(spawnDir.East) ? Direction = spawnDir.North : Direction-1);
        isRotating = true;
        rotatingClockwise = clockwise;
    }
}
