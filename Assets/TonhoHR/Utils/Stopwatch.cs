using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AntonioHR.Utils
{
    public class Stopwatch
    {
        private float startTime;

        private Stopwatch() { }

        public float ElapsedSeconds { get { return Time.time - startTime; } }

        private void Start()
        {
            Reset();
        }

        public void Reset()
        {
            startTime = Time.time;
        }




        public static Stopwatch CreateAndStart()
        {
            var result = new Stopwatch();
            result.Start();
            return result;
        }
    }
}