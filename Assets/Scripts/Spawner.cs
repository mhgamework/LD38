using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Spawner : BaseTimelineEntity
    {
        public void Start()
        {
            GetComponent<BendAroundPlanet>().GetTarget().SetActive(false);
        }
        public void OnDrawGizmos()
        {
            Gizmos.DrawSphere(getSpawnPosition(), 1);
        }

        private Vector3 getSpawnPosition()
        {
            return GetComponent<BendAroundPlanet>().GetTarget().transform.position;
        }

        public void OnDrawGizmosSelected()
        {

        }

        public void Spawn(GameObject obj)
        {
            Debug.Log("Spawning! " + obj.name);
            var newObj = Instantiate(obj);
            newObj.transform.position = getSpawnPosition();
            //newObj.transform.up = getSpawnPosition().normalized;
        }
     
    }
}