using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateUIElement : MonoBehaviour
{
    public GameObject uiElementToDeactivate;
    public GameObject uiElementToActivate;

    public void DeactivateElement()
    {
        // Check if the UI element to deactivate is not null
        if (uiElementToDeactivate != null)
        {
            // Deactivate the UI element
            uiElementToDeactivate.SetActive(false);
        }
        else
        {
            Debug.LogWarning("UI Element to deactivate is not assigned!");
        }

        // Check if the UI element to activate is not null
        if (uiElementToActivate != null)
        {
            // Activate the UI element
            uiElementToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("UI Element to activate is not assigned!");
        }
    }
}

