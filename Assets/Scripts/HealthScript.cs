/* Script for managing health of player characters
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{

    [SerializeField] public float startingHealth;
    [SerializeField] public GameObject GameManager;
    private bool isAlive;

    private void Start()
    {
        isAlive = true;
        GameManager = GameObject.Find("GameManager");
    }

    public void UpdateHealth(float change, bool isGrab)
    {
        if (this.GetComponent<PlayerMovement>().isBlocking && isGrab == false)
        {
            change *= 0.1f;
        }

        startingHealth -= change;
        //Debug.Log("health: " + startingHealth);

        if (startingHealth <= 0 && isAlive == true)
        {
            // kill player
            Debug.Log("called death");
            DisableUpdates();
            GameManager.GetComponent<GameManagerScript>().EndGame(gameObject);
            //Destroy(this);
        }
    }

    public void DisableUpdates()
    {
        isAlive = false;
    }
}
