using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] int HP;
    [SerializeField] TMP_Text hp;
    [SerializeField] int MaxHP;

    private void Awake()
    {
        HP = MaxHP;
    }
    public void EnemyAttaked(int attack)
    {
        HP -= attack;
        hp.text = HP + "";
    }
}
