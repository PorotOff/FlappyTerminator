using UnityEngine;

[CreateAssetMenu(fileName = "BulletConfiguration", menuName = "CONFIGURATIONS/Shooting/BulletConfiguration", order = 0)]
public class BulletConfiguration : ScriptableObject
{
    [field: SerializeField] public int Damage { get; private set; }
}