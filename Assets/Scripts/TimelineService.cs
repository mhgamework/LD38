using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Timeline;
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

        private int lastPoints = 0;

        public bool enableRestore = true;

        public void ActivateCheckpoint(TimelineTrigger id, Action reset)
        {
            //if (ActiveCheckpoint == id)
            //{
            //    Debug.LogError("Did not complete past checkpoint before starting the next one!");
            //}
            activeCheckpointReset = reset;
            ActiveCheckpoint = id;
            lastPoints = PlayerPoints.Points;
            PickupManager.CreateRestorePoint();
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
            KillAllEnemies();
            activeCheckpointReset();

            if (!enableRestore)
                return;

            PlayerPoints.Points = lastPoints;
            PickupManager.Restore();
            //StartCoroutine(begin().GetEnumerator());
            //}

        }

        private static void KillAllEnemies()
        {
            foreach (var e in FindObjectsOfType<AEnemy>())
                //.Concat(FindObjectsOfType<FastEnemy>().Cast<MonoBehaviour>())
                //.Concat(FindObjectsOfType<HeavyEnemy>());
                Destroy(e.gameObject);
        }


        private Dictionary<string, ITimelineEntity> dict = new Dictionary<string, ITimelineEntity>();
        public event Action OnStart;

        private bool first = true;
        public List<ITimelineEntity> TimelineGlobals { get; private set; }

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
            if (Input.GetKeyDown(KeyCode.KeypadMultiply))
                KillAllEnemies();

            if (first)
            {
                TimelineGlobals = FindObjectsOfType<TimelineGlobal>().SelectMany(c => c.GetComponents<ITimelineEntity>()).ToList();
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