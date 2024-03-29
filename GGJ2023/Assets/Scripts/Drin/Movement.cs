﻿using System.Collections;
using UnityEngine;

[System.Serializable]
public struct MovementInfo
{
    public float speed, groundFriction, airFriction, maxSpeed;
    public float jumpGravity, fallGravity, jumpForce, jumpTrauma, landTrauma, dJumpTruama;
    public float dashForce, dashWaitTime, dashTrauma, dashFreeze;
}

public class Movement : MonoBehaviour
{
    [SerializeField]
    public MovementInfo info;
    public AudioSource movementSource, landingSource, jumpSource, dashSource;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public static Movement instance;
    public bool died = false;

    public Animator anim;

    private Rigidbody2D body;

    private float dir, lastDir;
    [SerializeField]
    public bool jumped, grounded, lastGrounded, dashed, isDashing, canDash, canDJump;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        if (instance == null) instance = this;
    }

    void Update()
    {
        if (died)
        {
            body.velocity = Vector2.zero;
            return;
        }
        dir = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(dir) < 0.3f)
            movementSource.Stop();

        if (dir != 0)
        {
            if (dir != lastDir) Switch();
            lastDir = dir;
        }

        Collider2D[] grounds = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);

        grounded = false;

        foreach (Collider2D gr in grounds)
        {
            if (gr.isTrigger)
                continue;
            grounded = true;
        }

        anim.SetBool("grounded", grounded);
        if (grounded)
        {
            if (!lastGrounded)
            {
                landingSource.Play();
                CameraController.Shake(info.landTrauma);
            }
            canDash = true;
            body.velocity = new Vector2(body.velocity.x, 0f);
            anim.SetBool("moving", (body.velocity.x != 0));
            if (Input.GetButton("Jump"))
                jumped = true;
            else
                jumped = false;
        }
        else
        {
            movementSource.Stop();
            // anim.SetBool("moving", false);

            if (body.velocity.y < 0)
                body.gravityScale = info.fallGravity;
            else
                body.gravityScale = info.jumpGravity;
        }

        if (canDash && Input.GetButtonDown("Dash") && !isDashing)
        {
            dashed = true;
            canDash = false;
        }

        lastGrounded = grounded;
    }

    void FixedUpdate()
    {
        if (died)
        {
            return;
            body.velocity = Vector2.zero;
        }
        Vector2 newDir = new Vector2(dir * info.speed, 0f);

        if (jumped)
        {
            body.AddForce(Vector2.up * info.jumpForce, ForceMode2D.Impulse);
            CameraController.Shake(info.jumpTrauma);
            jumpSource.Stop();
            jumpSource.Play();
            jumped = false;
        }

        if (!isDashing)
        {
            if (dashed)
            {
                body.velocity = new Vector2(0, body.velocity.y);
                body.AddForce(new Vector2(lastDir * info.dashForce, 0f), ForceMode2D.Impulse);
                StartCoroutine(DashUp());
                dashed = false;
                CameraController.Shake(info.dashTrauma);
                dashSource.Stop();
                dashSource.Play();
                Utils.Freeze(info.dashFreeze);
                return;
            }

            if (Mathf.Abs(dir) > 0.3f && grounded)
            {
                if (!movementSource.isPlaying)
                    movementSource.Play();
            }

            body.AddForce(newDir);
            if (Mathf.Abs(body.velocity.x) > info.maxSpeed)
                body.velocity = new Vector2(body.velocity.normalized.x * info.maxSpeed, body.velocity.y);

            float friction = info.groundFriction;

            if (!grounded)
                friction = info.airFriction;

            body.AddForce(new Vector2(body.velocity.normalized.x * -friction, 0f));

            if (dir == 0 && Mathf.Abs(body.velocity.x) < 0.7f)
                body.velocity = new Vector2(0f, body.velocity.y);
        }
    }

    private IEnumerator DashUp()
    {
        isDashing = true;
        yield return new WaitForSeconds(info.dashWaitTime);
        isDashing = false;
        yield break;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    private void Switch()
    {
        Vector3 newScl = transform.localScale;
        newScl.x = Mathf.Sign(dir);

        transform.localScale = newScl;
    }
}