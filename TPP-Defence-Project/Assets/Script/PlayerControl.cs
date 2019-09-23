using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
public class PlayerControl : MonoBehaviour
{
    #region Singleton

    public static PlayerControl instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] LayerMask move_mask; // Grond Layer
    [SerializeField] GameObject move_point; // 목적지 표시 오브젝트
    Camera cam;
    RaycastHit hit;

    [SerializeField] InteractObject focus; // InteractObject 객체
    PlayerMove move; // PlayerMove 객체

    public bool attack_state { get; private set; }
    public bool pickup_state { get; private set; }

    void Start()
    {
        cam = Camera.main;
        move = GetComponent<PlayerMove>();

        move_point = Instantiate(move_point);
        move_point.SetActive(false);
        attack_state = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // 마우스 위치 저장
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // ray가 Grond Layer에 닿았을 때
            if (Physics.Raycast(ray, out hit, 100, move_mask))
            {
                RemoveFocus();
            }
            // ray가 InteractObject에 닿았을 때
            if (Physics.Raycast(ray, out hit, 100))
            {
                InteractObject target = hit.collider.GetComponent<InteractObject>();
                if (target != null)
                {
                    SetFocus(target);
                }
            }

            // ray가 Grond Layer에 닿았을 때 기본 이동
            if (focus == null)
            {
                attack_state = false;
                move.MovePoint(hit.point);
            }
            CreatPoint();
        }
    }

    void CreatPoint()
    {
        move_point.SetActive(true);
        move_point.transform.position = hit.point;
    }

    void SetFocus(InteractObject current_focus)
    {
        if (focus != current_focus)
        {
            if (focus != null)
            {
                focus.DeFocused();
            }
            focus = current_focus;
            current_focus.OnFocused(transform);

            if (current_focus.tag == "Enemy")
            {
                attack_state = true;
            }
            move.TargetChase(current_focus);
        }
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.DeFocused();
        }
        focus = null;
        move.StopChase();
    }
}
