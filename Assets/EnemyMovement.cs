using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;
    public float distance= 2f;
    private bool isMovingRight = true;
    public Transform GroundDetection;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (isMovingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                isMovingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isMovingRight = true;
            }
        }
    }

    public void Die()
    {
        enabled = false;
    }
}
