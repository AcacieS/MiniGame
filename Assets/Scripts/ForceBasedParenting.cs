using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DampenedSpringParenting : MonoBehaviour
{
    public Transform targetParent;  // The parent to follow
    public Vector3 positionOffset;  // Offset in local space for position
    public Vector3 rotationOffsetEuler;  // Offset in local space for rotation (Euler angles)

    public float positionSpring = 50f;  // Spring constant for position
    public float positionSpringmultiplyer = 1f;
    public float positionDamping = 5f;  // Damping constant for position
    public float rotationSpring = 50f;  // Spring constant for rotation
    public float rotationSpringmultiplyer = 1f;
    public float rotationDamping = 5f;  // Damping constant for rotation
    public float maxDistance = 5f;  // Maximum allowed distance from the target position

    private Rigidbody rb;
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

    private void OnValidate()
    {
        rotationOffset = Quaternion.Euler(rotationOffsetEuler);
    }
}
