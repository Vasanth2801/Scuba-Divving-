using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float fastSwim = 9f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Inputs")]
    [SerializeField] private float moveInputX;
    [SerializeField] private float moveInputY;
    [SerializeField] private bool isMoving;

    private void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");

        if(moveInputX > 0 ||  moveInputY > 0 || moveInputX < 0 || moveInputY < 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void FixedUpdate()
    {
        Move();

        FastSwim();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(moveInputX * speed, moveInputY * speed);
    }

    void FastSwim()
    {
        if(Input.GetKey(KeyCode.K) && isMoving)
        {
            rb.linearVelocity = new Vector2(moveInputX * fastSwim, moveInputY * fastSwim);
            Debug.Log("Player Swimming Fastly");
        }
    }
}