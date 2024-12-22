using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public AnimationCurve intensityCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private float shakeDuration = 0f;
    private float shakeFrequency = 1f;
    private float intensityMultiplier = 1f;
    private float maxShakeDuration = 0f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private float timeSinceLastPosition = 0f;

    private void Awake()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            // Reduce shake duration over time
            shakeDuration -= Time.deltaTime;

            // Determine the current intensity based on the curve
            float progress = Mathf.Clamp01(1 - (shakeDuration / maxShakeDuration));
            float currentIntensity = intensityMultiplier * intensityCurve.Evaluate(progress);

            // Update time to move to the next position
            timeSinceLastPosition += Time.deltaTime;

            // Smoothly move towards the target position
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, timeSinceLastPosition / shakeFrequency);

            // If the time for the current shake position is up, calculate a new target
            if (timeSinceLastPosition >= shakeFrequency)
            {
                timeSinceLastPosition = 0f;
                targetPosition = originalPosition + (Random.insideUnitSphere * currentIntensity);
            }

            // Ensure the camera smoothly returns to the original position when the shake ends
            if (shakeDuration <= 0)
            {
                transform.localPosition = originalPosition;
            }
        }
    }

    public void TriggerShake(float intensity, float frequency, float duration)
    {
        intensityMultiplier = intensity;
        shakeFrequency = frequency;
        shakeDuration = duration;
        maxShakeDuration = duration;

        // Initialize first target position
        targetPosition = originalPosition + (Random.insideUnitSphere * intensity);
        timeSinceLastPosition = 0f;
    }
}
