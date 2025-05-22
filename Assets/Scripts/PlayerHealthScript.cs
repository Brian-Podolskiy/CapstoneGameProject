/* Manages health for both players across project
 * Additionally manages the healthbar images on top of canvas
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    float P1health; // = GameManagerScript.P1.GetComponent<HealthScript>().startingHealth;
    float P2health; // = GameManagerScript.P2.GetComponent<HealthScript>().startingHealth;
    [SerializeField] public TMP_Text P1healthText;
    [SerializeField] public TMP_Text P2healthText;
    [SerializeField] public GameObject GameManager;
    public Image healthBarP1;
    public Image healthBarP2;

    void Update()
    {
        if (GameManagerScript.P1 != null && GameManagerScript.P2 != null)
        {
            P1health = GameManagerScript.P1.GetComponent<HealthScript>().startingHealth; // update HP values every frame
            P2health = GameManagerScript.P2.GetComponent<HealthScript>().startingHealth;
            P1healthText.text = P1health.ToString();
            P2healthText.text = P2health.ToString();
            healthBarP1.fillAmount = P1health / 1500f;
            healthBarP2.fillAmount = P2health / 1500f;

            if (P1health <= 0)
            {
                GameManager.GetComponent<GameManagerScript>().EndGame(GameManagerScript.P2);
                //Destroy(GameManagerScript.P2);
            }
            else if (P2health <= 0)
            {
                GameManager.GetComponent<GameManagerScript>().EndGame(GameManagerScript.P1);
                //Destroy(GameManagerScript.P1);
            }
        }
    }
}
