using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.TonhoHR
{
    class SimpleSpawn : MonoBehaviour
    {
        public GameObject obj;
        public void Spawn()
        {
            Instantiate(obj);
        }
    }
}
