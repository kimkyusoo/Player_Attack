using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("체력")]
    public float currentHp;
    public float maxHp = 100f;

    public event Action OnDied;
    public event Action<float, float> OnHealthChanged;

    private Animator animator;
    private Collider collider;
    public float displayTime = 4.0f;

    private void Awake()
    {
        if(animator == null) animator = GetComponentInChildren<Animator>();
        if(collider == null) collider = GetComponentInChildren<Collider>();
        currentHp = maxHp;

    }

    
    public void TakeDamage(float damage)
    {
        if (currentHp <= 0) return;

        currentHp -= damage;

        Debug.Log($"TakeDamage damage: {damage}, currentHp: {currentHp}");

        OnHealthChanged?.Invoke(currentHp, maxHp);

        if (currentHp <= 0)
        {
            IsDead();
        }
    }

    public void Heal(float heal)
    {
        if( heal <= 0) return;

        if (currentHp <= 0) return;

        currentHp = Mathf.Min(currentHp + heal, maxHp);

        Debug.Log($"TakeDamage heal: {heal}, currentHp: {currentHp}");

        OnHealthChanged?.Invoke(currentHp, maxHp);
    }

    private void IsDead()
    {
        //collider.enabled = false;
        Debug.Log("IsDead");
        OnDied?.Invoke();

        if (animator != null)
        {
            animator.SetTrigger("DieTrigger");
        }
        
        // 공부.
        // Invoke - 지연 호출
        // DistoryEnemy 함수를 displayTime만큼 지연 후 호출
        Invoke("DistoryEnemy", displayTime);
    }

    private void DistoryEnemy()
    {
        gameObject.SetActive(false);
    }
}