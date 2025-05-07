using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] public GameObject char1;
    [SerializeField] public GameObject char2;
    public static GameObject P1;
    public static GameObject P2;

    void Start()
    {
        int charNumP1 = CharacterSelectScript.charSelectP1;
        int charNumP2 = CharacterSelectScript.charSelectP2;
        Debug.Log("charNumP1: " + charNumP1 + "\n" + "charNumP2" + charNumP2);

        switch (charNumP1)
        {
            case (1):
                P1 = Instantiate(char1, new Vector2(-5, 0), Quaternion.identity);
                break;

            case (2):
                P1 = Instantiate(char2, new Vector2(-5, 0), Quaternion.identity);
                break;

            default:
                break;
        }

        switch (charNumP2)
        {
            case (1):
                P2 = Instantiate(char1, new Vector2(5, 0), Quaternion.identity);
                P2.GetComponent<PlayerMovement>().Flip();
                break;

            case (2):
                P2 = Instantiate(char2, new Vector2(5, 0), Quaternion.identity);
                P2.GetComponent<PlayerMovement>().Flip();
                break;

            default:
                break;
        }
        Debug.Log("P1: " + P1 + "\n" + "P2: " + P2);
    }
}
