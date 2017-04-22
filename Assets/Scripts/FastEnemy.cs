using UnityEngine;

namespace Assets.Scripts
{
    public class FastEnemy : MonoBehaviour
    {
        public Transform Player;
        private Rigidbody body;
        public float MovementSpeed = 1;

        public void Start()
        {
            body = GetComponent<Rigidbody>();
            transform.position += Vector3.up * (PlanetConfig.Instance.WalkSphereRadius - transform.position.y);
        }

        public void Update()
        {

            var diff = Player.position - transform.position;

            body.velocity = diff.normalized * MovementSpeed;

            body.position = body.position.normalized * PlanetConfig.Instance.WalkSphereRadius;

            //transform.forward = body.velocity;

            //transform.up = transform.position.normalized;

            transform.LookAt(body.position + body.velocity, transform.position.normalized);

        }
    }
}