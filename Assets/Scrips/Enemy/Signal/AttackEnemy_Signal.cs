using UnityEngine;

public class AttackEnemy_Signal
{
    public GameObject enemyObject;
    public GameObject itemObject;
    public float damage;
    public AttackEnemy_Signal(GameObject _enemyObject, GameObject _itemObject, float _damage)
    {
        enemyObject = _enemyObject;
        itemObject = _itemObject;
        damage = _damage;
    }
}
