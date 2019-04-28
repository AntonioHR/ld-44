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
            Vector3 speed;
            if (canWalk)
            {
                speed = input.MovementInIso.normalized * configs.MoveSpeed;
            } else
            {
                speed = Vector3.zero;
            }

            transform.rotation = Quaternion.FromToRotation(Vector3.forward, input.MovementInIso);

            controller.SimpleMove(speed);
        }
    }
}
