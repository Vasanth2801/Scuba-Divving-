using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float fastSwim = 9f;

    [Header("Rush Swimming Settings")]
    [SerializeField] private float rushForce = 13f;
    [SerializeField] private bool isRushing = false;
    [SerializeField] private bool canRush = true;
    [SerializeField] private float rushDuration = 0.7f;
    [SerializeField] private float rushCoolDown = 2f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Inputs")]
    [SerializeField] private float moveInputX;
    [SerializeField] private float moveInputY;
    [SerializeField] private bool isMoving = false;

    private void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");

        if(moveInputX > 0 || moveInputY > 0 || moveInputX < 0 || moveInputY < 0)
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

        RushSwim();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInputX * speed, moveInputY * speed);
    }

    void FastSwim()
    {
        if(Input.GetKey(KeyCode.K) && isMoving)
        {
            rb.linearVelocity = new Vector2(moveInputX * fastSwim, moveInputY * fastSwim);
            Debug.Log("Fastly Swimming");
        }
    }

    void RushSwim()
    {
        if(Input.GetKey(KeyCode.L) && isMoving && canRush)
        {
            StartCoroutine(Rushing());
            Debug.Log("Rushing through");
        }
    }

    IEnumerator Rushing()
    {
        isRushing = true;
        canRush = false;
        rb.AddForce(new Vector2(moveInputX * rushForce,moveInputY * rushForce), ForceMode2D.Impulse);
        yield return new WaitForSeconds(rushDuration);
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(rushCoolDown);
        isRushing = false;
        canRush = true;
    }
}