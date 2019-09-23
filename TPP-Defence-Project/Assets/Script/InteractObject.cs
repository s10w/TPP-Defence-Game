using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    Transform player;

    [SerializeField] float radius = 3f;
    public float getRadius() { return radius; }

    bool is_focus = false;
    bool interacted = false;

    public virtual void Interact()
    {
        // 이 함수는 재정의 될 것임
        Debug.Log("Interact : " + gameObject.name);
    }

    private void Update()
    {
        if (is_focus && !interacted)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= radius)
            {
                Interact();
                interacted = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OnFocused(Transform playerTransform)
    {
        is_focus = true;
        interacted = false;
        player = playerTransform;
    }

    public void DeFocused()
    {
        is_focus = false;
        interacted = true;
        player = null;
    }
}
