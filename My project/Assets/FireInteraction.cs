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

    private void OnTriggerEnter(Collider other)
    {
        // Check if the character collides with the fire and if the character is still alive
        if (other.CompareTag("Character") && characterIsAlive)
        {
            // Set characterIsAlive to false to prevent repeated destruction
            characterIsAlive = false;
        }
    }
}
