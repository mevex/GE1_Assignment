using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    right,
    bottom,
    left,
    top
}

public class RoomSpawner : MonoBehaviour
{
    public Directions openingDirections;
    public bool spawn = true;
    private RoomsContainer rooms;

    void Start()
    {
        rooms = GameObject.FindGameObjectWithTag("RoomsContainer").GetComponent<RoomsContainer>();
        Invoke("Spawn", 0.2f);
    }

    private void Update()
    {
        if(spawn)
            Invoke("Spawn", 0.5f);
    }

    // Update is called once per frame
    void Spawn()
    {
        if(spawn)
        {
            switch (openingDirections)
            {
                case Directions.right:
                    {
                        // LEFT
                        int rand = Random.Range(0, rooms.leftRooms.Length);
                        Instantiate(rooms.leftRooms[rand], transform.position, Quaternion.identity);
                    }
                    break;
                case Directions.bottom:
                    {
                        // TOP
                        int rand = Random.Range(0, rooms.topRooms.Length);
                        Instantiate(rooms.topRooms[rand], transform.position, Quaternion.identity);
                    }
                    break;
                case Directions.left:
                    {
                        // RIGHT
                        int rand = Random.Range(0, rooms.rightRooms.Length);
                        Instantiate(rooms.rightRooms[rand], transform.position, Quaternion.identity);
                    }
                    break;
                case Directions.top:
                    {
                        // BOTTOM
                        int rand = Random.Range(0, rooms.bottomRooms.Length);
                        Instantiate(rooms.bottomRooms[rand], transform.position, Quaternion.identity);
                    }
                    break;
            }
            spawn = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            //Debug.Log("Collided");
            /*if (other.CompareTag("SpawnPoint"))
            {
                RoomSpawner otherSpawner = other.gameObject.GetComponent<RoomSpawner>();
                if (!otherSpawner.spawn)
                {
                    if ((openingDirections == Directions.right && otherSpawner.openingDirections == Directions.bottom)
                        || (openingDirections == Directions.bottom && otherSpawner.openingDirections == Directions.right))
                    {
                        Instantiate(rooms.RB, transform.position, Quaternion.identity);
                    }
                    else if ((openingDirections == Directions.bottom && otherSpawner.openingDirections == Directions.left)
                        || (openingDirections == Directions.left && otherSpawner.openingDirections == Directions.bottom))
                    {
                        Instantiate(rooms.BL, transform.position, Quaternion.identity);
                    }
                    else if ((openingDirections == Directions.left && otherSpawner.openingDirections == Directions.top)
                        || (openingDirections == Directions.top && otherSpawner.openingDirections == Directions.left))
                    {
                        Instantiate(rooms.LT, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(rooms.RT, transform.position, Quaternion.identity);
                    }

                    Debug.Log("ROOM");
                }
            }*/
            spawn = false;
        }
    }
}
