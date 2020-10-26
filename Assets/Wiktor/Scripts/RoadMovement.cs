using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    private GameManager gameManager;
    GameManager.spawnDir Dir;
    private TileManager tileManager;
    public float moveSpeed = 1f;
    private bool isRampTile;
    Vector3 pos;
    public Vector3 tempAddSpeed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log(gameManager.Direction);
        tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        isRampTile = (gameObject.name.Equals("LeftRampRoadPiece(Clone)") || gameObject.name.Equals("RightRampRoadPiece(Clone)"));
        pos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Dir = gameManager.moveDir;
        pos = gameObject.transform.position;
        if (isRampTile && gameManager.hasShift)
        {
            //Debug.Break();
            Debug.Log("New dir: "+Dir);
        }

        if (isRampTile && !gameManager.hasShift)
        {
            if(Dir.Equals(GameManager.spawnDir.East)||Dir.Equals(GameManager.spawnDir.West))
            {
                if (Mathf.Abs(pos.x) < moveSpeed * Time.deltaTime)
                {
                    gameManager.rampCentered(Vector3.zero - pos);
                }
            }
            else if(Mathf.Abs(pos.y) < moveSpeed * Time.deltaTime)
            {
                gameManager.rampCentered(Vector3.zero - pos);
            }
            Debug.Log("Old dir: " + Dir);
        }

        Vector3 moveDir;
        switch (Dir)
        {
            case GameManager.spawnDir.East:
                moveDir = Vector2.left;
                if (transform.position.x < -18f && !isRampTile || transform.position.x < -90f)
                {
                    //tileManager.SpawnTile(gameManager.Direction);
                    gameManager.tileAmnt -= 1;
                    Destroy(gameObject);
                }
                break;
            case GameManager.spawnDir.North:
                moveDir = Vector2.down;
                if (transform.position.y < -18f && !isRampTile || transform.position.y < -90f)
                {
                    //tileManager.SpawnTile(gameManager.Direction);
                    gameManager.tileAmnt -= 1;
                    Destroy(gameObject);
                }
                break;
            case GameManager.spawnDir.South:
                moveDir = Vector2.up;
                if (transform.position.y > 18f && !isRampTile || transform.position.y > 90f)
                {
                    //tileManager.SpawnTile(gameManager.Direction);
                    gameManager.tileAmnt -= 1;
                    Destroy(gameObject);
                }
                break;
            case GameManager.spawnDir.West:
                moveDir = Vector2.right;
                if (transform.position.x > 18f && !isRampTile || transform.position.x > 90f)
                {
                    //tileManager.SpawnTile(gameManager.Direction);
                    gameManager.tileAmnt -= 1;
                    Destroy(gameObject);
                }
                break;
            case GameManager.spawnDir.Rotate:
                moveDir = Vector2.zero;
                //Code for rotation goes here...
                break;
            default:
                Debug.Log("Broke Road Movement...");
                Debug.Break();
                return;
        }
        moveSpeed = gameManager.speed;
        transform.position += moveDir * moveSpeed * Time.deltaTime + tempAddSpeed;
        //transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        //if(transform.position.x < -18f)
        //{
        //    tileManager.SpawnTile(gameManager.Direction);
        //    gameManager.tileAmnt -= 1;
        //    Destroy(gameObject);
        //}
    }
}
