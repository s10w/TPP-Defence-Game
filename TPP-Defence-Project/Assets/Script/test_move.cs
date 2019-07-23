using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_move : MonoBehaviour
{
    private bool move_state = false;
    private float tsl_move_speed = 10f;

    RaycastHit hit;
    Vector3 dir;
    Vector3 target_pos;

    Animator anim;
    CharacterController controll;

    private void Start()
    {
        anim = GetComponent<Animator>();
        controll = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Hit");

            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            target_pos = hit.point;

            move_state = true;
        }

        if (move_state == true)
        {
            anim.SetInteger("motion", 1);
            dir = (target_pos - transform.position).normalized;
            transform.Translate(dir * tsl_move_speed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, target_pos);
            if (distance < 0.5f)
            {
                anim.SetInteger("motion", 0);
                move_state = false;
            }
        }
    }
}
