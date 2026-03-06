using Zenject;
using UnityEngine;
using System.Collections;
using GameStates;

namespace MovableObjects
{
    public class LaunchedMissile : MovableObject
    {
        private float speed;

        [Inject]
        public override void Construct(GameStateController stateController, EventManager eventManager, ISpawnableObjectReturner objectReturner,
            GameConfig gameConfig)
        {
            this._stateController = stateController;
            this._eventManager = eventManager;
            this._objectReturner = objectReturner;
            speed = gameConfig.MissileSpeed;
            _thresholdZ = gameConfig.ThresholdZ;
        }

        protected override IEnumerator MoveCoroutine()
        {
            while (true)
            {
                Vector3 newPosition = transform.position;
                newPosition += Vector3.back * (speed * Time.deltaTime);

                if (newPosition.z <= _thresholdZ)
                {
                    StopMoving();
                    ReturnToPool();
                }

                transform.position = newPosition;
                yield return null;
            }
        }

        private void OnEnable() => StartMoving();

        private void OnDisable() => StopMoving();
    }
}
