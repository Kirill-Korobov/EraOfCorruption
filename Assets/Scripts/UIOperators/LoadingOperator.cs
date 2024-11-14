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
    private int currentAdviceIndex;

    private void Start()
    {
        LoadSceneAsync("MainGame");
        backgroundImage.sprite = backgroundSpritesInfo.BackgroundSprites[Random.Range(0, backgroundSpritesInfo.BackgroundSprites.Length)];
        currentAdviceIndex = Random.Range(0, adviceInfo.PiecesOfAdvice.Length);
        adviceText.text = adviceInfo.PiecesOfAdvice[currentAdviceIndex];
        StartCoroutine(ChangeAdviceCoroutine());
    }

    private IEnumerator ChangeAdviceCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            while (true)
            {
                int bufferNumber = Random.Range(0, adviceInfo.PiecesOfAdvice.Length);
                if (bufferNumber != currentAdviceIndex) 
                {
                    currentAdviceIndex = bufferNumber;
                    break;
                }
            }
            adviceText.text = adviceInfo.PiecesOfAdvice[currentAdviceIndex];
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