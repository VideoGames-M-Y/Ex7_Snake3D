using UnityEngine;

public class ThingSpawner : MonoBehaviour
{
    [Tooltip("Prefab of the things to spawn")]
    [SerializeField] private GameObject thingPrefab;
    [Tooltip("Number of things to spawn at the start")]
    [SerializeField] private int thingCount = 100;
    [Tooltip("Spawn area size (width and depth)")]
    [SerializeField] private Vector2 spawnArea = new Vector2(10f, 10f);

    void Start()
    {
        SpawnThings();
    }

    private void SpawnThings()
    {
        if (thingPrefab == null)
        {
            Debug.LogError("The thing prefab is not assigned in the AppleSpawner.");
            return;
        }

        for (int i = 0; i < thingCount; i++)
        {
            // Generate a random position within the spawn area
            Vector3 randomPosition = new Vector3(
                transform.position.x + Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                transform.position.y,
                transform.position.z + Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
            );

            // Adjust the y-coordinate to match the terrain height
            if (Physics.Raycast(new Vector3(randomPosition.x, 1000f, randomPosition.z), Vector3.down, out RaycastHit hit))
            {
                randomPosition.y = hit.point.y;
            }

            // Instantiate an apple at the adjusted position
            Instantiate(thingPrefab, randomPosition, Quaternion.identity);
        }
    }
}
