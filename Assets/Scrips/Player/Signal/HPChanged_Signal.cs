using UnityEngine;

public class HPChanged_Signal
{
    public float change;
    public float currentHP;
    public HPChanged_Signal(float _change, float _currentHP)
    {
        change = _change;
        currentHP = _currentHP;
    }
}
