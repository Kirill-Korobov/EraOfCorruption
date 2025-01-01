using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingTextBehaviour : MonoBehaviour
{
    private TMP_Text loadingText;
    [SerializeField] private string[] contents;
    [SerializeField] private float timeBetweenSwitchingContents;
    private WaitForSeconds waitForSeconds;
    private int currentContentIndex;

    private void Awake()
    {
        loadingText = GetComponent<TMP_Text>();
        waitForSeconds = new WaitForSeconds(timeBetweenSwitchingContents);
        StartCoroutine(LoadingTextAnimationCoroutine());
    }

    private IEnumerator LoadingTextAnimationCoroutine()
    {
        currentContentIndex = 0;
        while (true) 
        {
            loadingText.text = contents[currentContentIndex];
            currentContentIndex++;
            if (currentContentIndex == contents.Length)
            {
                currentContentIndex = 0;
            }
            yield return waitForSeconds;
        }
    }
}