using UnityEngine;
using UnityEngine.UI;

public class BloodyBackgroundBehaviour : MonoBehaviour
{
    [HideInInspector] public Image bloodyBackgroundImage;
    private Color transparentColor;
    public float bloodMultiplier, maxBloodyBackgroundOpacity;
    [SerializeField] private float returnToNormalSpeed;

    private void Awake()
    {
        bloodyBackgroundImage = GetComponent<Image>();
        transparentColor = new Color(1f, 0f, 0f, 0f);
        bloodyBackgroundImage.color = transparentColor;
    }

    void Update()
    {
        if (bloodyBackgroundImage.color.a > transparentColor.a)
        {
            bloodyBackgroundImage.color = new Color(1f, 0f, 0f, bloodyBackgroundImage.color.a - returnToNormalSpeed * Time.deltaTime);
        }
    }
}