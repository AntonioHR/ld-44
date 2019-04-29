using James.InsertCoinGame.Ingame.Coins;
using System.Collections;
using System.Collections.Generic;
using TonhoHR.ObjectCheckers;
using UnityEngine;
using UnityEngine.Events;

namespace James.InsertCoinGame.Ingame
{
    public class CoinKillerWell : MonoBehaviour
    {
        public TriggerNotifier notifier;

        private CheckForObjects<Coin> coinCheck;

        public UnityEvent OnCoin;
        private float lastCoinTime;
        public float minSoundDelay =.1f;
    void Start()
        {
            coinCheck = new CheckForObjects<Coin>(notifier);
            coinCheck.ObjectEntered += CoinCheck_ObjectEntered;
        }

        private void CoinCheck_ObjectEntered(Coin obj)
        {
            obj.OnHitWell();

            if(Time.time - lastCoinTime > minSoundDelay)
            {
                OnCoin.Invoke();
                lastCoinTime = Time.time;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}