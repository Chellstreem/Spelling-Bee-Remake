using UnityEngine;
using System.Collections;

namespace MovableObjects
{
    public class Background : MovableObject, IEventSubscriber<OnMovingStateEnter>, IEventSubscriber<OnMovingStateExit>
    {
        private Vector3 startPosition;

        private void Awake()
        {
            startPosition = transform.position;

            _eventManager.Subscribe<OnMovingStateEnter>(this);
            _eventManager.Subscribe<OnMovingStateExit>(this);
        }

        protected override IEnumerator MoveCoroutine()
        {
            while (true)
            {
                Vector3 newPosition = transform.position;
                newPosition += Vector3.back * (GameSpeed.Speed * Time.deltaTime);

                if (newPosition.z < _thresholdZ)
                {
                    newPosition = startPosition;
                }

                transform.position = newPosition;
                yield return null;
            }
        }

        public void OnEvent(OnMovingStateEnter eventData) => StartMoving();

        public void OnEvent(OnMovingStateExit eventData) => StopMoving();

        private void OnDestroy()
        {
            _eventManager.Unsubscribe<OnMovingStateEnter>(this);
            _eventManager.Unsubscribe<OnMovingStateExit>(this);
        }
    }
}
