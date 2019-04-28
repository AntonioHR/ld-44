using James.InsertCoinGame.Ingame.Coins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace James.InsertCoinGame.Ingame.Spawners
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField]
        private Coin prefab;
        [SerializeField]
        private CoinSpawnerSetup configs;
        [SerializeField]
        private Transform coinsParent;
        [SerializeField]
        private Transform spawnPoint;
        [SerializeField]
        private UnityEvent OnSpawn;

        private float nextSpawn;

        private void OnEnable()
        {
            nextSpawn = Time.time;
        }
        public void Update()
        {
            if(Time.time >= nextSpawn)
            {
                Spawn();
                OnSpawn.Invoke();
                nextSpawn += configs.interval;
            }
        }

        private void Spawn()
        {
            Vector3 impulseDirection = GenerateImpulseDirection();

            Coin c = Instantiate(prefab, coinsParent != null? coinsParent: transform.parent);
            c.transform.position = spawnPoint.position;
            c.KickAbsoulte(impulseDirection * configs.startingKick);
        }

        private Vector3 GenerateImpulseDirection()
        {
            Vector3 impulse = spawnPoint.TransformDirection(configs.spawnVector);
            var axis = new Vector3(0, 1, 0);
            if (Vector3.Cross(axis, impulse).magnitude == 0)
                axis = new Vector3(1, 0, 0);
            Vector3 rotation = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), axis) * impulse;
            impulse = Quaternion.AngleAxis(UnityEngine.Random.Range(0, configs.arc), rotation) * impulse;
            return impulse;
        }
    }
}
