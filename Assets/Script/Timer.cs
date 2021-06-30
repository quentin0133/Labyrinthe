using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time = 0.0f;
    public bool isStart = false;

    private void Update()
    {
        if (isStart) {
            time += Time.deltaTime;
            Parameter.instance.Time = time;
        }
    }
}
