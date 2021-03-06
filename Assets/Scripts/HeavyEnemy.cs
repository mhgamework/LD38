﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class HeavyEnemy : AEnemy
    {
        public Animator animator;
        private Rigidbody body;
        public float MovementSpeed = 1;

        public float RotationSpeed = 90;
        
        public float JumpInterval = 2;

        private Vector3 toTarget;
        private bool airbourne = false;
        private PlanetCamera planetCamera;

        public float StrikeRange = 5f;
        public float StrikeDamage = 3f;
        public float StrikeDistanceToStart = 2f;

        public GameObject LandEffect;

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, StrikeRange);
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, StrikeDistanceToStart);

        }

        protected override void Start()
        {
            base.Start();

            planetCamera = PlanetCamera.Instance;
            body = GetComponent<Rigidbody>();
            transform.position = transform.position.normalized * PlanetConfig.Instance.WalkSphereRadius;
            StartCoroutine(begin().GetEnumerator());
        }

        private IEnumerable<YieldInstruction> begin()
        {
            for (;;)
            {
                while (animator.GetCurrentAnimatorStateInfo(0).IsTag("walk")) yield return new WaitForSeconds(0);
                airbourne = false;
                animator.SetTrigger("jump");

                while (!airbourne) yield return new WaitForSeconds(0);
                do
                {
                    Debug.DrawLine(transform.position, transform.position + body.velocity, Color.green);

                    var diff = planetCamera.PlayerPosition - transform.position;

                    var right = Vector3.Cross(transform.position.normalized, diff.normalized).normalized;

                    var toTarget = Vector3.Cross(right, transform.position.normalized).normalized;
                    Debug.DrawRay(transform.position, right, Color.red);
                    Debug.DrawRay(transform.position, toTarget, Color.blue);
                    body.velocity = toTarget * MovementSpeed;
                    body.position = body.position.normalized * PlanetConfig.Instance.WalkSphereRadius;








                    //var diff = Player.position - transform.position;
                    //var right = Vector3.Cross(transform.position.normalized, diff.normalized).normalized;
                    //toTarget = Vector3.Cross(right, transform.position.normalized).normalized;

                    transform.rotation =
                    Quaternion.RotateTowards(transform.rotation,
                    Quaternion.LookRotation(toTarget, transform.position.normalized), Time.deltaTime * RotationSpeed);


                    //transform.forward = body.velocity;

                    //transform.up = transform.position.normalized;

                    //transform.LookAt(body.position + body.velocity, transform.position.normalized);

                    yield return new WaitForSeconds(0);

                } while (animator.GetCurrentAnimatorStateInfo(0).IsTag("walk"));
                body.velocity = new Vector3();
                var eff = Instantiate(LandEffect, transform);
                eff.gameObject.SetActive(true);
                // Strike!
                if ((planetCamera.PlayerPosition - transform.position).magnitude < StrikeRange)
                    PlayerHealthScript.Instance.TakeDamage(StrikeDamage);

                yield return new WaitForSeconds(JumpInterval);

            }
        }

        public void Update()
        {

            var right = Vector3.Cross(transform.position.normalized, transform.forward.normalized).normalized;

            var forward = Vector3.Cross(right, transform.position.normalized).normalized;

            transform.LookAt(transform.position + forward, transform.position.normalized);
            //Debug.DrawRay(transform.position, lookDir, Color.blue);
            ////Debug.DrawLine(transform.position, transform.position + body.velocity,Color.green);

            //var info = animator.GetCurrentAnimatorStateInfo(0);

            //var isWalking = info.IsTag("walk");

            //var diff = Player.position - transform.position;
            //var right = Vector3.Cross(transform.position.normalized, diff.normalized).normalized;
            //toTarget = Vector3.Cross(right, transform.position.normalized).normalized;

            //if (isWalking)
            //    transform.rotation =
            //    Quaternion.RotateTowards(transform.rotation,
            //    Quaternion.LookRotation(toTarget, transform.position.normalized), Time.deltaTime * RotationSpeed);

            //animator.SetBool("walking", Vector3.Dot(toTarget, transform.forward) < 0.999f);




            ////Debug.DrawLine(transform.position, transform.position + right, Color.red);
            ////Debug.DrawLine(transform.position, transform.position + toTarget, Color.blue);
            ////body.velocity = toTarget * MovementSpeed;
            ////body.position = body.position.normalized * PlanetConfig.Instance.WalkSphereRadius;

            //////transform.forward = body.velocity;

            //////transform.up = transform.position.normalized;

            ////transform.LookAt(body.position + lookDir, transform.position.normalized);

        }

        //public void Fire()
        //{
        //    var t = Instantiate(ProjectileTemplate);
        //    var g = t.transform.GetComponentInChildren<GrenadeProjectile>();
        //    g.InitialDirection = toTarget;
        //    t.SetActive(true);
        //    t.transform.up = transform.position.normalized;
        //    t.transform.position += transform.position.normalized * 3;
        //}
        public void MarkAirbourne()
        {
            airbourne = true;

        }
    }
}