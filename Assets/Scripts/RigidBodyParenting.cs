using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DampenedSpringParenting : MonoBehaviour
{
    [SerializeField]private Transform targetParent;  // The parent to follow
    [SerializeField]private Vector3 positionOffset;  // Offset in local space for position
    [SerializeField]private Vector3 rotationOffsetEuler;  // Offset in local space for rotation (Euler angles)

    [SerializeField]private float positionSpring = 50f;  // Spring constant for position
    [SerializeField]private float positionSpringmultiplyer = 1f;
    [SerializeField]private float positionDamping = 5f;  // Damping constant for position
    [SerializeField]private float rotationSpring = 50f;  // Spring constant for rotation
    [SerializeField]private float rotationSpringmultiplyer = 1f;
    [SerializeField]private float rotationDamping = 5f;  // Damping constant for rotation
    [SerializeField]private float maxDistance = 5f;  // Maximum allowed distance from the target position

    public Rigidbody rb;
    private Quaternion rotationOffset;  // Offset in quaternion form

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
         
        // Check if the object exceeds the max distance
        if (positionDelta.magnitude > maxDistance)
        {
            // Snap to the limited position
            transform.position = targetPosition - positionDelta.normalized * maxDistance;
        }
        
        // Position spring-damper system
        Vector3 velocityDelta = rb.linearVelocity;
        Vector3 positionForce = positionSpring * positionDelta * positionSpringmultiplyer - positionDamping * velocityDelta;
        rb.AddForce(positionForce, ForceMode.Force);
        

        // Calculate target rotation with offset
        Quaternion targetRotation = targetParent.rotation * rotationOffset;

        // Rotation spring-damper system
        Quaternion rotationDelta = targetRotation * Quaternion.Inverse(transform.rotation);
        rotationDelta.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle > 180f) angle -= 360f;
        Vector3 angularVelocity = rb.angularVelocity;

        // Torque based on spring and damping
        Vector3 springTorque = rotationSpring * (axis.normalized * angle * Mathf.Deg2Rad * rotationSpringmultiplyer);
        Vector3 dampingTorque = -rotationDamping * angularVelocity;
        rb.AddTorque(springTorque + dampingTorque, ForceMode.Force);
    }

    public void OnValidate()
    {
        rotationOffset = Quaternion.Euler(rotationOffsetEuler);
    }
}