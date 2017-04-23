using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace Assets.Scripts
{
    public class BeamProjectile : MonoBehaviour
    {
        public Vector3 MovementDirection;
        public float MovementSpeed;
        public float SphereRadius = 67;
        private Rigidbody rigidbody;

        public float DamageMin = 0.1f;
        public float DamageMax = 10f;
        public float DamageIncreaseSpeedMultiplier = 0.1f;


        public float CurrentDamage;
        public IEnemy CurrentTarget;

        public void Start()
        {
            //rigidbody = GetComponent<Rigidbody>();
            //StartCoroutine(begin().GetEnumerator());
        }

        //private IEnumerable<YieldInstruction> begin()
        //{
        //    var pos = transform.position;
        //    var dir = MovementDirection;
        //    for (;;)
        //    {
        //        for (int i = 0; i < 30; i++)
        //        {
        //            var length = 5;

        //            var start = pos;
        //            var end = (pos + dir * length).normalized * SphereRadius;

        //            var ray = new Ray(start, (end - start).normalized);

        //            RaycastHit hitInfo;
        //            if (Physics.Raycast(ray, out hitInfo, (end - start).magnitude + 0.001f))
        //            {
        //                Debug.DrawLine(hitInfo.point, hitInfo.point + hitInfo.point.normalized, Color.yellow, 3);
        //            }

        //            pos = end;
        //            dir = Vector3.Cross(Vector3.Cross(pos, dir), pos).normalized;

        //            Debug.DrawLine(start, end, Color.red, 3);


        //            yield return new WaitForSeconds(0);
        //        }
        //    }

        //}

        public bool PlanetRaycast(Vector3 point, Vector3 dir, float planetRadius, float step, out RaycastHit hitInfo)
        {
            Profiler.BeginSample("Planet Raycast");

            var totalDist = 0f;
            while (totalDist < 1.1 * 2 * Mathf.PI * planetRadius)
            {
                var start = point;
                var end = (point + dir * step).normalized * SphereRadius;

                var ray = new Ray(start, (end - start).normalized);

                if (Physics.Raycast(ray, out hitInfo, (end - start).magnitude + 0.001f))
                {
                    //Debug.DrawLine(hitInfo.point, hitInfo.point + hitInfo.point.normalized, Color.yellow, 3);
                    Profiler.EndSample();
                    return true;
                }
                totalDist += (end - start).magnitude;

                point = end;
                dir = Vector3.Cross(Vector3.Cross(point, dir), point).normalized;

                //Debug.DrawLine(start, end, Color.red, 3);


            }
            Profiler.EndSample();
            hitInfo = new RaycastHit();
            return false;
        }

        public void Update()
        {
            IEnemy enemy = null;
            RaycastHit hit;
            if (PlanetRaycast(transform.position, MovementDirection, SphereRadius, 5, out hit))
            {
                enemy = EnemiesHelper.GetEnemyForCollider(hit.collider);
                Debug.DrawLine(hit.point, hit.point + hit.point.normalized, Color.yellow);
            }

            if (enemy != CurrentTarget)
            {
                CurrentDamage = DamageMin;
                CurrentTarget = enemy;
            }
            if (enemy == null) return;


            CurrentDamage += CurrentDamage * DamageIncreaseSpeedMultiplier * Time.deltaTime; // This does not really seem correct, since it is an exponential integral
            CurrentDamage = Mathf.Min(CurrentDamage, DamageMax);
            enemy.TakeDamage(CurrentDamage);
        }

        //public void Update()
        //{
        //    transform.position = transform.position.normalized * SphereRadius;

        //    var pos = transform.position;

        //    MovementDirection = Vector3.Cross(Vector3.Cross(pos, MovementDirection), pos).normalized;

        //    rigidbody.velocity = MovementDirection * MovementSpeed;
        //}

        //public void OnCollisionEnter(Collision collision)
        //{
        //    var enemy = EnemiesHelper.GetEnemyForCollider(collision.collider);
        //    if (enemy == null) return;
        //    enemy.TakeDamage(DamageOnHit);
        //    Destroy(gameObject);
        //}
    }
}