using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sciptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int damage;
    public Sprite icon;
    public float healAmount;

    public bool isPlaceable;
    public GameObject placeableItem;
    public float raiseMaxHealthAmount;
    public bool isMainWeapon;
}
