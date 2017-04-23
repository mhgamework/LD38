using UnityEngine;

namespace Assets.Scripts
{
    public class PlanetConfig : Singleton<PlanetConfig>
    {
        [SerializeField]
        private float walkSphereRadius = 10;

        [SerializeField]
        private float gravitationalStrength = 10;

        public float GravitationalStrength
        {
            get { return gravitationalStrength; }
        }

        public float WalkSphereRadius
        {
            get { return walkSphereRadius; }
        }
    }
}