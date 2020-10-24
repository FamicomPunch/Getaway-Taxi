using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    private GameManager gameManager;
    private TileManager tileManager;
    public float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = gameManager.speed;
        transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        if(transform.position.x < -18f)
        {
            tileManager.SpawnTile();
            gameManager.tileAmnt -= 1;
            Destroy(gameObject);
        }
    }
}
