using System.Diagnostics;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DropedTakedItems))]
public class DropedTakedItemsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DropedTakedItems item = (DropedTakedItems)target;

        item.ID = EditorGUILayout.IntField("ID", item.ID);
        item.ItemInOneSlot = EditorGUILayout.IntField("Item In One Slot", item.ItemInOneSlot);
        item.Drop = (GameObject)EditorGUILayout.ObjectField("Drop game object", item.Drop, typeof(GameObject), false);
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
                item.WeaponName = EditorGUILayout.TextField("Weapon Name", item.WeaponName);
                item.Damage = EditorGUILayout.IntField("Damage", item.Damage);
                item.Reload = EditorGUILayout.IntField("Reload", item.Reload);
                item.ManaCost = EditorGUILayout.IntField("Mana Cost", item.ManaCost);
                item.WeaponType = (WeaponTypes)EditorGUILayout.EnumPopup("Weapon Type", item.WeaponType);
                switch (item.WeaponType)
                {
                    case WeaponTypes.Sword:
                        item.Splash = EditorGUILayout.Toggle("Splash", item.Splash);
                        item.Range = EditorGUILayout.IntField("Range", item.Range);
                        break;
                    case WeaponTypes.Bow:
                        item.Range = EditorGUILayout.IntField("Range", item.Range);
                        item.Speed = EditorGUILayout.IntField("Speed", item.Speed);
                        break;
                    case WeaponTypes.Mage:
                        item.Speed = EditorGUILayout.IntField("Speed", item.Speed);
                        item.HowMuch = EditorGUILayout.IntField("Timer", item.HowMuch);
                        item.MagicSplash = EditorGUILayout.Toggle("Magic Splash", item.MagicSplash);
                        break;

                }
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

        switch (item.Splash)
        {
            case true:
                item.AttackAngle = EditorGUILayout.IntField("AttackAngle", item.AttackAngle);
                EditorGUILayout.LabelField("Attack Angle Degrees", item.AttackAngle.ToString() + "°");
                break;
        }
        EditorUtility.SetDirty(item);
    }
}
