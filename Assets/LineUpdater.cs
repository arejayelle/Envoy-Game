using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineUpdater : MonoBehaviour
{
    [SerializeField] LineRenderer  lineRenderer;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;
    private Vector3 endPosition;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, startTransform.position);

        if (endTransform != null)
        {
            endPosition = endTransform.position;
        }            
        lineRenderer.SetPosition(1, endPosition);

    }

    void SetTarget(Transform target)
    {
        endTransform = target;
    }

    public IEnumerator PullTarget(Transform target)
    {
        endTransform = target;
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        lineRenderer.enabled = false;
        endTransform = null;

    }    
    public IEnumerator PullNothing(Vector3 away)
    {
        endPosition = away;
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.4f);
        lineRenderer.enabled = false;
        

    }
}
