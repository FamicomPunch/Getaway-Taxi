using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent rightTurnEvent;
    public UnityEvent leftTurnEvent;
    public UnityEvent backToNormalEvent;
    public UnityEvent turnSucceed;
    private TileManager tileManager;
    private CarSpawnManager carSpawnManager;
    private GameObject player;

    public float speed = 5f;
    public float defaultSpeed = 5f;
    public int tileAmnt = 3;
    public enum spawnDir { East, South, West, North, Rotate };
    public Vector2[] dirVectors = { Vector2.right, Vector2.down, Vector2.left, Vector2.up };
    public Vector3 tempAddValue = Vector3.zero;
    public spawnDir Direction = spawnDir.East, moveDir = spawnDir.East;
    public spawnDir nextDirection, oldDirection = spawnDir.East;
    public int tilesUntilTurn;
    private bool clearTurn = true, onRamp = false;
    public bool isRotating = false, rotatingClockwise = false, hasShift = true, spawnCars = true, gameOverTrigger = false;


    // Start is called before the first frame update
    void Start()
    {
        tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        var carSpawnGO = GameObject.Find("SpawnManager");
        if(carSpawnGO != null)
            carSpawnManager = carSpawnGO.GetComponent<CarSpawnManager>();
        //speed = speed / 4;
        Direction = spawnDir.East;
        if (rightTurnEvent == null) rightTurnEvent = new UnityEvent();
        if (leftTurnEvent == null) leftTurnEvent = new UnityEvent();
        if (backToNormalEvent == null) backToNormalEvent = new UnityEvent();
        if (turnSucceed == null) turnSucceed = new UnityEvent();

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (tileAmnt < 5)
        {
            tileManager.SpawnTile(Direction);
            tilesUntilTurn--;
            //Debug.Log(tileAmnt);
        }
        if (tilesUntilTurn == 3 && clearTurn)
        {
            bool nextTurn = Random.value > 0.5f ? false : true; //Right or Left
            if (nextTurn)
            {
                // Right Turn
                if (Direction.Equals(spawnDir.North))
                    nextDirection = spawnDir.East;
                else nextDirection = Direction + 1;
                tileManager.SpawnTurnTile(Direction, false);
                rotatingClockwise = true;
                if (rightTurnEvent != null) rightTurnEvent.Invoke();
            }
            else
            {
                // Left Turn
                if (Direction.Equals(spawnDir.East))
                    nextDirection = spawnDir.North;
                else nextDirection = Direction - 1;
                tileManager.SpawnTurnTile(Direction, true);
                rotatingClockwise = false;
                if (leftTurnEvent != null) leftTurnEvent.Invoke();
            }
            //Debug.Log("NextDir: "+nextDirection);
            startTurnIndicator(nextTurn, Direction);
            clearTurn = false;
            Direction = nextDirection;
        }
        else if (tilesUntilTurn == 0)
        {
            clearTurn = true;
            tilesUntilTurn = 5;
        }
        if (isRotating)
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
        Debug.Log("NextDirFunc: " + nextDirection + ", dirvector: " + dirVectors[(int)nextDirection]);
        //moveDir = spawnDir.Rotate;
        onRamp = true;
        spawnCars = false;
        tempAddValue = dirVectors[(int)nextDirection] * speed / 10 * Time.deltaTime * ((int)nextDirection % 2 == 0 ? 1 : -1);
        Debug.Log("GM VAL: " + tempAddValue.magnitude);
        //speed *= Mathf.Sqrt(2);
        //GameObject[] cars = GameObject.FindGameObjectsWithTag("Vehicle");
        //GameObject[] road = GameObject.FindGameObjectsWithTag("Road");
        //foreach (GameObject a in cars)
        //{ a.GetComponent<CarMovement>().tempAddSpeed = tempAddValue; }
        //foreach (GameObject a in road)
        //{ a.GetComponent<RoadMovement>().tempAddSpeed = tempAddValue; }
        hasShift = false;
    }

    public void rampCentered(Vector3 shift)
    {
        if(backToNormalEvent != null) backToNormalEvent.Invoke();
        Debug.Log("Ramp Check Shift: " + shift);
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Vehicle");
        GameObject[] road = GameObject.FindGameObjectsWithTag("Road");
        tempAddValue = Vector3.zero;
        foreach (GameObject a in cars)
        {
            a.transform.position -= shift;
            a.GetComponent<CarMovement>().tempAddSpeed = tempAddValue;
        }
        foreach (GameObject a in road)
        {
            a.transform.position -= shift;
            a.GetComponent<RoadMovement>().tempAddSpeed = tempAddValue;
        }
        hasShift = true;
        moveDir = nextDirection;
        //Debug.Break();
    }

    public void getOffRamp()
    {
        if(turnSucceed != null) turnSucceed.Invoke();
        if(backToNormalEvent != null) backToNormalEvent.Invoke();
        Direction = nextDirection;
        moveDir = Direction;
        onRamp = false;
        spawnCars = true;
        isRotating = true;
        if(carSpawnManager != null) {
            carSpawnManager.rotateSpawn(rotatingClockwise, Direction);
        }
        //speed /= Mathf.Sqrt(2);
    }

    public void GameOverActive()
    {
        gameOverTrigger = true;
        if (GameObject.Find("LeftRampRoadPiece(Clone)"))
            GameObject.Find("LeftRampRoadPiece(Clone)").GetComponent<Ramp>().enabled = false;
        else if (GameObject.Find("RightRampRoadPiece(Clone)"))
            GameObject.Find("RightRampRoadPiece(Clone)").GetComponent<Ramp>().enabled = false;
    }

    public void EndRound()
    {
        //Debug.Break();
        // Whatever game over is
    }
}
