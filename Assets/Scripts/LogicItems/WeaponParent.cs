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
    }
    private void Start()
    {
        start = true;
    }

    public abstract void Attack();
    private void OnEnable()
    {
        if (start)
        {
            Reload(dti.Reload - o);
        }
    }
    public IEnumerator Reload(int b)
    {
        attack = false;
        WaitForSeconds a = new WaitForSeconds(1);
        o = 0;
        while (o < b)
        {
            if(!LoadedSettings.ifAnyOpen && !LoadedSettings.ifInventoryOpen && !LoadedSettings.ifMapOpen && !LoadedSettings.ifQuestsOpen && !LoadedSettings.ifStatsOpen)
            {
                yield return a;
                o++;
            }
        }
        o = 0;
        attack = true;
    }
}
