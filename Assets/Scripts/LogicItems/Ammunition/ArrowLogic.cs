using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLogic : MonoBehaviour
{
    public int speed;
    public int range;
    public int attack;
    private Vector3 transformStart;

    private void OnEnable()
    {
        transformStart = GetComponent<Transform>().position;

    }
    private void Update()
    {
        if (!LoadedSettings.ifAnyOpen && !LoadedSettings.ifInventoryOpen && !LoadedSettings.ifMapOpen && !LoadedSettings.ifQuestsOpen && !LoadedSettings.ifStatsOpen)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, transformStart) > range)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Enemy"))
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
        else if(other.name != "item")
        {
            Destroy(gameObject);
        }
    }
}
