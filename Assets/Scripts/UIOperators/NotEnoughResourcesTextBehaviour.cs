using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotEnoughResourcesTextBehaviour : MonoBehaviour
{
    private TMP_Text notEnoughResourcesText;
    private Color textColor, transparentColor;
    [SerializeField] private float timeBeforeMakingTransparent, returnToNormalSpeed;
    private bool makeTransparent;

    private void Awake()
    {
        notEnoughResourcesText = GetComponent<TMP_Text>();
        textColor = notEnoughResourcesText.color;
        transparentColor = new Color(textColor.r, textColor.g, textColor.b, 0f);
        MakeTransparent();
    }

    private void Update()
    {
        if (makeTransparent && notEnoughResourcesText.color.a > transparentColor.a)
        {    
            notEnoughResourcesText.color = new Color(transparentColor.r, transparentColor.g, transparentColor.b, notEnoughResourcesText.color.a - returnToNormalSpeed * Time.unscaledDeltaTime);
        }
    }

    public void MakeTransparent()
    {
        notEnoughResourcesText.color = transparentColor;
    }

    public IEnumerator ShowNotEnoughResourcesText()
    {      
        makeTransparent = false;
        notEnoughResourcesText.color = new Color(textColor.r, textColor.g, textColor.b, 1f);
        yield return new WaitForSecondsRealtime(timeBeforeMakingTransparent);
        makeTransparent = true;
    }
}