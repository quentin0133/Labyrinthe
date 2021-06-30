using UnityEngine;
using UnityEngine.UI;

public class DisplayValue : MonoBehaviour
{
    public Slider slider;

    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void refreshValue()
    {
        text.text = slider.value.ToString();
    }
}
