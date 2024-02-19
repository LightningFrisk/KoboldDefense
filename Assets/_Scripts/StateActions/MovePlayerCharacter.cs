using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace R2
{
    public class MovePlayerCharacter : StateAction
    {
        PlayerStateManager states;

        public MovePlayerCharacter(PlayerStateManager playerStateManager)
        {
            states = playerStateManager;
        }

        void HandleRotation(){
            float horizontal = states.horizontal;
            float vertical = states.vertical;

            Vector3 targetDir = Camera.value.forward * vertical;
            targetDir += Camera.right * horizontal;
            targetDir.Normalize();

            targetDir.y=0;
            if(targetDir == Vector3.zero)
                targetDir = states.transform.forward;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(
                states.mTransform.rotation, tr,
                states.delta * states.moveAmount * states.rotationSpeed);

            states.mTransform.rotation = targetRotation;
        }

        void HandleAnimations(){
            if (states.isGrounded){
                float amount = states.moveAmount;
                float forward = 0;
                if (amount > 0 && amount < .5f)
                    forward = .5f;
                else if (amount > .5f)
                    forward = 1;

                states.anim.SetFloat("forward", forward, .2f, states.delta);
            }
        }
        public override bool Execute()
        {
            float frontY = 0;
            RaycastHit hit;
            Vector3 origin = states.mTransform.position + (states.mTransform.forward * states.frontRayOffset);
            origin.y += .5f;
            Debug.DrawRay(origin, -Vector3.up, Color.red, 0.1f, false);
            if (Physics.Raycast(origin, -Vector3.up, out hit, 1, states.ignoreForGroundCheck)){
                float y = hit.point.y;
                frontY = y - states.mTransform.position.y;
            }

            Vector3 currentVelocity = states.rigidbody.velocity;
            Vector3 targetVelocity = states.mTransform.forward * states.moveAmount * states.movementSpeed;

            // if(states.isLockingOn)
            // {
            //     targetVelocity = states.rotateDirection * states.moveAmount * movementSpeed;
            // }

            if (states.isGrounded)
            {
                float moveAmount = states.moveAmount;

                if (moveAmount > 0.1f){
                    states.rigidbody.isKinematic = false;
                    states.rigidbody.drag = 0;
                    if(Mathf.Abs(frontY) > 0.02f){
                        targetVelocity.y = ((frontY > 0) ? frontY + 0.2f : frontY - 0.2f) * states.movementSpeed;
                    }
                }
                else
                {
                    float abs = Mathf.Abs(frontY);

                    if (abs > 0.02f)
                    {
                        states.rigidbody.isKinematic = true;
                        targetVelocity.y = 0;
                        states.rigidbody.drag = 4;
                    }
                }
                HandleRotation();
            } else {
                //states.collider.height=colStartHeight;
                states.rigidbody.isKinematic = false;
                states.rigidbody.drag = 0;
                targetVelocity.y = currentVelocity.y;
            }

            HandleAnimations();

            Debug.DrawRay((states.mTransform.position + Vector3.up * .2f), targetVelocity, Color.green, .01f, false);

            states.rigidbody.velocity = targetVelocity;

            //states.rigidbody.velocity = Vector3.Lerp(currentVelocity, targetVelocity, states.delta * states.adaptSpeed);

            return false;
        }
    }
}

