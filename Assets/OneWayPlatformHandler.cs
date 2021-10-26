using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformHandler : MonoBehaviour
{
    private GameObject mCurrentPlatform;

    [SerializeField] private BoxCollider2D playerBoxCollider;
    [SerializeField] private CircleCollider2D playerCircleCollider;
    

    private void OnCollisionEnter2D(Collision2D other)
    {
         if (other.gameObject.CompareTag("OneWay"))
        {
            mCurrentPlatform = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("OneWay"))
        {
            mCurrentPlatform = null;
        }
    }

    public IEnumerator DisableCollision()
    {
        if (mCurrentPlatform == null) yield break;
        
        var platformCollider = mCurrentPlatform.GetComponent<BoxCollider2D>();
        
        Physics2D.IgnoreCollision(playerBoxCollider, platformCollider);
        Physics2D.IgnoreCollision(playerCircleCollider, platformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerBoxCollider, platformCollider, false);
        Physics2D.IgnoreCollision(playerCircleCollider, platformCollider, false);

    }
}
