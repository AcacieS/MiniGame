using UnityEngine;
using System.Collections;

public class PositionOffsetSource : MonoBehaviour
{
    [SerializeField] private Vector3 offset1; // First position offset
    [SerializeField] private Vector3 offset2; // Second position offset (delayed)
    [SerializeField] private Vector3 offset3; // Third position offset
    public float sprayPosDampMulti = 1f;

    private Vector3 currentOffset; // The currently active offset
    private Coroutine delayCoroutine; // Reference to the delay coroutine

    // Enum to easily track which offset is active
    public enum OffsetState { Offset1, Offset2, Offset3 }
    private OffsetState currentState;

    [SerializeField] private float delayDuration = 2f; // Delay duration for offset2 in seconds

    public void ActivateOffset1()
    {
        CancelDelayedOffset(); // Stop any ongoing delay
        currentOffset = offset1;
        currentState = OffsetState.Offset1;
        sprayPosDampMulti = 2;
        Debug.Log("Activated Offset 1");
    }

    public void ActivateOffset2()
    {
        CancelDelayedOffset(); // Reset any ongoing delay before starting a new one
        delayCoroutine = StartCoroutine(DelayOffset2());
    }

    public void ActivateOffset3()
    {
        CancelDelayedOffset(); // Stop any ongoing delay
        currentOffset = offset3;
        currentState = OffsetState.Offset3;
        sprayPosDampMulti = 2;
        Debug.Log("Activated Offset 3");
    }

    public Vector3 GetPositionOffset()
    {
        return currentOffset;
    }

    private IEnumerator DelayOffset2()
    {
        Debug.Log("Offset 2 activation delayed...");
        yield return new WaitForSeconds(delayDuration);

        // After the delay, set offset2 as the current offset
        currentOffset = offset2;
        currentState = OffsetState.Offset2;
        sprayPosDampMulti = 2.5f;
        Debug.Log("Activated Offset 2 (after delay)");
        delayCoroutine = null; // Clear the coroutine reference
    }

    private void CancelDelayedOffset()
    {
        if (delayCoroutine != null)
        {
            StopCoroutine(delayCoroutine);
            delayCoroutine = null;
            Debug.Log("Delayed Offset 2 activation canceled");
        }
    }

    void Start()
    {
        // Start with a default offset, e.g., offset1
        ActivateOffset1();
    }

    void FixedUpdate()
    {
        if (sprayPosDampMulti > 1 && !(sprayPosDampMulti == 1))
        {
            sprayPosDampMulti -= 0.05f;
        }
        else
        {
            sprayPosDampMulti = 1f;
        }
    }
    public float GetPositionDamp()
    {
        return sprayPosDampMulti;
    }
}
