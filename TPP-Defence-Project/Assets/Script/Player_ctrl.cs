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
    [SerializeField] private GameObject basic_attack;

    public bool move_state { get; private set; }
    public float attack_sight { get; private set; }
    public float attack_range { get; private set; }

    // Player_ctrl의 변수 초기화
    void Player_ctrl_init()
    {
        target_check = Instantiate(target_check);
        target_check.SetActive(false);

        basic_attack = Instantiate(basic_attack);
        basic_attack.SetActive(false);

        move_state = false;
        attack_sight = 2f;
        attack_range = 1.2f;
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
        if (Input.GetMouseButtonDown(1))
        {
            // 적 오브젝트가 있는 지 확인
            target_check.SetActive(true);
            Physics.Raycast(main_camera.ScreenPointToRay(Input.mousePosition), out hit, 100);
            target_pos = hit.point;
            target_check.transform.position = target_pos;

            //이동 준비
            move_state = true;
        }

        if (target_check.GetComponent<Target_check>().enemy_check == true)
        {
            Player_attack();
        }
        else
        {
            Player_move();
        }
    }

    // 플레이어 이동 함수
    void Player_move()
    {
        if (move_state == true)
        {
            anim.SetInteger("motion", 1);
            agent.SetDestination(target_pos);
        }

        // 목적지에 도달시 정지 모션
        float target_distance = Vector3.Distance(transform.position, target_pos);
        if (target_distance < 0.5f)
        {
            anim.SetInteger("motion", 0);
            move_state = false;
        }
    }

    // 플레이어 공격 함수
    void Player_attack()
    {
        //적 위치에 맞게 다시 목적지 설정
        anim.SetInteger("motion", 1);
        agent.SetDestination(target_check.GetComponent<Target_check>().Enemy_pos);

        // 공격 사정거리로 진입
        float Enemy_distance = Vector3.Distance(transform.position, target_check.GetComponent<Target_check>().Enemy_pos);
        if (Enemy_distance < attack_sight)
        {
            // 적 크기에 맞게 공격 위치 설정
            RaycastHit Enemy_hit;
            Physics.Raycast(transform.position, transform.forward, out Enemy_hit, 100);
            Vector3 fixed_Enemy_pos = Enemy_hit.point;

            // 사정거리에 도달 시 공격 시작
            Enemy_distance = Vector3.Distance(transform.position, fixed_Enemy_pos);
            if (Enemy_distance < attack_range)
            {
                anim.SetInteger("motion", 2);
                agent.velocity = Vector3.zero;
                transform.LookAt(new Vector3(target_check.GetComponent<Target_check>().Enemy_pos.x, 0f, target_check.GetComponent<Target_check>().Enemy_pos.z));
            }
        }
    }

    void Create_basic_attack()
    {
        Debug.Log("Attack");
        basic_attack.SetActive(true);
        basic_attack.transform.position = target_check.GetComponent<Target_check>().Enemy_pos;
    }
}
