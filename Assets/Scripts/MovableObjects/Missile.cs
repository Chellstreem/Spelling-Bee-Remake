using Zenject;
using UnityEngine;
using System.Collections;
using GameStates;

namespace MovableObjects
{
    public class Missile : InteractableObject
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _speedDelta = 5f;

        protected override IEnumerator MoveCoroutine()
        {
            while (true)
            {
                Vector3 newPosition = transform.position;
                newPosition += _moveDirection * ((_speedDelta + _speedController.CurrentSpeed) * Time.deltaTime);

                if (newPosition.z <= _thresholdZ)
                {
                    StopMoving();
                    _objectReturner.ReturnObject(gameObject);
                    yield break;
                }

                transform.position = newPosition;
                yield return null;
            }
        }

        private void OnEnable() => StartMoving();
        private void OnDisable() => StopMoving();
    }
}
