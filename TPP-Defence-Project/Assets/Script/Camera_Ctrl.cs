using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Ctrl : MonoBehaviour
{
    private GameObject player;
    private Vector3 camera_distance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        transform.rotation = Quaternion.Euler(70f, 140f, 0f);

        camera_distance = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + camera_distance, 2f);
    }
}
