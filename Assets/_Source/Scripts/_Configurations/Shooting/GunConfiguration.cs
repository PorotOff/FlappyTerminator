using UnityEngine;

[CreateAssetMenu(fileName = "GunConfiguration", menuName = "CONFIGURATIONS/Shooting/GunConfiguration", order = 0)]
public class GunConfiguration : ScriptableObject
{
    [field: SerializeField] public float ShootForce { get; private set; }
}