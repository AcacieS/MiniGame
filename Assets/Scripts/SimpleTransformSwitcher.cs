using UnityEngine;

public class TransformSwitcher : MonoBehaviour
{
    [Header("Local Transform Positions and Rotations")]
    public Vector3 localPosition1;
    public Vector3 localPosition2;
    public Quaternion localRotation1;
    public Quaternion localRotation2;

    [Header("Lerp Settings")]
    public float lerpDuration = 1.0f; // Duration of the lerp transition
    private float lerpTime = 0.0f;    // Current time of the lerp
    private bool isLerping = false;   // Whether lerping is in progress

    private Vector3 currentLocalPosition;
    private Quaternion currentLocalRotation;

    private Vector3 targetLocalPosition;
    private Quaternion targetLocalRotation;

    void Start()
    {
        // Start with localPosition1 and localRotation1 as the current values
        currentLocalPosition = localPosition1;
        currentLocalRotation = localRotation1;
        targetLocalPosition = localPosition1;
        targetLocalRotation = localRotation1;
        UpdateTransform();
    }

    void Update()
    {
        if (isLerping == true)
        {
            // Update lerp progress
            lerpTime += Time.deltaTime;

            // Lerp position and rotation
            transform.localPosition = Vector3.Lerp(currentLocalPosition, targetLocalPosition, lerpTime / lerpDuration);
            transform.localRotation = Quaternion.Lerp(currentLocalRotation, targetLocalRotation, lerpTime / lerpDuration);
            //Debug.Log(lerpTime);
            // Check if the lerp is complete
            if (lerpTime >= lerpDuration)
            {
                lerpTime = 0.0f;
                isLerping = false;
            }
        }
    }

    // Function to switch to localPosition1 and localRotation1 with lerp
    public void SwitchToTransform1()
    {
        StartLerp(localPosition1, localRotation1);
        UpdateTransform();
    }

    // Function to switch to localPosition2 and localRotation2 with lerp
    public void SwitchToTransform2()
    {
        StartLerp(localPosition2, localRotation2);
        UpdateTransform();
    }

    // Start the lerp transition to the target position and rotation
    private void StartLerp(Vector3 targetPosition, Quaternion targetRotation)
    {
        currentLocalPosition = transform.localPosition;
        currentLocalRotation = transform.localRotation;

        targetLocalPosition = targetPosition;
        targetLocalRotation = targetRotation;

        isLerping = true;
        lerpTime = 0;
        //Debug.Log(isLerping);
    }

    // Update the object's local position and rotation immediately (without lerp)
    private void UpdateTransform()
    {
        transform.localPosition = currentLocalPosition;
        transform.localRotation = currentLocalRotation;
    }
}
