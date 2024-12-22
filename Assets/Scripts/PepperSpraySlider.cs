using UnityEngine;
using UnityEngine.UI;

public class PepperSpraySlider : MonoBehaviour
{
    [SerializeField]private Slider slider; // Reference to the Slider UI component
    private float progress; // Variable to set progress (0 to 1)

    void Update()
    {
        // Clamp the value to ensure it's between 0 and 1
        progress = Mathf.Clamp01(SprayLogic.sprayTimeRemaing / SprayLogic.maxSprayTime);

        // Update the slider's value
        if (slider != null)
        {
            slider.value = progress;
        }
    }
}
