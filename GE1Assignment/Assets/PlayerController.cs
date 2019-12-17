using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        
        Vector3 origin = new Vector3(x, 0, z).normalized * 0.7f + new Vector3(0, 10, 0) + transform.position;
        RaycastHit info = new RaycastHit();
        Physics.Raycast(origin, Vector3.down, out info, 20.0f);
        //Debug.Log(info.transform.name);
        //Debug.DrawRay(origin, Vector3.down*20, Color.black);

        if(info.transform.name == "Floor(Clone)")
        {
            transform.Translate(0, 0, z);
            transform.Translate(x, 0, 0);
        }
    }
}
