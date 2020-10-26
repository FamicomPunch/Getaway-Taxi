using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameManager.spawnDir Dir;

    public GameObject[] roadPiece;
    private GameObject lastTile;
    public GameObject[] newHighwayTiles;
    private Transform playerTransform;
    private float SpawnX = 18f;
    private float famiTileWidth;
    //private float tileLength = 9.22f;
    //public int TileAmnt = 7;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lastTile = GameObject.Find("RoadPiece (4)");
        famiTileWidth = roadPiece[0].GetComponentInChildren<SpriteRenderer>().bounds.size.x;

        newHighwayTiles = new GameObject[5];
        for(int i = 0; i < newHighwayTiles.Length; i++)
        {
            newHighwayTiles[i] = Resources.Load<GameObject>("Road/RoadPiece");
        }
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //for(int i = 0; i < TileAmnt; i++)
        //{
        //    SpawnTile();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(lastTile.name);
    }

    public void SpawnTile(GameManager.spawnDir spawnDir, int index = -1)
    {
        if (lastTile.Equals(null))
        {
            Debug.Log("Missing Tile!!");
            Debug.Break();
        }
        Debug.Log("Check Tile: "+lastTile.transform.position);
        Vector3 spawnLoc;
        Vector3 spawnRot = Vector3.zero;
        switch (spawnDir)
        {
            case GameManager.spawnDir.North:
                spawnLoc = new Vector3(0, famiTileWidth - gameManager.speed * 0.015f);
                spawnRot = Vector3.forward * 90f;
                break;
            case GameManager.spawnDir.South:
                spawnLoc = new Vector3(0, -famiTileWidth + gameManager.speed * 0.015f);
                spawnRot = Vector3.forward * 90f;
                break;
            case GameManager.spawnDir.East:
                spawnLoc = new Vector3(famiTileWidth - gameManager.speed * 0.015f, 0);
                break;
            case GameManager.spawnDir.West:
                spawnLoc = new Vector3(-famiTileWidth + gameManager.speed * 0.015f, 0);
                break;
            default:
                Debug.Log("Broken?");
                Debug.Break();
                return;
        }
            
        GameObject road;
        road = Instantiate(roadPiece[0]) as GameObject;
        road.transform.SetParent(transform);
        
        road.transform.SetPositionAndRotation(lastTile.transform.position + spawnLoc, Quaternion.Euler(spawnRot));
        //road.transform.position = lastTile.transform.position + new Vector3(famiTileWidth - gameManager.speed * 0.015f,0,0);
        gameManager.tileAmnt += 1;
        lastTile = road;
        //Debug.Break();
    }

    public void SpawnTurnTile(GameManager.spawnDir spawnDir, bool leftTurn)
    {
        if (lastTile.Equals(null))
        {
            Debug.Log("Missing Tile!!");
            Debug.Break();
        }
        Vector3 spawnLoc;// = lastTile.transform.position;
        //Debug.Log("SpawnLoc"+spawnLoc);
        Vector3 spawnRot = Vector3.zero;
        switch (spawnDir)
        {
            case GameManager.spawnDir.North:
                spawnLoc = new Vector3(0, 3 * famiTileWidth - gameManager.speed * 0.015f); ;
                spawnRot = Vector3.forward * 90f * (leftTurn?1:-1);
                break;
            case GameManager.spawnDir.South:
                spawnLoc = new Vector3(0, -3*famiTileWidth + gameManager.speed * 0.015f);
                spawnRot = -Vector3.forward * 90f*(leftTurn ? 1 : -1);
                break;
            case GameManager.spawnDir.East:
                spawnLoc = new Vector2(3*famiTileWidth - gameManager.speed * 0.015f, 0);
                break;
            case GameManager.spawnDir.West:
                spawnLoc = new Vector3( -3 * famiTileWidth + gameManager.speed * 0.015f,0);
                spawnRot = Vector3.forward * 180f* (leftTurn ? 1 : -1);
                break;
            default:
                Debug.Log("Broken?");
                Debug.Break();
                return;
        }
        GameObject road;
        road = Instantiate(leftTurn ? roadPiece[1] : roadPiece[2]) as GameObject; //Left or right exit tile
        Quaternion a = road.transform.rotation;
        road.transform.SetParent(transform);
        road.transform.SetPositionAndRotation(lastTile.transform.position + spawnLoc, a * Quaternion.Euler(spawnRot));
        
        lastTile = road.transform.Find("NewLastTile").gameObject;
        Debug.Log("Assign tile: "+lastTile.transform.position);
    }
}
