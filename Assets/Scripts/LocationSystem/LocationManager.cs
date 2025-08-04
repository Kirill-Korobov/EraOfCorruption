using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private GameStatsManager gameStatsManager;
    private GameStats currentGameStats;
    [SerializeField] private Transform greenfieldSpawnPointTransform, milgardSpawnPointTransform, oakholmeSpawnPointTransform, grantarSpawnPointTransform, sharrukSpawnPointTransform;
    public Dictionary<Sublocation, Transform> spawnPoints;
    [SerializeField] private TMP_Text locationChangedText;
    [SerializeField] private Animator locationChangedTextAnimator;

    private void Awake()
    {
        spawnPoints = new Dictionary<Sublocation, Transform>
        {
            { Sublocation.Greenfield, greenfieldSpawnPointTransform },
            { Sublocation.Milgard, milgardSpawnPointTransform },
            { Sublocation.Oakholme, oakholmeSpawnPointTransform },
            { Sublocation.Grantar, grantarSpawnPointTransform },
            { Sublocation.Sharruk, sharrukSpawnPointTransform },
        };
        
    }

    void Start()
    {
        switch (GameStatsManager.currentGame)
        {
            case 1:
                currentGameStats = gameStatsManager.game1Stats;
                break;
            case 2:
                currentGameStats = gameStatsManager.game2Stats;
                break;
            case 3:
                currentGameStats = gameStatsManager.game3Stats;
                break;
            default:
                currentGameStats = gameStatsManager.game1Stats;
                break;
        }
    }

    public void ChangeSublocation(Sublocation sublocation)
    {
        currentGameStats.currentSublocation = sublocation;
        locationChangedText.text = $"<{sublocation.ToString()}>";
        locationChangedTextAnimator.Play("LocationChangedTextAnimation", 0, 0f);
    }
}

public enum Sublocation
{
    Greenfield,
    Milgard,
    Oakholme,
    Grantar,
    Sharruk
}