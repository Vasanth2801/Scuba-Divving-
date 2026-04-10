using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("Point References")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    Transform currentPoint;
    Vector3 initialScale;

    [Header("Movement Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float arrivalThreshold = 0.25f;
    [SerializeField] private int facingDirection = 1;

    void Start()
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

        if (Vector3.Distance(transform.position, targetPos) <= arrivalThreshold)
        {
            currentPoint = currentPoint == pointA ? pointB : pointA;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Game Over");
            UIManager.Instance.GameOver();
            Time.timeScale = 0f;
        }
    }
}