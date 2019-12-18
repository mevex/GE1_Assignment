using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlls : MonoBehaviour
{
    public float MaxHeight = 100.0f;
    public float MinHeight = 10.0f;
    public int roomWidth = 10;
    public int roomHeight = 10;

    private bool isUp = true;
    private Transform player;
    private int playerX = 0;
    private int playerZ = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(new Vector3(0, MaxHeight, 0), Quaternion.Euler(90, 0, 0));
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) == true)
        {
            if (isUp)
            {
                transform.Translate(0, 0, MaxHeight - MinHeight);
                isUp = false;
            }
            else
            {
                transform.Translate(0, 0, MinHeight - MaxHeight);
                isUp = true;
            }
        }

        if(player.position.x < 0)
            playerX = (int)((player.position.x - roomWidth / 2) / roomWidth);
        else
            playerX = (int)((player.position.x + roomWidth / 2) / roomWidth);

        if (player.position.z < 0)
            playerZ = (int)((player.position.z - roomHeight / 2) / roomHeight);
        else
            playerZ = (int)((player.position.z + roomHeight / 2) / roomHeight);

        transform.position = new Vector3(playerX*roomWidth, transform.position.y, playerZ*roomHeight);
    }
}
