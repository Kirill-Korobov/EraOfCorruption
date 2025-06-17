using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    const string audioMixerSounds = "SoundVolume";

    [SerializeField] private AudioClipsInfo clipsInfo;

    public IEnumerator PlaySound(AudioClip audioClip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.loop = false;
        audioSource.clip = audioClip;
        audioSource.volume = 1f;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(audioClip.length);
        Destroy(audioSource);
    }

    public void ChangeVolume(float volumeValueFrom0To1)
    {
        if (volumeValueFrom0To1 == 0f)
        {
            volumeValueFrom0To1 = 0.0001f;
        }
        audioMixerGroup.audioMixer.SetFloat(audioMixerSounds, Mathf.Log10(volumeValueFrom0To1) * 20);
    }
}