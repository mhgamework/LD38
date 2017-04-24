using System.Collections.Generic;
using DigitalRuby.LightningBolt;
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

        [SerializeField]
        private LayerMask layerMask;

        public float DamageMin = 0.1f;
        public float DamageMax = 10f;
        public float DamageIncreaseSpeedMultiplier = 0.1f;


        public float CurrentDamage;
        public IEnemy CurrentTarget;

        [SerializeField]
        private LightningBoltScript lightningPrefab = null;
        [SerializeField]
        private int maxCount = 100;

        private List<Vector3> segmentPoints = new List<Vector3>();
        private List<LightningBoltScript> bolts = new List<LightningBoltScript>();

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

        public bool PlanetRaycast(Vector3 point, Vector3 dir, float planetRadius, float step, out RaycastHit hitInfo, List<Vector3> out_segment_points, float max_dist = 20f)
        {
#if UNITY_EDITOR
            Profiler.BeginSample("Planet Raycast");
#endif
            out_segment_points.Clear();
            out_segment_points.Add(point);
            var totalDist = 0f;
            while (totalDist < max_dist)
            {
                var start = point;
                var end = (point + dir * step).normalized * SphereRadius;

                var ray = new Ray(start, (end - start).normalized);

                if (Physics.Raycast(ray, out hitInfo, (end - start).magnitude + 0.001f, layerMask))
                {
                    //Debug.DrawLine(hitInfo.point, hitInfo.point + hitInfo.point.normalized, Color.yellow, 3);
                    out_segment_points.Add(hitInfo.point);
#if UNITY_EDITOR
                    Profiler.EndSample();
#endif
                    return true;
                }
                out_segment_points.Add(end);

                totalDist += (end - start).magnitude;

                point = end;
                dir = Vector3.Cross(Vector3.Cross(point, dir), point).normalized;

                Debug.DrawLine(start, end, Color.red);
            }

#if UNITY_EDITOR
            Profiler.EndSample();
#endif
            hitInfo = new RaycastHit();
            return false;
        }

        public void Update()
        {
            IEnemy enemy = null;
            RaycastHit hit;
            if (PlanetRaycast(transform.position, MovementDirection, SphereRadius, 5, out hit, segmentPoints))
            {
                enemy = EnemiesHelper.GetEnemyForCollider(hit.collider);
                Debug.DrawLine(hit.point, hit.point + hit.point.normalized, Color.yellow);
            }

            UpdateParticles(); //uses segmentPoints

            if (enemy != CurrentTarget)
            {
                CurrentDamage = DamageMin;
                CurrentTarget = enemy;
            }
            if (enemy == null) return;


            CurrentDamage += CurrentDamage * DamageIncreaseSpeedMultiplier * Time.deltaTime; // This does not really seem correct, since it is an exponential integral
            CurrentDamage = Mathf.Min(CurrentDamage, DamageMax);
            enemy.TakeDamage(CurrentDamage, eDamageType.BEAM);
        }

        private void UpdateParticles()
        {
            var segment_count = Mathf.Min(segmentPoints.Count - 1, maxCount);
            while (bolts.Count > segment_count)
            {
                bolts.RemoveAt(bolts.Count - 1);
            }

            for (int i = 0; i < segment_count; i++)
            {
                if (i > bolts.Count - 1)
                {
                    var o = Instantiate(lightningPrefab.gameObject, transform);
                    o.SetActive(true);
                    var o_bolt = o.GetComponent<LightningBoltScript>();
                    o_bolt.ChaosFactor = Random.value * 0.25f + 0.1f;
                    bolts.Add(o_bolt);
                }

                const int inc = 2;
                var i_plus = Mathf.Min(i + inc, segment_count);
                var bolt_i = bolts[i];
                bolt_i.StartPosition = segmentPoints[i];
                bolt_i.EndPosition = segmentPoints[i_plus];

            }
        }

        void OnDestroy()
        {
            foreach (var bolt in bolts)
            {
                Destroy(bolt.gameObject);
            }
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