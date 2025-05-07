/* Track last input by a player
 * Works with tracking KB/M and controller
 * use WASD on KB for Navigate, Enter for Submit
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LastInputTracker : MonoBehaviour
{
    private PlayerInput playerInput; // input shared with main Canvas script

    private void Awake()
    {
        Debug.Log("Awake");
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        //Debug.Log("called");
        if (context.performed)
        {
            CharacterSelectScript.LastActivePlayer = playerInput;
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        // Debug.Log("called2");
        if (context.performed)
        {
            CharacterSelectScript.LastActivePlayer = playerInput;
        }
    }
}
