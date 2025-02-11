using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject prefab; // Prefab to instantiate
    public Transform[] spawnPoints; // Array of spawn points
    public float minUpwardSpeed = 1f; // Minimum movement speed
    public float maxUpwardSpeed = 3f; // Maximum movement speed
    public float minRotationSpeed = 10f; // Minimum rotation speed (Z-axis)
    public float maxRotationSpeed = 50f; // Maximum rotation speed (Z-axis)
    public float minSpawnDelay = 1f; // Minimum time between spawns
    public float maxSpawnDelay = 3f; // Maximum time between spawns
    public float prefabScale = 1f; // Scale factor for spawned prefabs

    public bool[] hasSpawned; // Tracks which spawn points have been used

    public void init() 
    {
        hasSpawned = new bool[spawnPoints.Length]; // Initialize tracking array
        StartCoroutine(SpawnPrefabs());
    }

    IEnumerator SpawnPrefabs()
    {
        int spawnedCount = 0;
        List<int> availableSpawnIndices = new List<int>();

        // Prepopulate available spawn indices
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            availableSpawnIndices.Add(i);
        }

        while (spawnedCount < spawnPoints.Length)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            if (availableSpawnIndices.Count == 0)
            {
                Debug.Log("All spawn points have been used. Stopping spawning.");
                yield break; // Exit coroutine safely
            }

            // Pick a random available spawn point
            int index = Random.Range(0, availableSpawnIndices.Count);
            int spawnIndex = availableSpawnIndices[index];

            Transform spawnPoint = spawnPoints[spawnIndex];
            availableSpawnIndices.RemoveAt(index); // Remove used index
            hasSpawned[spawnIndex] = true;
            spawnedCount++;

            float randomSpeed = Random.Range(minUpwardSpeed, maxUpwardSpeed);
            float randomRotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
            float randomZRotation = Random.Range(0f, 360f);

            GameObject instance = Instantiate(prefab, spawnPoint.position, Quaternion.Euler(0, 0, randomZRotation));
            instance.transform.localScale = Vector3.one * prefabScale;

            // Optimize by caching the component
            PrefabMover mover = instance.GetComponent<PrefabMover>();
            if (mover != null)
            {
                mover.spawnController = this;
                mover.title = prefab.name;
                mover.Initialize(spawnPoint.position, randomSpeed, randomRotationSpeed);
            }
        }

        Debug.Log("All spawn points have been used. Stopping spawning.");
    }

}
