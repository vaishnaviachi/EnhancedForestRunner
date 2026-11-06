
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    public int coinPoints = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            ScoreManager scoreManager =
                FindObjectOfType<ScoreManager>();

            if (scoreManager != null)
            {
                scoreManager.AddCoinPoints(coinPoints);
            }

            Destroy(other.gameObject);

            Debug.Log("COIN COLLECTED! +" + coinPoints);
        }
    }
}
