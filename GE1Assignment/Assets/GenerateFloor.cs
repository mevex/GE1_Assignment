using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloorType
{
    hole,
    floor,
    stone
}

public class GenerateFloor : MonoBehaviour
{
    public int res = 4;
    public GameObject floorPref;
    public GameObject stonePref;

    private bool[] doors = new bool[4];
    private int roomWidth;
    private int roomHeight;
    private int x;
    private int z;
    //private Transform player;
    //private Vector3[,] tilesPosition;
    //private GameObject[,] tiles;
    //private bool animate;
    //public float animationTime = 1.0f;
    //private float animT;
    //private List<GameObject> floors = new List<GameObject>();
    //private List<GameObject> stones = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        RoomSpawner[] spawners = transform.parent.gameObject.GetComponentsInChildren<RoomSpawner>();
        foreach (RoomSpawner spawner in spawners)
        {
            doors[(int)spawner.openingDirections] = true;
        }

        if (transform.position.x < 0)
            x = (int)((transform.position.x - 5) / 10);
        else
            x = (int)((transform.position.x + 5) / 10);

        if (transform.position.z < 0)
            z = (int)((transform.position.z - 5) / 10);
        else
            z = (int)((transform.position.z + 5) / 10);

        CameraControlls camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControlls>();
        roomWidth = camera.roomWidth;
        roomHeight = camera.roomHeight;

        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //animT = animationTime;

        Generate();
    }

    // Update is called once per frame
    void Update()
    {/*
        int playerX, playerZ;
        if (player.position.x < 0)
            playerX = (int)((player.position.x - 5) / 10);
        else
            playerX = (int)((player.position.x + 5) / 10);

        if (player.position.z < 0)
            playerZ = (int)((player.position.z - 5) / 10);
        else
            playerZ = (int)((player.position.z + 5) / 10);

        if (playerX == x && playerZ == z && animate)
        {
            //AnimateFloor();
        }
        else
        {
            animate = true;
            animT = animationTime;
        }*/
    }

    void Generate()
    {
        //tiles = new GameObject[roomWidth * res, roomHeight * res];
        //tilesPosition = new Vector3[roomWidth * res, roomHeight * res];
        FloorType[,] floor = new FloorType[roomWidth * res, roomHeight * res];
        float scaleX = 1.0f / roomWidth;
        float scaleY = 1.0f / roomHeight;
        float xOffset = Random.Range(0, 256);
        float yOffset = Random.Range(0, 256);

        //Generate floor
        for (int z = 0; z < roomHeight * res; z++)
        {
            for (int x = 0; x < roomWidth * res; x++)
            {
                float perlin = Mathf.PerlinNoise(x * scaleX + xOffset, z * scaleY + yOffset);
                if (perlin < 0.4)
                {
                    floor[x, z] = FloorType.hole;
                }
                else if (perlin < 0.75)
                {
                    floor[x, z] = FloorType.floor;
                }
                else
                {
                    floor[x, z] = FloorType.stone;
                }
            }
        }

        // Overwrite central tiles
        for (int z = (roomHeight * res - res) / 2; z < (roomHeight * res + res) / 2 + 1; z++)
        {
            for (int x = (roomWidth * res - res) / 2; x < (roomWidth * res + res) / 2 + 1; x++)
            {
                floor[x, z] = FloorType.floor;
            }
        }

        // Override door to door path
        for (int i = 0; i < 4; i++)
        {
            if (doors[i])
                switch ((Directions)i)
                {
                    case Directions.bottom:
                        {
                            for (int z = 0; z < (roomHeight * res - res) / 2; z++)
                            {
                                for (int x = (roomWidth * res - res) / 2; x < (roomWidth * res + res) / 2 + res % 2; x++)
                                {
                                    floor[x, z] = FloorType.floor;
                                }
                            }
                        }
                        break;
                    case Directions.left:
                        {
                            for (int z = (roomHeight * res - res) / 2; z < (roomHeight * res + res) / 2 + 1; z++)
                            {
                                for (int x = 0; x < (roomWidth * res - res) / 2; x++)
                                {
                                    floor[x, z] = FloorType.floor;
                                }
                            }
                        }
                        break;
                    case Directions.right:
                        {
                            for (int z = (roomHeight * res - res) / 2; z < (roomHeight * res + res) / 2 + 1; z++)
                            {
                                for (int x = (roomWidth * res + res) / 2 + 1; x < roomWidth * res; x++)
                                {
                                    floor[x, z] = FloorType.floor;
                                }
                            }
                        }
                        break;
                    case Directions.top:
                        {
                            for (int z = (roomHeight * res + res) / 2 + 1; z < roomHeight * res; z++)
                            {
                                for (int x = (roomWidth * res - res) / 2; x < (roomWidth * res + res) / 2 + 1; x++)
                                {
                                    floor[x, z] = FloorType.floor;
                                }
                            }
                        }
                        break;
                }
        }


        for (int z = 0; z < (roomHeight * res); z++)
        {
            for (int x = 0; x < (roomWidth * res); x++)
            {
                Vector3 pos = transform.position + new Vector3((float)x / (float)res - roomWidth / 2, 0, (float)z / (float)res - roomHeight / 2);
                //tilesPosition[x, z] = pos;
                if (floor[x, z] == FloorType.floor)
                {
                    GameObject tile = Instantiate(floorPref, pos, Quaternion.identity, transform);
                    tile.transform.localScale = new Vector3(1.0f / res, 0.2f, 1.0f / res);
                }
                else if (floor[x, z] == FloorType.stone)
                {
                    GameObject tile = Instantiate(stonePref, pos, Quaternion.identity, transform);
                    tile .transform.localScale = new Vector3(1.0f / res, 3.0f, 1.0f / res);
                }
            }
        }
        //CombineMeshes();
    }

    void AnimateFloor()
    {/*
        if (animT > 0) //&& animT <= animationTime)
        {
            animT -= Time.deltaTime;
            float t = Mathf.Min(animT / animationTime, 1);

            for (int z = 0; z < (roomHeight * res); z++)
            {
                for (int x = 0; x < (roomWidth * res); x++)
                {
                    if (tiles[x, z])
                    {
                        Debug.Log("Ok");
                        tiles[x, z].transform.position = Vector3.Lerp(tiles[x, z].transform.position, tilesPosition[x, z], t);
                        tiles[x, z].transform.rotation = Quaternion.Lerp(tiles[x, z].transform.rotation, Quaternion.identity, t);
                    }
                }
            }
        }
        else if (animate)
        {
            animate = false;
            DisplaceFloor();
        }*/
    }

    void DisplaceFloor()
    {
        foreach(Transform child in transform)
        {
            child.Translate(0, Random.Range(-5, -7), 0);
            child.Rotate(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-30, 30));
        }
    }

    void CombineMeshes()
    {
        /*foreach(GameObject tile in tiles)
        {
            //Vector3 position = tile.transform.position;
            //tile.transform.position = Vector3.zero;

            MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            for (int i = 0; i < meshFilters.Length; i++)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);
            }
            transform.GetComponent<MeshFilter>().mesh = new Mesh();
            transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
            transform.gameObject.SetActive(true);
        }*/
    }
}
