using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_hit : MonoBehaviour
{
    private float delete_time;

    void Update()
    {
        delete_time += Time.deltaTime;
        if (delete_time >= 0.2f)
        {
            gameObject.SetActive(false);
            delete_time = 0f;
        }
    }
}
