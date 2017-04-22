using UnityEngine;

namespace Assets.Scripts
{
    public class PlanetConfig : Singleton<PlanetConfig>
    {
        [SerializeField]
        private float walkSphereRadius = 10;

        public float WalkSphereRadius
        {
            get { return walkSphereRadius; }
        }
    }
}