using System.Collections;
using Spawn;
using UnityEngine;
using Zenject;

namespace GameStates
{
    public abstract class SpawnState : GameState
    {
        [Header("Spawn Settings")]
        [SerializeField] protected SpawnFlowInfo[] _spawnFlowInfos;
        protected Spawner _spawner;
        protected GameSpeedController _gameSpeedController;

        protected Coroutine _spawnCoroutine;

        [Inject]
        public void GetSpawnDependencies(Spawner spawner, GameSpeedController speedController)
        {
            _spawner = spawner;
            _gameSpeedController = speedController;
        }

        protected virtual IEnumerator RunSpawnCoroutine()
        {
            float[] timers = new float[_spawnFlowInfos.Length];

            while (true)
            {
                float deltaTime = Time.deltaTime;

                for (int i = 0; i < _spawnFlowInfos.Length; i++)
                {
                    timers[i] += deltaTime;

                    float interval = _spawnFlowInfos[i].SpawnDistance / _gameSpeedController.CurrentSpeed;

                    if (timers[i] >= interval)
                    {
                        var type = GetRandomType(_spawnFlowInfos[i]);
                        _spawner.SpawnObject(type);

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