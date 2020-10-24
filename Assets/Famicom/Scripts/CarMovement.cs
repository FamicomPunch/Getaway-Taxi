using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public enum spawnDir { North, East, South, West };
    public spawnDir Direction;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += movement(Direction);
    }

    protected Vector3 movement(spawnDir Direction)
    { switch ((int)Direction) {
            case 0: // North
                {
                    return Vector3.down * Time.deltaTime * speed;
                }
            case 1: // East
                {
                    return Vector3.left * Time.deltaTime * speed;
                }
            case 2: // South
                {
                    return Vector3.up * Time.deltaTime * speed;
                }
            case 3: // West
                {
                    return Vector3.right * Time.deltaTime * speed;
                }
            default:
                Debug.Log("???");
                return Vector3.down * Time.deltaTime * speed * 50;
        }
    }
}
