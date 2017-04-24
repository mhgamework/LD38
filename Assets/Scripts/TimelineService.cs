using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Collects all elements (trigger, or activatables) that can be scripted to implement level-specific behaviour
    /// </summary>
    public class TimelineService : Singleton<TimelineService>
    {

        public TimelineTrigger ActiveCheckpoint { get; private set; }
        private Action activeCheckpointReset;

        public void ActivateCheckpoint(TimelineTrigger id, Action reset)
        {
            //if (ActiveCheckpoint == id)
            //{
            //    Debug.LogError("Did not complete past checkpoint before starting the next one!");
            //}
            activeCheckpointReset = reset;
            ActiveCheckpoint = id;
        }

        ///// <summary>
        ///// TODO, maybe always just take the last checkpoint?
        ///// </summary>
        ///// <param name="id"></param>
        //public void CompleteCheckpoint(object id)
        //{
        //    if (ActiveCheckpoint != id)
        //        Debug.LogError("Deactivating another checkpoint then the one currently running!");
        //    ActiveCheckpoint = null;
        //}

        public void RestoreCheckpoint()
        {
            if (ActiveCheckpoint == null)
            {
                // Not in checkpoint !!
                Debug.Log("error, not in checkpoint");
                return;
            }
            //StopCoroutine(coroutine);
            //if (!done)
            //{
            PlanetCamera.Instance.PlayerPosition = ActiveCheckpoint.Position;
            PlayerHealthScript.Instance.Health = PlayerHealthScript.Instance.MaxHealth;
            foreach (var e in FindObjectsOfType<AEnemy>())
                //.Concat(FindObjectsOfType<FastEnemy>().Cast<MonoBehaviour>())
                //.Concat(FindObjectsOfType<HeavyEnemy>());
                Destroy(e.gameObject);
            activeCheckpointReset();
            //StartCoroutine(begin().GetEnumerator());
            //}

        }


        private Dictionary<string, ITimelineEntity> dict = new Dictionary<string, ITimelineEntity>();
        public event Action OnStart;

        private bool first = true;

        public void Register(ITimelineEntity entity)
        {
            //dict[entity.Id] = entity;
        }

        public void UnRegister(ITimelineEntity entity)
        {
            //dict.Remove(entity.Id);

        }

        public void Update()
        {
            if (first)
            {
                if (OnStart != null) OnStart.Invoke();
            }
            first = false;
        }

        public T Get<T>(string id) where T : class, ITimelineEntity
        {
            var ret = dict[id] as T;

            return ret;
        }
    }
}