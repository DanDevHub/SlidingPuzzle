using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRButtonController : MonoBehaviour {
    public GameManager gameManager;

    public enum MoveDirection { Up, Down, Left, Right }
    public MoveDirection direction;

    public void OnSelectEntered(SelectEnterEventArgs args) {
        if (gameManager == null) {
            Debug.LogError("GameManager is not assigned!");
            return;
        }

        Debug.Log($"Button Pressed: {direction}");

        switch (direction) {
            case MoveDirection.Up:
                gameManager.MoveUp();
                break;
            case MoveDirection.Down:
                gameManager.MoveDown();
                break;
            case MoveDirection.Left:
                gameManager.MoveLeft();
                break;
            case MoveDirection.Right:
                gameManager.MoveRight();
                break;
        }
    }

}
