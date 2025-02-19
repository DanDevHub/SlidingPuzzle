using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSceneLoader : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private string sceneToLoad = "YourSceneName";  // Scene name to load

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found. Please attach one.");
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnSelect()  // This method is hooked to the XR Simple Interactable "On Select" event
    {
        if (audioSource != null)
        {
            audioSource.Play();  // Play the audio
            StartCoroutine(LoadSceneAfterSound(audioSource.clip.length));  // Wait for the sound to finish
        }
        else
        {
            LoadScene();  // Load scene immediately if no sound is available
        }
    }

    private System.Collections.IEnumerator LoadSceneAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadScene();
    }

    private void LoadScene()
    {
        Debug.Log("Loading scene: " + sceneToLoad);
        foreach (var obj in FindObjectsOfType<GameObject>())
                        {
                            if (obj.scene.buildIndex == -1) // Objekte, die nicht zur Szene gehören
                            {
                                Destroy(obj);
                            }
                        }
        SceneManager.LoadScene(sceneToLoad);
    }
}