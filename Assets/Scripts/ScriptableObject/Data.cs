using JetBrains.Annotations;
using System;
using UnityEngine;

public enum Consumable
{
    Health,
    Energy
}

public enum ItemType
{
    Equipable,
    Consumable,
    Resource
}

[Serializable]
public class ItemDataConsumable
{
    public Consumable type;
    public float value;
}

[CreateAssetMenu(fileName = "ItemData", menuName = "SO/ItemData")]
public class Data : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public ItemType type;
    public string itemDescription;

    [Header("Consumable Info")]
    public ItemDataConsumable[] consumables;
}
