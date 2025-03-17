using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Environment Settings")]
    public List<EnvironmentUnit> environmentUnits = new List<EnvironmentUnit>(); // List of environment units
    public Transform spawnPoint; // Spawn point for new environment units
    public float destroyZ = -50f; // Position at which old units get removed
    public List<GameObject> environmentPrefabs; // List of environment prefabs

    private GameObject lastSpawnedPrefab; // Stores the last spawned prefab to avoid repetition

    private void Start()
    {
        if (environmentPrefabs.Count == 0)
        {
            Debug.LogError("No environment prefabs assigned in GameManager!");
            return;
        }

        // Ensure at least one environment unit is spawned at the beginning
        if (environmentUnits.Count == 0)
        {
            SpawnNewUnit();
        }
    }

    private void Update()
    {
        ManageEnvironmentUnits(); // Check and manage environment units
    }

    void ManageEnvironmentUnits()
    {
        if (environmentUnits.Count > 0)
        {
            EnvironmentUnit firstUnit = environmentUnits[0];

            // If the first unit moves out of view, remove it and spawn a new one
            if (firstUnit.transform.position.z <= destroyZ)
            {
                Debug.Log("Removing environment unit: " + firstUnit.gameObject.name);
                Destroy(firstUnit.gameObject);
                environmentUnits.RemoveAt(0);
                SpawnNewUnit();
            }
        }
    }

    void SpawnNewUnit()
    {
        if (environmentPrefabs.Count == 0)
        {
            Debug.LogError("No environment prefabs available for spawning!");
            return;
        }

        GameObject selectedPrefab;
        do
        {
            selectedPrefab = environmentPrefabs[Random.Range(0, environmentPrefabs.Count)];
        } while (selectedPrefab == lastSpawnedPrefab && environmentPrefabs.Count > 1);

        lastSpawnedPrefab = selectedPrefab;

        GameObject newUnit = Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);
        environmentUnits.Add(newUnit.GetComponent<EnvironmentUnit>());

        Debug.Log("Spawned new environment unit: " + newUnit.name);
    }
}
