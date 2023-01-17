using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    public Transform pos1, pos2;
    public Transform startPos;
    public float speed;
    public float rotatespeed;

    private GameObject target = null;
    private Vector3 offset;

    Vector3 nextPos;

    void Start()
    {
        nextPos = startPos.position;

        target = null;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotatespeed);

        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if(transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        target = other.gameObject;
        offset = target.transform.position - transform.position;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        target = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}