using UnityEngine;

public class PlayerDynamic : MonoBehaviour
{
    public float CurrentHP;
    public void OnEnable()
    {
        CurrentHP = this.GetComponent<PlayerStaticInformationHolder>().playerStaticInfo.MaxHP;
    }
}
