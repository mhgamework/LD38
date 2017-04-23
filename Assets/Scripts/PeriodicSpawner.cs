using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PeriodicSpawner : MonoBehaviour
    {
        public GameObject Template;
        public float Interval;
        public int Amount = int.MaxValue;
        public bool StartImmediately = false;


        public void Start()
        {
            Template.SetActive(false);
            StartCoroutine(begin().GetEnumerator());
        }

        public IEnumerable<YieldInstruction> begin()
        {
            if (!StartImmediately)
                yield return new WaitForSeconds(Interval);

            for (int i = 0; i < Amount; i++)
            {
                var theThing = Instantiate(Template, transform);
                theThing.gameObject.SetActive(true);

                yield return new WaitForSeconds(Interval);
            }
        }

    }
}