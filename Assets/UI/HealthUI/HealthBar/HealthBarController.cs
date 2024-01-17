using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarController : MonoBehaviour
{
    private Slider slider;

    private void Start() => slider = GetComponent<Slider>();

    public void SetValue(int value)
    {
        if (slider == null) return;
        slider.value = value;
    }
}
