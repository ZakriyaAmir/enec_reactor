using UnityEngine;

public class PrefabMover : MonoBehaviour
{
    private Vector3 spawnPosition;
    public SpawnerController spawnController;
    private float speed;
    private float rotationSpeed;
    public string title;

    public void Initialize(Vector3 spawnPos, float moveSpeed, float rotSpeed)
    {
        spawnPosition = spawnPos;
        speed = moveSpeed;
        rotationSpeed = rotSpeed;
    }

    void Update()
    {
        // Move **only** upward (not affected by rotation)
        transform.position += Vector3.up * speed * Time.deltaTime;

        // Rotate around **own Z-axis**
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RespawnZone")) // Ensure the red line has this tag
        {
            if (spawnController.prefab.name == title)
            {
                Respawn();

                if (title == "molecule") 
                {
                    GetComponent<moleculeBehavior>().init();
                }
            }
            else 
            {
                Destroy(gameObject);
            }
        }
    }

    void Respawn()
    {
        transform.position = spawnPosition;
    }
}
