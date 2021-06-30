using UnityEngine;
using UnityEngine.UI;

public class UpdateDataWidth : MonoBehaviour
{
    public Slider slider;
    public DisplayValue display;

    private void Start()
    {
        slider.value = Parameter.instance.Width;
        display.refreshValue();
    }
}
