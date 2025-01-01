using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "StartMenuVideoInfoConfig", menuName = "ScriptableObjects/StartMenuVideoInfo")]

public class StartMenuVideoInfo : ScriptableObject
{
    [SerializeField] private VideoClip[] startMenuVideoClips;
    public VideoClip[] StartMenuVideoClips => startMenuVideoClips;
}