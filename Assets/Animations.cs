using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class Animations : StateMachineBehaviour
{
    private Animator Animator1;

    public void Animation(string trigger, InputAction.CallbackContext context) { // whoever does the playeraction (player1 or 2) goes into parameter
        CharacterSelectScript x = new CharacterSelectScript();
        Animator1.SetInteger("Char", 0);
        Animator1.SetTrigger(trigger);

        if (context.control.device is Keyboard)
        { // player1 is doing action
            switch (x.charp1) {
                case 1:
                    Animator1.SetInteger("Char", 1);
                    break;
                case 2:
                    Animator1.SetInteger("Char", 2);
                    break;

            }
        }
        else { //player2
            switch (x.charp2) {
                case 1:
                    Animator1.SetInteger("Char", 1);
                    break;
                case 2:
                    Animator1.SetInteger("char", 2);
                    break;
            }
        }
        
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
