using James.InsertCoinGame.Ingame.InputModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace James.InsertCoinGame.Ingame.PlayerModule
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private CharacterController controller;
        [SerializeField]
        private bool canWalk;

        private InsertCoinInput input;
        private PlayerConfigs configs;

        public Vector3 Direction { get; set; }
        public bool DirectionLocked { get; set; }

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
            if(!DirectionLocked)
                Direction = input.MovementInIso.normalized;
            Vector3 speed;
            if (canWalk)
            {
                speed = Direction * configs.MoveSpeed;
            } else
            {
                speed = Vector3.zero;
            }
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, Direction);

            controller.SimpleMove(speed);
        }
    }
}
