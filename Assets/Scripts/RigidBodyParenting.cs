using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DampenedSpringParenting : MonoBehaviour
{
    [SerializeField] private Transform targetParent; // The parent to follow
    [SerializeField] private Vector3 positionOffset; // Offset in local space for position
    [SerializeField] private Vector3 rotationOffsetEuler; // Offset in local space for rotation (Euler angles)
    [SerializeField] private Transform lookAtTarget; // The object to look at

    [SerializeField] private float positionSpring = 50f; // Spring constant for position
    [SerializeField] private float positionSpringmultiplyer = 1f;
    [SerializeField] private float positionDamping = 5f; // Damping constant for position
    [SerializeField] private float rotationSpring = 50f; // Spring constant for rotation
    [SerializeField] private float rotationSpringmultiplyer = 1f;
    [SerializeField] private float rotationDamping = 5f; // Damping constant for rotation
    [SerializeField] private float maxDistance = 5f; // Maximum allowed distance from the target position

    private Rigidbody rb;
    private Quaternion rotationOffset; // Offset in quaternion form
    private bool limiting = false;
    private float currentDistance = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rotationOffset = Quaternion.Euler(rotationOffsetEuler);
    }

    void FixedUpdate()
    {
        if (targetParent == null) return;

        // Calculate target position with offset
        Vector3 targetPosition = targetParent.TransformPoint(positionOffset);
        Vector3 positionDelta = targetPosition - transform.position;
        float distance = positionDelta.magnitude;

        // Check if the object exceeds the max distance
        if (distance > maxDistance)
        {
            if (limiting)
            {
                currentDistance = distance;
            }
            
            limiting = true;

            // Adjust the rigidbody's pos
            if (distance >= currentDistance) 
            {  
                transform.position = targetPosition - (positionDelta.normalized * currentDistance);
            }
            else
            {
                currentDistance = maxDistance;
            }
        }
        else
        {
            limiting = false;
        }
        
        // Position spring-damper system
        Vector3 velocityDelta = rb.linearVelocity;
        Vector3 positionForce = positionSpring * positionDelta * positionSpringmultiplyer - positionDamping * velocityDelta;
        rb.AddForce(positionForce, ForceMode.Force);

        // Calculate target rotation with offset
        Quaternion targetRotation;
        if (lookAtTarget != null)
        {
            // Directly track the lookAtTarget
            Vector3 lookDirection = (lookAtTarget.position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }
        else
        {
            // Use parented rotation with offset
            targetRotation = targetParent.rotation * rotationOffset;
        }

        // Rotation spring-damper system
        Quaternion rotationDelta = targetRotation * Quaternion.Inverse(transform.rotation);
        rotationDelta.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle > 180f) angle -= 360f;

        Vector3 angularVelocity = rb.angularVelocity;
        Vector3 springTorque = rotationSpring * (axis.normalized * angle * Mathf.Deg2Rad * rotationSpringmultiplyer);
        Vector3 dampingTorque = -rotationDamping * angularVelocity;
        rb.AddTorque(springTorque + dampingTorque, ForceMode.Force);
    }

    public void OnValidate()
    {
        rotationOffset = Quaternion.Euler(rotationOffsetEuler);
    }
}
