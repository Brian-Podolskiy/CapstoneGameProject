/* Script for managing health of player characters
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{

    [SerializeField] public float startingHealth;

    public void UpdateHealth(float change)
    {
        startingHealth -= change;
        Debug.Log("health: " + startingHealth);

        if (startingHealth <= 0)
        {
            // kill player
        }
    }
}
