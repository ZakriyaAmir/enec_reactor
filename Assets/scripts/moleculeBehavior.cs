using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moleculeBehavior : MonoBehaviour
{
    public float forceAmount = 5f; // Adjust the force strength
    public float destroyTime = 3f; // Time before the objects get destroyed
    public Transform atomsParent;

    void Start()
    {
        init();
    }

    public void init()
    {
        ApplyRandomForceToChildren();
    }

    void ApplyRandomForceToChildren()
    {
        Debug.Log("zak1");
        foreach (Transform child in atomsParent) // Loop through all children
        {
            GameObject go = Instantiate(child.gameObject);
            Debug.Log("zak2 = " + go.name);
            go.transform.parent = null;
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized; // Get a random direction
                rb.AddForce(randomDirection * forceAmount, ForceMode2D.Impulse); // Apply force

                Destroy(go.gameObject, destroyTime); // Destroy after 3 seconds
            }
        }
    }
}
