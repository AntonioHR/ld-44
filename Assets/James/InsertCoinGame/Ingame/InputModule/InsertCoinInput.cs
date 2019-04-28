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

        public bool PressedStart { get { return Input.anyKeyDown; } }

        public float VertInput { get { return configs.useRawAxis ? Input.GetAxisRaw("Vertical") : Input.GetAxis("Vertical"); } }
        public float HorInput { get { return configs.useRawAxis ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal"); } }

        public Vector3 MovementSimple { get { return new Vector3(HorInput, 0, VertInput); } }
        public Vector3 MovementInIso { get { return Quaternion.AngleAxis(45, Vector3.up) * MovementSimple; } }

        public bool KickDown { get { return Input.GetKeyDown(configs.KickKey); } }
        public bool KickUp { get { return Input.GetKeyUp(configs.KickKey); } }
    }
}
