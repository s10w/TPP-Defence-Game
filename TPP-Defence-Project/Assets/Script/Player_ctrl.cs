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
    public GameObject target_check;

    private Coroutine attack_Loop;

    public bool move_state { get; private set; }
    public bool attack_state { get; private set; }

    // Player_ctrl의 변수 초기화
    void Player_ctrl_init()
    {
        target_check = Instantiate(target_check);
        target_check.SetActive(false);

        move_state = false;
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
        // 적 오브젝트가 있는 지 확인 
        if (Input.GetMouseButton(1))
        {
            target_check.SetActive(true);
            Physics.Raycast(main_camera.ScreenPointToRay(Input.mousePosition), out hit, 100);
            target_check.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
        }

        if (target_check.GetComponent<Target_check>().enemy_check == true)
        {
            attack_state = true;
            anim.SetInteger("motion", 2);
            attack_Loop = StartCoroutine(Player_attack());
        }

        if (target_check.GetComponent<Target_check>().enemy_check == false)
        {
            if (attack_state == true)
            {
                StopCoroutine(attack_Loop);
            }
            attack_state = false;
            Player_move();
        }
    }

    // 플레이어 이동
    void Player_move()
    {
        if (Input.GetMouseButton(1))
        {
            move_state = true;
            anim.SetInteger("motion", 1);

            target_pos = hit.point;
            agent.SetDestination(target_pos);
        }
        float target_distance = Vector3.Distance(transform.position, target_pos);
        if (target_distance < 0.5f)
        {
            move_state = false;
            anim.SetInteger("motion", 0);
        }
    }

    IEnumerator Player_attack()
    {
        Debug.Log("attack");
        yield return new WaitForSeconds(1f);
    }
}
