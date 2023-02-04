using System;
using System.Collections;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    private Rigidbody2D body;

    public float moveSpeed = 10;

    public float switchRadius = 0.5f;
    private bool patroling = true;

    public Transform groundCheck;

    public LayerMask groundMask, playerLayer, switchlayer;

    public float groundCheckRadius;

    private bool recentlySwitched = false;
    public int dir = 1;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!patroling)
            return;

        if (Physics2D.OverlapCircle(transform.position, switchRadius, switchlayer))
        {
            if (!recentlySwitched) {
                Switch();
                body.velocity = new Vector2(0, body.velocity.y);
                recentlySwitched = true;
            }
        }
        else
            recentlySwitched = false;

        body.AddForce(Vector2.right * dir * moveSpeed);
    }

    private void Switch()
    {
        dir *= -1;

        Vector3 newScl = transform.localScale;
        newScl.x *= -1;

        transform.localScale = newScl;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}