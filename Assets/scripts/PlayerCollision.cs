
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameManager gameManager;

    private bool gameOver = false;

    void OnTriggerEnter(Collider other)
    {
        if (gameOver) return;

        if (other.gameObject.name == "Obstacle")
        {
            gameOver = true;
            gameManager.GameOver();
        }
    }
}
