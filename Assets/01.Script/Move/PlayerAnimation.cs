using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private PlayerJump playerJump;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        playerJump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        if (anim == null || rb == null || playerJump == null) return;

        float hSpeed = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude;
        anim.SetFloat("Speed", hSpeed);

        anim.SetBool("IsGrounded", playerJump.groundCheck);

        float yVel = rb.linearVelocity.y;
        anim.SetFloat("yVelocity", yVel);

    }
}