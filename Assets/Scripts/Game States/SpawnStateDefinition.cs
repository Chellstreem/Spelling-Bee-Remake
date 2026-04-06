using System.Collections;
using Spawn;
using UnityEngine;

namespace GameStates
{
    public abstract class SpawnStateDefinition : GameStateDefinition
    {
        [Header("Spawn Settings")]
        [SerializeField] protected SpawnFlowInfo[] _spawnFlowInfos;

        public override GameState CreateGameState(GameServices context)
        {
            return new SpawnState(this, context);
        }

        public virtual IEnumerator SpawnCoroutine(UnitSpawner spawner, GameSpeedController speedController)
        {
            float[] timers = new float[_spawnFlowInfos.Length];

            for (int i = 0; i < timers.Length; i++)
                timers[i] = -_spawnFlowInfos[i].SpawnDelay;

            while (true)
            {
                float deltaTime = Time.deltaTime;

                for (int i = 0; i < _spawnFlowInfos.Length; i++)
                {
                    timers[i] += deltaTime;

                    if (timers[i] < 0f)
                        continue;

                    float interval = _spawnFlowInfos[i].SpawnDistance / speedController.CurrentSpeed;

                    if (timers[i] >= interval)
                    {
                        var binder = GetRandomType(_spawnFlowInfos[i]);

                        if (Random.value <= binder.SpawnProbabilty)
                            spawner.SpawnObject(binder.Type);

                        timers[i] -= interval;
                    }
                }

                yield return null;
            }
        }

        private SpawnBinder GetRandomType(SpawnFlowInfo flow)
        {
            float randomValue = Random.value;

            var binders = flow.Binders;

            for (int i = 0; i < binders.Length; i++)
            {
                randomValue -= binders[i].Weight;

                if (randomValue <= 0f)
                    return binders[i];
            }

            return binders[^1];
        }

        private void OnValidate()
        {
            if (_spawnFlowInfos == null)
                return;

            foreach (var flow in _spawnFlowInfos)
            {
                float sum = 0f;

                foreach (var binder in flow.Binders)
                    sum += binder.Weight;

                if (!Mathf.Approximately(sum, 1f))
                    Debug.LogWarning($"Spawn Flow weight sum is {sum}, but should be 1", this);
            }
        }
    }
}