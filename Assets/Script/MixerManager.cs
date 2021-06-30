using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string name;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(name, volume);
    }
}
