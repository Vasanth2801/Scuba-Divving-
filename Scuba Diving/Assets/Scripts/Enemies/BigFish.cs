using UnityEngine;

public class BigFish : MonoBehaviour
{
    [Header("Point Preferences")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    Transform currentPoint;
    Vector3 initialScale;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 0.07f;
    [SerializeField] private float arrivalThreshold = 0.25f;

    private void Start()
    {
        currentPoint = pointB;
        initialScale = transform.localScale;
    }

    private void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        Vector3 targetPos = currentPoint.position;
        transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed);

        if(Vector3.Distance(transform.position, targetPos) <= arrivalThreshold)
        {
            currentPoint = currentPoint == pointA? pointB : pointA;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Game Over");
            Time.timeScale = 0f;
        }
    }
}