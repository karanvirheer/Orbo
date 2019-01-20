using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float speed;

    /* Update is called once per frame
     * Rotates the camera at a set speed around the y axis.
     */
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
    
}
