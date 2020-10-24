using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed;
    protected GameManager.spawnDir Direction;
    
    // Start is called before the first frame update
    void Start()
    {
        Direction = GameObject.Find("GameManager").GetComponent<GameManager>().Direction;
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += movement(Direction);
    }

    protected Vector3 movement(GameManager.spawnDir Direction)
    { switch (Direction) {
            case GameManager.spawnDir.North: // North
                {
                    return Vector3.down * Time.deltaTime * speed;
                }
            case GameManager.spawnDir.East: // East
                {
                    return Vector3.left * Time.deltaTime * speed;
                }
            case GameManager.spawnDir.South: // South
                {
                    return Vector3.up * Time.deltaTime * speed;
                }
            case GameManager.spawnDir.West: // West
                {
                    return Vector3.right * Time.deltaTime * speed;
                }
            default:
                Debug.Log("???");
                return Vector3.down * Time.deltaTime * speed * 50;
        }
    }
}
