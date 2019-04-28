using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace James.InsertCoinGame.Ingame
{
    public class DisableUntilGameStarts : MonoBehaviour
    {
        [Inject]
        private InsertCoinIngameScene scene;

        [SerializeField]
        private MonoBehaviour[] components;
        private void Start()
        {
            if (!scene.HasGameStarted)
            {
                foreach (var component in components)
                {
                    component.enabled = false;
                }

                scene.GameStarted += Scene_GameStarted;
            }
        }

        private void Scene_GameStarted()
        {
            foreach (var c in components)
            {
                c.enabled = true;
            }
        }
    }
}
