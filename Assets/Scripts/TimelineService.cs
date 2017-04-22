using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Collects all elements (trigger, or activatables) that can be scripted to implement level-specific behaviour
    /// </summary>
    public class TimelineService : Singleton<TimelineService>
    {
        private Dictionary<string, ITimelineEntity> dict = new Dictionary<string, ITimelineEntity>();

        public void Register(ITimelineEntity entity)
        {
            dict[entity.Id] = entity;
        }

        public void UnRegister(ITimelineEntity entity)
        {
            dict.Remove(entity.Id);

        }
    }
}