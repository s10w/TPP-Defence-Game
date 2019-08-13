using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 85.0f;
    private const float DISTANCE_MIN = 2.0f;
    private const float DISTANCE_MAX = 15.0f;

    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    private float distance = 15.0f;
    private float currentX = 0.0f;
    private float currentY = 85.0f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;


    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(2))
        {
            currentX += (2 * Input.GetAxis("Mouse X"));
            currentY += (2 * Input.GetAxis("Mouse Y"));
        }


        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }
    private void LateUpdate()
    {
        Vector3 Dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * Dir;
        camTransform.LookAt(lookAt.position);


        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            distance += 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            distance -= 1;
        }
        distance = Mathf.Clamp(distance, DISTANCE_MIN, DISTANCE_MAX);
    }
}
