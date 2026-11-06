
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform player;

    public float spawnDistance = 40f;
    public float spawnInterval = 15f;

    private float nextSpawnZ;

    void Start()
    {
        nextSpawnZ = player.position.z + spawnDistance;
    }

    void Update()
    {
        while (player.position.z + spawnDistance >= nextSpawnZ)
        {
            SpawnCoins();
            nextSpawnZ += spawnInterval;
        }
    }

    void SpawnCoins()
    {
        float[] lanes = { -2f, 0f, 2f };

        int selectedLane = Random.Range(0, lanes.Length);

        for (int i = 0; i < 5; i++)
        {
            Vector3 position = new Vector3(
                lanes[selectedLane],
                1f,
                nextSpawnZ + i * 2f
            );

            Instantiate(
                coinPrefab,
                position,
                Quaternion.identity
            );
        }
    }
}
