using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class FloatToTextMeshPro : MonoBehaviour
{
    public float value;

    [Tooltip("The number of decimal places to display.")]
    [Range(0, 10)]
    public int decimalPlaces = 2;
    private TextMeshPro textMeshPro;

    void Start()
    {
        // Get the TextMeshPro component attached to this GameObject
        textMeshPro = GetComponent<TextMeshPro>();
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component not found!");
        }
    }

    void Update()
    {
        if (textMeshPro != null)
        {
            // Determine the value to display
            float displayValue = Mathf.Max((SprayLogic.sprayTimeRemaing / SprayLogic.maxSprayTime * 100 - 1) , 0);

            // Format the float value and update the TextMeshPro text
            textMeshPro.text = displayValue.ToString("F" + decimalPlaces) + "%";

        }
    }
}