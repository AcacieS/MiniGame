using UnityEngine;

public class RaycastMover : MonoBehaviour
{
    [SerializeField]private Transform target; // The target object from which the ray will be cast
    [SerializeField]private float raycastDistance = 10f; // Maximum distance the ray will check for a hit
    [SerializeField]private float offsetDistance = 2f; // Distance in front of the target if no hit is detected
    [SerializeField]private LayerMask includeLayers; // Layers to include in the raycast
    [SerializeField]private LayerMask excludeLayers; // Layers to exclude from the raycast

    void Update()
    {
        if (target == null) return;

        Vector3 rayOrigin = target.position;
        Vector3 rayDirection = target.forward;

        // Perform the raycast while considering the layers to include and exclude
        RaycastHit hit;
        int mask = includeLayers.value & ~excludeLayers.value; // Combine include and exclude layers

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, raycastDistance, mask))
        {
            // If hit, move the object to the hit point
            transform.position = hit.point;
        }
        else
        {
            // If no hit, position the object in front of the target
            transform.position = rayOrigin + rayDirection * offsetDistance;
        }
    }
}
