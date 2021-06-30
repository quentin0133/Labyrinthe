using UnityEngine;
using UnityEngine.UI;

public class UpdateDataHeight : MonoBehaviour
{
    public Slider slider;
    public DisplayValue display;

    private void Start()
    {
        slider.value = Parameter.instance.Height;
        display.refreshValue();
    }
}
