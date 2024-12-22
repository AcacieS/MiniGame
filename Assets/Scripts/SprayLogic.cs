using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class SprayLogic : MonoBehaviour
{
    public static float sprayTimeRemaing = 0f; // Initial value of the global variable
    [SerializeField]private float sprayTime = 120f;
    public static float maxSprayTime = 0f;
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
    
    public void InitializeSprayLogic()
    {
        isActionInProgress = false;
        isActive = true;
        sprayTimeRemaing = sprayTime;
        maxSprayTime = sprayTime;
    }  
    void Awake()
    {
        InitializeSprayLogic();
        maxSprayTime = sprayTime;
    }
    void Update()
    {
        if (isActive && !isActionInProgress )
        {
            if (Input.GetKeyDown(targetKey))
            {
                if (!(sprayTimeRemaing <= 0))
                {
                    isActionInProgress = true;
                    startSpray.Invoke();
                }
                else
                {
                    sprayTimeRemaing = 0;
                    isActive = false;
                    endSpray.Invoke();
                    isActionInProgress = false;
                }
            }
            
        } 
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            InitializeSprayLogic();
        }
        
        if (Input.GetKeyUp(targetKey))
        {
            endSpray.Invoke();
            isActionInProgress = false;
        } 

        // Handle left-click when the global variable is > 0
        if (isActive && Input.GetMouseButtonDown(0) && !isActionInProgress && sprayTimeRemaing > 0) StartCoroutine(HandleHitEvents()); // 0 represents left mouse button

    }

    void FixedUpdate()
    {
       if (isActionInProgress == true)
        {
            sprayTimeRemaing -= sprayDecrementAmount * Time.deltaTime; 
            
            if (sprayTimeRemaing < 0) 
            {
                Debug.Log("reset spray");
                isActionInProgress = false;
                isActive = false;
                sprayTimeRemaing = 0;
                endSpray.Invoke();
            } 
        }
    }
    private System.Collections.IEnumerator HandleHitEvents()
    {
        isActionInProgress = true;
        startSprayMelee.Invoke();
        yield return new WaitForSeconds(hitDelay);
        endSprayMelee.Invoke();
        isActionInProgress = false;
        sprayTimeRemaing = Mathf.Max(0, (sprayTimeRemaing - hitSprayUseAmount));
    }  
}
