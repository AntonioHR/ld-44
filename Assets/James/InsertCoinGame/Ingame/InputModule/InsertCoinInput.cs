using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace James.InsertCoinGame.Ingame.InputModule
{
    public class InsertCoinInput
    {
        private InputConfigs configs;

        public InsertCoinInput(InputConfigs configs)
        {
            this.configs = configs;
        }

        public bool PressedStart { get { return Input.anyKeyDown;} }
    }
}
