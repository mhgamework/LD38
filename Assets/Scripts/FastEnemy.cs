using UnityEngine;

namespace Assets.Scripts
{
    public class FastEnemy : MonoBehaviour, IEnemy
    {
        private Rigidbody body;
        public float MovementSpeed = 1;

        public float Health = 3;
        private PlanetCamera planetCamera;

        public void Start()
        {
            body = GetComponent<Rigidbody>();
            transform.position = transform.position.normalized * PlanetConfig.Instance.WalkSphereRadius;
            planetCamera = PlanetCamera.Instance;

        }

        public void Update()
        {
            
            Debug.DrawLine(transform.position, transform.position + body.velocity,Color.green);

            var diff = planetCamera.PlayerPosition - transform.position;

            var right = Vector3.Cross(transform.position.normalized, diff.normalized).normalized;
          
            var toTarget = Vector3.Cross(right, transform.position.normalized).normalized;
            Debug.DrawLine(transform.position, transform.position + right, Color.red);
            Debug.DrawLine(transform.position, transform.position + toTarget, Color.blue);
            body.velocity = toTarget * MovementSpeed;
            body.position = body.position.normalized * PlanetConfig.Instance.WalkSphereRadius;

            //transform.forward = body.velocity;

            //transform.up = transform.position.normalized;

            transform.LookAt(body.position + body.velocity, transform.position.normalized);

        }

        public void TakeDamage(float amount)
        {
            Health -= amount;
            if (Health <= 0) Destroy(gameObject);
        }
    }
}