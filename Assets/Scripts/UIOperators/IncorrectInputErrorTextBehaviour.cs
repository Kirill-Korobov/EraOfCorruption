using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IncorrectInputErrorTextBehaviour : MonoBehaviour
{
    private TMP_Text incorrectInputErrorText;
    private Color textColor, transparentColor;
    private bool makeTransparent;
    [SerializeField] private float returnToNormalSpeed, timeBeforeMakingTransparent;

    private void Awake()
    {
        incorrectInputErrorText = GetComponent<TMP_Text>();
        textColor = incorrectInputErrorText.color;
        transparentColor = new Color(textColor.r, textColor.g, textColor.b, 0f);
        incorrectInputErrorText.color = transparentColor;
        makeTransparent = false;
    }

    private void Update()
    {
        if (makeTransparent && incorrectInputErrorText.color.a > transparentColor.a)
        {
            incorrectInputErrorText.color = new Color(transparentColor.r, transparentColor.g, transparentColor.b, incorrectInputErrorText.color.a - returnToNormalSpeed * Time.deltaTime);
        }
    }

    public IEnumerator ShowIncorrectInputText()
    {
        makeTransparent = false;
        incorrectInputErrorText.color = new Color(textColor.r, textColor.g, textColor.b, 1f);
        yield return new WaitForSeconds(timeBeforeMakingTransparent);
        makeTransparent = true;
    }
}