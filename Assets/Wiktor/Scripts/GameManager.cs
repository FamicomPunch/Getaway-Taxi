using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TileManager tileManager;

    public float speed = 2;
    public float defaultSpeed = 2f;
    public int tileAmnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        //speed = speed / 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(tileAmnt < 4)
        {
            tileManager.SpawnTile();
        }
    }
}
