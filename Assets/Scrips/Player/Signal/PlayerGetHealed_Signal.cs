using UnityEngine;

public class PlayerGetHealed_Signal
{
    public GameObject itemObject;
    public float heal;
    public PlayerGetHealed_Signal(GameObject _itemObject, float _heal)
    {
        itemObject = _itemObject;
        heal = _heal;
    }
}
