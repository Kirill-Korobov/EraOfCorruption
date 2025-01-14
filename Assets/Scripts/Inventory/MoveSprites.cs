using Inventory;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveSprites : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int i;
    [SerializeField] private Sprite trans;
    private Image image;
    [HideInInspector] public DropedTakedItems dti;
    [HideInInspector] public int howMuch;
    private TMP_Text txt;

    private void Start()
    {
        txt = gameObject.GetComponentsInChildren<TMP_Text>()[0];
        image = gameObject.GetComponentsInChildren<Image>()[1];
    }
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

            image.sprite = trans;
            howMuch = 0;
            dti = null;
            txt.text = "";

            StaticDropTake.sl.Move(i, dti, howMuch, image);
            StaticDropTake.sl.Image.gameObject.SetActive(true);
            StaticDropTake.sl.Image.sprite = StaticSaveMovesSprites.spriteTaken;
        }
        else
        {
            if (dti == null)
            {
                if(image == null)
                {
                    Debug.Log("1");
                }
                dti = StaticSaveMovesSprites.dtiTaken;
                txt.text = $"{StaticSaveMovesSprites.howMuchTaken}";
                howMuch = StaticSaveMovesSprites.howMuchTaken;
                image.sprite = StaticSaveMovesSprites.spriteTaken;

                StaticSaveMovesSprites.spriteTaken = null;
                StaticSaveMovesSprites.howMuchTaken = 0;
                StaticSaveMovesSprites.dtiTaken = null;
                StaticSaveMovesSprites.isTaken = false;
                StaticDropTake.sl.Move(i, dti, howMuch, image);
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


                dti = saveDTI;
                txt.text = $"{savehowMuch}";
                howMuch = savehowMuch;
                image.sprite = saveImage;

                StaticDropTake.sl.Move(i, dti, howMuch, image);
                StaticDropTake.sl.Image.sprite = StaticSaveMovesSprites.spriteTaken;
            }
        }
    }

}