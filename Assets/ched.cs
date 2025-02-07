using UnityEngine;


public class XRMovementController : MonoBehaviour
{
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found! Please add a Rigidbody to the object.");
        }
        
        if (grabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable not found! Make sure this object is an XR interactable.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is a wall (tag it as "Wall" in Unity)
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collided with a wall! Restricting movement.");

            // Freeze rotation completely
            rb.freezeRotation = true;

            // Allow only forward/backward movement (lock X and Y movement)
            rb.constraints = RigidbodyConstraints.FreezePositionX |
                             RigidbodyConstraints.FreezePositionY |
                             RigidbodyConstraints.FreezeRotation;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Restore movement when leaving the wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Left the wall, restoring movement.");
            rb.constraints = RigidbodyConstraints.None; // Remove all constraints
            rb.freezeRotation = false; // Allow rotation again
        }
    }
}

