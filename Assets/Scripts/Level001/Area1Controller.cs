using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Level001
{
    public class Area1Controller : MonoBehaviour
    {
        public GameObject Bomber;
        public GameObject Fast;
        public Transform Root;

        public void Start()
        {
            StartCoroutine(begin().GetEnumerator());
        }

        public IEnumerable<YieldInstruction> begin()
        {
            while (!playerInTrigger("Trigger1")) yield return new WaitForSeconds(0);
            Spawn("Fast1", Fast);

            while (!playerInTrigger("Trigger2")) yield return new WaitForSeconds(0);

            preventMovement();

            Spawn("Bomber2", Bomber);
            yield return new WaitForSeconds(1f);
            Spawn("Fast2", Fast);

            //resumeMovement();

            yield return new WaitForSeconds(0);
        }



        private void Spawn(string name, GameObject thing)
        {
            foreach (var s in Get<Spawner>(name))
            {
                s.Spawn(thing);
            }
        }
        private void preventMovement()
        {
        }

        private bool playerInTrigger(string area1trigger)
        {
            return Get<TimelineTrigger>(area1trigger).Any(t => t.PlayerInTrigger);
        }

        private Dictionary<string, ITimelineEntity[]> cache = new Dictionary<string, ITimelineEntity[]>();
        private IEnumerable<T> Get<T>(string name)
        {
            ITimelineEntity[] ret;
            if (cache.TryGetValue(name, out ret)) return ret.OfType<T>();

            ret = Root.GetComponentsInChildren<ITimelineEntity>().Where(o => ((MonoBehaviour)o).name == name).ToArray();
            cache[name] = ret;
            return ret.OfType<T>();

        }
    }
}