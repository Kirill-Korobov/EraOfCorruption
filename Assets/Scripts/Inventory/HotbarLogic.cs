using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HotbarLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] go;
    [SerializeField] private List<ScriptableObjectUsedItems> weapons;
    [SerializeField] private List<TMP_Text> textsSlots;
    [SerializeField] private Transform player;
    private Image[] images = new Image[10];
    private TMP_Text[] texts = new TMP_Text[10];
    private DropedTakedItems[] dtis = new DropedTakedItems[10];
    public int activeNow;
    private int indexActive;
    private void Awake()
    {
        textsSlots[0].color = Color.red;
        for (int i = 0; i < go.Length; i++)
        {
            images[i] = go[i].GetComponentsInChildren<Image>()[1];
            texts[i] = go[i].GetComponentsInChildren<TMP_Text>()[0];
        }
        StaticDropTake.hotbar = this;
    }
    public void DropItem()
    {
        if (dtis[activeNow] != null)
        {
            Vector3 spawnPosition = player.position + player.forward * 2.5f; spawnPosition.y = player.position.y + 1;
            GameObject go = Instantiate(dtis[activeNow].Drop, spawnPosition, Quaternion.identity);
            go.GetComponent<TakeItem>().howMuchDroped = Convert.ToInt32(texts[activeNow].text);
            dtis[activeNow] = null;
            texts[activeNow].text = "";
            images[activeNow].sprite = StaticDropTake.sl.empty;
            StaticDropTake.sl.Drop(activeNow, null, 0, images[activeNow]);
        }
    }
    public void DropItem(DropedTakedItems dti)
    {
        Vector3 spawnPosition = player.position + player.forward * 2.5f; spawnPosition.y = player.position.y + 1;
        GameObject go = Instantiate(dti.Drop, spawnPosition, Quaternion.identity);
        go.GetComponent<TakeItem>().howMuchDroped = Convert.ToInt32(texts[activeNow].text);
    }
    public void SetHotbar(List<Image> image, int[] texts, List<DropedTakedItems> dti)
    {
        for (int i = 0;i < 10;i++)
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
        TakeAtArm();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            activeNow = 0;
            TakeAtArm();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            activeNow = 1;
            TakeAtArm();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            activeNow = 2;
            TakeAtArm();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            DropItem();
        }
    }

    public void TakeAtArm()
    {
        for(int i = 0;i < 10; i++)
        {
            textsSlots[i].color = Color.black;
        }
        textsSlots[activeNow].color = Color.red;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (dtis[activeNow] != null && dtis[activeNow].ID == weapons[i].dti.ID)
            {
                weapons[indexActive].go.SetActive(false);
                weapons[i].go.SetActive(true);
                weapons[i].GetComponent<Rigidbody>().useGravity = false;
                indexActive = i;
            }
            else if(dtis[activeNow] == null)
            {
                weapons[indexActive].go.SetActive(false);
            }

        }
    }
}
