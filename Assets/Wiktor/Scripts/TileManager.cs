using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject[] roadPiece;

    private Transform playerTransform;
    private float SpawnX = 18f;
    //private float tileLength = 9.22f;
    //public int TileAmnt = 7;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //for(int i = 0; i < TileAmnt; i++)
        //{
        //    SpawnTile();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTile(int index = -1)
    {
        GameObject road;
        road = Instantiate(roadPiece[0]) as GameObject;
        road.transform.SetParent(transform);
        road.transform.position = new Vector3(1, 0, 0) * SpawnX;
        gameManager.tileAmnt += 1;
    }
}
