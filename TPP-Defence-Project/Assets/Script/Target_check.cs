using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_check : MonoBehaviour
{
    public Vector3 Enemy_pos { get; private set; }
    public bool enemy_check { get; private set; }

    private void Start()
    {
        enemy_check = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy_check = true;
            Enemy_pos = other.transform.position;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy_check = false;
        }
    }
}
