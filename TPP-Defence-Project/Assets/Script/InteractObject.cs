using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    Transform player;

    public float radius = 3f;
    bool is_focus = false;
    bool interacted = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public virtual void Interact()
    {

    }

    private void Update()
    {
        if (is_focus && !interacted)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= radius)
            {
                Debug.Log("INTERECT");
                Interact();
                interacted = true;
            }
        }
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
