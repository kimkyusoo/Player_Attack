using UnityEngine;

public class PlayerZoneTracker : MonoBehaviour
{
    [SerializeField]
    private HealthManager healthManager;

    [SerializeField]
    private Zone currentZone;

    private float lastTickTime;

    private void Awake()
    {
        if(healthManager == null) healthManager = GetComponent<HealthManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Zone zone = other.GetComponent<Zone>();

        if (zone == null) return;

        Debug.Log($"PlayerZoneTracker Zone : {zone.zoneType}");

        switch (zone.zoneType)
        {
            case ZoneType.Heal:
                ExecuteHeal(zone);
                Destroy(zone.gameObject);
                break;

            case ZoneType.Poison:
                currentZone = zone;
                lastTickTime = Time.time;
                ExecuteTickDamage(zone);
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(currentZone == null) return;

        if(other.gameObject != currentZone.gameObject) return;

        Debug.Log($"PlayerZoneTracker OnTriggerStay CurrentZone : {currentZone.zoneType}");

        if (Time.time >= lastTickTime + currentZone.tickCooldown)
        {
            lastTickTime = Time.time; 
            ExecuteTickDamage(currentZone);
        }
        else
        {
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentZone != null && other.gameObject == currentZone.gameObject)
        {
            Debug.Log($"PlayerZoneTracker Delete Current Zone : {currentZone}");
            currentZone = null;
        }
    }

    private void ExecuteHeal(Zone zone)
    {
        if(zone.zoneType != ZoneType.Heal) return;

        if(healthManager == null) return;

        if(zone.effectAmount <= 0) return;

        Debug.Log($"PlayerZoneTracker Heal : {zone.effectAmount}");

        healthManager.Heal(zone.effectAmount);

    }

    private void ExecuteTickDamage(Zone zone)
    {
        if(zone.zoneType != ZoneType.Poison) return;

        if(healthManager == null) return;

        if(zone.effectAmount <= 0) return;

        Debug.Log($"PlayerZoneTracker Tick Damage : {zone.effectAmount}");

        healthManager.TakeDamage(zone.effectAmount);
    }

}
