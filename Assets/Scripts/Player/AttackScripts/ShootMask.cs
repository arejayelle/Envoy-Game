using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Player;
using UnityEngine;

public class ShootMask : MonoBehaviour
{
    public GameObject mProjectile;
    public Transform shotPoint;
    
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetComponent<PlayerController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire4"))
        {
            if (player.state == PlayerState.Chilling)
            {
                player.state = PlayerState.Masking;
                Instantiate(mProjectile,shotPoint.position, transform.rotation);

                Invoke("RestoreState", .07f);
            }
        }
    }

    void RestoreState()
    {
        player.state = PlayerState.Chilling;

    }
}
