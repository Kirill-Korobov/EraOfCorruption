using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClipsInfo audioClipsInfo;
    [SerializeField] private AudioMixer audioMixer;
    const string audioMixerMusic = "MusicVolume";

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            PlayMusic(audioClipsInfo.StartMenuMusic);
        }
        else if (SceneManager.GetActiveScene().name == "Loading")
        {
            PlayMusic(audioClipsInfo.LoadingMenuMusic);
        }
        else
        {
            // test:
            PlayMusic(audioClipsInfo.StartMenuMusic);
            // .
        }
    }

    public void PlayMusic(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void ChangeVolume(float volumeValueFrom0To1)
    {
        if (volumeValueFrom0To1 == 0f)
        {
            volumeValueFrom0To1 = 0.0001f;
        }
        audioMixer.SetFloat(audioMixerMusic, Mathf.Log10(volumeValueFrom0To1) * 20);
    }
}