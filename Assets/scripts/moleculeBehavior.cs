using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moleculeBehavior : MonoBehaviour
{
    public float forceAmount = 5f; // Adjust the force strength
    public float destroyTime = 3f; // Time before the objects get destroyed
    public Transform atomsParent;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        init();
    }

    public void init()
    {
        transform.localScale = originalScale;
        StartCoroutine(ApplyRandomForceToChildren());
    }

    IEnumerator ApplyRandomForceToChildren()
    {
        float timedelay = Random.Range(1f, 2f);
        yield return new WaitForSeconds(timedelay);

        //for scale anim
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;
        //

        Debug.Log("zak1");
        foreach (Transform child in atomsParent) // Loop through all children
        {
            GameObject go = Instantiate(child.gameObject, transform);
            Debug.Log("zak2 = " + go.name);
            go.transform.parent = null;
            go.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
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
