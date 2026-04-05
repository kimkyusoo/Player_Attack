using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatSystem : MonoBehaviour
{
    [Header("공격 설정")]
    public float attackDamage = 25f;
    public float attackRange = 3f;
    public float attackCooldown = 0.5f;

    [Header("입력 설정")]
    public InputAction attackAction;

    private Animator animator;
    private float lastAttackTime = -999f;

    private void Awake()
    {
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        attackAction.Enable();
    }

    private void OnDisable()
    {
        attackAction.Disable();
    }

    private void Update()
    {
        if (attackAction.WasPerformedThisFrame())
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
            }
            else
            {
                Debug.Log("attackAction 동작중 쿨타임 발생");
            }
        }
    }

    private void Attack()
    {
        lastAttackTime = Time.time;

        if (animator != null)
        {
            animator.SetTrigger("AttackTrigger");
            Debug.Log("Attack Animation 실행");
        }

        
        Vector3 attackCenter = transform.position + transform.forward * 1.0f;

        // 공부
        // Physics.OverlapSphere
        // 공을 던져서 그 범위 내에 닿는 모든 물체 찾아오기(=범위 체크용)
        Collider[] hitColliders = Physics.OverlapSphere(attackCenter, attackRange);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject == gameObject)
            {
                continue;
            }

            HealthManager targetHealth = collider.GetComponent<HealthManager>();

            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 attackCenter = transform.position + transform.forward * 1.0f;
        Gizmos.DrawWireSphere(attackCenter, attackRange);
    }
}
