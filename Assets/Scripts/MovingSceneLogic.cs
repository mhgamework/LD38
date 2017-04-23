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

            for (int i = 0; i < 100; i++)
            {
                TimelineService.Instance.Get<Spawner>("Area1").Spawn(Enemy);

                yield return new WaitForSeconds(0.5f);
                TimelineService.Instance.Get<Spawner>("Area2").Spawn(Enemy);
                yield return new WaitForSeconds(0.5f);

            }


        }
    }
}