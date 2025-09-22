using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponParent : MonoBehaviour
{
    public MC_ManaManager MC_ManaManager;
    public DropedTakedItems dti;
    public bool attack = true;
    private int o = 0;
    private bool start;

    private void Awake()
    {
        dti = GetComponent<ScriptableObjectUsedItems>().dti;
        attack = true;
    }
    private void Start()
    {
        start = false;
    }

    public abstract void Attack();
    private void OnEnable()
    {
        if (start)
        {
            StartCoroutine(Reload(dti.Reload - o));
        }
    }
    public IEnumerator Reload(int b)
    {
        attack = false;
        WaitForSeconds a = new WaitForSeconds(1);
        start = true;
        o = 0;
        while (o < b)
        {
            Debug.Log("reload");
            if(!LoadedSettings.ifAnyOpen && !LoadedSettings.ifInventoryOpen && !LoadedSettings.ifMapOpen && !LoadedSettings.ifQuestsOpen && !LoadedSettings.ifStatsOpen)
            {
                yield return a;
                o++;
            }
        }
        o = 0;
        attack = true;
        start = false;
    }
}
