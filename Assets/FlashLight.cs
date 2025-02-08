using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TorchToggle : MonoBehaviour
{
    [Tooltip("The Light component representing the torch's light.")]
    [SerializeField] private Light torchLight;

    // Reference to the XR Interactable component (e.g., XRGrabInteractable)
    private XRBaseInteractable xrInteractable;

    // Track the torch's state
    private bool isTorchOn = true;

    private void Awake()
    {
        // Auto-assign the torch light if it's not assigned via Inspector
        if (torchLight == null)
        {
            torchLight = GetComponentInChildren<Light>();
            if (torchLight == null)
            {
                Debug.LogError("TorchToggle: No Light component found. Please assign one in the inspector.");
            }
        }

        // Get the XR Interactable (such as XRGrabInteractable) from the same GameObject
        xrInteractable = GetComponent<XRGrabInteractable>();
        if (xrInteractable != null)
        {
            // Subscribe to the updated 'activated' event (instead of the deprecated 'onActivate')
            xrInteractable.activated.AddListener(ToggleTorch);
        }
        else
        {
            Debug.LogWarning("TorchToggle: No XR Interactable component found on this GameObject.");
        }
    }

    /// <summary>
    /// Called when the XR Interactable is activated.
    /// </summary>
    /// <param name="args">Activate event arguments.</param>
    private void ToggleTorch(ActivateEventArgs args)
    {
        // Toggle the torch's state
        isTorchOn = !isTorchOn;

        // Enable or disable the light accordingly
        if (torchLight != null)
        {
            torchLight.enabled = isTorchOn;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid potential memory leaks
        if (xrInteractable != null)
        {
            xrInteractable.activated.RemoveListener(ToggleTorch);
        }
    }
}
