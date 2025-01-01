using TMPro;
using UnityEngine;

public class GameVersionBehaviour : MonoBehaviour
{
    [SerializeField] private string gameVersion;
    private TMP_Text gameVersionText;

    private void Awake()
    {
        gameVersionText = GetComponent<TMP_Text>();
        gameVersionText.text = $"Version: {gameVersion}";
    }
}