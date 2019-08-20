using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Ctrl : MonoBehaviour
{
    private GameObject player;
    private Vector3 camera_distance;

    void Start()
    {
        //transform.position = new Vector3(140f, 10f, 145f);
        transform.rotation = Quaternion.Euler(70f, 140f, 0f);

        player = GameObject.FindGameObjectWithTag("Player");
        camera_distance = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + camera_distance, 2f);
    }
}
