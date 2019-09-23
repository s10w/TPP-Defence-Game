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

        // navmesh가 계산을 완료한 상태에서 남아있는 거리 <= 도착 거리
        if ((!agent.pathPending) && (agent.remainingDistance <= agent.stoppingDistance))
        {
            //공격 대상이 있는 경우
            if (PlayerControl.instance.attack_state == true)
            {
                anim.SetInteger("motion", 2);
                FaceTarget();
            }
            else
            {
                anim.SetInteger("motion", 0);
            }
        }
    }

    public void MovePoint(Vector3 point)
    {
        anim.SetInteger("motion", 1);
        agent.SetDestination(point);
    }

    public void TargetChase(InteractObject current_target)
    {
        agent.stoppingDistance = current_target.radius * 0.8f; // 타겟과 일정거리 유지
        target = current_target.transform;
    }

    public void StopChase()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    void FaceTarget()
    {
        agent.updateRotation = false;

        // 정지 상태에서 target을 바라봄
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion look_rotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, look_rotation, Time.deltaTime * 10f);
    }
}
