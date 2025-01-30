using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] int HP;
    [SerializeField] Image hp;
    [SerializeField] int MaxHP;

    private void Awake()
    {
        HP = MaxHP;
    }
    public void EnemyAttaked(int attack)
    {
        HP -= attack;
        hp.fillAmount = HP/MaxHP;
    }
}
