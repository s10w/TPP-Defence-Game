using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Ctrl : MonoBehaviour
{
    private float time = 0.0f;
    private float checkTime = 1.2f;
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
        //카메라 조정 상태 false
        if (Input.GetKeyDown(KeyCode.Space))
        {
            time = 0;
            camera_ctrl_state = false;
        }
        //카메라 조정 상태 true
        if ((Input.mousePosition.y > Screen.height - edge_size || Input.mousePosition.y < edge_size ||
            Input.mousePosition.x > Screen.width - edge_size || Input.mousePosition.x < edge_size) &&
            (current_x == 135.0f && current_y == 70.0f))
        {
            time += Time.deltaTime;
            if (time > checkTime)
            {
                camera_ctrl_state = true;
            }

        }
        //카메라 컨트롤 함수 호출
        if (camera_ctrl_state == true)
        {
            Cam_ctrl();
        }
        //카메라 유닛 컨트롤 함수 호출
        if (camera_ctrl_state == false)
        {
            Cam_unit_ctrl();
        }

    }
    void Cam_ctrl()
    {
        float movement = 10f;//카메라 이동속도

        Vector3 campos = cam.position;

        if (Input.mousePosition.y > Screen.height - edge_size) //위로이동
        {
            campos.x += movement * Time.deltaTime;
            campos.z -= movement * Time.deltaTime;
        }
        if (Input.mousePosition.y < edge_size) //아래이동
        {
            campos.x -= movement * Time.deltaTime;
            campos.z += movement * Time.deltaTime;
        }
        if (Input.mousePosition.x > Screen.width - edge_size) //왼쪽이동
        {
            campos.x -= movement * Time.deltaTime;
            campos.z -= movement * Time.deltaTime;
        }
        if (Input.mousePosition.x < edge_size) //오른쪽이동
        {
            campos.x += movement * Time.deltaTime;
            campos.z += movement * Time.deltaTime;
        }
        campos.y -= Input.GetAxis("Mouse ScrollWheel") * 150f * Time.deltaTime;//마우스 상하위치조절

        //마우스 위치 범위설정
        campos.y = Mathf.Clamp(campos.y, 5, 25);
        campos.x = Mathf.Clamp(campos.x, 10, 190);
        campos.z = Mathf.Clamp(campos.z, 10, 190);

        cam.position = campos;

    }
    void Cam_unit_ctrl()
    {
        float Y_ANGLE_MIN = 1.0f;
        float Y_ANGLE_MAX = 80.0f;
        float DISTANCE_MIN = 3.0f;
        float DISTANCE_MAX = 18.0f;
        float distance = 13.0f;

        // 카메라 위치 초기화
        if (Input.GetKeyDown(KeyCode.Space))

        {
            current_x = 135.0f;
            current_y = 70.0f;
            distance = 15.0f;
        }
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
        distance -= Input.GetAxis("Mouse ScrollWheel") * 150f * Time.deltaTime;
        distance = Mathf.Clamp(distance, DISTANCE_MIN, DISTANCE_MAX);
        Debug.DrawRay(cam.position, player.transform.position - cam.position, Color.green);
    }
}