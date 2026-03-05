using System.Collections;
using UnityEngine;

namespace MovableObjects
{
    public class CrazyMonkey : InteractableObject
    {
        private float speed;

        public override void Construct(EventManager eventManager, ISpawnableObjectReturner objectReturner, GameConfig gameConfig)
        {
            base.Construct(eventManager, objectReturner, gameConfig);
            speed = gameConfig.MonkeySpeed;
        }

        protected override IEnumerator MoveCoroutine()
        {
            while (true)
            {
                Vector3 newPosition = transform.position;
                newPosition += Vector3.back * (speed * Time.deltaTime);

                if (newPosition.z <= thresholdZ)
                {
                    StopMoving();
                    ReturnToOriginalState();
                }

                transform.position = newPosition;
                yield return null;
            }
        }
    }
}
