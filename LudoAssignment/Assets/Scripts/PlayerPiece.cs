using UnityEngine;

//Detects when the player piece is click and request to move from gameManager, the gameManager handles the movement
public class PlayerPiece : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.MovePlayer();
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            GameManager.Instance.MovePlayer();
        }
    }
}