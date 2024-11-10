using UnityEngine;
using PLAYERTWO.PlatformerProject; // Ensure this is added to reference PPProjectileTrigger
using UnityEngine.UI; // Or using TMPro; if you're using TextMeshPro
using TMPro;  // Import TextMeshPro namespace

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ammoCountText; // UI Text component to show the current ammo count
    public TextMeshProUGUI ammoPickupFeedbackText; // UI Text component to show feedback for ammo pickup
    public float feedbackDisplayTime = 2f; // Time for feedback to be visible
    public PPProjectileTrigger playerProjectileTrigger; // Reference to PPProjectileTrigger

    private void Start()
    {
        playerProjectileTrigger = FindObjectOfType<PPProjectileTrigger>(); // Get the player's projectile trigger script
        UpdateAmmoUI(); // Set the initial ammo count UI
    }

    private void Update()
    {
        // Update the ammo count UI regularly in case it changes in runtime
        UpdateAmmoUI();
    }

    // Update the ammo UI based on the player's current ammo count
   public void UpdateAmmoUI()
    {
        if (ammoCountText != null && playerProjectileTrigger != null)
        {
            ammoCountText.text = "" + playerProjectileTrigger.currentAmmo.ToString();
        }
    }

    // Show the feedback message when ammo is collected
    public void ShowAmmoPickupFeedback(int ammoAmount)
    {
        if (ammoPickupFeedbackText != null)
        {
            ammoPickupFeedbackText.text = "+" + ammoAmount.ToString() + " Ammo!";
            ammoPickupFeedbackText.gameObject.SetActive(true); // Make the feedback visible
            Invoke(nameof(HideAmmoPickupFeedback), feedbackDisplayTime); // Hide the feedback after the set time
        }
    }

    // Hide the ammo pickup feedback message
    private void HideAmmoPickupFeedback()
    {
        if (ammoPickupFeedbackText != null)
        {
            ammoPickupFeedbackText.gameObject.SetActive(false);
        }
    }
}


