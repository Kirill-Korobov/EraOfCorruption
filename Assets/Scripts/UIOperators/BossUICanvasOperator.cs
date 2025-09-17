using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossUICanvasOperator : MonoBehaviour
{
    [SerializeField] private GameObject fightBossUI, congratsWindow;
    [SerializeField] private TMP_Text bossNameText, bossHealthValueText;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private RectTransform phaseDividerImageRectTransform;
    [SerializeField] private GameObject darkServant;
    private PauseManager pauseManager;
    private MainGameUIOperator mainGameUIOperator;
    private float bossMaxHP;

    private void Awake()
    {
        fightBossUI.SetActive(false);
        congratsWindow.SetActive(false);
        pauseManager = FindAnyObjectByType<PauseManager>();
        mainGameUIOperator = FindAnyObjectByType<MainGameUIOperator>();
    }

    public void SetUIActive(string _bossNameText, float _bossMaxHP, float changePhaseHPPersent)
    {
        bossNameText.text = _bossNameText;
        bossMaxHP = _bossMaxHP;
        bossHealthValueText.text = $"{bossMaxHP.ToString("f0")} / {bossMaxHP.ToString("f0")}";
        phaseDividerImageRectTransform.localPosition = new Vector3(-healthBarImage.rectTransform.rect.width / 2f + healthBarImage.rectTransform.rect.width * changePhaseHPPersent, phaseDividerImageRectTransform.localPosition.y, phaseDividerImageRectTransform.localPosition.z);
        fightBossUI.SetActive(true);
    }

    public void UpdateHPUI(float currentHP)
    {
        bossHealthValueText.text = $"{currentHP.ToString("f0")} / {bossMaxHP.ToString("f0")}";
        healthBarImage.fillAmount = currentHP / bossMaxHP;
    }

    public void SetUIInactive()
    {
        bossNameText.text = string.Empty;
        bossHealthValueText.text = string.Empty;
        fightBossUI.SetActive(false);
    }

    public void ShowCongratsWindow()
    {
        mainGameUIOperator.SetAllCanvasesInactive();
        congratsWindow.SetActive(true);
        pauseManager.SetGamePaused();
        mainGameUIOperator.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseCongratsWindow()
    {
        congratsWindow.SetActive(false);
        pauseManager.SetGameNotPaused();
        mainGameUIOperator.enabled = true;
        darkServant.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }
}