using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HotbarLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] go;
    [SerializeField] private List<GameObject> weapons;
    private Image[] images = new Image[10];
    private TMP_Text[] texts = new TMP_Text[10];
    private DropedTakedItems[] dtis = new DropedTakedItems[10];
    private void Awake()
    {
        for (int i = 0; i < go.Length; i++)
        {
            images[i] = go[i].GetComponentsInChildren<Image>()[1];
            texts[i] = go[i].GetComponentsInChildren<TMP_Text>()[0];
        }
        StaticDropTake.hotbar = this;
    }

    public void SetHotbar(List<Image> image, int[] texts, List<DropedTakedItems> dti)
    {
        for(int i = 0;i < 10;i++)
        {
            images[i].sprite = image[i].sprite;
            if (texts[i] != 0)
            {
                this.texts[i].text = $"{texts[i]}";
            }
            else
            {
                this.texts[i].text = "";
            }
            dtis[i] = dti[i]; 
        }
        
    }

    [System.Obsolete]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            for(int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].active)
                {
                    weapons[i].SetActive(false);
                }
                if(weapons[i].GetComponent<TakeItem>().dti.ID == dtis[0].ID && dtis[0].ItemType == ItemTypes.Weapon)
                {
                    weapons[i].SetActive(true);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].active)
                {
                    weapons[i].SetActive(false);
                }
                if (weapons[i].GetComponent<TakeItem>().dti.ID == dtis[1].ID && dtis[1].ItemType == ItemTypes.Weapon)
                {
                    weapons[i].SetActive(true);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].active)
                {
                    weapons[i].SetActive(false);
                }
                if (weapons[i].GetComponent<TakeItem>().dti.ID == dtis[2].ID && dtis[2].ItemType == ItemTypes.Weapon)
                {
                    weapons[i].SetActive(true);
                }
            }
        }
    }
}
