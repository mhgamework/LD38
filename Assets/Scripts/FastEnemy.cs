using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class FastEnemy : AEnemy
    {
        private Rigidbody body;
        public float MovementSpeed = 1;

        private PlanetCamera planetCamera;

        public float StrikeDamage = 1;
        public float StrikeDistanceToStart = 0.5f;
        public float StrikeRange = 1.5f;
        public float StrikeChargeDuration = 1;
        public float StrikeInterval = 2;
        private Vector3 toTarget;

        public GameObject OnDeathAnim;

        public void Start()
        {
            body = GetComponent<Rigidbody>();
            transform.position = transform.position.normalized * PlanetConfig.Instance.WalkSphereRadius;
            planetCamera = PlanetCamera.Instance;

            StartCoroutine(begin().GetEnumerator());
        }

        public void Update()
        {
            var diff = planetCamera.PlayerPosition - transform.position;

            var right = Vector3.Cross(transform.position.normalized, diff.normalized).normalized;

            toTarget = Vector3.Cross(right, transform.position.normalized).normalized;
            Debug.DrawLine(transform.position, transform.position + right, Color.red);
            Debug.DrawLine(transform.position, transform.position + toTarget, Color.blue);

            transform.LookAt(body.position + body.velocity, transform.position.normalized);

        }

        public IEnumerable<YieldInstruction> begin()
        {
            for (;;)
            {
                Debug.Log("Walking");

                while (getDistToPlayer() > StrikeDistanceToStart)
                {
                    Debug.DrawLine(transform.position, transform.position + body.velocity, Color.green);

                    body.velocity = toTarget * MovementSpeed;
                    body.position = body.position.normalized * PlanetConfig.Instance.WalkSphereRadius;
                    yield return null;

                }
                body.velocity = new Vector3();
                yield return null;

                var time = 0f;
                Debug.Log("Charge");

                while (time < StrikeChargeDuration)
                {

                    time += Time.deltaTime;
                    yield return null;
                }

                if (getDistToPlayer() < StrikeRange)
                {
                    Debug.Log("Strike");

                    // Strike!
                    PlayerHealthScript.Instance.TakeDamage(StrikeDamage);
                }
                Debug.Log("tired");

                while (time < StrikeInterval)
                {

                    time += Time.deltaTime;
                    yield return null;
                }

                yield return null;
            }


        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, StrikeRange);
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, StrikeDistanceToStart);

        }

        private float getDistToPlayer()
        {
            return (planetCamera.PlayerPosition - transform.position).magnitude;
        }

        public void TakeDamage(float amount)
        {
            Health -= amount;
            if (Health <= 0)
            {
                Destroy(gameObject);
                var inst = Instantiate(OnDeathAnim);
                inst.gameObject.SetActive(true);
                inst.transform.position = transform.position;
                inst.transform.up = OnDeathAnim.transform.position.normalized;
            }
        }
    }
}