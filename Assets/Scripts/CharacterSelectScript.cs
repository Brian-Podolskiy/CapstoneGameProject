/* Script for Character Selection Screen
 * Players click button, check which player click button
 * Select character for player based on which one clicked button
 * Load main game scene with player selections when both click "ready" button
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class CharacterSelectScript : MonoBehaviour
{
    public static PlayerInput pi;
    public static ArrayList PlayerList = new ArrayList();
    public GameObject player1;
    public GameObject player2;
    [SerializeField] public static int charSelectP1;
    [SerializeField] public static int charSelectP2;
    public int charp1; // not static, initializable
    public int charp2; // not static initializable
    public bool P1Ready;
    public bool P2Ready;
    int playernum = 0;

    public static PlayerInput LastActivePlayer;

    public Button button1;
    public Button button2;

    private void Start()
    {
        playernum = 0;
        button1.onClick.AddListener(OnOption1);
        button2.onClick.AddListener(OnOption2);
    }

    public void PlayerJoin(GameObject a) // adds reference to player that joined to a list
    {
        PlayerList.Add(a);
        Debug.Log("Info of Joined Player: " + a);
        playernum++;

        for (int i = 0; i < PlayerList.Count; i++)
        {
            GameObject player = (GameObject)PlayerList[i];
            PlayerInput input = player.GetComponent<PlayerInput>();

            if (input != null && input.playerIndex != -1) // fix issue where empty prefab would be registered as a player
            {
                Debug.Log(player.name + " - PlayerIndex: " + input.playerIndex);
                Debug.Log(player.name + " - ControlScheme: " + input.currentControlScheme);
                Debug.Log(player.name + " - Devices: " + string.Join(", ", input.devices));
            }
            else if (input.playerIndex == -1)
            {
                PlayerList.RemoveAt(i);
            }

            RegisterPlayer(PlayerList);
        }
    }

    public void RegisterPlayer(ArrayList list) // assigns Player 1/2
    {
        switch (list.Count)
        {
            case (1):
                player1 = (GameObject)PlayerList[0]; 
                break;

            case (2):
                player2 = (GameObject)PlayerList[1];
                break;

            default:
                break;
        }
    }

    public void OnOption1()
    {
        if (LastActivePlayer != null)
        {
            Debug.Log("Button clicked by Player: " + LastActivePlayer.playerIndex);
            if (LastActivePlayer.playerIndex == 0)
            {
                charSelectP1 = 1;
                charp1 = 1;
            }
            else
            {
                charSelectP2 = 1;
                charp2 = 1;
            }
        }
        else
        {
            Debug.Log("no player found");
        }
    }

    public void OnOption2()
    {
        if (LastActivePlayer != null)
        {
            Debug.Log("Button clicked by Player: " + LastActivePlayer.playerIndex); 
            if (LastActivePlayer.playerIndex == 0)
            {
                charSelectP1 = 2;
                charp1 = 2;
            }
            else
            {
                charSelectP2 = 2;
                charp2 = 2;
            }
        }
        else
        {
            Debug.Log("no player found");
        }
    }

    public void OnP1Ready()
    {
        if (charSelectP1 != 0 && P1Ready == false)
        {
            P1Ready = true;
        }
        else
        {
            P1Ready = false;
        }
    }
    public void OnP2Ready()
    {
        if (charSelectP2 != 0 && P2Ready == false)
        {
            P2Ready = true;
        }
        else
        {
            P2Ready = false;
        }
    }



    public void OnBackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Update()
    {
        // Debug.Log("P1: " + charSelectP1 + "\n" + "P2: " + charSelectP2);
        if (P1Ready && P2Ready)
        {
            SceneManager.LoadScene(2);
        }
    }
}
