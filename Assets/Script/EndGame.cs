using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndGame : MonoBehaviour
{
    VideoPlayer video;
    public GameObject display;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        if (video.isPrepared)
            display.SetActive(false);

        if (!video.isPlaying && video.isPrepared)
        {
            Application.Quit();
        }
    }
}
