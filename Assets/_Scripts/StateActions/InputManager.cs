using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace R2
{
    public class InputManager : StateAction
    {
        PlayerStateManager currentState;


     //triggers
        bool Rb;
        bool Rt;
        bool Lb;
        bool Lt;

        bool isAttacking;

     //inventory
        bool inventoryInput;

     //prompts
        bool b_Input;
        bool y_Input;
        bool x_Input;

     //dpad
        bool leftArrow;
        bool rightArrow;
        bool upArrow;
        bool downArrow;

        public InputManager(PlayerStateManager states)
        {
            currentState = states;
        }

        public override bool Execute()
        {
            bool retVal = false;

            currentState.horizontal = Input.GetAxis("Horizontal");
            currentState.vertical = Input.GetAxis("Vertical");
            Rb = Input.GetButton("RB");
            Rt = Input.GetButton("RT");
            Lb = Input.GetButton("LB");
            Lt = Input.GetButton("LT");
            inventoryInput = Input.GetButton("Inventory");
            b_Input = Input.GetButton("B");
            y_Input = Input.GetButtonDown("Y");
            x_Input = Input.GetButton("X");
            leftArrow = Input.GetButton("Left");
            rightArrow = Input.GetButton("Right");
            upArrow = Input.GetButton("Up");
            downArrow = Input.GetButton("Down");
            currentState.mouseX = Input.GetAxis("Mouse X");
            currentState.mouseY = Input.GetAxis("Mouse Y");

            currentState.moveAmount = Mathf.Clamp01(Mathf.Abs(currentState.horizontal) + Mathf.Abs(currentState.vertical));

            retVal = HandleAttacking();

            return retVal;
        }

        bool HandleAttacking()
        {
            if (Rb || Rt || Lb || Lt)
                isAttacking = true;

            if (y_Input)
                isAttacking = false;

            //Logic that interrupts attack
            if (isAttacking)
            {
                //find acttack aninmation from items
                //play animation
                //currentState.PlayTargetanimation("");
                //currentState.ChangeState(currentState.attackStateId);

                
            }

            return isAttacking;
        }
    }
}

