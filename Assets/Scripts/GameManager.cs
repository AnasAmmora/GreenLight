using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Enemy Cars Settings")]
    public float enemyCarsSpeed = 10f;
    public float timeBetweenSpawningCars = 3f;
    [SerializeField] private List<GameObject> enemyCarPrefabs;
    [SerializeField] private List<Transform> enemyCarSpawnPoints;

    private float enemySpawnTimer;

    [Header("Environment Settings")]
    public float environmentSpeed = 10f;
    [SerializeField] private GameObject environmentPrefab;
    [SerializeField] private float destroyZ = -50f;
    [SerializeField] private float spawnTriggerZ = -1f;
    [SerializeField] private Transform firstSpawnPosition;
    [SerializeField] private Transform nextSpawnPosition;

    private Queue<GameObject> activeUnits = new Queue<GameObject>();
    private bool hasSpawnedNext = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (environmentPrefab == null)
        {
            Debug.LogError("No environment prefab assigned!");
            return;
        }

        SpawnNewUnit(firstSpawnPosition);
    }

    private void Update()
    {
        HandleSpawnEnvironments();
        HandleSpawnEnemyCars();
    }

    private void HandleSpawnEnemyCars()
    {

    }

    private void HandleSpawnEnvironments()
    {
        if (activeUnits.Count > 0)
        {
            GameObject firstUnit = activeUnits.Peek();

            if (firstUnit.transform.position.z <= spawnTriggerZ && !hasSpawnedNext)
            {
                SpawnNewUnit(nextSpawnPosition);
                hasSpawnedNext = true;
            }

            if (firstUnit.transform.position.z <= destroyZ)
            {
                Destroy(activeUnits.Dequeue());
                hasSpawnedNext = false;
            }
        }
    }

    private void SpawnNewUnit(Transform spawnPosition)
    {
        GameObject newUnit = Instantiate(environmentPrefab, spawnPosition.position, Quaternion.identity);
        activeUnits.Enqueue(newUnit);
    }
}
