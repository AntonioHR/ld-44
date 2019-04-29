using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.TonhoHR
{
    class RandomSpawn : MonoBehaviour
    {
        public GameObject[] objs;
        public void Spawn()
        {
            int index = UnityEngine.Random.Range(0, objs.Length);
            Instantiate(objs[index]);
        }
    }
}
