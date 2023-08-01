using UnityEngine;

public class FireInteraction : MonoBehaviour
{
    public float fireDuration = 3f; // Duration of fire after character touches it (in seconds)

    private bool characterIsAlive = true;
    private ParticleSystem fireParticles;
    private float timeSinceCollision = 0f;

    private void Start()
    {
        fireParticles = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!characterIsAlive)
        {
            timeSinceCollision += Time.deltaTime;

            // Check if the time since collision exceeds the fireDuration
            if (timeSinceCollision >= fireDuration)
            {
                // Stop emitting particles and destroy the fire game object
                fireParticles.Stop();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the character collides with the fire and if the character is still alive
        if (collision.gameObject.CompareTag("Explodable") && characterIsAlive)
        {
            // Destroy the character object
            Destroy(collision.gameObject);
            // Set characterIsAlive to false to prevent repeated destruction
            characterIsAlive = false;

            // Start the coroutine to extinguish the fire after fireDuration
            StartCoroutine(ExtinguishFire());
        }
    }

    private System.Collections.IEnumerator ExtinguishFire()
    {
        yield return new WaitForSeconds(fireDuration);

        // Stop emitting particles and destroy the fire game object
        fireParticles.Stop();
        Destroy(gameObject);
    }
}
