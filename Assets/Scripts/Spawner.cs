using UnityEngine;

namespace Assets.Scripts
{
    public class Spawner : MonoBehaviour, ITimelineEntity
    {
        [SerializeField]
        private string id;

        public string Id
        {
            get { return id; }
            private set { id = value; }
        }

        public void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, 1);
        }

        public void OnDrawGizmosSelected()
        {

        }
    }
}