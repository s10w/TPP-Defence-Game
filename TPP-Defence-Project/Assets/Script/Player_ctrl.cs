using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Player_ctrl : MonoBehaviour
{
    private Camera main_camera;
    private NavMeshAgent agent;
    private Animator anim;
    private RaycastHit hit;
    private Vector3 target_pos;
    private Coroutine attack_Loop;

    [SerializeField] private GameObject target_check;
    public bool attack_state { get; private set; }

    // Player_ctrl의 변수 초기화
    void Player_ctrl_init()
    {
        target_check = Instantiate(target_check);
        target_check.SetActive(false);

        attack_state = false;
    }

    // 시작 시 오브젝트 설정 함수
    private void Start()
    {
        main_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Player_ctrl_init();
    }

    // 매 프레임마다 호출
    void FixedUpdate()
    {
        if (target_check.GetComponent<Target_check>().enemy_check == true)
        {
            Player_attack();
        }
        else
        {
            Player_stop();
        }

        if (Input.GetMouseButtonDown(1))
        {
            // 적 오브젝트가 있는 지 확인
            target_check.SetActive(true);
            Physics.Raycast(main_camera.ScreenPointToRay(Input.mousePosition), out hit, 100);
            target_check.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);

            //플레이어 이동
            Player_move();
        }
    }

    // 플레이어 이동 함수
    void Player_move()
    {
        // 이동 시작 시 공격 중지
        if (attack_state == true)
        {
            StopCoroutine(attack_Loop);
        }
        attack_state = false;

        anim.SetInteger("motion", 1);
        target_pos = hit.point;
        agent.SetDestination(target_pos);
    }

    // 플레이어 정지 함수
    void Player_stop()
    {
        // 목적지에 도달시 정지 모션
        float target_distance = Vector3.Distance(transform.position, target_pos);
        if (target_distance < 0.5f)
        {
            anim.SetInteger("motion", 0);
        }
    }

    // 플레이어 공격 함수
    void Player_attack()
    {
        //적 위치에 맞게 다시 목적지 설정
        agent.SetDestination(target_check.GetComponent<Target_check>().Enemy_pos + (-transform.forward.normalized * 1.5f)); // 사정거리

        // 사정거리에 도달 시 공격 시작
        float Enemy_distance = Vector3.Distance(transform.position, target_check.GetComponent<Target_check>().Enemy_pos);
        if (Enemy_distance < 3f)
        {
            attack_state = true;
            anim.SetInteger("motion", 2);
            attack_Loop = StartCoroutine(Create_hit());
        }
    }

    // 공격 오브젝트 생성 함수
    IEnumerator Create_hit()
    {
        Debug.Log("attack");
        yield return new WaitForSeconds(1f);
    }
}
