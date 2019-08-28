using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera_Ctrl : MonoBehaviour
{
    private float distance = 15.0f;
    private float current_x = 135.0f;
    private float current_y = 70.0f;
    private float edge_size = 30f;


    private bool camera_ctrl_state = false;

    private GameObject player;

    private Transform cam;





    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            camera_ctrl_state = false;
        }
        if (current_x == 140.0f && current_y == 70.0f){
            if (Input.mousePosition.y > Screen.height - edge_size || Input.mousePosition.y < edge_size ||
                Input.mousePosition.x > Screen.width - edge_size || Input.mousePosition.x < edge_size)
            {
                camera_ctrl_state = true;
            }
        }
        if (camera_ctrl_state == true)
        {
            Cam_ctrl();
        }

        if (camera_ctrl_state == false)
        {
            Cam_unit_ctrl();
        }

    }
    void Cam_ctrl()
    {
        float movement = 10f;

        Vector3 campos = cam.position;

        if (Input.mousePosition.y > Screen.height - edge_size)
        {
            campos.x += movement * Time.deltaTime;
            campos.z -= movement * Time.deltaTime;
        }
        if (Input.mousePosition.y < edge_size)
        {
            campos.x -= movement * Time.deltaTime;
            campos.z += movement * Time.deltaTime;
        }
        if (Input.mousePosition.x > Screen.width - edge_size)
        {
            campos.x -= movement * Time.deltaTime;
            campos.z -= movement * Time.deltaTime;
        }
        if (Input.mousePosition.x < edge_size)
        {
            campos.x += movement * Time.deltaTime;
            campos.z += movement * Time.deltaTime;
        }

        cam.position = campos;

    }
    void Cam_unit_ctrl()
    {
        float Y_ANGLE_MIN = 1.0f;
        float Y_ANGLE_MAX = 80.0f;
        float DISTANCE_MIN = 3.0f;
        float DISTANCE_MAX = 18.0f;

        // 카메라 좌우회전
        if (Input.GetMouseButton(2))
        {
            current_x += (2 * Input.GetAxis("Mouse X"));
            current_y += -(2 * Input.GetAxis("Mouse Y"));
        }
        current_y = Mathf.Clamp(current_y, Y_ANGLE_MIN, Y_ANGLE_MAX);

        Vector3 Dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(current_y, current_x, 0);

        // 카메라 위치 이동
        cam.position = player.transform.position + player.transform.up * 2 + rotation * Dir;
        cam.LookAt(player.transform.position + player.transform.up * 2);

        // 카메라 상하

        distance -= Input.GetAxis("Mouse ScrollWheel") * 100f * Time.deltaTime;
        distance = Mathf.Clamp(distance, DISTANCE_MIN, DISTANCE_MAX);

        // 카메라 위치 초기화
        if (Input.GetKeyUp(KeyCode.Space))
        {
            current_x = 140.0f;
            current_y = 70.0f;
            distance = 15.0f;
        }
    }
}