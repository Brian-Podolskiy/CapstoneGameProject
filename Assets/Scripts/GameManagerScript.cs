using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] public GameObject char1;
    [SerializeField] public GameObject char2;
    public static GameObject P1;
    public static GameObject P2;
    public static int P1points = 0;
    public static int P2points = 0;

    void Start()
    {
        P1points = 0;
        P2points = 0;
        int charNumP1 = CharacterSelectScript.charSelectP1;
        int charNumP2 = CharacterSelectScript.charSelectP2;
        Debug.Log("charNumP1: " + charNumP1 + "\n" + "charNumP2: " + charNumP2);

        switch (charNumP1)
        {
            case (1):
                P1 = Instantiate(char1, new Vector2(-5, 0), Quaternion.identity);
                P1.layer = LayerMask.NameToLayer("Enemy");
                break;

            case (2):
                P1 = Instantiate(char2, new Vector2(-5, 0), Quaternion.identity);
                P1.layer = LayerMask.NameToLayer("Enemy");
                break;

            default:
                break;
        }

        switch (charNumP2)
        {
            case (1):
                P2 = Instantiate(char1, new Vector2(5, 0), Quaternion.identity);
                P2.layer = LayerMask.NameToLayer("Enemy");
                P2.GetComponent<PlayerMovement>().Flip();
                break;

            case (2):
                P2 = Instantiate(char2, new Vector2(5, 0), Quaternion.identity);
                P2.layer = LayerMask.NameToLayer("Enemy");
                P2.GetComponent<PlayerMovement>().Flip();
                break;

            default:
                break;
        }
        Debug.Log("P1: " + P1 + "\n" + "P2: " + P2);
    }

    public void NextRound()
    {
        Debug.Log("Started next round...");

        Destroy(P1);
        Destroy(P2);

        int charNumP1 = CharacterSelectScript.charSelectP1;
        int charNumP2 = CharacterSelectScript.charSelectP2;
        Debug.Log("charNumP1: " + charNumP1 + "\n" + "charNumP2: " + charNumP2);

        switch (charNumP1)
        {
            case (1):
                P1 = Instantiate(char1, new Vector2(-5, 0), Quaternion.identity);
                P1.layer = LayerMask.NameToLayer("Enemy");
                break;

            case (2):
                P1 = Instantiate(char2, new Vector2(-5, 0), Quaternion.identity);
                P1.layer = LayerMask.NameToLayer("Enemy");
                break;

            default:
                break;
        }

        switch (charNumP2)
        {
            case (1):
                P2 = Instantiate(char1, new Vector2(5, 0), Quaternion.identity);
                P2.layer = LayerMask.NameToLayer("Enemy");
                P2.GetComponent<PlayerMovement>().Flip();
                break;

            case (2):
                P2 = Instantiate(char2, new Vector2(5, 0), Quaternion.identity);
                P2.layer = LayerMask.NameToLayer("Enemy");
                P2.GetComponent<PlayerMovement>().Flip();
                break;

            default:
                break;
        }
        Debug.Log("P1: " + P1 + "\n" + "P2: " + P2);
    }

    public void EndGame(GameObject winner)
    {
        if (winner.Equals(P1))
        {
            P1points++;
        }
        else
        {
            P2points++;
        }

        if (P1points != 2 && P2points != 2)
        {
            Debug.Log("P1points: " + P1points + "\n" + "P2points: " + P2points);
            NextRound();
        }
    }
}
