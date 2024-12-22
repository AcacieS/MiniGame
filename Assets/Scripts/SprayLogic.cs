using UnityEngine;
using UnityEngine.Events;

public class SprayLogic : MonoBehaviour
{
    public static float sprayTimeRemaing = 120f; // Initial value of the global variable
    [SerializeField]private float sprayDecrementAmount = 0.1f; // Amount to decrement per key press
    private KeyCode targetKey = KeyCode.Space; // Key to monitor
    public UnityEvent startSpray; // Event triggered when key is pressed
    public UnityEvent endSpray; // Event triggered when key is released or variable <= 0

    public UnityEvent startSprayMelee; // First event triggered on left click when variable < 0
    public UnityEvent endSprayMelee; // Second event triggered on left click when variable < 0
    [SerializeField]private float hitDelay = 0.5f; // Delay between the two left-click events
    [SerializeField]private float hitSprayUseAmount = 2f;

    private bool isActive = true;
    private bool isActionInProgress = false;
    void Update()
    {
        if (isActive && !isActionInProgress)
        {
            if (Input.GetKeyDown(targetKey))
            {
                isActionInProgress = true;
                startSpray.Invoke();

                if (sprayTimeRemaing <= 0)
                {
                    sprayTimeRemaing = 0;
                    isActive = false;
                    endSpray.Invoke();
                    isActionInProgress = false;
                }
            }
        } 
        
        if (Input.GetKeyUp(targetKey))
        {
            endSpray.Invoke();
            isActionInProgress = false;
            Debug.Log(sprayTimeRemaing);
        } 

        // Handle left-click when the global variable is > 0
        if (isActive && Input.GetMouseButtonDown(0) && !isActionInProgress) StartCoroutine(HandleHitEvents()); // 0 represents left mouse button

    }

    void FixedUpdate()
    {
       if (isActionInProgress == true) sprayTimeRemaing -= sprayDecrementAmount * Time.deltaTime; 
    }
    private System.Collections.IEnumerator HandleHitEvents()
    {
        isActionInProgress = true;
        startSprayMelee.Invoke();
        yield return new WaitForSeconds(hitDelay);
        endSprayMelee.Invoke();
        isActionInProgress = false;
        sprayTimeRemaing -= hitSprayUseAmount;
        Debug.Log(sprayTimeRemaing);
    }
}
