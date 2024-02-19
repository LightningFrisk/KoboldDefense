using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace R2
{
    public class PlayerStateManager : CharacterStateManager
    {
        [Header("Inputs")]
        public float mouseX;
        public float mouseY;
        public float moveAmount;
        public Vector3 rotateDirection;

        [Header("States")]
        public bool isGrounded;

        [Header("References")]
        public new Transform camera;

        [Header("Movement Stats")]
        public float frontRayOffset = .5f;
        public float movementSpeed = 2;
        public float adaptSpeed = 10;
        public float rotationSpeed = 10;

        [HideInInspector]
        public LayerMask ignoreForGroundCheck;
        [HideInInspector]
        public const string locomotionId = "locomotion";
        [HideInInspector]
        public const string attackStateId = "attackState";

        public override void Init()
        {
            base.Init();

            State locomotion = new State(
                new List<StateAction>() //Fixed Update
                {
                    
                    new MovePlayerCharacter(this)
                },
                new List<StateAction>() //Update
                {
                    new InputManager(this),
                },
                new List<StateAction>() //Late Update
                {
                }
                );
            State attackState = new State(
                new List<StateAction>() //Fixed Update
                {
                },
                new List<StateAction>() //Update
                {
                },
                new List<StateAction>() //Late Update
                {
                }
                );

            RegisterState(locomotionId, locomotion);
            RegisterState(attackStateId, attackState);

            ChangeState(locomotionId);

            ignoreForGroundCheck = ~(1<<9 | 1 << 10);
        }

        private void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;

            base.FixedTick();
        }

        private void Update()
        {
            delta = Time.deltaTime;
            base.Tick();
        }

        private void LateUpdate()
        {
            base.LateTick();
        }
        //allows actions on all cases

    }
}