using James.InsertCoinGame.Ingame.InputModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace James.InsertCoinGame.Ingame.PlayerModule
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private CharacterController controller;
        [SerializeField]
        private bool canWalk;

        [SerializeField]
        private UnityEvent startedMoving;
        [SerializeField]
        private UnityEvent stoppedMoving;


        private InsertCoinInput input;
        private PlayerConfigs configs;
        private bool isMoving;


        public Vector3 LookDirection { get; set; }
        public bool DirectionLocked { get; set; }
        public bool IsMoving
        {
            get
            {
                return isMoving;
            }
            set
            {
                if (value == isMoving)
                    return;

                isMoving = value;

                if (value)
                    startedMoving.Invoke();
                else
                    stoppedMoving.Invoke();
            }
        }

        [Inject]
        public void Inject(InsertCoinInput input, PlayerConfigs configs)
        {
            this.input = input;
            this.configs = configs;
        }

        public void DisableWalk()
        {
            canWalk = false;
        }
        public void EnableWalk()
        {
            canWalk = true;
        }
        private void Update()
        {
            var moveDirection = input.MovementInIso;
            if (!DirectionLocked)
            {
                LookDirection = moveDirection.magnitude > .3f ? moveDirection.normalized : LookDirection;
            }
            moveDirection.Normalize();
            Vector3 speed;
            if (canWalk)
            {
                speed = moveDirection * configs.MoveSpeed;
            } else
            {
                speed = Vector3.zero;
            }

            IsMoving = moveDirection.sqrMagnitude > .01f;
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, LookDirection);

            controller.SimpleMove(speed);
        }
    }
}
