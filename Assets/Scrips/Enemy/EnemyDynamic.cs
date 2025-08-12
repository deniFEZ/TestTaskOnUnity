using UnityEngine;
using UnityEngine.Rendering;

public class EnemyDynamic : MonoBehaviour
{
    public float CurrentHP;

    private void OnEnable()
    {
        CurrentHP = this.GetComponent<EnemyStaticInformationHolder>().enemyStaticInfo.MaxHP;
    }
}
