using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// The leve script for the moving scene
    /// </summary>
    public class MovingSceneLogic : MonoBehaviour
    {
        public GameObject Enemy;

        public void Start()
        {
            TimelineService.Instance.OnStart += () =>
            {
                StartCoroutine(begin().GetEnumerator());
            };

        }

        public IEnumerable<YieldInstruction> begin()
        {
                TimelineService.Instance.Get<Spawner>("Area1").Spawn(Enemy);

            yield return new WaitForSeconds(1);
            TimelineService.Instance.Get<Spawner>("Area2").Spawn(Enemy);

        }
    }
}