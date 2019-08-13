using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class player_move : MonoBehaviour
{
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Hit");

            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100);

            agent.SetDestination(hit.point);
        }
    }
}
