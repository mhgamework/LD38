using UnityEngine;

namespace Assets.Scripts
{
    public class DummyEnemy : MonoBehaviour, IEnemy
    {
        public void TakeDamage(float amount)
        {
            Debug.Log("AUCH! " + amount);
        }

    }
}