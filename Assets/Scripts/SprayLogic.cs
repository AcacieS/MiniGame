using UnityEngine;
using UnityEngine.Events;

public class SprayLogic : MonoBehaviour
{
    public static float sprayTimeRemaing = 120f; // Initial value of the global variable
    public static float sprayPosDampMulti = 1f;
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
                sprayPosDampMulti = 2;

                if (sprayTimeRemaing <= 0)
                {
                    sprayTimeRemaing = 0;
                    isActive = false;
                    endSpray.Invoke();
                    sprayPosDampMulti = 2;
                    isActionInProgress = false;
                }
            }
        } 
        
        if (Input.GetKeyUp(targetKey))
        {
            endSpray.Invoke();
            sprayPosDampMulti = 2;
            isActionInProgress = false;
        } 

        // Handle left-click when the global variable is > 0
        if (isActive && Input.GetMouseButtonDown(0) && !isActionInProgress) StartCoroutine(HandleHitEvents()); // 0 represents left mouse button

    }

    void FixedUpdate()
    {
       if (isActionInProgress == true) sprayTimeRemaing -= sprayDecrementAmount * Time.deltaTime; 
       if (sprayPosDampMulti > 1 && !(sprayPosDampMulti == 1))
       {
            sprayPosDampMulti -= 0.05f;
       }
       else
       {
            sprayPosDampMulti = 1f;
       }
    }
    private System.Collections.IEnumerator HandleHitEvents()
    {
        isActionInProgress = true;
        startSprayMelee.Invoke();
        sprayPosDampMulti = 3;
        yield return new WaitForSeconds(hitDelay);
        endSprayMelee.Invoke();
        isActionInProgress = false;
        sprayTimeRemaing -= hitSprayUseAmount;
        Debug.Log(sprayTimeRemaing);
    }
}
