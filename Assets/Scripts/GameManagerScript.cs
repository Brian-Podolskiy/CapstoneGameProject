/* Game Manager Script
 * Manages both players and how rounds progress 
 * Keeps track of which rounds player win
 * Text for what current round is + countdown
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] public GameObject char1;
    [SerializeField] public GameObject char2;
    public static GameObject P1;
    public static GameObject P2;
    public static int P1points = 0;
    public static int P2points = 0;
    private int roundNum;
    private float waitTime = 0.0f;
    private float timer = 4.0f;
    private int timerSec;

    [SerializeField] public TMP_Text ReadyText;
    [SerializeField] public TMP_Text CountDownText;

    public static bool gameStarted = false;
    private bool gameEnded = false;
    private int winner;

    void Start()
    {
        gameEnded = false;
        P1points = 0;
        P2points = 0;
        roundNum = 1;
        ReadyText.text = "Round " + roundNum + " Ready...";
        SetRound();
        Debug.Log("P1: " + P1 + "\n" + "P2: " + P2);
    }

    private void Update()
    {
        ReadyText.text = "Round " + roundNum + " Ready...";
        timer -= Time.deltaTime;
        if (timer >= waitTime && gameEnded == false)
        {
            gameStarted = false;
            timerSec = (int)timer;
            CountDownText.enabled = true;
            CountDownText.text = timerSec.ToString();
        }
        else if (gameEnded == false)
        {
            CountDownText.enabled = false;
            ReadyText.text = "FIGHT";
            if (timer <= -2)
            {
                ReadyText.enabled = false;
                gameStarted = true;
            }
        }
        else
        {
            DisplayWinner(winner);
        }
    }

    public void StartTimer()
    {
        timer = 4.0f;
    }

    public void NextRound()
    {
        Debug.Log("Started next round...");

        roundNum++;
        ReadyText.enabled = true;
        ReadyText.text = "Round " + roundNum + " Ready";
        StartTimer();

        Destroy(P1);
        Destroy(P2);

        SetRound();
        Debug.Log("P1: " + P1 + "\n" + "P2: " + P2);
    }

    public void SetRound()
    {
        StartTimer();

        int charNumP1 = CharacterSelectScript.charSelectP1;
        int charNumP2 = CharacterSelectScript.charSelectP2;
        Debug.Log("charNumP1: " + charNumP1 + "\n" + "charNumP2: " + charNumP2);

        switch (charNumP1)
        {
            case (1): // player1 is lion
                P1 = Instantiate(char1, new Vector2(-5, 0), Quaternion.identity);
                P1.layer = LayerMask.NameToLayer("Enemy");
                // add the animation components I want added here

                //P1.GetComponent<PlayerMovement>().Flip();
                break;

            case (2): // player 1 is sahan
                P1 = Instantiate(char2, new Vector2(-5, 0), Quaternion.identity);
                P1.layer = LayerMask.NameToLayer("Enemy");
                //P1.GetComponent<PlayerMovement>().Flip();
                break;

            default:
                break;
        }

        switch (charNumP2)
        {
            case (1): // player2 is lion
                P2 = Instantiate(char1, new Vector2(5, 0), Quaternion.identity);
                P2.layer = LayerMask.NameToLayer("Enemy");
                P2.GetComponent<PlayerMovement>().Flip();
                break;

            case (2): // player2 is sahan
                P2 = Instantiate(char2, new Vector2(5, 0), Quaternion.identity);
                P2.layer = LayerMask.NameToLayer("Enemy");
                P2.GetComponent<PlayerMovement>().Flip();
                break;

            default:
                break;
        }
    }

    public void EndGame(GameObject loser) // huge amount of if/else statements, maybe look into cleaning this up but I don't care anymore, problem for "later me" to fix
    {
        if (loser.Equals(P1) && gameEnded == false)
        {
            Debug.Log("added P2");
            P2points++;
        }
        else if (loser.Equals(P2) && gameEnded == false)
        {
            Debug.Log("added P1");
            P1points++;
        }
        Debug.Log("P1points: " + P1points + "\n" + "P2points: " + P2points);

        if (P1points != 2 && P2points != 2)
        {
            Debug.Log("next round");
            NextRound();
        }
        else if (P1points >= 2)
        {
            Debug.Log("Player 1 Wins!!");
            gameStarted = false;
            winner = 1;
            DisplayWinner(winner);
        }
        else if (P2points >= 2)
        {
            Debug.Log("Player 2 Wins!!");
            gameStarted = false;
            winner = 2;
            DisplayWinner(winner);
        }
    }

    private void DisplayWinner(int winner)
    {
        gameEnded = true;
        gameStarted = false;

        switch (winner)
        {
            case (1):
                ReadyText.enabled = true;
                ReadyText.text = "PLAYER 1 WINS";
                break;
            case (2):
                ReadyText.enabled = true;
                ReadyText.text = "PLAYER 2 WINS";
                break;
        }


    }
}
