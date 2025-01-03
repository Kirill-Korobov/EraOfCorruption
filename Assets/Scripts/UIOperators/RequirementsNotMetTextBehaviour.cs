using System.Collections;
using TMPro;
using UnityEngine;

public class RequirementsNotMetTextBehaviour : MonoBehaviour
{
    private TMP_Text requirementsNotMetText;
    private Color textColor, transparentColor;
    [SerializeField] private float timeBeforeMakingTransparent, returnToNormalSpeed;
    private bool makeTransparent;

    private void Awake()
    {
        requirementsNotMetText = GetComponent<TMP_Text>();
        textColor = requirementsNotMetText.color;
        transparentColor = new Color(textColor.r, textColor.g, textColor.b, 0f);
    }

    private void Update()
    {
        if (makeTransparent && requirementsNotMetText.color.a > transparentColor.a)
        {    
            requirementsNotMetText.color = new Color(transparentColor.r, transparentColor.g, transparentColor.b, requirementsNotMetText.color.a - returnToNormalSpeed * Time.unscaledDeltaTime);
        }
    }

    public IEnumerator ShowRequirementsNotMetText()
    {      
        makeTransparent = false;
        requirementsNotMetText.color = new Color(textColor.r, textColor.g, textColor.b, 1f);
        yield return new WaitForSecondsRealtime(timeBeforeMakingTransparent);
        makeTransparent = true;
    }
}