using System;
using UnityEngine;

public class Parameter : MonoBehaviour
{
    public static Parameter instance;

    private float width = 10f;
    private float height = 10f;
    private float time = 0.0f;

    public float Width { get => width; set => width = value; }
    public float Height { get => height; set => height = value; }
    public float Time { get => time; set => time = value; }

    private void Awake()
    {
        if (instance != null)
        {
            width = instance.Width;
            height = instance.Height;
            time = instance.Time;

            Destroy(instance.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
