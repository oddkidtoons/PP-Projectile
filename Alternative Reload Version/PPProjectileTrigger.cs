using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace PLAYERTWO.PlatformerProject
{
    public class PPProjectileTrigger : MonoBehaviour
    {
        public UIManager uiManager;  // Reference to the UIManager to update ammo UI
        public OddKid_Input playerControls;
        private InputAction fire;

        public GameObject projectile;
        public Transform projectile_spawn_loc;
        public Vector3 projectileVel;

        private Animator anim;
        private Animator camAnim;

        public int currentAmmo = 10;  // Initial ammo count
        public int maxAmmo = 10;  // Max ammo capacity
        public AudioSource audioSource;  // Reference to the AudioSource component

        // Add audio clips for reload and empty ammo sounds
        public AudioClip reloadSound;
        public AudioClip emptyAmmoSound;

        private void Awake()
        {
            playerControls = new OddKid_Input();
        }

        private void OnEnable()
        {
            fire = playerControls.Player.Fire;
            fire.Enable();
            fire.performed += shootFireBall;
        }

        private void OnDisable()
        {
            fire.Disable();
        }

        private void shootFireBall(InputAction.CallbackContext Context)
        {
            if (currentAmmo > 0) // Ensure there is ammo before shooting
            {
                StartCoroutine(Shoot_Projectile());
            }
            else
            {
                PlayEmptyAmmoSound();  // Play empty ammo sound if out of ammo
                Debug.Log("Out of Ammo!");
                // Optionally show a message on UI indicating out of ammo
            }
        }

        IEnumerator Shoot_Projectile()
        {
            if (currentAmmo > 0)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject Clone = Instantiate(projectile, projectile_spawn_loc.position, projectile.transform.rotation);
                Clone.GetComponent<PPProjectile>().enabled = true;
                Clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(projectileVel.x, projectileVel.y, projectileVel.z); // initial speed

                currentAmmo--; // Decrease ammo on each shot
                uiManager.UpdateAmmoUI(); // Update the ammo UI
            }
            else
            {
                // Play empty ammo sound or feedback
                Debug.Log("Out of ammo!");
            }
        }

        public void ReloadAmmo(int ammoToReload)
        {
            if (ammoToReload + currentAmmo > maxAmmo)
            {
                currentAmmo = maxAmmo;  // Cap ammo at maxAmmo
            }
            else
            {
                currentAmmo += ammoToReload;  // Reload the ammo
            }

            // Play reload sound
            PlayReloadSound();

            // Update UI after reloading
            if (uiManager != null)
            {
                uiManager.UpdateAmmoUI();
            }
        }

        public void CollectAmmunition(int ammoAmount)
        {
            // Add ammo to the player, but don't exceed maxAmmo
            currentAmmo = Mathf.Min(currentAmmo + ammoAmount, maxAmmo);
            
            // Update the ammo UI
            uiManager.UpdateAmmoUI();
            
            // Show feedback for ammo pickup
            uiManager.ShowAmmoPickupFeedback(ammoAmount);
            // Play reload sound (optional)
            PlayReloadSound();

            Debug.Log("Collected Ammo. Current ammo: " + currentAmmo);
        }

        private void PlayReloadSound()
        {
            if (audioSource != null && reloadSound != null)
            {
                audioSource.PlayOneShot(reloadSound);  // Play reload sound
            }
        }

        private void PlayEmptyAmmoSound()
        {
            if (audioSource != null && emptyAmmoSound != null)
            {
                audioSource.PlayOneShot(emptyAmmoSound);  // Play empty ammo sound
            }
        }
    }
}
