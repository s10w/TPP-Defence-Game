using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Player_ctrl : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 target_pos;

    Animator anim;

    private bool move_state = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Player_move();
        Player_motion();
    }

    void Player_move()
    {
        if (Input.GetMouseButtonDown(1))
        {
            move_state = true;

            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100);
            target_pos = hit.point;

            agent.SetDestination(hit.point);
        }

        float target_distance = Vector3.Distance(transform.position, target_pos);
        if (target_distance < 0.5f)
        {
            move_state = false;
        }
    }

    void Player_motion()
    {
        if (move_state == true)
        {
            anim.SetInteger("motion", 1);
        }
        else
        {
            anim.SetInteger("motion", 0);
        }
    }
}
