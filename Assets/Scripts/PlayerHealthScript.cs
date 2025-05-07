using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthScript : MonoBehaviour
{
    float P1health; // = GameManagerScript.P1.GetComponent<HealthScript>().startingHealth;
    float P2health; // = GameManagerScript.P2.GetComponent<HealthScript>().startingHealth;
    [SerializeField] public TMP_Text P1healthText;
    [SerializeField] public TMP_Text P2healthText;

    void Update()
    {
        if (GameManagerScript.P1 != null && GameManagerScript.P2 != null)
        {
            float P1health = GameManagerScript.P1.GetComponent<HealthScript>().startingHealth; // update HP values every frame
            float P2health = GameManagerScript.P2.GetComponent<HealthScript>().startingHealth;
            P1healthText.text = P1health.ToString();
            P2healthText.text = P2health.ToString();
        }
    }
}
