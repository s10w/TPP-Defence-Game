using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
public class PlayerControl : MonoBehaviour
{
    public LayerMask move_mask;
    public GameObject target_check;

    Camera cam;
    RaycastHit hit;
    PlayerMove move;
    public TargetObject focus;

    void Start()
    {
        cam = Camera.main;
        move = GetComponent<PlayerMove>();

        target_check = Instantiate(target_check);
        target_check.SetActive(false);
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
            // ray가 TargetObject에 닿았을 때
            if (Physics.Raycast(ray, out hit, 100))
            {
                TargetObject object_check = hit.collider.GetComponent<TargetObject>();
                if (object_check != null)
                {
                    SetFocus(object_check);
                }
            }

            // 오브젝트에 닿지 않았을 때 기본 이동
            if (focus == null)
            {
                move.MovePoint(hit.point);
            }
            target_check.SetActive(true);
            target_check.transform.position = hit.point;
        }
    }

    void SetFocus(TargetObject current_focus)
    {
        focus = current_focus;
        move.TargetChase(current_focus);
    }

    void RemoveFocus()
    {
        focus = null;
        move.StopChase();
    }
}
