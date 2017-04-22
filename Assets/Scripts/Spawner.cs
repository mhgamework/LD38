using UnityEngine;

namespace Assets.Scripts
{
    public class Spawner : BaseTimelineEntity
    {
        public void OnDrawGizmos()
        {
            Gizmos.DrawSphere(GetComponent<BendAroundPlanet>().GetTarget().transform.position, 1);
        }

        public void OnDrawGizmosSelected()
        {

        }

        public void Spawn()
        {
            Debug.Log("Spawning!");
        }
    }
}