using System;
using System.Collections;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    private Rigidbody2D body;

    private bool patroling = true, shooting = false, canShoot = false;

    public Transform groundCheck, bulletHole;

    public LayerMask groundMask, playerLayer;

    public float groundCheckRadius;

    public int dir = 1;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!patroling)
            return;

        if (!Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask))
        {
            Switch();
            body.velocity = new Vector2(0, body.velocity.y);
        }

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