using UnityEngine;

namespace Assets.Scripts
{
    public class PlanetGravity : MonoBehaviour
    {
        private Rigidbody rigidBody;
        private PlanetConfig config;
        public void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            config = PlanetConfig.Instance;
        }

        public void FixedUpdate()
        {
            rigidBody.AddForce(-transform.position.normalized * config.GravitationalStrength, ForceMode.Acceleration);

        }
    }
}