using UnityEngine;

namespace Assets.Scripts
{
    public class FastProjectile : MonoBehaviour
    {
        public Vector3 MovementDirection;
        public float MovementSpeed;
        public float SphereRadius = 67;
        private Rigidbody rigidbody;
        public int DamageOnHit = 1;

        public void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Update()
        {
            transform.position = transform.position.normalized * SphereRadius;

            var pos = transform.position;

            MovementDirection = Vector3.Cross(Vector3.Cross(pos, MovementDirection), pos).normalized;

            rigidbody.velocity = MovementDirection * MovementSpeed;
            transform.right = MovementDirection;
        }

        public void OnCollisionEnter(Collision collision)
        {
            var enemy = EnemiesHelper.GetEnemyForCollider(collision.collider);
            if (enemy == null) return;
            enemy.TakeDamage(DamageOnHit);
            Destroy(gameObject);
        }
    }
}