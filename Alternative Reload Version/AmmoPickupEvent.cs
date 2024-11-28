using UnityEngine;
using UnityEngine.Events;

namespace PLAYERTWO.PlatformerProject
{
    public class AmmoPickupEvent : MonoBehaviour
    {
        [Header("Ammo Settings")]
        public int ammoAmount = 1; // Amount of ammo this pickup will provide
        public AudioClip collectSound; // Sound to play when ammo is collected
        public GameObject pickupEffect; // Optional: a particle effect or visual feedback

        [Header("Events")]
        public UnityEvent<int> onAmmoCollected; // Event triggered when ammo is collected

        /// <summary>
        /// Method to trigger ammo collection. Should be called via an event.
        /// </summary>
        /// <param name="player">The player GameObject collecting the ammo.</param>
        public void TriggerPickup(GameObject player)
        {
            // Debug log to confirm pickup is triggered
            Debug.Log($"Pickup triggered by: {player.name}");

            // Check if the player has the PPProjectileTrigger component
            PPProjectileTrigger projectileTrigger = player.GetComponent<PPProjectileTrigger>();
            if (projectileTrigger != null)
            {
                // Add ammo to the player's component
                projectileTrigger.CollectAmmunition(ammoAmount);

                // Invoke the event with the ammo amount
                onAmmoCollected.Invoke(ammoAmount);

                // Optionally play a collect sound
                if (collectSound != null)
                {
                    AudioSource.PlayClipAtPoint(collectSound, transform.position);
                }

                // Optionally instantiate a pickup effect
                if (pickupEffect != null)
                {
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);
                }

                // Destroy the pickup object after use
                //Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Player GameObject does not have a PPProjectileTrigger component!");
            }
        }
    }
}
