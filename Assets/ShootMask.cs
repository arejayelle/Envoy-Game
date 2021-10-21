using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMask : MonoBehaviour
{
    public GameObject mProjectile;
    public Transform shotPoint;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire4"))
        {
            Instantiate(mProjectile,shotPoint.position, transform.rotation);
        }
    }
}
