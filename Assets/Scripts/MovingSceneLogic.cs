using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// The leve script for the moving scene
    /// </summary>
    public class MovingSceneLogic : MonoBehaviour
    {
        public void Start()
        {
            TimelineService.Instance.OnStart += () =>
            {
                TimelineService.Instance.Get<Spawner>("Area1").Spawn();
            };
        }
    }
}