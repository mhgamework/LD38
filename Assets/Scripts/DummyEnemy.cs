using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class DummyEnemy : FastEnemy
    {
        [Serializable]
        public class KilledEventHandler : UnityEvent { }
        public KilledEventHandler OnKilled = new KilledEventHandler();

        [SerializeField]
        private eDamageType damageWeakness = eDamageType.UNSPECIFIED;

        public override void TakeDamage(float amount, eDamageType type)
        {
            if (type == damageWeakness)
                base.TakeDamage(amount, type);

            if (Health <= 0f)
                OnKilled.Invoke();
        }
    }
}