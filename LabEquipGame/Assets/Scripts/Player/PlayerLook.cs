using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSens = 30f;
    public float ySens = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //calculate cam rotation for up/down view
        xRotation -= (mouseY * Time.deltaTime) * ySens;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //now set to camera
        cam.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        //left/right view
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSens);
    }

}
