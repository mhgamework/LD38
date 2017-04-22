using UnityEngine;

namespace Assets.Scripts
{
    public class BaseTimelineEntity : MonoBehaviour,ITimelineEntity
    {
        [SerializeField]
        private string id;

        public void OnEnable()
        {
            OnEnable(this);
        }

        public void OnDisable()
        {
            OnDisable(this);
        }

        public static void OnEnable(ITimelineEntity entity)
        {
            TimelineService.Instance.Register(entity);
        }

        public static void OnDisable(ITimelineEntity entity)
        {
            TimelineService.Instance.UnRegister(entity);
        }

        public string Id
        {
            get { return id; }
        }
    }
}