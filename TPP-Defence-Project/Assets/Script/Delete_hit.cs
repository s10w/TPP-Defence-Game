using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_hit : MonoBehaviour
{
    private float delete_time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
