using UnityEngine;

public enum ZoneType
{
    None,
    Poison,
    Heal 
}

public class Zone : MonoBehaviour
{
    [Header("Zone에 대한 설정")]
    public ZoneType zoneType;
    public float effectAmount = 10f;
    public float tickCooldown = 1.0f;
}
