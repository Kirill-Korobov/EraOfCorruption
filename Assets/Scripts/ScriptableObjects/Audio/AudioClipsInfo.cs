using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipsInfoConfig", menuName = "ScriptableObjects/AudioClipsInfo")]

public class AudioClipsInfo : ScriptableObject
{
    // Music
    [Header("Music")]
    
    [SerializeField] private AudioClip startMenuMusic;
    public AudioClip StartMenuMusic => startMenuMusic;

    [SerializeField] private AudioClip loadingMenuMusic;
    public AudioClip LoadingMenuMusic => loadingMenuMusic;

    [SerializeField] private AudioClip forestLocationMusic;
    public AudioClip ForestLocationMusic => forestLocationMusic;

    // Write other music.

    // Sounds
    [Header("Sounds")]

    [SerializeField] private AudioClip pressButtonSound;
    public AudioClip PressButtonSound => pressButtonSound;

    [SerializeField] private AudioClip dashSound;
    public AudioClip DashSound => dashSound;

    [SerializeField] private AudioClip teleportSound;
    public AudioClip TeleportSound => teleportSound;

    [SerializeField] private AudioClip getDamageSound;
    public AudioClip GetDamageSound => getDamageSound;

    [SerializeField] private AudioClip levelUpSound;
    public AudioClip LevelUpSound => levelUpSound;

    [SerializeField] private AudioClip deathSound;
    public AudioClip DeathSound => deathSound;

    [SerializeField] private AudioClip finishQuestOrMakeTradeSound;
    public AudioClip FinishQuestOrMakeTradeSound => finishQuestOrMakeTradeSound;
}