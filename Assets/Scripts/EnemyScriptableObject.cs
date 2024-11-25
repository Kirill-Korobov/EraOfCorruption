using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/Create New Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Основні характеристики")]
    public string enemyName;
    public int health;
    public int defense;
    public int damage;
    public float moveSpeed;

    [Header("Налаштування дропу")]
    public GameObject[] dropItems;
    public float dropChance; 

    [Header("Інші параметри")]
    public GameObject deathEffect;
}
