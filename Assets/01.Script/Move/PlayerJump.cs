using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    public float jumpPower = 10.0f;
    public bool groundCheck = true;
    public InputAction JumpAction;

    private void Awake()
    {
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() => JumpAction.Enable();
    private void OnDisable() => JumpAction.Disable();

    private void Update()
    {
        if (JumpAction.WasPerformedThisFrame()) Jump();

        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("IsGrounded", groundCheck);
    }

    public void Jump()
    {
        if (groundCheck)
        {
            groundCheck = false;
            anim.SetTrigger("JumpTrigger");
            anim.SetBool("IsGrounded", false);
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            groundCheck = true;
            anim.SetBool("IsGrounded", true);
        }
    }
}