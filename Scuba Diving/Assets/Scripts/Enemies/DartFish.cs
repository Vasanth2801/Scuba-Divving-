using UnityEngine;

public class DartFish : MonoBehaviour
{
    [Header("Point References")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    Transform currentPoint;
    Vector3 initialScale;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 0.07f;
    [SerializeField] private float arrivalThresHold = 0.25f;
    [SerializeField] private float facingDirection = 1;

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

        if (Vector3.Distance(transform.position, targetPos) <= arrivalThresHold)
        {
            currentPoint = currentPoint == pointA ? pointB : pointA;
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Game Over");
            UIManager.Instance.GameOver();
            Time.timeScale = 0f;
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}