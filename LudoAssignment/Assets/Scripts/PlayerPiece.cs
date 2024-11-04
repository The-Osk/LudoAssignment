using UnityEngine;

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