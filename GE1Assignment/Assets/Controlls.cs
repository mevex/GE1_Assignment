using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) == true)
        {
            GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
            foreach(GameObject room in rooms)
            {
                Destroy(room);
            }

            GameObject main = GameObject.Find("Main");
            RoomSpawner[] spawners = main.transform.GetComponentsInChildren<RoomSpawner>();
            foreach(RoomSpawner spawner in spawners)
            {
                spawner.spawn = true;
            }
        }
    }
}
