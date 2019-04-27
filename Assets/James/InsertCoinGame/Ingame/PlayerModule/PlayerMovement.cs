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
        private InsertCoinInput input;
        private PlayerConfigs configs;

        [Inject]
        public void Inject(InsertCoinInput input, PlayerConfigs configs)
        {
            this.input = input;
            this.configs = configs;
        }


        private void Update()
        {
            transform.position += input.MovementInIso * Time.deltaTime * configs.MoveSpeed;
        }
    }
}
