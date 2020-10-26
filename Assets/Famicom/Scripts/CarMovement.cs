using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed;
    protected GameManager manager;
    protected GameManager.spawnDir Direction;
    public Vector3 tempAddSpeed;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Direction = manager.moveDir;
        speed = manager.speed;
    }

    // Update is called once per frame
    void Update()
    {
        Direction = manager.moveDir;
        gameObject.transform.position += movement(Direction)+tempAddSpeed;
    }

    protected Vector3 movement(GameManager.spawnDir Direction)
    {
        speed = manager.speed;
        switch (Direction) {
            case GameManager.spawnDir.North: // North
                {
                    if (gameObject.transform.position.y < -18)
                        Destroy(gameObject);
                    return Vector3.down * Time.deltaTime * speed;
                }
            case GameManager.spawnDir.East: // East
                {
                    if (gameObject.transform.position.x < -18)
                        Destroy(gameObject);
                    return Vector3.left * Time.deltaTime * speed;
                }
            case GameManager.spawnDir.South: // South
                {
                    if (gameObject.transform.position.y > 18)
                        Destroy(gameObject);
                    return Vector3.up * Time.deltaTime * speed;
                }
            case GameManager.spawnDir.West: // West
                {
                    if (gameObject.transform.position.x > 18)
                        Destroy(gameObject);
                    return Vector3.right * Time.deltaTime * speed;
                }
            default:
                Debug.Log("???");
                return Vector3.down * Time.deltaTime * speed * 50;
        }
    }
}
