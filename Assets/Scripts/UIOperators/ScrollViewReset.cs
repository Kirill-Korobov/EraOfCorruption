using UnityEngine;
using UnityEngine.UI;

public class ScrollViewReset : MonoBehaviour
{
    private ScrollRect scrollRect;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void OnEnable()
    {
        scrollRect.verticalNormalizedPosition = 1f;
    }
}