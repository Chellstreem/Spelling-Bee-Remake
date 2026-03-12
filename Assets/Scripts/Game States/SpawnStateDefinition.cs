using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Spawn;
using UnityEngine;

namespace GameStates
{
    public abstract class SpawnStateDefinition : GameStateDefinition
    {
        [Header("Spawn Settings")]
        [SerializeField] protected SpawnFlowInfo[] _spawnFlowInfos;

        public override GameState CreateGameState(GameStateController stateController, CoroutineRunner runner, Spawner spawner,
         GameSpeedController speedController)
        {
            return new SpawnState(this, stateController, runner, spawner, speedController);
        }

        protected virtual IEnumerator RunSpawnCoroutine(Spawner spawner, GameSpeedController speedController)
        {
            float[] timers = new float[_spawnFlowInfos.Length];

            while (true)
            {
                float deltaTime = Time.deltaTime;

                for (int i = 0; i < _spawnFlowInfos.Length; i++)
                {
                    timers[i] += deltaTime;

                    float interval = _spawnFlowInfos[i].SpawnDistance / speedController.CurrentSpeed;

                    if (timers[i] >= interval)
                    {
                        var type = GetRandomType(_spawnFlowInfos[i]);
                        spawner.SpawnObject(type);

                        timers[i] -= interval;
                    }
                }

                yield return null;
            }
        }

        private SpawnableType GetRandomType(SpawnFlowInfo flow)
        {
            float randomValue = Random.value;

            var binders = flow.Binders;

            for (int i = 0; i < binders.Length; i++)
            {
                randomValue -= binders[i].Weight;

                if (randomValue <= 0f)
                    return binders[i].Type;
            }

            return binders[^1].Type;
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