using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    private GameManager gameManager;
    GameManager.spawnDir Dir;
    private TileManager tileManager;
    public float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log(gameManager.Direction);
        tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        Dir = gameManager.Direction;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir;
        switch (Dir)
        {
            case GameManager.spawnDir.East:
                moveDir = Vector2.left;
                if (transform.position.x < -18f)
                {
                    tileManager.SpawnTile(gameManager.Direction);
                    gameManager.tileAmnt -= 1;
                    Destroy(gameObject);
                }
                break;
            case GameManager.spawnDir.North:
                moveDir = Vector2.down;
                if (transform.position.y < -18f)
                {
                    tileManager.SpawnTile(gameManager.Direction);
                    gameManager.tileAmnt -= 1;
                    Destroy(gameObject);
                }
                break;
            case GameManager.spawnDir.South:
                moveDir = Vector2.up;
                if (transform.position.y > 18f)
                {
                    tileManager.SpawnTile(gameManager.Direction);
                    gameManager.tileAmnt -= 1;
                    Destroy(gameObject);
                }
                break;
            case GameManager.spawnDir.West:
                moveDir = Vector2.right;
                if (transform.position.x > 18f)
                {
                    tileManager.SpawnTile(gameManager.Direction);
                    gameManager.tileAmnt -= 1;
                    Destroy(gameObject);
                }
                break;
            default:
                Debug.Log("Broke Road Movement...");
                Debug.Break();
                return;
        }
        moveSpeed = gameManager.speed;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        //transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        //if(transform.position.x < -18f)
        //{
        //    tileManager.SpawnTile(gameManager.Direction);
        //    gameManager.tileAmnt -= 1;
        //    Destroy(gameObject);
        //}
    }
}
