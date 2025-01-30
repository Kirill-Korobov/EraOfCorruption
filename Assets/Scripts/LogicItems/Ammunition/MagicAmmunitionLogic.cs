using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAmmunitionLogic : MonoBehaviour
{
    public bool ifMagicSplash;
    public int timer;
    public int speed;
    public int attack;
    

    private void OnEnable()
    {
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        int n = 0;
        WaitForSeconds a = new WaitForSeconds(1);
        while (n < timer)
        {
            if(!LoadedSettings.ifAnyOpen || !LoadedSettings.ifInventoryOpen || !LoadedSettings.ifMapOpen || !LoadedSettings.ifQuestsOpen || !LoadedSettings.ifStatsOpen)
            {
                n++;
                yield return a;
            }
        }
        Destroy(gameObject);

    }
    private void Update()
    {
        if (!LoadedSettings.ifAnyOpen && !LoadedSettings.ifInventoryOpen && !LoadedSettings.ifMapOpen && !LoadedSettings.ifQuestsOpen && !LoadedSettings.ifStatsOpen)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!ifMagicSplash)
            {
                other.gameObject.GetComponent<AttackEnemy>().EnemyAttaked(attack);
                if (StaticEffects.vampirismHP)
                {
                    StaticEffects.VampirismHPLogic(attack);
                }
                if (StaticEffects.vampirismMana)
                {
                    StaticEffects.VampirismManaLogic(attack);
                }
                Destroy(gameObject);
            }
            else
            {
                other.gameObject.GetComponent<AttackEnemy>().EnemyAttaked(attack);
                if (StaticEffects.vampirismHP)
                {
                    StaticEffects.VampirismHPLogic(attack);
                }
                if (StaticEffects.vampirismMana)
                {
                    StaticEffects.VampirismManaLogic(attack);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }  
}
