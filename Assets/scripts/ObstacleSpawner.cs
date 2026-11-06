
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform player;

    public float spawnDistance = 35f;
    public float spawnInterval = 18f;
    public float destroyDistance = 12f;

    private float nextSpawnZ;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Assign the Player in ObstacleSpawner!");
            enabled = false;
            return;
        }

        nextSpawnZ = player.position.z + spawnDistance;
    }

    void Update()
    {
        while (player.position.z + spawnDistance >= nextSpawnZ)
        {
            SpawnObstacle();
            nextSpawnZ += spawnInterval;
        }

        foreach (Transform obstacle in transform)
        {
            if (obstacle.position.z < player.position.z - destroyDistance)
            {
                Destroy(obstacle.gameObject);
            }
        }
    }

    void SpawnObstacle()
    {
        float[] lanes = { -2f, 0f, 2f };
        float lane = lanes[Random.Range(0, lanes.Length)];

        GameObject obstacle;

        int type = Random.Range(0, 3);

        if (type == 0)
        {
            obstacle = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obstacle.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        else if (type == 1)
        {
            obstacle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obstacle.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
        else
        {
            obstacle = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            obstacle.transform.localScale = new Vector3(0.7f, 0.8f, 0.7f);
        }

        obstacle.transform.position = new Vector3(lane, 0.7f, nextSpawnZ);

        obstacle.transform.SetParent(transform);

        Renderer renderer = obstacle.GetComponent<Renderer>();
        renderer.material.color = type == 0
            ? new Color(0.45f, 0.22f, 0.10f)
            : type == 1
                ? Color.gray
                : new Color(0.2f, 0.35f, 0.15f);

        Collider obstacleCollider = obstacle.GetComponent<Collider>();
        obstacleCollider.isTrigger = true;

        obstacle.name = "Obstacle";
    }
}
