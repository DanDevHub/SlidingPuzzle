using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Teleportation : MonoBehaviour
{
    private Rigidbody rb;

        private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on " + gameObject.name);
        }

        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    
    private void OnEnable()
    {
        // Holen der XRGrabInteractable-Komponente
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        else
        {
            Debug.LogError("XRGrabInteractable component is missing on " + gameObject.name);
        }
    }

    private void OnDisable()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Bewegung auf allen Achsen einfrieren
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
