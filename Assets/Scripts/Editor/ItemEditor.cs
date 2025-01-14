using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DropedTakedItems))]
public class DropedTakedItemsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DropedTakedItems item = (DropedTakedItems)target;

        item.ItemInOneSlot = EditorGUILayout.IntField("Item In One Slot", item.ItemInOneSlot);
        item.GameObject = (GameObject)EditorGUILayout.ObjectField("Game Object", item.GameObject, typeof(GameObject), false);
        item.Image = (Sprite)EditorGUILayout.ObjectField("Image", item.Image, typeof(Sprite), false);
        item.Min = EditorGUILayout.IntField("Min", item.Min);
        item.Max = EditorGUILayout.IntField("Max", item.Max);
        item.ItemType = (ItemTypes)EditorGUILayout.EnumPopup("Item Type", item.ItemType);

        // Відступ між загальними і специфічними властивостями
        GUILayout.Space(10);

        // Відображення специфічних властивостей залежно від типу предмета
        switch (item.ItemType)
        {
            case ItemTypes.Weapon:
                item.Damage = EditorGUILayout.IntField("Damage", item.Damage);
                item.Range = EditorGUILayout.IntField("Range", item.Range);
                item.Speed = EditorGUILayout.IntField("Speed", item.Speed);
                item.WeaponType = (WeaponTypes)EditorGUILayout.EnumPopup("Weapon Type", item.WeaponType);
                break;

            case ItemTypes.Armor:
                item.Defense = EditorGUILayout.IntField("Defense", item.Defense);
                item.DefenseType = (DefenseTypes)EditorGUILayout.EnumPopup("Defense Type", item.DefenseType);
                break;

            case ItemTypes.Food:
                item.Nutrition = EditorGUILayout.IntField("Nutrition", item.Nutrition);
                break;

            case ItemTypes.Poison:
                item.PoisonsType = (Poisons)EditorGUILayout.EnumPopup("Poison Type", item.PoisonsType);
                break;
        }

        EditorUtility.SetDirty(item);
    }
}
