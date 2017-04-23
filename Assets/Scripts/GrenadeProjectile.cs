﻿using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class GrenadeProjectile : MonoBehaviour
    {
        public float InitialSpeed = 10;
        public Vector3 InitialDirection;
        public float InitialAngle = 45;
        public float SphereRadius = 67;

        public float ExplosionRadius = 10;
        public float ExplosionDamage = 5;
        public float ExplosionForce = 10;


        private Rigidbody rigidbody;

        public void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

            var right = Vector3.Cross(transform.position, InitialDirection).normalized;

            // Flatten to sphere
            InitialDirection = Vector3.Cross(right, transform.position).normalized;

            InitialDirection = Quaternion.AngleAxis(-InitialAngle, right) * InitialDirection;

            Debug.DrawLine(transform.position, transform.position + InitialDirection, Color.red, 10);

            rigidbody.velocity = InitialDirection * InitialSpeed;
        }

        public void OnCollisionEnter(Collision collision)
        {
            var inside = Physics.OverlapSphere(transform.position, ExplosionRadius);

            var enemies = inside.Select(f => EnemiesHelper.GetEnemyForCollider(f)).Where(item => item != null);
            foreach (var e in enemies)
            {
                e.TakeDamage(ExplosionDamage);
                ((MonoBehaviour) e).GetComponent<Rigidbody>()
                    .AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);
            }

            Destroy(gameObject);
        }
    }
}