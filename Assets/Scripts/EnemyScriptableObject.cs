using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/Create New Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("������ ��������������")]
    public string enemyName;
    public int health;
    public int defense;
    public int damage;
    public float moveSpeed;

    [Header("������������ �����")]
    public GameObject[] dropItems;
    public float dropChance; 

    [Header("���� ���������")]
    public GameObject deathEffect;
}
