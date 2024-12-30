using System.Collections;
using TMPro;
using UnityEngine;

public class LevelUpTextBehaviour : MonoBehaviour
{
    private TMP_Text levelUpText;
    private Color textColor, transparentColor;
    [SerializeField] private float timeBeforeMakingTransparent, returnToNormalSpeed;
    [HideInInspector] public bool makeTransparent;

    private void Awake()
    {
        levelUpText = GetComponent<TMP_Text>();
        textColor = levelUpText.color;
        transparentColor = new Color(textColor.r, textColor.g, textColor.b, 0f);
        makeTransparent = false;
    }

    private void Update()
    {
        if (makeTransparent && levelUpText.color.a > transparentColor.a)
        {
            levelUpText.color = new Color(transparentColor.r, transparentColor.g, transparentColor.b, levelUpText.color.a - returnToNormalSpeed * Time.deltaTime);
        }
    }

    public IEnumerator ShowLevelUpText()
    {
        levelUpText.color = new Color(textColor.r, textColor.g, textColor.b, 1f);
        yield return new WaitForSeconds(timeBeforeMakingTransparent);
        makeTransparent = true;
    }
}