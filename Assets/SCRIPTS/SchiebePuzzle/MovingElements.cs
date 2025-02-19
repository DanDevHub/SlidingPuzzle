using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MovingElements : MonoBehaviour
{
    private Rigidbody rb;
    private SchiebePuzzleManager gameManager; 

    public enum MoveAxis { X, Y, Z }  // Enum zur Auswahl der Achse
    [SerializeField]
    private MoveAxis moveAxis = MoveAxis.X;
    private static bool isHeld;
    public float delayBeforeDestroy = 1f; // Verzögerung vor der Zerstörung

    void Start()
    {
        gameManager = SchiebePuzzleManager.Instance;
    }
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
        isHeld = true;
        switch (moveAxis)
        {
            case MoveAxis.X:
                // Bewegung auf der X-Achse erlauben
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ |
                                 RigidbodyConstraints.FreezeRotation;
                break;
            case MoveAxis.Y:
                // Bewegung auf der Y-Achse erlauben
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
                                 RigidbodyConstraints.FreezeRotation;
                break;
            case MoveAxis.Z:
                // Bewegung auf der Z-Achse erlauben
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY |
                                 RigidbodyConstraints.FreezeRotation;
                break;
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        // Bewegung auf allen Achsen einfrieren
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnCollisionEnter(Collision collision)
    {
    Rigidbody rb = GetComponent<Rigidbody>();
    if (rb != null)
    {
        // Bewegungen auf allen Achsen einfrieren
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    }

    public void Solved(GameObject puzzle,bool timeOver){
        StartCoroutine(DestroyAndSpawnPuzzle(puzzle, timeOver));
    }
    public IEnumerator DestroyAndSpawnPuzzle(GameObject puzzleObject, bool timeOver)
    {
        while (isHeld)
        {
            yield return null;
        }
        gameManager.OnPuzzleSolved(puzzleObject, timeOver);
    }    
}
