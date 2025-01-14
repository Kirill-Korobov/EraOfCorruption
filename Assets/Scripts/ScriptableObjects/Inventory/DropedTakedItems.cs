using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ItemTypes
{
    Armor,
    Weapon,
    Poison,
    Food
}

public enum Poisons
{
    Regeneration,
    Strength,
    Speed,
    VampirismHP,
    VampirismMana,
    Resistance,
    Random
}
public enum DefenseTypes
{
    Helmet,
    Armor,
    Boots,
}
public enum WeaponTypes
{
    Sword,
    Bow,
    Mage
}

[CreateAssetMenu(fileName = "DropedTakedItemsConfig", menuName = "ScriptableObjects/DropedTakedItems")]
public class DropedTakedItems : ScriptableObject
{
    [SerializeField] private int id;
    public int ID { get => id; set => id = value; }
    [SerializeField] private int itemInOneSlot;
    public int ItemInOneSlot { get => itemInOneSlot; set => itemInOneSlot = value; }

    [SerializeField] private GameObject gameObject;
    public GameObject GameObject { get => gameObject; set => gameObject = value; }

    [SerializeField] private Sprite image;
    public Sprite Image { get => image; set => image = value; }

    [SerializeField] private int min;
    public int Min { get => min; set => min = value; }

    [SerializeField] private int max;
    public int Max { get => max; set => max = value; }

    [SerializeField] private int buy;
    public int Buy { get => buy; set => buy = value; }

    [SerializeField] private ItemTypes itemType;
    public ItemTypes ItemType { get => itemType; set => itemType = value; }

    [SerializeField] private int damage;     // Weapon specific
    public int Damage { get => damage; set => damage = value; }

    [SerializeField] private int range;      // Weapon specific
    public int Range { get => range; set => range = value; }

    [SerializeField] private int speed;      // Weapon specific
    public int Speed { get => speed; set => speed = value; }

    [SerializeField] private WeaponTypes weaponType; // Weapon specific
    public WeaponTypes WeaponType { get => weaponType; set => weaponType = value; }

    [SerializeField] private int defense;    // Armor specific
    public int Defense { get => defense; set => defense = value; }

    [SerializeField] private DefenseTypes defenseType; // Weapon specific
    public DefenseTypes DefenseType { get => defenseType; set => defenseType = value; }

    [SerializeField] private int nutrition;  // Food specific
    public int Nutrition { get => nutrition; set => nutrition = value; }

    [SerializeField] private Poisons poisonsType; // Poison specific
    public Poisons PoisonsType { get => poisonsType; set => poisonsType = value; }
}
