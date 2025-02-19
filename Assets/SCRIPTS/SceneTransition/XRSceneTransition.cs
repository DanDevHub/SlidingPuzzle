using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class XRSceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Putrexia";  // Scene to load
    [SerializeField] private CanvasGroup fadeCanvasGroup;      // CanvasGroup of the fade UI panel
    [SerializeField] private AudioSource audioSource;          // Audio source to play sound

    private void Start()
    {
        if (fadeCanvasGroup == null)
        {
            Debug.LogError("Fade CanvasGroup is not assigned in the inspector.");
        }
    }

    public void OnSelect() // Called when the XR interactable button is selected
    {
        if (audioSource != null)
        {
            audioSource.Play();  // Play the sound effect
        }

        // Start the fade out and scene load process
        StartCoroutine(TransitionScene());
    }

    private IEnumerator TransitionScene()
    {
        // Fade out (panel becomes fully opaque)
        yield return StartCoroutine(Fade(1f));

        // Load the scene
        SceneManager.LoadScene(sceneToLoad);

        // Wait a moment for the scene to load and then fade in
        yield return new WaitForSeconds(1f); // Optional: Wait a bit before fading back in

        // Fade in (panel becomes fully transparent)
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float duration = 1f; // Duration of the fade (you can adjust this)

        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha; // Ensure the target alpha is set at the end
    }
}