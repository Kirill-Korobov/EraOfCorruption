using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class StartMenuVideoOperator : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private StartMenuVideoInfo videoInfo;
    private int[] clipOrder;
    private int currentClipIndex, bufferRandomVideoClipIndex1, bufferRandomVideoClipIndex2, bufferClipIndex;

    private void Awake()
    {
        clipOrder = new int[videoInfo.StartMenuVideoClips.Length];
        for (int i = 0; i < clipOrder.Length; i++)
        {
            clipOrder[i] = i;
        }
        if (videoInfo.StartMenuVideoClips.Length >= 2)
        {
            for (int i = 0; i < videoInfo.StartMenuVideoClips.Length; i++)
            {
                bufferRandomVideoClipIndex1 = Random.Range(0, videoInfo.StartMenuVideoClips.Length);
                while (true)
                {
                    bufferRandomVideoClipIndex2 = Random.Range(0, videoInfo.StartMenuVideoClips.Length);
                    if (bufferRandomVideoClipIndex1 != bufferRandomVideoClipIndex2)
                    {
                        break;
                    }
                }
                bufferClipIndex = clipOrder[bufferRandomVideoClipIndex1];
                clipOrder[bufferRandomVideoClipIndex1] = clipOrder[bufferRandomVideoClipIndex2];
                clipOrder[bufferRandomVideoClipIndex2] = bufferClipIndex;
            }
        }
        StartCoroutine(PlayVideoClips());
    }

    private IEnumerator PlayVideoClips()
    {
        while (true)
        {
            currentClipIndex = 0;
            for (int i = 0; i < clipOrder.Length; i++)
            {
                videoPlayer.clip = videoInfo.StartMenuVideoClips[clipOrder[currentClipIndex]];
                yield return new WaitForSeconds((float)videoInfo.StartMenuVideoClips[clipOrder[currentClipIndex]].length);
                currentClipIndex++;
            }
        }
    }
}