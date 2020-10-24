using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TileManager tileManager;

    public float speed = 2;
    public float defaultSpeed = 2f;
    public int tileAmnt = 0;
    public enum spawnDir { East, South, West, North };
    public spawnDir Direction = spawnDir.East;
    public spawnDir nextDirection, oldDirection = spawnDir.East;
    public int tilesUntilTurn;
    private bool nextTurn;
    public bool isRotating = false, rotatingClockwise = false;

    // Start is called before the first frame update
    void Start()
    {
        tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        //speed = speed / 4;
        tilesUntilTurn = 20;
        Direction = spawnDir.East;
    }

    // Update is called once per frame
    void Update()
    {
        if(tileAmnt < 4)
        {
            tileManager.SpawnTile(Direction);
            tilesUntilTurn--;
        }
        if(tilesUntilTurn == 3)
        {
            nextTurn = Random.value > 0.5f ? true : false; //Right or Left
            if (nextTurn)
            {
                if (Direction.Equals(spawnDir.North))
                    nextDirection = spawnDir.East;
                else nextDirection = Direction + 1;
            }
            else
            {
                if (Direction.Equals(spawnDir.East))
                    nextDirection = spawnDir.North;
                else nextDirection = Direction - 1;
            }
            startTurnIndicator(nextTurn, Direction);
        }
        else if(tilesUntilTurn == 0)
        {
            tileManager.SpawnTurnTile(nextTurn);
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

    void getOnRamp()
    {
        //PlayerController.getOnRamp();

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
