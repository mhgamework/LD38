﻿using UnityEngine;

namespace Assets.Scripts
{
    public class BomberEnemy : AEnemy
    {
        public Animator animator;
        private Rigidbody body;
        public GameObject ProjectileTemplate;
        public float MovementSpeed = 1;

        public float RotationSpeed = 90;
        private Vector3 lookDir;
        private Vector3 toTarget;
        private PlanetCamera planetCamera;

        protected override void Start()
        {
            base.Start();

            body = GetComponent<Rigidbody>();
            transform.position = transform.position.normalized * PlanetConfig.Instance.WalkSphereRadius;
            planetCamera = PlanetCamera.Instance;
            lookDir = transform.forward;
        }

        public void Update()
        {
            Debug.DrawRay(transform.position, lookDir, Color.blue);
            //Debug.DrawLine(transform.position, transform.position + body.velocity,Color.green);

            var info = animator.GetCurrentAnimatorStateInfo(0);

            var isWalking = info.IsTag("walk");
            var isAttacking = info.IsTag("attack");

            var diff = planetCamera.PlayerPosition - transform.position;
            var right = Vector3.Cross(transform.position.normalized, diff.normalized).normalized;
            toTarget = Vector3.Cross(right, transform.position.normalized).normalized;

            if (isWalking)
                transform.rotation =
                Quaternion.RotateTowards(transform.rotation,
                Quaternion.LookRotation(toTarget, transform.position.normalized), Time.deltaTime * RotationSpeed);

            animator.SetBool("walking", Vector3.Dot(toTarget, transform.forward) < 0.999f);




            //Debug.DrawLine(transform.position, transform.position + right, Color.red);
            //Debug.DrawLine(transform.position, transform.position + toTarget, Color.blue);
            //body.velocity = toTarget * MovementSpeed;
            //body.position = body.position.normalized * PlanetConfig.Instance.WalkSphereRadius;

            ////transform.forward = body.velocity;

            ////transform.up = transform.position.normalized;

            //transform.LookAt(body.position + lookDir, transform.position.normalized);

        }

        
        public void Fire()
        {
            var t = Instantiate(ProjectileTemplate);
            var g = t.transform.GetComponentInChildren<GrenadeProjectile>();
            g.InitialDirection = toTarget;
            t.SetActive(true);
            t.transform.up = transform.position.normalized;
            t.transform.position += transform.position.normalized * 3;
        }
    }
}