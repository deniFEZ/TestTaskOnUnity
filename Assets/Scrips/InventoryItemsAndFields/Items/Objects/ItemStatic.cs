using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemStatic", menuName = "Scriptable Objects/Item")]
public class ItemStatic : ScriptableObject
{
    public int ID;
    public int Cost;
    public int Level;
    public float ChanceToBeSpawnedInPercentage;
    public float Damage;
    public float AttackSpeed;
    public string NameItem;
    public string DescriptionItem;
    public int x;
    public int y;
    public GameObject Prefab;
    public GameObject NextLevelItem;
    public enum FormsOfActivation { Manually, Automated };
    public FormsOfActivation FormOfActivation;
    public enum FormsOfWeapon { Normal, r_Form };
    public FormsOfWeapon FormOfWeapon;
    public enum TypesOfItem { Healing, Attacking };
    public TypesOfItem Type;
}
