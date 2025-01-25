using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : MonoBehaviour
{
    public DropedTakedItems dti;
    public bool ifIsMustReload;
    public int howMuchDroped;
    [SerializeField] GameObject go;
    [SerializeField] private int reload;
    public int reloadIfLets;
    private void Start()
    {
        if (reloadIfLets != 0)
        {
            StartCoroutine(Reload());
        }
    }
    public void Take()
    {
        if (ifIsMustReload && go.activeSelf)
        {
            int a = Random.Range(dti.Min, dti.Max);
            StaticDropTake.sl.AddItem(dti, a);
            reloadIfLets = reload;
            StartCoroutine(Reload());
        }
        else if (!ifIsMustReload)
        {
            StaticDropTake.sl.AddItem(dti, howMuchDroped);
            Destroy(gameObject);
        }
    }
    private IEnumerator Reload()
    {
        go.SetActive(false);
        WaitForSeconds a = new WaitForSeconds(1);
        while (reloadIfLets > 0)
        {
            yield return a;
            reloadIfLets--;

        }
        go.SetActive(true);
    }
}
