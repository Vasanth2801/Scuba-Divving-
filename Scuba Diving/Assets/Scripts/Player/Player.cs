using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float fastSwim = 9f;
    [SerializeField] private int facingDirection = 1;

    [Header("Rush Swimming Settings")]
    [SerializeField] private float rushForce = 13f;
    [SerializeField] private bool isRushing = false;
    [SerializeField] private bool canRush = true;
    [SerializeField] private float rushDuration = 0.7f;
    [SerializeField] private float rushCoolDown = 2f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

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

        if(moveInputX > 0 && transform.localScale.x < 0 || moveInputX < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        Move();

        FastSwim();

        StopFastSwim();

        RushSwim();

        HandleAnimations();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInputX * speed, moveInputY * speed);
    }

    void FastSwim()
    {
        if(Input.GetKey(KeyCode.K) && isMoving)
        {
            animator.SetBool("isSwimming",true);
            rb.linearVelocity = new Vector2(moveInputX * fastSwim, moveInputY * fastSwim);
        }
    }

    void StopFastSwim()
    {
        if(Input.GetKeyUp(KeyCode.K))
        {
            animator.SetBool("isSwimming", false);
        }
    }

    void RushSwim()
    {
        if(Input.GetKey(KeyCode.L) && isMoving && canRush)
        {
            StartCoroutine(Rushing());
            animator.SetTrigger("RushSwim");
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

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,transform.localScale.z);
    }

    void HandleAnimations()
    {
        animator.SetFloat("Speed",Mathf.Abs(moveInputX));
    }
}