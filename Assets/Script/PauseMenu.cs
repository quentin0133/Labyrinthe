using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public bool onPause = false;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (onPause)
                UnPause();
            else
                Pause();
            onPause = !onPause;
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    private void UnPause()
    {
        Time.timeScale = 1;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }
}
