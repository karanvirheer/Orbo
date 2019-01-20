using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMover : MonoBehaviour
{
    //Speed at which the W A S D keys will move the camera
    public float panSpeed = 50f;

    //The speed at which the camera will move if mouse hovers near the edges
    public float panBorderthickness = 40f;

    /* If W or S is pressed, or the mouse is close to the border of the screen, 
     * then the Z coordinate of the camera will increase or decreases by whatever 
     * value the panSpeed is set to.
     * If A or D is pressed, or the mouse of close to the border of the screen,
     * then the X coordinate of the camera will increase or decrease by whatever
     * the value of panSpeed is set to.
     */
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
