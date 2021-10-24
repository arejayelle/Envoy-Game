using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;

    private float kWaitTime = 0.3f;
    private float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            waitTime = kWaitTime;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = kWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Fire1"))
            effector.rotationalOffset = 0;
    }
}
