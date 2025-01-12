using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeMenuOperator : MonoBehaviour
{
    [SerializeField] private TradesInfo tradesInfo;
    [SerializeField] private NPCsInfo _NPCsInfo;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private GameObject tradePrefab, content;
    [SerializeField] private VerticalLayoutGroup contentVerticalLayoutGroup;
    [SerializeField] private NotEnoughResourcesTextBehaviour notEnoughResourcesTextBehaviour;
    private Coroutine showNotEnoughResourcesTextCoroutine;
    private GameObject[] bufferTrades;
    private RectTransform contentRectTransform;
    private int interactingNPCID;

    private void Awake()
    {
        interactingNPCID = -1;
        contentRectTransform = content.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        notEnoughResourcesTextBehaviour.MakeTransparent();
    }

    private void OnDisable()
    {
        DeleteTrades();
    }

    public void SpawnTrades(int _interactingNPCID)
    {
        interactingNPCID = _interactingNPCID;
        titleText.text = $"{_NPCsInfo._NPCsInfo[interactingNPCID].name}'s trades";
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, _NPCsInfo._NPCsInfo[interactingNPCID].tradesIndexes.Length * (tradePrefab.GetComponent<RectTransform>().sizeDelta.y + contentVerticalLayoutGroup.spacing) - contentVerticalLayoutGroup.spacing);
        bufferTrades = new GameObject[_NPCsInfo._NPCsInfo[interactingNPCID].tradesIndexes.Length];
        for (int i = 0; i < bufferTrades.Length; i++)
        {
            bufferTrades[i] = Instantiate(tradePrefab, contentRectTransform);
            bufferTrades[i].GetComponentsInChildren<TMP_Text>()[0].text = $"Price: {tradesInfo._TradeInfo[_NPCsInfo._NPCsInfo[interactingNPCID].tradesIndexes[i]].priceDescription}.";
            bufferTrades[i].GetComponentsInChildren<TMP_Text>()[1].text = $"Goods: {tradesInfo._TradeInfo[_NPCsInfo._NPCsInfo[interactingNPCID].tradesIndexes[i]].goodsDescription}.";
            int bufferNumber = _NPCsInfo._NPCsInfo[interactingNPCID].tradesIndexes[i];
            bufferTrades[i].GetComponentInChildren<Button>().onClick.AddListener(() => MakeTradeButton(bufferNumber));
        }
    }

    public void DeleteTrades()
    {
        if (bufferTrades != null)
        {
            for (int i = 0; i < bufferTrades.Length; i++)
            {
                Destroy(bufferTrades[i]);
            }
        }
    }

    private void MakeTradeButton(int _questIndex)
    {
        // Check if the player has enough resources.
        // if ()
        // {
        //     MakeTrade(_questIndex);
        // }
        // else if ()
        // {
        //     if (showNotEnoughResourcesTextCoroutine != null)
        //     {
        //         StopCoroutine(showNotEnoughResourcesTextCoroutine);
        //     }
        //     showNotEnoughResourcesTextCoroutine = StartCoroutine(notEnoughResourcesTextBehaviour.ShowNotEnoughResourcesText());
        // }
    }

    private void MakeTrade(int _questIndex)
    {
        // Pay the price and get goods.
    }
}