using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace James.InsertCoinGame.Ingame.InputModule
{
    [CreateAssetMenu(fileName = "PlayerConfigs", menuName = "InsertCoin/PlayerConfigs")]
    public class PlayerConfigs : ScriptableObject
    {
        public float MoveSpeed = 5;
        public float KickLoadTime = .5f;
        public float KickMin = .3f;
        public AnimationCurve kickCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    }
}
