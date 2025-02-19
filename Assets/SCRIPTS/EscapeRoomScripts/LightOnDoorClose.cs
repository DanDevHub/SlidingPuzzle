using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightOnDoorClose : MonoBehaviour
{
    public GameObject doors;
    public GameObject hamster1;
    public GameObject hamster2;


    float moveDistance = 1f;
    float moveDuration = 0.1f; // Duration for movement

    private TextMeshProUGUI[] textFields;


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
            {
            doors.SetActive(true);
            hamster1.gameObject.SetActive(false);  // Deactivate the old GameObject
            hamster2.gameObject.SetActive(true);   // Activate the new GameObject
            StartCoroutine(MoveUpAndDown(hamster2.transform)); // Start movement


            if (hamster2 != null)
            {
                // Find the Canvas child inside hamster2
                Transform canvasTransform = hamster2.transform.Find("Canvas");

                if (canvasTransform != null)
                {
                    // Activate the entire Canvas GameObject
                    canvasTransform.gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogError("Canvas child not found inside hamster2.");
                }
            }
            else
            {
                Debug.LogError("hamster2 GameObject is not assigned!");
            }
        }
    }

    IEnumerator MoveUpAndDown(Transform obj)
    {
        float startY = obj.position.y;  // Store initial Y position
        float targetY = startY + moveDistance;  // Compute target Y position dynamically
        float elapsedTime = 0f;

        // Move up
        while (elapsedTime < moveDuration)
        {
            float newY = Mathf.Lerp(startY, targetY, elapsedTime / moveDuration);
            obj.position = new Vector3(obj.position.x, newY, obj.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.position = new Vector3(obj.position.x, targetY, obj.position.z); // Ensure it reaches the top

        elapsedTime = 0f;

        // Move down
        while (elapsedTime < moveDuration)
        {
            float newY = Mathf.Lerp(targetY, startY, elapsedTime / moveDuration);
            obj.position = new Vector3(obj.position.x, newY, obj.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.position = new Vector3(obj.position.x, startY, obj.position.z); // Ensure it returns to the start
    }
}