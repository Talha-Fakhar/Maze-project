
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using UnityEngine;

public class wolia_bomb : MonoBehaviour
{
    public float radius = 5f;
    public float force = 700f;
    public GameObject explosionEffect1;

    bool hasExploded = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded && collision.gameObject.layer == LayerMask.NameToLayer("CharacterLayer"))
        {
            hasExploded = true;
            StartCoroutine(ExplodeWithDelay());
        }
    }

    private IEnumerator ExplodeWithDelay()
    {
        yield return new WaitForSeconds(0.1f); // A small delay before the explosion

        Instantiate(explosionEffect1, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyobject in colliders)
        {
            if (nearbyobject.CompareTag("Explodable"))
            {
                Rigidbody rb = nearbyobject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                    if (nearbyobject.gameObject != gameObject) // Check if it's not the bomb itself
                    {
                        float distanceToBomb = Vector3.Distance(transform.position, nearbyobject.transform.position);
                        if (distanceToBomb <= 2f) // Adjust the radius (2f) as needed
                        {
                            Destroy(nearbyobject.gameObject);
                        }
                    }
                }
            }
        }

        // Destroy the bomb after the explosion
        Destroy(gameObject, 0.1f);
    }
}
