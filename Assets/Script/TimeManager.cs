using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public void SetCurrentTime(float newCurrentTime)
    {
        Time.timeScale = newCurrentTime;
    }
}
