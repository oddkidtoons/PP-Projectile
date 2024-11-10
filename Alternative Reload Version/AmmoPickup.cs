using UnityEngine;

namespace PLAYERTWO.PlatformerProject
{
    public class AmmoPickup : MonoBehaviour
    {
        public int ammoAmount = 1; // Amount of ammo this pickup will give
        public AudioClip collectSound; // Sound to play when ammo is collected
        public GameObject pickupEffect; // Optional: a particle effect or visual feedback

        private void OnTriggerEnter(Collider other)
        {
            // Debug log to check what is colliding
            Debug.Log("Trigger entered by: " + other.gameObject.name);

            // Check if the player (or the object you want to pick up the ammo) collides with this
            if (other.CompareTag("Player")) // Assuming the player has a "Player" tag
            {
                // Log to confirm the player is colliding
                Debug.Log("Player has collected the ammo!");

                // Collect the ammo by calling the CollectAmmunition method on the player's projectile trigger
                PPProjectileTrigger projectileTrigger = other.GetComponent<PPProjectileTrigger>();
                if (projectileTrigger != null)
                {
                    // Collect ammo and play the sound effect
                    projectileTrigger.CollectAmmunition(ammoAmount);

                    // Optionally play a collect sound
                    if (collectSound != null)
                    {
                        AudioSource.PlayClipAtPoint(collectSound, transform.position);
                    }

                    // Optionally, instantiate a pickup effect (e.g., particles or visual feedback)
                    if (pickupEffect != null)
                    {
                        Instantiate(pickupEffect, transform.position, Quaternion.identity);
                    }

                    // Destroy the ammo pickup object after collection
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning("Player doesn't have PPProjectileTrigger component!");
                }
            }
        }
    }
}
