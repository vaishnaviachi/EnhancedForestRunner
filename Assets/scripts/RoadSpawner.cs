
using UnityEngine;
using System.Collections.Generic;

public class RoadSpawner : MonoBehaviour
{
    [Header("Forest Settings")]
    public GameObject forestChunk;
    public GameObject startingEnvironment;
    public Transform player;

    [Header("Generation Settings")]
    public float chunkLength = 120f;
    public int chunksOnScreen = 3;

    private float nextSpawnZ = 120f;

    private List<GameObject> activeChunks =
        new List<GameObject>();

    void Start()
    {
        if (forestChunk == null || startingEnvironment == null || player == null)
        {
            Debug.LogError("Assign Forest Chunk, Starting Environment and Player!");
            enabled = false;
            return;
        }

        // Keep the existing forest as the first section
        activeChunks.Add(startingEnvironment);

        // Generate two sections ahead
        for (int i = 1; i < chunksOnScreen; i++)
        {
            SpawnChunk();
        }
    }

    void Update()
    {
        // Generate new forest sections ahead of the player
        while (player.position.z +
               (chunksOnScreen - 1) * chunkLength > nextSpawnZ)
        {
            SpawnChunk();
        }

        // Remove old sections behind the player
        while (activeChunks.Count > chunksOnScreen &&
               player.position.z >
               activeChunks[0].transform.position.z + chunkLength)
        {
            Destroy(activeChunks[0]);
            activeChunks.RemoveAt(0);
        }
    }

    void SpawnChunk()
    {
        GameObject chunk = Instantiate(
            forestChunk,
            new Vector3(0f, 0f, nextSpawnZ),
            Quaternion.identity
        );

        activeChunks.Add(chunk);

        nextSpawnZ += chunkLength;
    }
}
