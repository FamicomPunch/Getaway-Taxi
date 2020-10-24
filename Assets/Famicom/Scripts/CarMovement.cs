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
        switch ((int)Direction) {
            case 0: // North
                {
                    gameObject.transform.position += Vector3.down * Time.deltaTime * speed;
                    break;
                }
            case 1: // East
                {
                    gameObject.transform.position += Vector3.left * Time.deltaTime * speed;
                    break;
                }
            case 2: // South
                {
                    gameObject.transform.position += Vector3.up * Time.deltaTime * speed;
                    break;
                }
            case 3: // West
                {
                    gameObject.transform.position += Vector3.right * Time.deltaTime * speed;
                    break;
                }
            default:
                Debug.Log("???");
                break;
        }
    }
}
