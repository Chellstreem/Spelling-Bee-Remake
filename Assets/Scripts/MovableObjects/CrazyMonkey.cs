using System.Collections;
using GameStates;
using UnityEngine;

namespace MovableObjects
{
    public class CrazyMonkey : InteractableObject
    {
        [SerializeField] private float _speedOffset = 5f;

        protected override IEnumerator MoveCoroutine()
        {
            while (true)
            {
                Vector3 newPosition = transform.position;
                newPosition += Vector3.back * ((_speedOffset + GameSpeed.Speed) * Time.deltaTime);

                if (newPosition.z <= _thresholdZ)
                {
                    StopMoving();
                    ReturnToPool();
                }

                transform.position = newPosition;
                yield return null;
            }
        }
    }
}
