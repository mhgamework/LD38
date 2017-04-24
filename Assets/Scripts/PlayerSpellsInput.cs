using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerSpellsInput : MonoBehaviour
    {
        public float FastSpellFireRate = 2f;
        public float GrenadeSpellChargeRate = 3f;
        public float GrenadeSpellChargeMin = 5f;
        public float GrenadeSpellChargeMax = 20f;

        public float GrenadeSpellCooldown = 6;

        private float lastGrenade = -1000;

        public GameObject FastSpell;
        public GameObject BeamSpell;
        public GameObject GrenadeSpell;

        public Transform PlayerSphereTransform;
        private PlanetCamera planetCamera;


        public float READGrenadeCharge = 0f;

        public void Start()
        {
            StartCoroutine(begin().GetEnumerator());
            planetCamera = GetComponent<PlanetCamera>();
        }

        public void Update()
        {

        }

        public IEnumerable<YieldInstruction> begin()
        {
            for (;;)
            {
                if (Input.GetMouseButton(0))
                {
                    var inst = Instantiate(FastSpell);
                    inst.gameObject.SetActive(true);
                    inst.transform.up = (PlayerSphereTransform.position + planetCamera.PlayerLookDirection * 2).normalized;
                    var proj = inst.GetComponentInChildren<FastProjectile>();
                    proj.MovementDirection = planetCamera.PlayerLookDirection;

                    yield return new WaitForSeconds(1f / FastSpellFireRate);

                }
                if (Input.GetMouseButton(1))
                {
                    // Start beam

                    var inst = Instantiate(BeamSpell);
                    inst.gameObject.SetActive(true);
                    var proj = inst.GetComponentInChildren<BeamProjectile>();

                    while (Input.GetMouseButton(1))
                    {
                        inst.transform.up = (PlayerSphereTransform.position + planetCamera.PlayerLookDirection * 2).normalized;
                        proj.MovementDirection = planetCamera.PlayerLookDirection;
                        yield return new WaitForSeconds(0);

                    }

                    Destroy(inst);


                }
                if (Input.GetMouseButton(2) && lastGrenade + GrenadeSpellCooldown < Time.time)
                {
                    lastGrenade = Time.time;

                    // Charge grenade

                    var charge = GrenadeSpellChargeMin;

                    while (Input.GetMouseButton(2))
                    {
                        READGrenadeCharge = charge;
                        charge += Time.deltaTime * GrenadeSpellChargeRate;
                        charge = Mathf.Min(charge, GrenadeSpellChargeMax);
                        yield return new WaitForSeconds(0);

                    }
                    var inst = Instantiate(GrenadeSpell);
                    inst.gameObject.SetActive(true);
                    var proj = inst.GetComponentInChildren<GrenadeProjectile>();

                    inst.transform.up = PlayerSphereTransform.position.normalized;
                    inst.transform.position += planetCamera.PlayerLookDirection * 2;
                    proj.InitialDirection = planetCamera.PlayerLookDirection;
                    proj.InitialSpeed = charge * 10;

                }
                yield return new WaitForSeconds(0);
            }
        }
    }
}