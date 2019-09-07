using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMove : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            anim.SetInteger("motion", 1);
            agent.SetDestination(target.position);
        }
        StopMotion();
    }

    public void MovePoint(Vector3 point)
    {
        anim.SetInteger("motion", 1);
        agent.SetDestination(point);
    }

    public void StopMotion()
    {
        // navmesh가 계산을 완료한 상태에서 남아있는 거리 <= 도착 거리
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            anim.SetInteger("motion", 0);
        }
    }

    public void TargetChase(TargetObject current_target)
    {
        agent.stoppingDistance = current_target.radius * 0.8f;
        target = current_target.transform;
    }

    public void StopChase()
    {
        agent.stoppingDistance = 0f;
        target = null;
    }
}
