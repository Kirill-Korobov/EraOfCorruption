using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingOperator : MonoBehaviour
{
    [SerializeField] private Image progressBarFillingImage, backgroundImage;
    [SerializeField] private TMP_Text adviceText;
    [SerializeField] private AdviceInfo adviceInfo;
    [SerializeField] private BackgroundSpritesInfo backgroundSpritesInfo;
    [SerializeField] private float timeBeforeSwitchingAdvice, timeBeforeSwitchingBackground;
    private int[] adviceOrder, backgroundOrder;
    private int bufferRandomIndex1, bufferRandomIndex2, bufferIndex, currentAdviceIndex, currentBackgroundIndex;

    private void Start()
    {
        LoadSceneAsync("MainGame");
        adviceOrder = new int[adviceInfo.PiecesOfAdvice.Length];
        for (int i = 0; i < adviceOrder.Length; i++)
        {
            adviceOrder[i] = i;
        }
        if (adviceInfo.PiecesOfAdvice.Length >= 2)
        {
            for (int i = 0; i < adviceInfo.PiecesOfAdvice.Length; i++)
            {
                bufferRandomIndex1 = Random.Range(0, adviceInfo.PiecesOfAdvice.Length);
                while (true)
                {
                    bufferRandomIndex2 = Random.Range(0, adviceInfo.PiecesOfAdvice.Length);
                    if (bufferRandomIndex1 != bufferRandomIndex2)
                    {
                        break;
                    }
                }
                bufferIndex = adviceOrder[bufferRandomIndex1];
                adviceOrder[bufferRandomIndex1] = adviceOrder[bufferRandomIndex2];
                adviceOrder[bufferRandomIndex2] = bufferIndex;
            }
        }
        StartCoroutine(ChangeAdviceCoroutine());
        backgroundOrder = new int[backgroundSpritesInfo.BackgroundSprites.Length];
        for (int i = 0; i < backgroundOrder.Length; i++)
        {
            backgroundOrder[i] = i;
        }
        if (backgroundSpritesInfo.BackgroundSprites.Length >= 2)
        {
            for (int i = 0; i < backgroundSpritesInfo.BackgroundSprites.Length; i++)
            {
                bufferRandomIndex1 = Random.Range(0, backgroundSpritesInfo.BackgroundSprites.Length);
                while (true)
                {
                    bufferRandomIndex2 = Random.Range(0, backgroundSpritesInfo.BackgroundSprites.Length);
                    if (bufferRandomIndex1 != bufferRandomIndex2)
                    {
                        break;
                    }
                }
                bufferIndex = backgroundOrder[bufferRandomIndex1];
                backgroundOrder[bufferRandomIndex1] = backgroundOrder[bufferRandomIndex2];
                backgroundOrder[bufferRandomIndex2] = bufferIndex;
            }
        }
        StartCoroutine(ChangeBackgroundCoroutine());
    }

    private IEnumerator ChangeAdviceCoroutine()
    {
        while (true)
        {
            currentAdviceIndex = 0;
            for (int i = 0; i < adviceOrder.Length; i++)
            {
                adviceText.text = adviceInfo.PiecesOfAdvice[adviceOrder[currentAdviceIndex]];
                yield return new WaitForSeconds(timeBeforeSwitchingAdvice);
                currentAdviceIndex++;
            }
        }
    }

    private IEnumerator ChangeBackgroundCoroutine()
    {
        while (true)
        {
            currentBackgroundIndex = 0;
            for (int i = 0; i < backgroundOrder.Length; i++)
            {
                backgroundImage.sprite = backgroundSpritesInfo.BackgroundSprites[backgroundOrder[currentBackgroundIndex]];
                yield return new WaitForSeconds(timeBeforeSwitchingBackground);
                currentBackgroundIndex++;
            }
        }
    }

    private async void LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            progressBarFillingImage.fillAmount = asyncOperation.progress / 0.9f;
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            await Task.Yield();
        }
    }
}