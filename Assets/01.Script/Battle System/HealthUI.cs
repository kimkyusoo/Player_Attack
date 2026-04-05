using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public HealthManager healthManager;
    public Slider hpBar;

    private void Awake()
    {
        if(healthManager == null) healthManager = GetComponent<HealthManager>();
        if(hpBar == null) hpBar = GetComponent<Slider>();
    }

    private void Start()
    {
        UpdateSlider(healthManager.currentHp, healthManager.maxHp);
    }

    private void OnEnable()
    {
        if(healthManager != null)
        {
            healthManager.OnHealthChanged += UpdateSlider;
        }
    }

    private void OnDisable()
    {
        if (healthManager != null)
        {
            healthManager.OnHealthChanged -= UpdateSlider;
        }
    }

    private void UpdateSlider(float currentHp, float maxHp)
    {
        hpBar.value = currentHp / maxHp;
    }


}
