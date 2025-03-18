using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Environment Settings")]
    public GameObject environmentPrefab; // The prefab for environment units
    public float destroyZ = -50f; // Position where the environment gets destroyed
    public float spawnTriggerZ = -1f; // When to spawn a new environment
    public Transform firstSpawnPosition;
    public Transform nextSpawnPosition;

    private Queue<GameObject> activeUnits = new Queue<GameObject>();
    private bool hasSpawnedNext = false; // Flag to track if next environment has been spawned

    private void Start()
    {
        if (environmentPrefab == null)
        {
            Debug.LogError("No environment prefab assigned!");
            return;
        }

        // Spawn the first environment unit
        SpawnNewUnit(firstSpawnPosition);
    }

    private void Update()
    {
        if (activeUnits.Count > 0)
        {
            GameObject firstUnit = activeUnits.Peek();

            // If the first unit reaches the trigger point and we haven't spawned the next one yet
            if (firstUnit.transform.position.z <= spawnTriggerZ && !hasSpawnedNext)
            {
                SpawnNewUnit(nextSpawnPosition);
                hasSpawnedNext = true; // Ensure only one new environment spawns
            }

            // If the first unit reaches the destroy point, remove it
            if (firstUnit.transform.position.z <= destroyZ)
            {
                Destroy(activeUnits.Dequeue());
                hasSpawnedNext = false; // Reset the flag so we can spawn again
            }
        }
    }

    void SpawnNewUnit(Transform spawnPosition)
    {
        GameObject newUnit = Instantiate(environmentPrefab, spawnPosition.position, Quaternion.identity);
        activeUnits.Enqueue(newUnit);
    }
}
