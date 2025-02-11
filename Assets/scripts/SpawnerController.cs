using System.Collections;
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

        while (spawnedCount < spawnPoints.Length)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, spawnPoints.Length);
            }
            while (hasSpawned[randomIndex]); // Find an unused spawn point

            Transform spawnPoint = spawnPoints[randomIndex];
            hasSpawned[randomIndex] = true; // Mark this spawn point as used
            spawnedCount++;

            float randomSpeed = Random.Range(minUpwardSpeed, maxUpwardSpeed); // Assign random upward speed
            float randomRotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed); // Assign random rotation speed
            float randomZRotation = Random.Range(0f, 360f); // Assign random initial Z-axis rotation

            GameObject instance = Instantiate(prefab, spawnPoint.position, Quaternion.Euler(0, 0, randomZRotation));
            instance.GetComponent<PrefabMover>().spawnController = this;
            instance.GetComponent<PrefabMover>().title = prefab.name;

            // Set the predefined scale
            instance.transform.localScale = Vector3.one * prefabScale;

            PrefabMover mover = instance.GetComponent<PrefabMover>();
            if (mover != null)
            {
                mover.Initialize(spawnPoint.position, randomSpeed, randomRotationSpeed);
            }
        }

        Debug.Log("All spawn points have been used. Stopping spawning.");
    }
}
