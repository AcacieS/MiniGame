using UnityEngine;

public class ShakeTester : MonoBehaviour
{
    public CameraShake cameraShake;

    public float intensity = 1f;
    public float frequency = 1f;
    public float duration = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            cameraShake.TriggerShake(intensity, frequency, duration);
        }
    }
}
