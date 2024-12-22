using UnityEngine;

public class ColliderToggle : MonoBehaviour
{
    private Collider _collider;

    

    // Method to enable the collider
    public void EnableCollider()
    {
        if (_collider != null)
        {
            _collider.enabled = true;
        }
    }

    // Method to disable the collider
    public void DisableCollider()
    {
        if (_collider != null)
        {
            _collider.enabled = false;
        }
    }
    
    void Awake()
    {
        // Get the Collider component on this GameObject
        _collider = GetComponent<Collider>();

        // Check if a collider is attached
        if (_collider == null)
        {
            Debug.LogError("No Collider found on this GameObject.");
        }
        
        DisableCollider();

    }
}
