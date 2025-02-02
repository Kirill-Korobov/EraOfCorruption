using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum WhatTheSlot
{
    Normal,
    Delete,
    Armor,
}
enum TypesOfArmorSlots
{
    None,
    Armor,
    Helmet,
    Boots,
    
}
public class MoveSprites : MonoBehaviour, IPointerClickHandler
{
    public WhatTheSlot wts;
    [SerializeField] private TypesOfArmorSlots wtsSlots;
    [SerializeField] private int i;
    [SerializeField] private Sprite trans;
    public Image image;
    public TMP_Text text;
    [HideInInspector] public DropedTakedItems dti;
    [HideInInspector] public int howMuch;
    public TMP_Text txt;

    private void Start()
    {
        if (wts == WhatTheSlot.Normal)
        {
            txt = gameObject.GetComponentsInChildren<TMP_Text>()[0];
            image = gameObject.GetComponentsInChildren<Image>()[1];
        }
        else
        {
            image = gameObject.GetComponentsInChildren<Image>()[1];
        }
    }
    [System.Obsolete]
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (!StaticSaveMovesSprites.isTaken)
        {
            if (dti == null)
                return;
            StaticSaveMovesSprites.isTaken = true;
            StaticSaveMovesSprites.spriteTaken = image.sprite;
            StaticSaveMovesSprites.howMuchTaken = howMuch;
            StaticSaveMovesSprites.dtiTaken = dti;

            if (wts != WhatTheSlot.Armor)
            {
                image.sprite = trans;
                howMuch = 0;
                dti = null;
                txt.text = "";
                text.text = $"{StaticSaveMovesSprites.howMuchTaken}";

                StaticDropTake.sl.Move(i, dti, howMuch, image, false);
                StaticDropTake.sl.Image.gameObject.SetActive(true);
                StaticDropTake.sl.Image.sprite = StaticSaveMovesSprites.spriteTaken;
            }
            else
            {
                image.sprite = trans;
                howMuch = 0;
                dti = null;
                text.text = $"{StaticSaveMovesSprites.howMuchTaken}";

                StaticDropTake.sl.MoveArmor(i, dti, image);
                StaticDropTake.sl.Image.gameObject.SetActive(true);
                StaticDropTake.sl.Image.sprite = StaticSaveMovesSprites.spriteTaken;
            }
        }
        else
        {
            if (dti == null)
            {
                if (wts == WhatTheSlot.Armor)
                {

                    if (StaticSaveMovesSprites.dtiTaken.ItemType == ItemTypes.Armor && wtsSlots == TypesOfArmorSlots.Armor && StaticSaveMovesSprites.dtiTaken.DefenseType == DefenseTypes.Armor)
                    {
                        dti = StaticSaveMovesSprites.dtiTaken;
                        howMuch = StaticSaveMovesSprites.howMuchTaken;
                        image.sprite = StaticSaveMovesSprites.spriteTaken;

                        StaticSaveMovesSprites.spriteTaken = null;
                        StaticSaveMovesSprites.howMuchTaken = 0;
                        StaticSaveMovesSprites.dtiTaken = null;
                        StaticSaveMovesSprites.isTaken = false;
                        StaticDropTake.sl.MoveArmor(i, dti,image);
                        text.text = $"{StaticSaveMovesSprites.howMuchTaken}";
                        StaticDropTake.sl.Image.gameObject.SetActive(false);
                    }
                    else if (StaticSaveMovesSprites.dtiTaken.ItemType == ItemTypes.Armor && wtsSlots == TypesOfArmorSlots.Helmet && StaticSaveMovesSprites.dtiTaken.DefenseType == DefenseTypes.Helmet )
                    {
                        dti = StaticSaveMovesSprites.dtiTaken;
                        howMuch = StaticSaveMovesSprites.howMuchTaken;
                        image.sprite = StaticSaveMovesSprites.spriteTaken;

                        StaticSaveMovesSprites.spriteTaken = null;
                        StaticSaveMovesSprites.howMuchTaken = 0;
                        StaticSaveMovesSprites.dtiTaken = null;
                        StaticSaveMovesSprites.isTaken = false;
                        StaticDropTake.sl.MoveArmor(i, dti, image);
                        text.text = $"{StaticSaveMovesSprites.howMuchTaken}";
                        StaticDropTake.sl.Image.gameObject.SetActive(false);
                    }
                    else if (StaticSaveMovesSprites.dtiTaken.ItemType == ItemTypes.Armor && wtsSlots == TypesOfArmorSlots.Boots && StaticSaveMovesSprites.dtiTaken.DefenseType == DefenseTypes.Boots)
                    {
                        dti = StaticSaveMovesSprites.dtiTaken;
                        howMuch = StaticSaveMovesSprites.howMuchTaken;
                        image.sprite = StaticSaveMovesSprites.spriteTaken;

                        StaticSaveMovesSprites.spriteTaken = null;
                        StaticSaveMovesSprites.howMuchTaken = 0;
                        StaticSaveMovesSprites.dtiTaken = null;
                        StaticSaveMovesSprites.isTaken = false;
                        StaticDropTake.sl.MoveArmor(i, dti, image);
                        text.text = $"{StaticSaveMovesSprites.howMuchTaken}";
                        StaticDropTake.sl.Image.gameObject.SetActive(false);
                    }
                }
                else if (wts == WhatTheSlot.Delete)
                {
                    StaticSaveMovesSprites.spriteTaken = null;
                    StaticSaveMovesSprites.howMuchTaken = 0;
                    StaticSaveMovesSprites.dtiTaken = null;
                    StaticSaveMovesSprites.isTaken = false;
                    StaticDropTake.sl.Image.gameObject.SetActive(false);
                }
                else
                {
                    dti = StaticSaveMovesSprites.dtiTaken;
                    txt.text = $"{StaticSaveMovesSprites.howMuchTaken}";
                    howMuch = StaticSaveMovesSprites.howMuchTaken;
                    image.sprite = StaticSaveMovesSprites.spriteTaken;

                    StaticSaveMovesSprites.spriteTaken = null;
                    StaticSaveMovesSprites.howMuchTaken = 0;
                    StaticSaveMovesSprites.dtiTaken = null;
                    StaticSaveMovesSprites.isTaken = false;
                    StaticDropTake.sl.Move(i, dti, howMuch, image, false);
                    text.text = $"{StaticSaveMovesSprites.howMuchTaken}";
                    StaticDropTake.sl.Image.gameObject.SetActive(false);
                }
            }
            else
            {
                if (wts == WhatTheSlot.Armor)
                {
                    if (StaticSaveMovesSprites.dtiTaken.ItemType == ItemTypes.Armor && wtsSlots == TypesOfArmorSlots.Armor && StaticSaveMovesSprites.dtiTaken.DefenseType == DefenseTypes.Armor )
                    {
                        DropedTakedItems saveDTI = StaticSaveMovesSprites.dtiTaken;
                        int savehowMuch = StaticSaveMovesSprites.howMuchTaken;
                        Sprite saveImage = StaticSaveMovesSprites.spriteTaken;

                        StaticSaveMovesSprites.spriteTaken = image.sprite;
                        StaticSaveMovesSprites.howMuchTaken = howMuch;
                        StaticSaveMovesSprites.dtiTaken = dti;


                        dti = saveDTI;
                        howMuch = savehowMuch;
                        image.sprite = saveImage;


                        text.text = $"{StaticSaveMovesSprites.howMuchTaken}";
                        StaticDropTake.sl.MoveArmor(i, dti, image);
                        StaticDropTake.sl.Image.sprite = StaticSaveMovesSprites.spriteTaken;
                    }
                    else if (StaticSaveMovesSprites.dtiTaken.ItemType == ItemTypes.Armor && wtsSlots == TypesOfArmorSlots.Helmet && StaticSaveMovesSprites.dtiTaken.DefenseType == DefenseTypes.Helmet )
                    {
                        DropedTakedItems saveDTI = StaticSaveMovesSprites.dtiTaken;
                        int savehowMuch = StaticSaveMovesSprites.howMuchTaken;
                        Sprite saveImage = StaticSaveMovesSprites.spriteTaken;

                        StaticSaveMovesSprites.spriteTaken = image.sprite;
                        StaticSaveMovesSprites.howMuchTaken = howMuch;
                        StaticSaveMovesSprites.dtiTaken = dti;


                        dti = saveDTI;
                        howMuch = savehowMuch;
                        image.sprite = saveImage;


                        text.text = $"{StaticSaveMovesSprites.howMuchTaken}";
                        StaticDropTake.sl.MoveArmor(i, dti, image);
                        StaticDropTake.sl.Image.sprite = StaticSaveMovesSprites.spriteTaken;
                    }
                    else if (StaticSaveMovesSprites.dtiTaken.ItemType == ItemTypes.Armor && wtsSlots == TypesOfArmorSlots.Boots && StaticSaveMovesSprites.dtiTaken.DefenseType == DefenseTypes.Boots )
                    {
                        DropedTakedItems saveDTI = StaticSaveMovesSprites.dtiTaken;
                        int savehowMuch = StaticSaveMovesSprites.howMuchTaken;
                        Sprite saveImage = StaticSaveMovesSprites.spriteTaken;

                        StaticSaveMovesSprites.spriteTaken = image.sprite;
                        StaticSaveMovesSprites.howMuchTaken = howMuch;
                        StaticSaveMovesSprites.dtiTaken = dti;


                        dti = saveDTI;
                        howMuch = savehowMuch;
                        image.sprite = saveImage;


                        text.text = $"{StaticSaveMovesSprites.howMuchTaken}";
                        StaticDropTake.sl.MoveArmor(i, dti, image);
                        StaticDropTake.sl.Image.sprite = StaticSaveMovesSprites.spriteTaken;
                    }
                }
                else if(wts == WhatTheSlot.Delete)
                {
                    StaticSaveMovesSprites.spriteTaken = null;
                    StaticSaveMovesSprites.howMuchTaken = 0;
                    StaticSaveMovesSprites.dtiTaken = null;
                    StaticSaveMovesSprites.isTaken = false;
                    StaticDropTake.sl.Image.gameObject.SetActive(false);
                }
                else
                {
                    DropedTakedItems saveDTI = StaticSaveMovesSprites.dtiTaken;
                    int savehowMuch = StaticSaveMovesSprites.howMuchTaken;
                    Sprite saveImage = StaticSaveMovesSprites.spriteTaken;

                    StaticSaveMovesSprites.spriteTaken = image.sprite;
                    StaticSaveMovesSprites.howMuchTaken = howMuch;
                    StaticSaveMovesSprites.dtiTaken = dti;

                    StaticDropTake.sl.Image.sprite = StaticSaveMovesSprites.spriteTaken;
                    text.text = $"{StaticSaveMovesSprites.howMuchTaken}";

                    dti = saveDTI;
                    howMuch = savehowMuch;
                    image.sprite = saveImage;
                    txt.text = $"{savehowMuch}";


                    StaticDropTake.sl.Move(i, dti, howMuch, image, true);
                }
            }
        }
    }
}