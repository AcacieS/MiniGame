using UnityEngine;
using System.Collections;

public class PositionOffsetSource : MonoBehaviour
{
    [SerializeField] private Vector3 offset1; // First position offset
    [SerializeField] private Vector3 offset2; // Second position offset
    [SerializeField] private Vector3 offset3; // Third position offset

    private Vector3 currentOffset; // The currently active offset
    private Coroutine delayCoroutine; // Reference to the delay coroutine

    // Enum to easily track which offset is active
    public enum OffsetState { Offset1, Offset2, Offset3 }
    private OffsetState currentState;

    [SerializeField] private float delayDuration = 2f; // Delay duration in seconds

    // Functions that activate the events to change the offset
    public void ActivateOffset1()
    {
        currentOffset = offset1;
        currentState = OffsetState.Offset1;
        Debug.Log("Activated Offset 1");
    }

    public void ActivateOffset2()
    {
        StartOrResetDelay(() =>
        {
            currentOffset = offset2;
            currentState = OffsetState.Offset2;
            Debug.Log("Activated Offset 2");
        });
    }

    public void ActivateOffset3()
    {
        currentOffset = offset3;
        currentState = OffsetState.Offset3;
        Debug.Log("Activated Offset 3");
    }

    // Return the current offset for the parent script to use
    public Vector3 GetPositionOffset()
    {
        return currentOffset;
    }

    private void StartOrResetDelay(System.Action activationAction)
    {
        // If a delay coroutine is running, stop it
        if (delayCoroutine != null)
        {
            StopCoroutine(delayCoroutine);
        }

        // Start a new coroutine for the delay
        delayCoroutine = StartCoroutine(DelayCoroutine(activationAction));
    }

    private IEnumerator DelayCoroutine(System.Action activationAction)
    {
        yield return new WaitForSeconds(delayDuration); // Wait for the specified delay duration

        // Perform the action after the delay
        activationAction?.Invoke();
        delayCoroutine = null; // Clear the coroutine reference
    }

    void Start()
    {
        // Start with a default offset, e.g., offset1
        ActivateOffset1();
    }
}
