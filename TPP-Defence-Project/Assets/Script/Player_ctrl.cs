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

    [SerializeField] private GameObject target_check;
    [SerializeField] private GameObject onePoint_hit;
    public bool attack_state { get; private set; }

    // Player_ctrl의 변수 초기화
    void Player_ctrl_init()
    {
        target_check = Instantiate(target_check);
        target_check.SetActive(false);

        onePoint_hit = Instantiate(onePoint_hit);
        onePoint_hit.SetActive(false);

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
    void Update()
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
            Debug.Log("Hit");
            //플레이어 이동
            Player_move();

            // 적 오브젝트가 있는 지 확인
            target_check.SetActive(true);
            target_check.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
        }
    }

    // 플레이어 이동 함수
    void Player_move()
    {
        anim.SetInteger("motion", 1);
        Physics.Raycast(main_camera.ScreenPointToRay(Input.mousePosition), out hit, 100);
        target_pos = hit.point;
        agent.SetDestination(target_pos);
    }

    // 플레이어 정지 함수
    void Player_stop()
    {
        // 적이 없으면 공격 중지
        if (attack_state == true)
        {

        }
        attack_state = false;

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
        agent.SetDestination(target_check.GetComponent<Target_check>().Enemy_pos);

        // 적 크기에 맞게 사정거리 계산
        RaycastHit Enemy_hit;
        Physics.Raycast(transform.position, transform.forward, out Enemy_hit, 100);
        Vector3 fixed_Enemy_pos = Enemy_hit.point;

        // 사정거리에 도달 시 공격 시작
        float Enemy_distance = Vector3.Distance(transform.position, fixed_Enemy_pos);
        if (Enemy_distance < 1.2f)
        {
            agent.velocity = Vector3.zero;
            attack_state = true;
            anim.SetInteger("motion", 2);
        }
    }
}
