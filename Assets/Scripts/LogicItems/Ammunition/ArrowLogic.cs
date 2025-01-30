using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLogic : MonoBehaviour
{
    public int speed;
    public int range;
    public int attack;
    private Transform transformStart;

    private void OnEnable()
    {
        transformStart = GetComponent<Transform>();
    }
    private void Update()
    {
        if (!LoadedSettings.ifAnyOpen && !LoadedSettings.ifInventoryOpen && !LoadedSettings.ifMapOpen && !LoadedSettings.ifQuestsOpen && !LoadedSettings.ifStatsOpen)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, transformStart.position) > range)
            {
                Debug.Log(Vector3.Distance(transform.position, transformStart.position));
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<AttackEnemy>().EnemyAttaked(attack);
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
            Destroy(gameObject);
        }
    }
}
