using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private ParticleSystem deathEffect;
    public float dropChance;
    [SerializeField] private GameObject[] possibleDrops;
    private int randomIndex;
    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        TryDropItem();

        Destroy(gameObject);
    }

    private void TryDropItem()
    {
        if (Random.value <= dropChance && possibleDrops.Length > 0)
        {
            int randomIndex = Random.Range(0, possibleDrops.Length);
            GameObject drop = possibleDrops[randomIndex];

            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }
}
