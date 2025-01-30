using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    [SerializeField] private List<GameObject> ArmorSlors = new List<GameObject>();
    [SerializeField] private Image[] imageArmor = new Image[3];
    [SerializeField] private Canvas canvas;
    [SerializeField] private MC_HealthManager healthManager;
    [SerializeField] private TMP_Text moneyTxt;
    public int money;
    private DropedTakedItems[] dpisArmor = new DropedTakedItems[3];
    private int[] ints = new int[50];
    public Image Image;
    public Sprite empty;
    private TMP_Text text;
    private string path;


    public bool TakeItemForTrade(int[] indexs, int[] howMuch)
    {
        int[] howMuchHave = new int[howMuch.Length];
        List<int>[] index = new List<int>[indexs.Length];
        for (int i = 0; i < indexs.Length; i++)
        {

            for (int j = 0; j < dpis.Count; j++)
            {
                if (dpis[j] != null && dpis[j].ID == indexs[i])
                {
                    howMuchHave[j] += ints[j];
                    index[i].Add(j);
                }
            }
            
        }
        List<bool> list = new List<bool>();
        for (int i = 0;i < howMuch.Length; i++)
        {
            list.Add(howMuch[i] <= howMuchHave[i]);
        }
        if (!list.Contains(false))
        {
            for (int i = 0; i < howMuch.Length; i++)
            {
                if (howMuch == howMuchHave)
                {
                    for (int j = 0; j < index[i].Count; j++)
                    {
                        dpis[j] = null;
                        texts[i].text = "";
                        slots[i].sprite = empty;
                        MoveSprites[i].dti = dpis[i];
                        MoveSprites[i].howMuch = 0;
                        MoveSprites[i].text.text = "";
                        MoveSprites[i].image.sprite = empty;
                    }
                }
                else
                {
                    for (int j = 0; j < index[i].Count; j++)
                    {
                        if (ints[j] == howMuch[i])
                        {
                            dpis[j] = null;
                            texts[j].text = "";
                            slots[j].sprite = empty;
                            MoveSprites[j].dti = dpis[j];
                            MoveSprites[j].howMuch = 0;
                            MoveSprites[j].text.text = "";
                            MoveSprites[j].image.sprite = empty;
                            ints[j] = 0;
                            break;
                        }
                        else if (ints[j] > howMuch[i])
                        {
                            ints[j] -= howMuch[i];
                            break;
                        }
                        else
                        {
                            howMuch[i] -= ints[j];
                            dpis[j] = null;
                            texts[j].text = "";
                            slots[j].sprite = empty;
                            MoveSprites[j].dti = dpis[j];
                            MoveSprites[j].howMuch = 0;
                            MoveSprites[j].text.text = "";
                            MoveSprites[j].image.sprite = empty;
                            ints[j] = 0;
                        }
                    }
                }
            }
            StaticDropTake.hotbar.SetHotbar(slots, ints, dpis);
            return true;
        }
        return false;

    }
    public void MoneySet(int setMoney)
    {
        money += setMoney;
        moneyTxt.text = $"Money: {money}";
    }
    public DropedTakedItems dtiArrow;
    public bool ArrowUse()
    {
        for (int i = 0; i < dpis.Count; i++)
        {
            if (dpis[i] != null && dpis[i].ItemType == ItemTypes.Arrow)
            {
                dtiArrow = dpis[i];
                ints[i]--;
                if(ints[i] == 0)
                {
                    dpis[i] = null;
                    texts[i].text = "";
                    slots[i].sprite = empty;
                    StaticDropTake.hotbar.SetHotbar(slots, ints, dpis);
                    MoveSprites[i].dti = dpis[i];
                    MoveSprites[i].howMuch = 0;
                    MoveSprites[i].text.text = "";
                    MoveSprites[i].image.sprite = empty;
                    return true;
                }
                texts[i].text = $"{ints[i]}";
                MoveSprites[i].howMuch--;
                MoveSprites[i].text.text = $"{ints[i]}";
                StaticDropTake.hotbar.SetHotbar(slots, ints, dpis);
                return true;
            }
        }
        return false;
    }
    public bool EatOrDrink()
    {
        int i = StaticDropTake.hotbar.activeNow;
        ints[i]--;
        if (ints[i]  == 0)
        {
            dpis[i] = null;
            slots[i].sprite = empty;
            texts[i].text = "";
            StaticDropTake.hotbar.SetHotbar(slots, ints, dpis);
            MoveSprites[i].dti = dpis[i];
            MoveSprites[i].howMuch = 0;
            MoveSprites[i].text.text = "";
            MoveSprites[i].image.sprite = slots[i].sprite;
            return true;
        }
        texts[i].text = $"{ints[i]}";
        MoveSprites[i].howMuch--;
        MoveSprites[i].text.text = $"{ints[i]}";
        StaticDropTake.hotbar.SetHotbar(slots, ints, dpis);
        return false;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    public void Exit()
    {
        if(ssms.isTaken)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (dpis[i] != null && dpis[i].ID == ssms.dtiTaken.ID)
                {
                    if (dpis[i].ItemInOneSlot >= ssms.howMuchTaken + ints[i])
                    {
                        ints[i] += ssms.howMuchTaken;
                        texts[i].text = $"{ints[i]}";
                        MoveSprites[i].howMuch = ints[i];
                        ssms.dtiTaken = null;
                        ssms.spriteTaken = null;
                        ssms.howMuchTaken = 0;
                        ssms.isTaken = false;
                        break;
                    }
                    else if (dpis[i].ItemInOneSlot < ssms.howMuchTaken + ints[i])
                    {
                        ssms.howMuchTaken = ints[i] + ssms.howMuchTaken - dpis[i].ItemInOneSlot;
                        ints[i] = dpis[i].ItemInOneSlot;
                        texts[i].text = $"{ints[i]}";
                        MoveSprites[i].howMuch = ints[i];
                    }
                }
            }
            if(ssms.howMuchTaken > 0)
            {

                for (int i = 0; i < slots.Count; i++)
                {
                    if(dpis[i] == null)
                    {
                        dpis[i] = ssms.dtiTaken;
                        texts[i].text = $"{ssms.howMuchTaken}";
                        ints[i] = ssms.howMuchTaken;
                        slots[i].sprite = ssms.spriteTaken;
                        MoveSprites[i].dti = dpis[i];
                        MoveSprites[i].howMuch = ints[i];
                        ssms.dtiTaken = null;
                        ssms.spriteTaken = null;
                        ssms.howMuchTaken = 0;
                        ssms.isTaken = false;
                    
                        break;
                    }

                }
                if(ssms.howMuchTaken > 0)
                {
                    StaticDropTake.hotbar.DropItem(ssms.dtiTaken);
                    ssms.dtiTaken = null;
                    ssms.spriteTaken = null;
                    ssms.howMuchTaken = 0;
                    ssms.isTaken = false;
                }
            }
            StaticDropTake.sl.Image.gameObject.SetActive(false);
            StaticDropTake.hotbar.SetHotbar(slots, ints, dpis);
            Save();
        }
    }
    private IEnumerator Wiat()
    {
        canvas.gameObject.SetActive(true);
        yield return null;
        canvas.gameObject.SetActive(false);
    }
    private void Awake()
    {
        StartCoroutine(Wiat());
        path = $"{Application.persistentDataPath}/Inventory.json";
        ArmorSlors.Add(null);
        ArmorSlors.Add(null);
        ArmorSlors.Add(null);
        StaticDropTake.sl = this;
        for (int i = 0; i < slotsGet.Count; i++)
        {
            dpis.Add(null);
            texts.Add(slotsGet[i].GetComponentsInChildren<TMP_Text>()[0]);
            slots.Add(slotsGet[i].GetComponentsInChildren<Image>()[1]);
        }
        text = Image.GetComponentsInChildren<TMP_Text>()[0];
        string json = "";
        using (var reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null) { json += line; }
        }
        dpis = JsonUtility.FromJson<SaveInventory>(json).dti;
        ints = JsonUtility.FromJson<SaveInventory>(json).ints;
        dpisArmor = JsonUtility.FromJson<SaveInventory>(json).dtiArmor;
        money = JsonUtility.FromJson<SaveInventory>(json).money;
        moneyTxt.text = $"Money: {money}";
        for (int i = 0; i < dpisArmor.Length; i++)
        {
            if (dpisArmor[i] != null)
            {
                imageArmor[i].sprite = dpisArmor[i].Image;
            }
        }
        for (int i = 0; i < dpis.Count; i++)
        {
            if(dpis[i] != null)
            {
                slots[i].sprite = dpis[i].Image;
                texts[i].text = $"{ints[i]}";
            }
        }
        for(int i = 0;i < MoveSprites.Count; i++)
        {
            if (i <= 49)
            {
                MoveSprites[i].dti = dpis[i];   
                MoveSprites[i].howMuch = ints[i];
            }
            else
            {
                MoveSprites[i].dti = dpisArmor[i - 50];
                if (MoveSprites[i].dti != null)
                {
                    healthManager.Defense += dpisArmor[i - 50].Defense;
                    MoveSprites[i].howMuch = 1;
                }
            }
            
        }

    }
    private void Start()
    {
        StaticDropTake.hotbar.SetHotbar(slots, ints, dpis);
    }
    public void MoveArmor(int i, DropedTakedItems dpi, Image image)
    {
        if (dpisArmor[i] != null)
        {
            healthManager.Defense -= dpisArmor[i].Defense;
        }
        imageArmor[i] = image;
        dpisArmor[i] = dpi;
        if (dpi != null)
        {
            healthManager.Defense += dpi.Defense;
        }
    }
        
    public void Move(int i, DropedTakedItems dpi, int howmuch, Image image, bool mage)
    {
        if (mage)
        {
            if(dpi.ID == dpis[i].ID)
            {
                if(dpi.ItemInOneSlot >= ints[i] + howmuch)
                {
                    ints[i] += howmuch;
                    ssms.spriteTaken = null;
                    ssms.howMuchTaken = 0;
                    ssms.dtiTaken = null;
                    ssms.isTaken = false;
                    texts[i].text = $"{ints[i]}";
                    MoveSprites[i].howMuch = ints[i];
                    Image.gameObject.SetActive(false);
                }
                else
                {
                    howmuch = ints[i] + howmuch - dpi.ItemInOneSlot;
                    ints[i] = dpi.ItemInOneSlot;

                    ssms.isTaken = true;
                    ssms.spriteTaken = image.sprite;
                    ssms.howMuchTaken = howmuch;
                    ssms.dtiTaken = dpis[i];
                    texts[i].text = $"{ints[i]}";
                    MoveSprites[i].howMuch = ints[i];
                    text.text = $"{ssms.howMuchTaken}";
                }
            }
            else
            {
                dpis[i] = dpi;
                ints[i] = howmuch;
                slots[i] = image;
            }
        }
        else
        {
            dpis[i] = dpi;
            ints[i] = howmuch;
            slots[i] = image;
        }
        StaticDropTake.hotbar.SetHotbar(slots, ints, dpis);
    }
    public void Drop(int i, DropedTakedItems dpi, int howmuch, Image image)
    {
        dpis[i] = dpi;
        ints[i] = howmuch;
        slots[i].sprite = image.sprite;
        texts[i].text = "";
        MoveSprites[i].dti = dpi;
        MoveSprites[i].howMuch = howmuch;
        MoveSprites[i].text.text = "";
        MoveSprites[i].image.sprite = image.sprite;
        StaticDropTake.hotbar.SetHotbar(slots, ints, dpis);
    }

    public bool AddItem(DropedTakedItems item, int num)
    {
        if (dpis.Contains(item))
        {
            List<int> list = new List<int>();
            for(int i = 0; i < dpis.Count; i++)
            {
                if(dpis[i] == null)
                {
                    continue;
                }
                if (dpis[i].ID == item.ID)
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
                if (d < 50)
                {
                    dpis[k] = item;

                    slots[k].sprite = item.Image;

                    ints[k] += num;
                    texts[k].text = $"{ints[k]}";
                    MoveSprites[k].dti = item;
                    MoveSprites[k].howMuch = ints[k];
                }
                else if (d >= 50)
                {
                    return false;
                }
            }
        }
        else
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
            if (d < 50)
            {
                dpis[k] = item;

                slots[k].sprite = item.Image;

                ints[k] += num;
                texts[k].text = $"{ints[k]}";
                MoveSprites[k].dti = item;
                MoveSprites[k].howMuch = ints[k];
            }

        }
        StaticDropTake.hotbar.SetHotbar(slots,ints,dpis);
        return true;
    }
    public void Save()
    {
        SaveInventory dpi = new SaveInventory();
        dpi.dti = dpis;
        dpi.ints = ints;
        dpi.dtiArmor = dpisArmor;
        dpi.money = money;
        using (var writer = new StreamWriter(path))
        {
            writer.WriteLine(JsonUtility.ToJson(dpi));
        }
    }
    class SaveInventory
    {
        public List<DropedTakedItems> dti;
        public int[] ints;
        public DropedTakedItems[] dtiArmor;
        public int money;
    }
}
