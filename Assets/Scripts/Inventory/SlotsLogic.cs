using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ssms = StaticSaveMovesSprites;
public class SlotsLogic : MonoBehaviour
{
    private List<DropedTakedItems> dpis = new List<DropedTakedItems>();
    private List<TMP_Text> texts = new List<TMP_Text>();
    private List<Image> slots = new List<Image>();
    [SerializeField] private List<GameObject> slotsGet = new List<GameObject>();
    [SerializeField] private List<MoveSprites> MoveSprites = new List<MoveSprites>();
    private int[] ints = new int[50];
    public Image Image;
    
    private void Awake()
    {
        StaticDropTake.sl = this;
        for (int i = 0; i < slotsGet.Count; i++)
        {
            dpis.Add(null);
            texts.Add(slotsGet[i].GetComponentsInChildren<TMP_Text>()[0]);
            slots.Add(slotsGet[i].GetComponentsInChildren<Image>()[1]);
        }
    }
    public void Move(int i, DropedTakedItems dpi, int howmuch, Image image)
    {
        dpis[i] = dpi;
        ints[i] = howmuch;
        slots[i] = image;
        StaticDropTake.hotbar.SetHotbar(slots, ints, dpis);
    }
    public void AddItem(DropedTakedItems item, int num)
    {
        if (dpis.Contains(item))
        {
            List<int> list = new List<int>();
            for(int i = 0; i < dpis.Count; i++)
            {
                if (dpis[i] == item)
                {
                    list.Add(i);
                }
            }
            for (int index = 0; index < list.Count; index++)
            {
                if (dpis[list[index]].ItemInOneSlot >= ints[list[index]] + num)
                {
                    ints[list[index]] += num;
                    num = 0;
                    texts[list[index]].text = $"{ints[list[index]]}";
                    MoveSprites[list[index]].howMuch = ints[list[index]];
                }
                else if (dpis[list[index]].ItemInOneSlot < ints[list[index]] + num)
                {
                    num = num - (dpis[list[index]].ItemInOneSlot - ints[list[index]]);
                    ints[list[index]] = dpis[list[index]].ItemInOneSlot;
                    texts[list[index]].text = $"{ints[list[index]]}";
                    MoveSprites[list[index]].howMuch = ints[list[index]];
                }
                if(num <= 0)
                {
                    break;
                }
            }
            if (num > 0)
            {
                int d = 0;
                int k = dpis.Count - 1;
                for (int i = 0; i < dpis.Count; i++)
                {
                    if (dpis[i] == null)
                    {
                        k = i;
                        break;
                    }
                    else
                    {
                        d++;
                    }
                }
                if(d < 50)
                {
                    dpis[k] = item;

                    slots[k].sprite = item.Image;

                    ints[k] += num;
                    texts[k].text = $"{ints[k]}";
                    MoveSprites[k].dti = item;
                    MoveSprites[k].howMuch = ints[k];
                }
            }
        }
        else
        {
            int d = 0;
            int k = dpis.Count - 1;
            Debug.Log(k);
            for (int i = 0; i < dpis.Count; i++)
            {
                if (dpis[i] == null)
                {
                    k = i;
                    break;
                }
                else
                {
                    d++;
                }
            }
            if (d < 50)
            {
                Debug.Log(k);
                dpis[k] = item;

                slots[k].sprite = item.Image;

                ints[k] += num;
                texts[k].text = $"{ints[k]}";
                MoveSprites[k].dti = item;
                MoveSprites[k].howMuch = ints[k];
            }

        }
        StaticDropTake.hotbar.SetHotbar(slots,ints,dpis);
    }
}
