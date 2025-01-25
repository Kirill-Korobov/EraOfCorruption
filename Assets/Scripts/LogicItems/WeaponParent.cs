using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponParent : MonoBehaviour
{
    [SerializeField] private MC_ManaManager MC_ManaManager;
    public DropedTakedItems dti { get; [SerializeField] private set; }
    private bool attack = true;
    private int o = 0;


    private void Awake()
    {
        dti = GetComponent<ScriptableObjectUsedItems>().dti;
    }

    private void Update()
    {
        if(Input.GetKeyDown(LoadedSettings.attack))
        {
            if (dti.WeaponType == WeaponTypes.Bow )
            {
                if (StaticDropTake.sl.ArrowUse() && attack && dti.ManaCost <= MC_ManaManager.Mana)
                {
                    BowUse();
                }
            }
            else if (attack && dti.ManaCost <= MC_ManaManager.Mana)
            {
                MC_ManaManager.SpendMana(dti.ManaCost);
                Attack();
                Debug.Log(1);
                StartCoroutine(Reload(dti.Reload));
            }
        }
    }

    public abstract void BowUse();
    public abstract void Attack();
    private void OnEnable()
    {
        Debug.Log(o);
        Reload(dti.Reload - o);
    }
    private IEnumerator Reload(int b)
    {
        attack = false;
        WaitForSeconds a = new WaitForSeconds(1);
        o = 0;
        Debug.Log(o >= b);
        while (o < b)
        {
            yield return a;
            o++;
            Debug.Log(o);
        }
        o = 0;
        attack = true;
    }
}
