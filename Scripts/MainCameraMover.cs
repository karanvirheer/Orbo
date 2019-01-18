using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMover : MonoBehaviour
{
    public float panSpeed = 50f;
    public float panBorderthickness = 40f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderthickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y >= panBorderthickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.height - panBorderthickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.y >= panBorderthickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

    }
}
