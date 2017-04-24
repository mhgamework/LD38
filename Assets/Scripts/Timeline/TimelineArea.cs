using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Timeline
{
    public class TimelineArea : MonoBehaviour, ITimelineEntity
    {
        public GameObject Bomber;
        public GameObject Fast;
        public GameObject Heavy;

        public Transform Root;

        public void Start()
        {
            var inst = TimelineService.Instance; // Initialize the service, bit hacky
        }

        public void Spawn(string name, GameObject thing)
        {
            foreach (var i in SpawnAsync(name, thing, 0))
            {

            }
        }
        public IEnumerable<YieldInstruction> SpawnAsync(string name, GameObject thing, float interval)
        {
            foreach (var s in Get<Spawner>(name))
            {
                s.Spawn(thing);
                yield return new WaitForSeconds(interval);
            }
        }

        public void preventMovement()
        {
        }

        public void RunWithCheckpoint(string checkpointName, Func<IEnumerable<YieldInstruction>> coroutine)
        {
            Coroutine running = null;

            Func<bool, IEnumerable<YieldInstruction>> createWrappedRoutine = null;
            createWrappedRoutine = (first) =>
            {
                return checkpointWrapper(checkpointName, coroutine, () =>
                {
                    if (running != null)
                        StopCoroutine(running);
                    running = StartCoroutine(createWrappedRoutine(false).GetEnumerator());
                }, first);
            };

            running = StartCoroutine(createWrappedRoutine(true).GetEnumerator());


        }



        public IEnumerable<YieldInstruction> checkpointWrapper(string checkpointName, Func<IEnumerable<YieldInstruction>> coroutine, Action restoreCheckpoint, bool first)
        {
            yield return null;
            if (first) foreach (var i in waitForCheckpoint(checkpointName, restoreCheckpoint)) yield return i;
            foreach (var i in coroutine()) yield return i;

            yield return null;
            //done = true;
            Debug.Log("Done");
        }

        public IEnumerable<YieldInstruction> waitForCheckpoint(string checkpoint, Action restore)
        {
            while (!playerInTrigger(checkpoint) || Get<TimelineCheckpoint>(checkpoint).Any(c => !c.CheckpointActive)) yield return null;
            TimelineService.Instance.ActivateCheckpoint(Get<TimelineTrigger>(checkpoint).First(t => t.PlayerInTrigger), restore);
        }

        public bool playerInTrigger(string area1trigger)
        {
            return Get<TimelineTrigger>(area1trigger).Any(t => t.PlayerInTrigger);
        }

        private Dictionary<string, ITimelineEntity[]> cache = new Dictionary<string, ITimelineEntity[]>();

        public IEnumerable<T> Get<T>(string name)
        {
            ITimelineEntity[] ret;
            if (cache.TryGetValue(name, out ret)) return ret.OfType<T>();


            var oriName = name;

            IEnumerable<ITimelineEntity> timelineEntities = null;

            if (name.ToLower().StartsWith("global."))
            {
                name = name.Substring("global.".Length);
                timelineEntities = TimelineService.Instance.TimelineGlobals.Cast<ITimelineEntity>();
            }
            else
                timelineEntities = transform.GetComponentsInChildren<ITimelineEntity>();


            ret = timelineEntities.Where(o => ((MonoBehaviour)o).name == name).ToArray();
            ret =
                ret.Concat(
                    ret.OfType<TimelineParent>()
                        .SelectMany(t => ((MonoBehaviour)t).GetComponentsInChildren<ITimelineEntity>())).ToArray();
            cache[oriName] = ret;
            var realRet = ret.OfType<T>().ToList();
            if (!realRet.Any())
                Debug.LogError("Did not find any object with name " + oriName + " and type " + typeof(T).Name);

            return realRet;

        }


        public void Disable(string name)
        {
            foreach (var o in Get<ITimelineEntity>(name).Select(t => gameObject).Distinct())
                o.SetActive(false);
        }
        public void Enable(string name)
        {
            foreach (var o in Get<ITimelineEntity>(name).Select(t => gameObject).Distinct())
                o.SetActive(true);
        }
    }
}