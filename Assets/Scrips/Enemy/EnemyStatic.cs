using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatic", menuName = "Scriptable Objects/EnemyStatic")]
public class EnemyStatic : ScriptableObject
{
    public float MaxHP;
    public float Damage;
    public float Speed;
}
